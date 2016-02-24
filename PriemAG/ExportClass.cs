using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Linq;

using EducServLib;
using PriemLib;
namespace Priem
{
    public static class ExportClass
    {
        public static void SetStudyNumbers()
        {
            if (!MainClass.IsPasha())
                return;
            
            using (PriemEntities context = new PriemEntities())
            {
                //взять максимум номера, если еще ничего не назначено
                string num = (from ab in context.extAbit
                              where ab.StudyLevelGroupId == MainClass.lstStudyLevelGroupId.First()
                              select ab.StudyNumber).Max();
               
                var abits = from ab in context.extAbit
                            join ev in context.extEntryView
                            on ab.Id equals ev.AbiturientId
                            where MainClass.lstStudyLevelGroupId.Contains(ab.StudyLevelGroupId) && (ab.StudyNumber == null || ab.StudyNumber.Length == 0)
                            orderby ab.FacultyId, ab.FIO
                            select ab;

                List<Guid> lstAbits = (from a in abits select a.Id).ToList();

                int stNum = 0;

                if (num != null && num.Length != 0)
                    stNum = int.Parse(num.Substring(3));

                stNum++;

                foreach (Guid abitId in lstAbits)
                {
                    string sNum = "0000" + stNum.ToString();
                    sNum = sNum.Substring(sNum.Length - 4, 4);
                    sNum = "13" + 6 + sNum;                

                    context.Abiturient_UpdateStudyNumber(sNum, abitId);                    
                    stNum++;
                }
                MessageBox.Show("Done");
            }            
        }

        public static void ExportVTB()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV files|*.csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                using (StreamWriter writer = new StreamWriter(sfd.OpenFile(), Encoding.GetEncoding(1251)))
                {
                    string[] headers = new string[] { 
                "Фамилия","Имя","Отчество","Пол","Дата рождения","Место рождения","Гражданство","Код документа"
                ,"Серия паспорта","Номер паспорта","Когда выдан паспорт","Кем выдан паспорт","Код подразделения"
                ,"Адрес регистрации","Индекс","Район","Город","Улица","Дом","Корпус","Квартира","Телефон по месту работы"
                ,"Контактный телефон","Рабочий телефон","Должность","Кодовое слово","Основной доход","Тип карты","Дата приема на работу"};


                    writer.WriteLine(string.Join(";", headers));


                    string query = @"select 
ed.person.surname, ed.person.name, ed.person.secondname,
case when ed.person.sex>0 then 'М' else 'Ж' end as sex,

CAST(
(
STR( DAY( ed.person.Birthdate ) ) + '/' +
STR( MONTH( ed.person.Birthdate ) ) + '/' +
STR( YEAR( ed.person.Birthdate ) )
)
AS DATETIME
) as birthdate,
ed.person.birthplace,
nation.name as nationality,
ed.passporttype.name as passporttype,
case when passporttypeid=1 then substring(ed.person.passportseries,1,2)+ ' ' + substring(ed.person.passportseries,3,2) else ed.person.passportseries end as passportseries, 
ed.person.passportnumber, ed.person.passportauthor, ed.person.passportcode,
CAST(
(
STR( DAY( ed.person.passportDate ) ) + '/' +
STR( MONTH( ed.person.passportDate ) ) + '/' +
STR( YEAR( ed.person.passportDate ) )
)
AS DATETIME
) as passportwhen,


ed.person.code,
ed.region.name as region,
ed.person.city,
ed.person.street,
ed.person.house,
ed.person.korpus,
ed.person.flat,

ed.person.codereal,
ed.person.cityreal,
ed.person.streetreal,
ed.person.housereal,
ed.person.korpusreal,
ed.person.flatreal,

ed.person.phone,
ed.person.mobiles
from
ed.extentryview 
inner join ed.extAbitAspirant on ed.extAbitAspirant.id=ed.extentryview.abiturientid
inner join ed.person on ed.person.id=ed.extAbitAspirant.personid
inner join ed.country as nation on nation.id=ed.person.nationalityid
inner join ed.passporttype on ed.passporttype.id=ed.person.passporttypeid
left join ed.region on ed.region.id=ed.person.regionid
where ed.extentryview.studyformid=1 and ed.extentryview.studybasisid=1 and ed.extAbitAspirant.studylevelgroupid IN (" + Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId) + ")";


                    DataSet ds = MainClass.Bdc.GetDataSet(query);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        List<string> list = new List<string>();

                        list.Add(row["surname"].ToString());
                        list.Add(row["name"].ToString());
                        list.Add(row["secondname"].ToString());
                        list.Add(row["sex"].ToString());
                        list.Add(DateTime.Parse(row["birthdate"].ToString()).ToString("dd/MM/yyyy"));

                        list.Add(row["birthplace"].ToString());
                        list.Add(row["nationality"].ToString());
                        list.Add(row["passporttype"].ToString());
                        list.Add(row["passportseries"].ToString());
                        list.Add(row["passportnumber"].ToString());

                        list.Add(DateTime.Parse(row["passportwhen"].ToString()).ToString("dd/MM/yyyy"));
                        list.Add(row["passportauthor"].ToString());
                        list.Add(row["passportcode"].ToString());
                        list.Add("по паспорту");
                        list.Add(row["code"].ToString());

                        list.Add(row["region"].ToString());
                        list.Add(row["city"].ToString());
                        list.Add(row["street"].ToString());
                        list.Add(row["house"].ToString());
                        list.Add(row["korpus"].ToString());

                        list.Add(row["flat"].ToString());
                        list.Add("");
                        list.Add(row["phone"].ToString() + ", " + row["mobiles"].ToString().Replace(";", ","));
                        list.Add("");
                        list.Add("студент");

                        list.Add("");
                        list.Add("0");
                        list.Add("visaelectron");
                        list.Add("01/09/2012");

                        writer.WriteLine(string.Join(";", list.ToArray()));
                    }
                }
            }
            catch
            {
                WinFormsServ.Error("Ошибка при экспорте");
            }
            return;
        }

        public static void ExportSber()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV files|*.csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                using (StreamWriter writer = new StreamWriter(sfd.OpenFile(), Encoding.GetEncoding(1251)))
                {
                    string[] headers = new string[] { 
                "Пол","ФИО","Паспорт","Дата выдачи", "Кем выдан", "Дата рождения","Место рождения",
                "Контрольное слово","Индекс","Адрес 1","Адрес 2","Адрес 3","Адрес 4","Телефон домашний","Телефон мобильный",
                "Телефон рабочий","Должность","Гражданство"};


                    writer.WriteLine(string.Join(";", headers));


                    string query = @"select 
ed.person.surname, ed.person.name, ed.person.secondname,
case when ed.person.sex>0 then 'М' else 'Ж' end as sex,

CAST(
(
STR( DAY( ed.person.Birthdate ) ) + '/' +
STR( MONTH( ed.person.Birthdate ) ) + '/' +
STR( YEAR( ed.person.Birthdate ) )
)
AS DATETIME
) as birthdate,
ed.person.birthplace,
nation.name as nationality,
ed.passporttype.name as passporttype,
case when passporttypeid=1 then substring(ed.person.passportseries,1,2)+ ' ' + substring(ed.person.passportseries,3,2) else ed.person.passportseries end as passportseries, 
ed.person.passportnumber, ed.person.passportauthor, ed.person.passportcode,
CAST(
(
STR( DAY( ed.person.passportDate ) ) + '/' +
STR( MONTH( ed.person.passportDate ) ) + '/' +
STR( YEAR( ed.person.passportDate ) )
)
AS DATETIME
) as passportwhen,


ed.person.code,
ed.region.name as region,
ed.person.city,
ed.person.street,
ed.person.house,
ed.person.korpus,
ed.person.flat,

ed.person.codereal,
ed.person.cityreal,
ed.person.streetreal,
ed.person.housereal,
ed.person.korpusreal,
ed.person.flatreal,

ed.person.phone,
ed.person.mobiles



from
ed.extentryview 
inner join ed.extAbitAspirant on ed.extAbitAspirant.id=ed.extentryview.abiturientid
inner join ed.person on ed.person.id=ed.extAbitAspirant.personid
inner join ed.country as nation on nation.id=ed.person.nationalityid
inner join ed.passporttype on ed.passporttype.id=ed.person.passporttypeid
left join ed.region on ed.region.id=ed.person.regionid
where ed.extentryview.studyformid=1 and ed.extentryview.studybasisid=1 and ed.extAbitAspirant.studylevelgroupid IN " + Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId);


                    DataSet ds = MainClass.Bdc.GetDataSet(query);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        List<string> list = new List<string>();

                        list.Add(row["sex"].ToString());
                        list.Add((row["surname"].ToString() + " " + row["name"].ToString() + " " + row["secondname"].ToString()).Trim());
                        list.Add((row["passportseries"].ToString() + " " + row["passportnumber"].ToString()).Trim());
                        list.Add(DateTime.Parse(row["passportwhen"].ToString()).ToString("dd.MM.yyyy"));
                        list.Add(row["passportauthor"].ToString());

                        list.Add(DateTime.Parse(row["birthdate"].ToString()).ToString("dd.MM.yyyy"));
                        list.Add(row["birthplace"].ToString());
                        list.Add("");
                        list.Add(row["code"].ToString());
                        list.Add(row["region"].ToString() + " " + row["city"].ToString());

                        list.Add(row["street"].ToString() + ", " + row["house"].ToString());
                        list.Add(row["korpus"].ToString());
                        list.Add(row["flat"].ToString());
                        list.Add(row["phone"].ToString());
                        list.Add(row["mobiles"].ToString().Replace(";", ","));

                        list.Add("");
                        list.Add("студент");
                        list.Add(row["nationality"].ToString());

                        writer.WriteLine(string.Join(";", list.ToArray()));
                    }
                }
            }
            catch
            {
                WinFormsServ.Error("Ошибка при экспорте");
            }
            return;
        }

        public static void SetAvgBall()
        {
            if (!MainClass.IsPasha())
                return;

            MessageBox.Show("Метод не описан!");
        }

        public static void SetPayData()
        {
            if (!MainClass.IsPasha())
                return;

            MessageBox.Show("Метод не описан!");
        }

        public static void ReSetMarksForPaid()
        {
            int cnt = 0;
            using (PriemEntities context = new PriemEntities())
            using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
            {
                try
                {
                    var abitsids = context.Abiturient.Select(x => x.Id);
                    var markabits = context.Mark.Select(x => x.AbiturientId);
                    var nomarkabits = abitsids.Except(markabits);

                    var abitsBudz = context.Abiturient.Where(x => x.Entry.StudyBasisId == 1 && x.Entry.StudyLevelId == 15 && x.Mark.Count() == 3).Select(x => x.PersonId);
                    var abitsPlat = context.Abiturient.Where(x => x.Entry.StudyBasisId == 2 && x.Entry.StudyLevelId == 15 && nomarkabits.Contains(x.Id)).Select(x => x.PersonId);

                    var intersected = abitsPlat.Intersect(abitsBudz).Distinct();

                    var AbitsToWork = context.Abiturient.Where(x => intersected.Contains(x.PersonId));
                    foreach (var Ab in AbitsToWork)
                    {
                        var AbitB = context.Abiturient.Where(x => x.Entry.StudyLevelId == 15 && x.Entry.StudyBasisId == 1 && x.Entry.StudyFormId == Ab.Entry.StudyFormId && x.PersonId == Ab.PersonId
                            && x.Entry.LicenseProgramId == Ab.Entry.LicenseProgramId && x.Entry.ObrazProgramId == x.Entry.ObrazProgramId);
                        if (AbitB.Count() > 1)
                        {
                            WinFormsServ.Error(Ab.Person.Surname + " " + (Ab.Person.Name ?? "") + " " + (Ab.Person.SecondName ?? "") + " - совпадения для " + Ab.Entry.SP_LicenseProgram.Code + " " + Ab.Entry.SP_LicenseProgram.Name + " " + Ab.Entry.SP_ObrazProgram.Name);
                            continue;
                        }
                        if (AbitB.Count() == 0)
                            continue;
                        var Mrks = AbitB.First().Mark;
                        if (Mrks.Count() == 0)
                        {
                            WinFormsServ.Error(Ab.Person.Surname + " " + (Ab.Person.Name ?? "") + " " + (Ab.Person.SecondName ?? "") + " - нет оценок для " + Ab.Entry.SP_LicenseProgram.Code + " " + Ab.Entry.SP_LicenseProgram.Name + " " + Ab.Entry.SP_ObrazProgram.Name);
                            continue;
                        }
                        foreach (var M in Mrks)
                        {
                            Guid? exInEnt = context.extExamInEntry.Where(x => x.EntryId == Ab.EntryId && x.ExamId == M.ExamInEntryBlockUnit.ExamId).Select(x => x.Id).FirstOrDefault();
                            if (!exInEnt.HasValue)
                            {
                                WinFormsServ.Error(Ab.Person.Surname + " " + (Ab.Person.Name ?? "") + " " + (Ab.Person.SecondName ?? "") + " - нет экзамена <" + M.ExamInEntryBlockUnit.Exam.ExamName.Name + "> для " + Ab.Entry.SP_LicenseProgram.Code + " " + Ab.Entry.SP_LicenseProgram.Name + " " + Ab.Entry.SP_ObrazProgram.Name);
                                continue;
                            }
                            if (context.Mark.Where(x => x.AbiturientId == Ab.Id && x.ExamInEntryBlockUnitId == exInEnt).Count() == 0)
                            {
                                context.Mark_Insert(Ab.Id, exInEnt, M.Value, M.PassDate, false, false, M.IsManual, M.ExamVedId, null, null);
                                cnt++;
                            }
                        }
                    }
                    tran.Complete();
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error(ex);
                }
                MessageBox.Show("Перезачтено оценок - " + cnt);
            }
        }

        //public static void ImportLoginsFromCSV()
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    if (ofd.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding(1251)))
        //            using (PriemEntities context = new PriemEntities())
        //            {
        //                while (!sr.EndOfStream)
        //                {
        //                    string str = sr.ReadLine();
        //                    string[] splitted = str.Split(';');
        //                    if (splitted.Count() < 7)
        //                        continue;

        //                    string displayName = splitted[4];
        //                    string birthdate = splitted[3];
        //                    DateTime bDate = DateTime.Now;
        //                    if (!DateTime.TryParse(birthdate, out bDate))
        //                        continue;
        //                    string account = splitted[5];

        //                    Guid? AbiturientId =
        //                        (from extPers in context.extPersonAll
        //                         join Abit in context.Abiturient on extPers.Id equals Abit.PersonId
        //                         join extEnt in context.extEntryView on Abit.Id equals extEnt.AbiturientId
        //                         join Ad in context.ADUserData on Abit.Id equals Ad.AbiturientId
        //                         where extPers.FIO == displayName && extPers.BirthDate == bDate
        //                         select Abit.Id).FirstOrDefault();

        //                    if (!AbiturientId.HasValue || AbiturientId.Value == Guid.Empty)
        //                        continue;

        //                    context.ADUserData_Update(AbiturientId, account + "@spbu.ru", account, "", true);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            WinFormsServ.Error(ex);
        //        }
        //    }
        //}
    }
}
