using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Data.Entity.Core.Objects;
using WordOut;
using iTextSharp.text;
using iTextSharp.text.pdf;

using EducServLib;
using PriemLib;

namespace Priem
{
    public class Print
    {
        public static void PrintApplication(Guid? abitId, bool forPrint, string savePath)
        {
            FileStream fileS = null;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    var PersonId = (from ab in context.Abiturient
                                    where ab.Id == abitId
                                  select ab.PersonId).FirstOrDefault();

                    var abitList = (from x in context.Abiturient
                                    join Entry in context.extEntry on x.EntryId equals Entry.Id
                                    where Entry.StudyLevelGroupId == 4
                                    && x.PersonId == PersonId
                                    && x.BackDoc == false
                                    select new
                                    {
                                        x.Id,
                                        x.PersonId,
                                        x.Barcode,
                                        Faculty = Entry.FacultyName,
                                        Profession = Entry.LicenseProgramName,
                                        ProfessionCode = Entry.LicenseProgramCode,
                                        ObrazProgram = Entry.ObrazProgramCrypt + " " + Entry.ObrazProgramName,
                                        Specialization = Entry.ProfileName,
                                        Entry.StudyFormId,
                                        Entry.StudyFormName,
                                        Entry.StudyBasisId,
                                        EntryType = (Entry.StudyLevelId == 17 ? 2 : 1),
                                        Entry.StudyLevelId,
                                        x.Priority,
                                        x.Entry.IsForeign,
                                        //Entry.CommissionId,
                                        //ComissionAddress = Entry.CommissionId
                                    }).OrderBy(x => x.Priority).ToList();

                    var person = (from x in context.Person
                                  join EdInfo in context.extPerson_EducationInfo on x.Id equals EdInfo.PersonId
                                  where x.Id == PersonId
                                  select new
                                  {
                                      x.Surname,
                                      x.Name,
                                      x.SecondName,
                                      x.Barcode,
                                      x.Person_AdditionalInfo.HostelAbit,
                                      x.BirthDate,
                                      BirthPlace = x.BirthPlace ?? "",
                                      Sex = x.Sex,
                                      ForeignNationality = x.ForeignNationalityId,
                                      Nationality = x.Nationality.Name,
                                      Country = x.Person_Contacts.Country.Name,
                                      ForeignCountryName = x.Person_Contacts.ForeignCountry.Name,
                                      PassportType = x.PassportType.Name,
                                      x.PassportSeries,
                                      x.PassportNumber,
                                      x.PassportAuthor,
                                      x.PassportDate,
                                      x.Person_Contacts.City,
                                      Region = x.Person_Contacts.Region.Name,
                                      ProgramName = EdInfo.HEProfession,
                                      x.Person_Contacts.Code,
                                      x.Person_Contacts.Street,
                                      x.Person_Contacts.House,
                                      x.Person_Contacts.Korpus,
                                      x.Person_Contacts.Flat,
                                      x.Person_Contacts.Phone,
                                      x.Person_Contacts.Email,
                                      x.Person_Contacts.Mobiles,
                                      EdInfo.SchoolExitYear,
                                      EdInfo.SchoolName,
                                      AddInfo = x.Person_AdditionalInfo.ExtraInfo,
                                      Parents = x.Person_AdditionalInfo.PersonInfo,
                                      x.Person_AdditionalInfo.StartEnglish,
                                      x.Person_AdditionalInfo.EnglishMark,
                                      EdInfo.IsEqual,
                                      EdInfo.EqualDocumentNumber,
                                      CountryEduc = EdInfo.CountryEducName,
                                      EdInfo.CountryEducId,
                                      EdInfo.ForeignCountryEducId,
                                      Qualification = EdInfo.HEQualification,
                                      EdInfo.SchoolTypeId,
                                      EducationDocumentSeries = EdInfo.DiplomSeries,
                                      EducationDocumentNumber = EdInfo.DiplomNum,
                                      EdInfo.AttestatSeries,
                                      EdInfo.AttestatNum,
                                      Language = x.Person_AdditionalInfo.Language.Name,
                                      HasPrivileges = x.Person_AdditionalInfo.Privileges > 0,
                                      x.Person_AdditionalInfo.HasTRKI,
                                      x.Person_AdditionalInfo.TRKICertificateNumber,
                                      x.Person_AdditionalInfo.HostelEduc,
                                      IsRussia = (x.Person_Contacts.CountryId == 1),
                                      x.HasRussianNationality,
                                      x.Person_AdditionalInfo.Stag,
                                      x.Person_AdditionalInfo.WorkPlace,
                                      x.Num
                                  }).FirstOrDefault();

                    string tmp;
                    string dotName;
                    
                    dotName = "ApplicationAsp_2014";
                    using (FileStream fs = new FileStream(string.Format(@"{0}\{1}.pdf", MainClass.dirTemplates, dotName), FileMode.Open, FileAccess.Read))
                    {

                        byte[] bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        fs.Close();

                        PdfReader pdfRd = new PdfReader(bytes);

                        try
                        {
                            fileS = new FileStream(string.Format(savePath), FileMode.Create);
                        }
                        catch
                        {
                            if (fileS != null)
                                fileS.Dispose();
                            WinFormsServ.Error("Пожалуйста, закройте открытые файлы pdf");
                            return;
                        }


                        PdfStamper pdfStm = new PdfStamper(pdfRd, fileS);
                        pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "",
        PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING |
        PdfWriter.AllowPrinting);
                        AcroFields acrFlds = pdfStm.AcroFields;

                        PdfContentByte cb = pdfStm.GetOverContent(1);

                        string FIO = ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim();
                        acrFlds.SetField("FIO", FIO);

                        for (int ii = 0; ii < abitList.Count; ii++)
                        {
                            acrFlds.SetField("Priority" + (ii + 1).ToString(), abitList[ii].Priority.ToString());
                            acrFlds.SetField("Profession" + (ii + 1).ToString(), "(" + abitList[ii].ProfessionCode + ") " + abitList[ii].Profession);
                            acrFlds.SetField("Specialization" + (ii + 1).ToString(), abitList[ii].Specialization);
                            acrFlds.SetField("ObrazProgram" + (ii + 1).ToString(), abitList[ii].ObrazProgram);
                            acrFlds.SetField("StudyBasis" + abitList[ii].StudyBasisId.ToString() + (ii + 1).ToString(), "1");
                            acrFlds.SetField("StudyForm" + abitList[ii].StudyFormId.ToString() + (ii + 1).ToString(), "1");
                        }

                        //acrFlds.SetField("StudyForm1", "1");
                        acrFlds.SetField("ExitYear", person.SchoolExitYear.ToString());

                        string[] splitStr = GetSplittedStrings(person.SchoolName ?? "", 50, 70, 2);
                        for (int i = 1; i <= 2; i++)
                            acrFlds.SetField("School" + i, splitStr[i - 1]);

                        string attestat = (person.EducationDocumentSeries ?? "") + (person.EducationDocumentNumber ?? "");
                        string DiplomaNumber = !String.IsNullOrEmpty(person.EducationDocumentNumber) ? (" №" + person.EducationDocumentNumber) : "";
                        string DiplomaSeries = !String.IsNullOrEmpty(person.EducationDocumentSeries) ? ("серия " + person.EducationDocumentSeries + " ") : "";
                        acrFlds.SetField("Attestat", String.IsNullOrEmpty(attestat) ? "" : "диплом " + DiplomaSeries + DiplomaNumber);

                        acrFlds.SetField("HEProfession", person.ProgramName ?? "");
                        acrFlds.SetField("Qualification", person.Qualification);
                        
                        if ((person.SchoolTypeId != 4) || (person.SchoolTypeId == 4 && (person.Qualification).ToLower().IndexOf("аспирант") < 0))
                            acrFlds.SetField("NoEduc", "1");
                        else
                        {
                            acrFlds.SetField("HasEduc", "1");
                            acrFlds.SetField("HighEducation", person.SchoolName);
                        }

                        acrFlds.SetField("HostelEducYes", (person.HostelEduc) ? "1" : "0");
                        acrFlds.SetField("HostelEducNo", (person.HostelEduc) ? "0" : "1");
                        acrFlds.SetField("HostelAbitYes", (person.HostelAbit) ? "1" : "0");
                        acrFlds.SetField("HostelAbitNo", (person.HostelAbit) ? "0" : "1");
                        if (person.IsEqual && (person.ForeignCountryEducId != 193))
                        {
                            acrFlds.SetField("IsEqual", "1");
                            acrFlds.SetField("EqualSertificateNumber", person.EqualDocumentNumber);
                        }
                        else
                        {
                            acrFlds.SetField("NoEqual", "1");
                        }

                        acrFlds.SetField("BirthDateYear", person.BirthDate.Year.ToString("D2"));
                        acrFlds.SetField("BirthDateMonth", person.BirthDate.Month.ToString("D2"));
                        acrFlds.SetField("BirthDateDay", person.BirthDate.Day.ToString());
                        acrFlds.SetField("BirthPlace", person.BirthPlace);

                        acrFlds.SetField("Male", person.Sex ? "1" : "0");
                        acrFlds.SetField("Female", person.Sex ? "0" : "1");

                        if (person.Nationality.Contains("зарубеж"))
                        {
                            string ForeignNationality = context.ForeignCountry.Where(x => x.Id == person.ForeignNationality).Select(x => x.Name).FirstOrDefault();
                            acrFlds.SetField("Nationality", ForeignNationality);
                        }
                        else
                            acrFlds.SetField("Nationality", person.Nationality);

                        acrFlds.SetField("PassportSeries", person.PassportSeries);
                        acrFlds.SetField("PassportNumber", person.PassportNumber);

                        splitStr = GetSplittedStrings(person.PassportAuthor + " " + person.PassportDate.Value.ToString("dd.MM.yyyy"), 60, 70, 2);
                        for (int i = 1; i <= 2; i++)
                            acrFlds.SetField("PassportAuthor" + i, splitStr[i - 1]);

                        string country = person.Country;
                        string region = person.Region;
                        if (person.Country.Contains("зарубеж"))
                        {
                            country = person.ForeignCountryName;
                            region = "";
                        }

                        acrFlds.SetField("Address1", string.Format("{0} {1} {2}, {3}, ", person.Code, country, region, person.City));
                        acrFlds.SetField("Address2", string.Format("{0} дом {1} {2} кв. {3}", person.Street, person.House, (person.Korpus == string.Empty || person.Korpus == "-") ? "" : "корп. " + person.Korpus, person.Flat));

                        string addInfo = person.Mobiles.Replace('\r', ',').Replace('\n', ' ').Trim();//если начнут вбивать построчно, то хотя бы в одну строку сведём
                        if (addInfo.Length > 100)
                        {
                            int cutpos = 0;
                            cutpos = addInfo.Substring(0, 100).LastIndexOf(',');
                            addInfo = addInfo.Substring(0, cutpos) + "; ";
                        }


                        acrFlds.SetField("Phone", person.Phone);
                        acrFlds.SetField("Email", person.Email);
                        acrFlds.SetField("Mobiles", person.Mobiles);

                        acrFlds.SetField("Orig", "0");
                        acrFlds.SetField("Copy", "0");

                        string CountryEducName = context.Country.Where(x => x.Id == person.CountryEducId).Select(x => x.Name).FirstOrDefault();
                        string ForeignCountryEducName = context.ForeignCountry.Where(x => x.Id == person.ForeignCountryEducId).Select(x => x.Name).FirstOrDefault();

                        acrFlds.SetField("CountryEduc", CountryEducName ?? (ForeignCountryEducName ?? ""));
                        acrFlds.SetField("Language", person.Language ?? "");

                        if (person.Stag != string.Empty)
                        {
                            acrFlds.SetField("HasStag", "1");
                            acrFlds.SetField("Stag", person.Stag);
                            acrFlds.SetField("WorkPlace", person.WorkPlace);
                        }
                        else
                            acrFlds.SetField("NoStag", "1");

                        if (person.HasPrivileges)
                            acrFlds.SetField("HasPrivileges", "1");

                        // олимпиады
                        acrFlds.SetField("Extra", person.AddInfo ?? "");

                        //экстр. случаи
                        tmp = person.Parents.Replace('\r', ';').Replace('\n', ' ').Trim();
                        string[] mamaPapa = GetSplittedStrings(tmp, 40, 80, 3);
                        acrFlds.SetField("Parents1", mamaPapa[0]);
                        acrFlds.SetField("Parents2", mamaPapa[1]);
                        acrFlds.SetField("Parents3", mamaPapa[2]);

                        pdfStm.FormFlattening = true;
                        pdfStm.Close();
                        pdfRd.Close();

                        Process pr = new Process();
                        if (forPrint)
                        {
                            pr.StartInfo.Verb = "Print";
                            pr.StartInfo.FileName = string.Format(savePath);
                            pr.Start();
                        }
                        else
                        {
                            pr.StartInfo.Verb = "Open";
                            pr.StartInfo.FileName = string.Format(savePath);
                            pr.Start();
                        }
                    }
                }
            }

            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
            finally
            {
                if (fileS != null)
                    fileS.Dispose();
            }
        }

        public static void PrintDogovor(Guid dogId, Guid abitId, bool forPrint)
        {
            using (PriemEntities context = new PriemEntities())
            {
                var abit = context.extAbit.Where(x => x.Id == abitId).FirstOrDefault();
                if (abit == null)
                {
                    WinFormsServ.Error("Не удалось загрузить данные заявления");
                    return;
                }

                var person = context.extPersonAll.Where(x => x.Id == abit.PersonId).FirstOrDefault();
                if (person == null)
                {
                    WinFormsServ.Error("Не удалось загрузить данные абитуриента");
                    return;
                }

                var dogovorInfo =
                    (from pd in context.PaidData
                     join pi in context.PayDataEntry on pd.Abiturient.EntryId equals pi.EntryId into pi2
                     from pi in pi2.DefaultIfEmpty()
                     where pd.Id == dogId
                     select new
                     {
                         pd.DogovorNum,
                         DogovorTypeName = pd.DogovorType.Name,
                         pd.DogovorDate,
                         pd.Qualification,
                         pd.Srok,
                         pd.SrokIndividual,
                         pd.DateStart,
                         pd.DateFinish,
                         pd.SumTotal,
                         pd.SumFirstYear,
                         pd.SumFirstPeriod,
                         pd.Parent,
                         Prorector = pd.Prorektor.NameFull,
                         PayPeriodName = pd.PayPeriod.Name,
                         pd.AbitFIORod,
                         pd.AbiturientId,
                         pd.Customer,
                         pd.CustomerLico,
                         pd.CustomerReason,
                         pd.CustomerAddress,
                         pd.CustomerPassport,
                         pd.CustomerPassportAuthor,
                         pd.CustomerINN,
                         pd.CustomerRS,
                         pd.Prorektor.DateDov,
                         pd.Prorektor.NumberDov,
                         PayPeriod = pd.PayPeriod.Name,
                         PayPeriodPad = pd.PayPeriod.NamePad,
                         DogovorTypeId = pd.DogovorTypeId,
                         pi.UniverName,
                         pi.UniverAddress,
                         pi.UniverINN,
                         pi.UniverRS,
                         pi.Props
                     }).FirstOrDefault();

                string dogType = dogovorInfo.DogovorTypeId.ToString();
                
                WordDoc wd = new WordDoc(string.Format(@"{0}\Dogovor{1}.dot", MainClass.dirTemplates, dogType), !forPrint);

                //вступление
                wd.SetFields("DogovorNum", dogovorInfo.DogovorNum.ToString());
                wd.SetFields("DogovorDate", dogovorInfo.DogovorDate.ToLongDateString());

                //проректор и студент
                wd.SetFields("Lico", dogovorInfo.Prorector);
                wd.SetFields("LicoDate", dogovorInfo.DateDov.ToString() + "г.");
                wd.SetFields("LicoNum", dogovorInfo.NumberDov.ToString());
                wd.SetFields("FIO", person.FIO);
                wd.SetFields("Sex", (person.Sex) ? "ый" : "ая");

                string programcode = abit.ObrazProgramCrypt.Trim();
                string profcode = abit.LicenseProgramCode.Trim();

                wd.SetFields("ObrazProgramName", "(" + programcode + ") " + abit.ObrazProgramName.Trim());

                wd.SetFields("Profession", "(" + profcode + ") " + abit.LicenseProgramName);

                wd.SetFields("StudyCourse", "1");
                wd.SetFields("StudyFaculty", abit.FacultyName);
                string form = context.StudyForm.Where(x => x.Id == abit.StudyFormId).Select(x => x.Name).FirstOrDefault().ToLower();
                wd.SetFields("StudyForm", form.ToLower());

                wd.SetFields("Qualification", dogovorInfo.Qualification);

                //сроки обучения
                wd.SetFields("Srok", dogovorInfo.Srok);

                DateTime dStart = dogovorInfo.DateStart;
                wd.SetFields("DateStart", dStart.ToLongDateString());
                DateTime dFinish = dogovorInfo.DateFinish;
                wd.SetFields("DateFinish", dFinish.ToLongDateString());

                //суммы обучения
                wd.SetFields("SumTotal", dogovorInfo.SumTotal);

                wd.SetFields("SumFirstPeriod", dogovorInfo.SumFirstPeriod);

                wd.SetFields("Address1", string.Format("{0} {1}, {2}, {3}, ", person.Code, person.CountryName, person.RegionName, person.City));
                wd.SetFields("Address2", string.Format("{0} дом {1} {2} кв. {3}", person.Street, person.House, person.Korpus == string.Empty ? "" : "корп. " + person.Korpus, person.Flat));

                wd.SetFields("Passport", "серия " + person.PassportSeries + " № " + person.PassportNumber);
                wd.SetFields("PassportAuthorDate", person.PassportDate.Value.ToShortDateString());
                wd.SetFields("PassportAuthor", person.PassportAuthor);

                wd.SetFields("PhoneNumber", person.Phone + (String.IsNullOrEmpty(person.Mobiles) ? "" : ", доп.: " + person.Mobiles));

                wd.SetFields("UniverName", dogovorInfo.UniverName);
                wd.SetFields("UniverAddress", dogovorInfo.UniverAddress);
                wd.SetFields("UniverINN", dogovorInfo.UniverINN);
                //wd.SetFields("UniverRS", dogovorInfo.UniverRS);
                wd.SetFields("Props", dogovorInfo.Props);

                switch (dogType)
                {
                    // обычный
                    case "1":
                        {
                            break;
                        }
                    // физ лицо
                    case "2":
                        {
                            wd.SetFields("CustomerLico", dogovorInfo.Customer);
                            wd.SetFields("CustomerAddress", dogovorInfo.CustomerAddress);
                            wd.SetFields("CustomerINN", "Паспорт: " + dogovorInfo.CustomerPassport);
                            wd.SetFields("CustomerRS", "Выдан: " + dogovorInfo.CustomerPassportAuthor);

                            break;
                        }
                    // мат кап
                    case "4":
                        {
                            wd.SetFields("Customer", dogovorInfo.Customer);
                            wd.SetFields("CustomerAddress", dogovorInfo.CustomerAddress);
                            wd.SetFields("CustomerINN", dogovorInfo.CustomerPassport);
                            wd.SetFields("CustomerRS", dogovorInfo.CustomerPassportAuthor);

                            break;
                        }
                    // юридическое лицо
                    case "3":
                        {
                            wd.SetFields("Customer", dogovorInfo.Customer);
                            wd.SetFields("CustomerLico", dogovorInfo.CustomerLico);
                            wd.SetFields("CustomerReason", dogovorInfo.CustomerReason);
                            wd.SetFields("CustomerAddress", dogovorInfo.CustomerAddress);
                            wd.SetFields("CustomerINN", "ИНН " + dogovorInfo.CustomerINN);
                            wd.SetFields("CustomerRS", "Р/С " + dogovorInfo.CustomerRS);

                            break;
                        }
                }

                if (forPrint)
                {
                    wd.Print();
                    wd.Close();
                }

            }
        }

        public static void PrintEntryView(string protocolId, string savePath)
        {
            FileStream fileS = null;
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    Guid? protId = new Guid(protocolId);
                    var prot = (from pr in context.extProtocol
                                where pr.Id == protId
                                select pr).FirstOrDefault();

                    string docNum = prot.Number.ToString();
                    DateTime docDate = prot.Date.Date;
                    string form = prot.StudyFormRodName;
                    string form2 = prot.StudyFormRodName;
                    string facDat = prot.FacultyDatName;

                    string basisId = prot.StudyBasisId.ToString();
                    string basis = string.Empty;

                    bool? isSec = prot.IsSecond;
                    bool? isReduced = prot.IsReduced;
                    bool? isParallel = prot.IsParallel;
                    bool? isList = prot.IsListener;

                    string profession = (from extabit in context.extAbit
                                         join extentryView in context.extEntryView on extabit.Id equals extentryView.AbiturientId
                                         where extentryView.Id == protId
                                         select extabit.LicenseProgramName
                                  ).FirstOrDefault();

                    string professionCode = (from extabit in context.extAbit
                                             join extentryView in context.extEntryView on extabit.Id equals extentryView.AbiturientId
                                             where extentryView.Id == protId
                                             select extabit.LicenseProgramCode
                                  ).FirstOrDefault();

                    switch (basisId)
                    {
                        case "1":
                            basis = "обучение за счет средств федерального бюджета";
                            break;
                        case "2":
                            basis = "обучение по договорам с оплатой стоимости обучения";
                            break;
                    }

                    string list = string.Empty, sec = string.Empty;

                    string copyDoc = "оригиналы";
                    if (isList.HasValue && isList.Value)
                    {
                        list = " в качестве слушателя";
                        copyDoc = "заверенные ксерокопии";
                    }

                    if (isReduced.HasValue && isReduced.Value)
                        sec = " (сокращенной)";
                    if (isParallel.HasValue && isParallel.Value)
                        sec = " (параллельной)";

                    Document document = new Document(PageSize.A4, 50, 50, 50, 50);

                    using (fileS = new FileStream(savePath, FileMode.Create))
                    {

                        BaseFont bfTimes = BaseFont.CreateFont(string.Format(@"{0}\times.ttf", MainClass.dirTemplates), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font font = new Font(bfTimes, 12);

                        PdfWriter writer = PdfWriter.GetInstance(document, fileS);
                        document.Open();

                        float firstLineIndent = 30f;
                        //HEADER
                        Paragraph p = new Paragraph("Правительство Российской Федерации", new Font(bfTimes, 12, Font.BOLD));
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);

                        p = new Paragraph("Федеральное государственное бюджетное образовательное учреждение", new Font(bfTimes, 12));
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);

                        p = new Paragraph("высшего профессионального образования", new Font(bfTimes, 12));
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);

                        p = new Paragraph("САНКТ-ПЕТЕРБУРГСКИЙ ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ", new Font(bfTimes, 12, Font.BOLD));
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);

                        p = new Paragraph("ПРЕДСТАВЛЕНИЕ", new Font(bfTimes, 20, Font.BOLD));
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);

                        p = new Paragraph(string.Format("От {0} г. № {1}", Util.GetDateString(docDate, true, true), docNum), font);
                        p.SpacingBefore = 10f;
                        document.Add(p);

                        p = new Paragraph(10f);
                        p.Add(new Paragraph("по " + facDat, font));

                        string bakspec = "", naprspecRod = "", naprobProgRod = "", educDoc = ""; ;

                        naprobProgRod = "образовательной программе";
                        naprspecRod = "направлению";

                        educDoc = "о высшем профессиональном образовании";
                        
                        p.Add(new Paragraph("по основной образовательной программе послевузовского профессионального образования (аспирантура)", font));
                        p.Add(new Paragraph(("по " + form + " форме обучения,").ToLower(), font));
                        p.Add(new Paragraph(basis, font));
                        p.IndentationLeft = 320;
                        document.Add(p);

                        p = new Paragraph();
                        p.Add(new Paragraph("О зачислении в аспирантуру", font));
                        //p.Add(new Paragraph("граждан Российской Федерации", font));
                        p.SpacingBefore = 10f;
                        document.Add(p);

                        p = new Paragraph("В соответствии с Положением о подготовке научно-педагогических и научных кадров в системе послевузовского профессионального образования в Российской Федерации, утвержденного приказом Минобразования Российской Федерации от 27.03.1998 № 814, Правилами приема на основные образовательные программы послевузовского профессионального образования (программы подготовки научно-педагогических кадров в аспирантуре) Санкт-Петербургского государственного университета в 2013 году", font);
                        p.SpacingBefore = 10f;
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        p.FirstLineIndent = firstLineIndent;
                        document.Add(p);

                        p = new Paragraph(string.Format("Представить на рассмотрение Приемной комиссии СПбГУ по вопросу зачисления c 01.09.2013 года на 1 курс{2} с освоением основной{3} образовательной программы подготовки {0} по {1} форме обучения следующих граждан, успешно выдержавших вступительные испытания:", bakspec, form2, list, sec), font);
                        p.FirstLineIndent = firstLineIndent;
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        p.SpacingBefore = 20f;
                        document.Add(p);

                        string curObProg = "-";
                        string curHeader = "-";

                        int counter = 0;

                        using (PriemEntities ctx = new PriemEntities())
                        {
                            var lst = (from extabit in ctx.extAbit
                                       join extentryView in ctx.extEntryView on extabit.Id equals extentryView.AbiturientId
                                       join extperson in ctx.extPersonAll on extabit.PersonId equals extperson.Id
                                       join competition in ctx.Competition on extabit.CompetitionId equals competition.Id
                                       join entryHeader in ctx.EntryHeader on extentryView.EntryHeaderId equals entryHeader.Id into entryHeader2
                                       from entryHeader in entryHeader2.DefaultIfEmpty()
                                       where extentryView.Id == protId
                                       orderby extabit.ObrazProgramName, extabit.ProfileName, entryHeader.Id, extperson.FIO
                                       select new
                                       {
                                           Id = extabit.Id,
                                           Рег_Номер = extabit.RegNum,
                                           Ид_номер = extperson.PersonNum,
                                           TotalSum = extabit.Sum,
                                           ФИО = extperson.FIO,
                                           LicenseProgramName = extabit.LicenseProgramName,
                                           LicenseProgramCode = extabit.LicenseProgramCode,
                                           ObrazProgram = extabit.ObrazProgramName,
                                           ObrazProgramId = extabit.ObrazProgramId,
                                           ObrazProgramCrypt = extabit.ObrazProgramCrypt,
                                           ProfileName = extabit.ProfileName,
                                           EntryHeaderId = entryHeader.Id,
                                           EntryHeaderName = entryHeader.Name
                                       }).ToList().Distinct().Select(x =>
                                           new
                                           {
                                               Id = x.Id.ToString(),
                                               Рег_Номер = x.Рег_Номер,
                                               Ид_номер = x.Ид_номер,
                                               TotalSum = x.TotalSum,
                                               ФИО = x.ФИО,
                                               LicenseProgramName = x.LicenseProgramName,
                                               x.LicenseProgramCode,
                                               ObrazProgram = x.ObrazProgram,
                                               ObrazProgramId = x.ObrazProgramId,
                                               ObrazProgramCrypt = x.ObrazProgramCrypt,
                                               ProfileName = x.ProfileName,
                                               EntryHeaderId = x.EntryHeaderId,
                                               EntryHeaderName = x.EntryHeaderName
                                           }
                                       );

                            foreach (var v in lst)
                            {
                                ++counter;
                                string obProg = v.ObrazProgram;
                                string obProgCrypt = v.ObrazProgramCrypt;
                                string obProgId = v.ObrazProgramId.ToString();

                                if (obProgId != curObProg)
                                {
                                    p = new Paragraph();
                                    p.Add(new Paragraph(string.Format("{3}по {0} {1} \"{2}\"", naprspecRod, v.LicenseProgramCode, v.LicenseProgramName, curObProg == "-" ? "" : "\r\n"), font));

                                    if (!string.IsNullOrEmpty(obProg))
                                        p.Add(new Paragraph(string.Format("по {0} {1} \"{2}\"", naprobProgRod, obProgCrypt, obProg), font));

                                    p.IndentationLeft = 40;
                                    document.Add(p);

                                    curObProg = obProgId;
                                    curHeader = "NULL";
                                }

                                string header = v.EntryHeaderName;
                                if (header != curHeader)
                                {
                                    p = new Paragraph();
                                    p.Add(new Paragraph(string.Format("{0}:", header), font));
                                    p.IndentationLeft = 40;
                                    document.Add(p);

                                    curHeader = header;
                                }

                                p = new Paragraph();
                                p.Add(new Paragraph(string.Format("{0}. {1} {2}", counter, v.ФИО, v.TotalSum.ToString()), font));
                                p.IndentationLeft = 60;
                                document.Add(p);
                            }
                        }

                        //FOOTER
                        p = new Paragraph();
                        p.SpacingBefore = 30f;
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        p.FirstLineIndent = firstLineIndent;
                        p.Add(new Phrase("ОСНОВАНИЕ:", new Font(bfTimes, 12)));
                        p.Add(new Phrase(string.Format(" личные заявления, протоколы вступительных испытаний, {0} документов государственного образца {1}.", copyDoc, educDoc), font));
                        document.Add(p);

                        p = new Paragraph();
                        p.SpacingBefore = 30f;
                        p.KeepTogether = true;
                        p.Add(new Paragraph("Ответственный секретарь", font));
                        p.Add(new Paragraph("комиссии по приему документов СПбГУ                                                                                          ", font));
                        document.Add(p);

                        p = new Paragraph();
                        p.SpacingBefore = 30f;
                        p.Add(new Paragraph("Заместитель начальника управления - ", font));
                        p.Add(new Paragraph("советник проректора по направлениям", font));
                        document.Add(p);

                        document.Close();

                        Process pr = new Process();

                        pr.StartInfo.Verb = "Open";
                        pr.StartInfo.FileName = string.Format(savePath);
                        pr.Start();

                    }
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
            finally
            {
                if (fileS != null)
                    fileS.Dispose();
            }
        }

        public static void PrintOrder(Guid protocolId, bool isRus, bool isCel)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\EntryOrder1.dot", MainClass.dirTemplates));
                TableDoc td = wd.Tables[0];
                
                string docNum;
                DateTime docDate;
                string formId;
                string facDat;

                bool? isSec;
                bool? isParallel;
                bool? isReduced;
                bool? isList;

                string basisId;
                string basis = string.Empty;
                string form = string.Empty;
                string form2 = string.Empty;

                string LicenseProgramName;
                string LicenseProgramCode;

                string DateStart = "01.09.2014";
                string DateOfFinish = "30.06.2015";

                using (PriemEntities ctx = new PriemEntities())
                {

                    docNum = (from protocol in ctx.Protocol
                              where protocol.Id == protocolId
                              select protocol.Number).FirstOrDefault();

                    docDate = (DateTime)(from protocol in ctx.Protocol
                                         where protocol.Id == protocolId
                                         select protocol.Date).FirstOrDefault();

                    formId = (from protocol in ctx.Protocol
                              join studyForm in ctx.StudyForm on protocol.StudyFormId equals studyForm.Id
                              where protocol.Id == protocolId
                              select studyForm.Id).FirstOrDefault().ToString();

                    facDat = (from protocol in ctx.Protocol
                              join sP_Faculty in ctx.SP_Faculty on protocol.FacultyId equals sP_Faculty.Id
                              where protocol.Id == protocolId
                              select sP_Faculty.DatName).FirstOrDefault();

                    isSec = (from protocol in ctx.Protocol
                             where protocol.Id == protocolId
                             select protocol.IsSecond).FirstOrDefault();

                    isParallel = (from protocol in ctx.Protocol
                                  where protocol.Id == protocolId
                                  select protocol.IsParallel).FirstOrDefault();

                    isReduced = (from protocol in ctx.Protocol
                                 where protocol.Id == protocolId
                                 select protocol.IsReduced).FirstOrDefault();

                    isList = (from protocol in ctx.Protocol
                              where protocol.Id == protocolId
                              select protocol.IsListener).FirstOrDefault();

                    basisId = (from protocol in ctx.Protocol
                               join studyBasis in ctx.StudyBasis on protocol.StudyBasisId equals studyBasis.Id
                               where protocol.Id == protocolId
                               select studyBasis.Id).FirstOrDefault().ToString();

                    LicenseProgramName = (from entry in ctx.Entry
                                          join extentryView in ctx.extEntryView on entry.LicenseProgramId equals extentryView.LicenseProgramId
                                          where extentryView.Id == protocolId
                                          select entry.SP_LicenseProgram.Name).FirstOrDefault();

                    LicenseProgramCode = (from entry in ctx.Entry
                                          join extentryView in ctx.extEntryView on entry.LicenseProgramId equals extentryView.LicenseProgramId
                                          where extentryView.Id == protocolId
                                          select entry.SP_LicenseProgram.Code).FirstOrDefault();
                }
                string studySrok = string.Empty, studyFinish = string.Empty;
                switch (formId)
                {
                    case "1":
                        form = "очная форма обучения";
                        form2 = "очной";
                        studySrok = "1";
                        studyFinish = "31.08.2016";
                        break;
                    case "2":
                        form = "заочная форма обучения";
                        form2 = "заочной";
                        studySrok = "1";
                        studyFinish = "31.08.2017";
                        break;
                }

                //string educDoc = "об образовании";
                //string copyDoc = "оригиналы";
                //if (isList.HasValue && isList.Value)
                //{
                //    copyDoc = "заверенные ксерокопии";
                //}

                string dogovorDoc = "";
                switch (basisId)
                {
                    case "1":
                        basis = "за счет бюджетных ассигнований федерального бюджета";
                        dogovorDoc = ", оригиналы документов установленного образца об образовании";
                        break;
                    case "2":
                        basis = "по договорам об образовании";
                        dogovorDoc = ", договоры об образовании";
                        break;
                }


                wd.SetFields("Граждан", isRus ? "граждан Российской Федерации" : "иностранных граждан");
                wd.SetFields("Граждан2", isRus ? "граждан Российской Федерации" : "");
                wd.SetFields("Стипендия", (basisId == "2" || formId == "2") ? "" : "и назначении стипендии");
                wd.SetFields("Форма2", form2);
                wd.SetFields("Основа2", basis);
               // wd.SetFields("CopyDoc", copyDoc);
                wd.SetFields("DogovorDoc", dogovorDoc);
                wd.SetFields("Srok", studySrok);
                wd.SetFields("DateStart", DateStart);

                int curRow = 4;
                string curObProg = "-";
                string curCountry = "-";
                string curLPHeader = "-";
                string curHeader = "-";
                string curMotivation = "-";
                string Motivation = string.Empty;
                string stipendia = "", naprobProgRod = "";
                naprobProgRod = "образовательной программе";
                string naprspecRod = "направлению подготовки";

                using (PriemEntities ctx = new PriemEntities())
                {
                    var lst = (from extabit in ctx.extAbit
                               join extentryView in ctx.extEntryView on extabit.Id equals extentryView.AbiturientId
                               join extperson in ctx.extPersonAll on extabit.PersonId equals extperson.Id
                               join country in ctx.Country on extperson.NationalityId equals country.Id
                               join competition in ctx.Competition on extabit.CompetitionId equals competition.Id
                               join entryHeader in ctx.EntryHeader on extentryView.EntryHeaderId equals entryHeader.Id into entryHeader2
                               from entryHeader in entryHeader2.DefaultIfEmpty()
                               join celCompetition in ctx.CelCompetition on extabit.CelCompetitionId equals celCompetition.Id into celCompetition2
                               from celCompetition in celCompetition2.DefaultIfEmpty()
                               where extentryView.Id == protocolId && (isRus ? extperson.NationalityId == 1 : extperson.NationalityId != 1)
                               orderby celCompetition.TvorName, extabit.ObrazProgramName, extabit.ProfileName, country.NameRod, entryHeader.SortNum, extabit.FIO
                               select new
                               {
                                   Id = extabit.Id,
                                   extperson.Sex,
                                   Рег_Номер = extabit.RegNum,
                                   Ид_номер = extabit.PersonNum,
                                   TotalSum = (extabit.CompetitionId == 8 || extabit.CompetitionId == 1) ? null : extabit.Sum,
                                   ФИО = extabit.FIO,
                                   CelCompName = celCompetition.TvorName,
                                   LicenseProgramName = extabit.LicenseProgramName,
                                   LicenseProgramCode = extabit.LicenseProgramCode,
                                   ProfileName = extabit.ProfileName,
                                   ObrazProgram = extabit.ObrazProgramName,
                                   ObrazProgramId = extabit.ObrazProgramId,
                                   EntryHeaderId = entryHeader.Id,
                                   SortNum = entryHeader.SortNum,
                                   EntryHeaderName = entryHeader.Name,
                                   NameRod = country.NameRod
                               }).ToList().Distinct().Select(x =>
                                   new
                                   {
                                       Id = x.Id.ToString(),
                                       x.Sex,
                                       Рег_Номер = x.Рег_Номер,
                                       Ид_номер = x.Ид_номер,
                                       TotalSum = x.TotalSum.ToString(),
                                       ФИО = x.ФИО,
                                       CelCompName = x.CelCompName,
                                       LicenseProgramName = x.LicenseProgramName,
                                       LicenseProgramCode = x.LicenseProgramCode,
                                       ProfileName = x.ProfileName,
                                       ObrazProgram = x.ObrazProgram.Replace("(очно-заочная)", "").Replace(" ВВ", ""),
                                       ObrazProgramId = x.ObrazProgramId,
                                       EntryHeaderId = x.EntryHeaderId,
                                       SortNum = x.SortNum,
                                       EntryHeaderName = x.EntryHeaderName,
                                       NameRod = x.NameRod
                                   }
                               );

                    int pos = 0;
                    bool bFirstRun = true;

                    foreach (var v in lst)
                    {
                        pos++;
                        string header = v.EntryHeaderName;

                        if (!isCel && !bFirstRun)
                        {
                            if (header != curHeader)
                            {
                                td.AddRow(1);
                                curRow++;
                                td[0, curRow] = string.Format("\t{0}:", header);

                                curHeader = header;
                            }
                        }

                        bFirstRun = false;

                        string LP = v.LicenseProgramName;
                        string LPCode = v.LicenseProgramCode;
                        if (curLPHeader != LP)
                        {
                            td.AddRow(1);
                            curRow++;
                            td[0, curRow] = string.Format("{3}\tпо {0} {1} \"{2}\"", naprspecRod, LPCode, LP, curObProg == "-" ? "" : "\r\n");
                            curLPHeader = LP;
                        }

                        int Code = 0;
                        string Num = LPCode.Substring(LPCode.IndexOf('.') + 1);
                        Num = Num.Substring(0, Num.IndexOf('.'));
                        if (int.TryParse(Num, out Code))
                        {
                            if (Code < 38)
                                stipendia = "6330";
                            else
                                stipendia = "2637";
                        }

                        string ObrazProgramId = v.ObrazProgramId.ToString();
                        string obProg = v.ObrazProgram;
                        string obProgCode = (from entry in ctx.extEntry
                                             where entry.ObrazProgramId == v.ObrazProgramId
                                             select entry.ObrazProgramCrypt).FirstOrDefault();

                        if (ObrazProgramId != curObProg)
                        {
                            if (obProg != String.Empty)
                            {   
                                td.AddRow(1);
                                curRow++;
                                td[0, curRow] = string.Format("\tпо {0} {1} \"{2}\"", naprobProgRod, obProgCode, obProg);
                            }
                            curObProg = ObrazProgramId;
                            if (!isCel)
                            {
                                if (header != curHeader)
                                {
                                    td.AddRow(1);
                                    curRow++;
                                    td.AddRow(1);
                                    curRow++;
                                    td[0, curRow] = string.Format("\t{0}:", header);
                                    td.AddRow(1);
                                    curRow++;
                                    curHeader = header;
                                }
                            }
                        }

                        if (!isRus)
                        {
                            string country = v.NameRod;
                            if (country != curCountry)
                            {
                                td.AddRow(1);
                                curRow++;
                                td[0, curRow] = string.Format("\r\n граждан {0}:", country);

                                curCountry = country;
                            }
                        }



                        string balls = v.TotalSum;
                        string ballToStr = " балл";

                        if (balls.Length == 0)
                            ballToStr = "";
                        else if (balls.EndsWith("1"))
                        { 
                            if (balls.EndsWith("11"))
                                ballToStr +="ов";
                            else
                                ballToStr += ""; 
                        }
                        else if (balls.EndsWith("2") || balls.EndsWith("3") || balls.EndsWith("4"))
                        {
                            if ((balls.EndsWith("12") || balls.EndsWith("13") || balls.EndsWith("14")))
                                ballToStr += "ов";
                            else
                                ballToStr += "а";
                        }
                        else
                            ballToStr += "ов";

                        td.AddRow(1);
                        curRow++;
                        td[0, curRow] = string.Format("\t\t1.{0}. {1} {2} ", pos, v.ФИО + ',', balls + ballToStr);
                    }
                }

                if (!string.IsNullOrEmpty(curMotivation) && isCel)
                    td[0, curRow] += "\n\t\t" + curMotivation + "\n";


                if (basisId != "2" && formId == "1")//платникам и всем очно-заочникам стипендия не платится
                {
                    td.AddRow(1);
                    curRow++;
                    td[0, curRow] = "\r\n2.  Назначить лицам, указанным в п. 1 настоящего приказа, стипендию в размере  " + stipendia + @" рублей ежемесячно с " + DateStart + " по " + DateOfFinish + ".";
                }
            }
            catch (WordException we)
            {
                WinFormsServ.Error(we);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
        }

        public static void PrintOrderReview(Guid protocolId, bool isRus)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\EntryOrderList.dot", MainClass.dirTemplates));

                string query = @"SELECT extAbitAspirant.Id as Id, extAbitAspirant.RegNum as Рег_Номер, 
                     extAbitAspirant.PersonNum as 'Ид. номер', extAbitMarksSum.TotalSum, 
                     extAbitAspirant.FIO as ФИО, 
                     extAbitAspirant.LicenseProgramCode + ' ' + extAbitAspirant.LicenseProgramName, extAbitAspirant.ProfileName, 
                     replace(replace(extAbitAspirant.ObrazProgramName, '(очно-заочная)', ''), ' ВВ', '')  as ObrazProgram, extAbitAspirant.ObrazProgramCrypt, 
                     EntryHeader.Id as EntryHeaderId, EntryHeader.Name as EntryHeaderName, Country.NameRod,
                     extAbitMarksSum.TotalSumFiveGrade,
                     extAbitAspirant.CompetitionId,
                     extEntryView.SignerName,
                     extEntryView.SignerPosition
                     FROM ed.extAbitAspirant 
                     INNER JOIN ed.extEntryView ON extEntryView.AbiturientId=extAbitAspirant.Id 
                     INNER JOIN ed.Person ON Person.Id = extAbitAspirant.PersonId 
                     INNER JOIN ed.Country ON Person.NationalityId = Country.Id 
                     INNER JOIN ed.Competition ON Competition.Id = extAbitAspirant.CompetitionId 
                     LEFT JOIN ed.EntryHeader ON EntryHeader.Id = extEntryView.EntryHeaderId 
                     LEFT JOIN ed.extAbitMarksSum ON extAbitAspirant.Id = extAbitMarksSum.Id";

                string where = " WHERE extEntryView.Id = @protocolId ";
                where += " AND Person.NationalityId" + (isRus ? "=1 " : "<>1 ");
                string orderby = " ORDER BY ObrazProgram, extAbitAspirant.ProfileName, NameRod, EntryHeader.Id, ФИО ";
                SortedList<string, object> slDel = new SortedList<string, object>();

                slDel.Add("@protocolId", protocolId);

                DataSet ds = MainClass.Bdc.GetDataSet(query + where + orderby, slDel);

                DateTime protocolDate = (DateTime)MainClass.Bdc.GetValue(string.Format("SELECT Protocol.Date FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));
                string protocolNum = MainClass.Bdc.GetStringValue(string.Format("SELECT Protocol.Number FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));

                string DateStart = "01.09.2014";
                string DateOfFinish = "30.06.2015";
                string docNum = "НОМЕР";
                string docDate = "ДАТА";
                DateTime tempDate;
                if (isRus)
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNum FROM ed.OrderNumbers WHERE ProtocolId='{0}'", protocolId));
                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDate FROM ed.OrderNumbers WHERE ProtocolId='{0}'", protocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }
                else
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNumFor FROM ed.OrderNumbers WHERE ProtocolId='{0}'", protocolId));

                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDateFor FROM ed.OrderNumbers WHERE ProtocolId='{0}'", protocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }

                string formId = MainClass.Bdc.GetStringValue("SELECT StudyForm.Id FROM ed.Protocol INNER JOIN ed.StudyForm ON Protocol.StudyFormId=StudyForm.Id WHERE Protocol.Id= @protocolId", slDel);
                string facDat = MainClass.Bdc.GetStringValue("SELECT SP_Faculty.DatName FROM ed.Protocol INNER JOIN ed.SP_Faculty ON Protocol.FacultyId=SP_Faculty.Id WHERE Protocol.Id= @protocolId", slDel);

                string basisId = MainClass.Bdc.GetStringValue("SELECT StudyBasis.Id FROM ed.Protocol INNER JOIN ed.StudyBasis ON ed.Protocol.StudyBasisId=StudyBasis.Id WHERE Protocol.Id= @protocolId", slDel);
                string basis = string.Empty;
                string form = string.Empty;
                string form2 = string.Empty;

                string profession = MainClass.Bdc.GetStringValue("SELECT LicenseProgramName FROM ed.qEntry INNER JOIN ed.extEntryView ON qEntry.LicenseProgramId=extEntryView.LicenseProgramId WHERE extEntryView.Id= @protocolId", slDel);
                string professionCode = MainClass.Bdc.GetStringValue("SELECT LicenseProgramCode FROM ed.qEntry INNER JOIN ed.extEntryView ON qEntry.LicenseProgramId=extEntryView.LicenseProgramId WHERE extEntryView.Id= @protocolId", slDel);

                string dogovorDoc = "";
                switch (basisId)
                {
                    case "1":
                        basis = "за счет бюджетных ассигнований федерального бюджета";
                        dogovorDoc = ", оригиналы документов установленного образца об образовании";
                        break;
                    case "2":
                        basis = "по договорам об образовании";
                        dogovorDoc = ", договоры об образовании";
                        break;
                }
                string studySrok = string.Empty, studyFinish = string.Empty;
                switch (formId)
                {
                    case "1":
                        form = "очная форма обучения";
                        form2 = "очной ";
                        studySrok = "1";
                        studyFinish = "31.08.2016";
                        break;
                    case "2":
                        form = "заочная форма обучения";
                        form2 = "заочной";
                        studySrok = "1";
                        studyFinish = "31.08.2017";
                        break;
                }

                string  naprspecRod = "", profspec = "";
                string naprobProgRod = "образовательной программе"; ;

                if (MainClass.dbType == PriemType.PriemAspirant)
                {
                    naprspecRod = "направлению";
                    profspec = "магистерской программе";
                }

                int curRow = 5, counter = 0;
                TableDoc td = null;

                int Code = 0;
                string stipendia = "";
                string Num = professionCode.Substring(professionCode.IndexOf('.') + 1);
                Num = Num.Substring(0, Num.IndexOf('.'));
                if (int.TryParse(Num, out Code))
                {
                    if (Code < 38)
                        stipendia = "6330";
                    else
                        stipendia = "2637";
                }

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    wd.InsertAutoTextInEnd("выписка", true);
                    wd.GetLastFields(12);
                    td = wd.Tables[counter];

                    wd.SetFields("Граждан", isRus ? "граждан РФ" : "иностранных граждан");
                    wd.SetFields("Граждан2", isRus ? "граждан Российской Федерации" : "");
                    wd.SetFields("Стипендия", (basisId == "2" || formId == "2") ? "" : "и назначении стипендии");
                    wd.SetFields("Форма2", form2);
                    wd.SetFields("Основа2", basis);

                    wd.SetFields("ПриказДата", docDate);
                    wd.SetFields("ПриказНомер", "№ " + docNum);

                    wd.SetFields("DogovorDoc", dogovorDoc);
                    wd.SetFields("Srok", studySrok);
                    wd.SetFields("DateStart", DateStart);

                    wd.SetFields("SignerPosition", r["SignerPosition"].ToString());
                    wd.SetFields("SignerName", r["SignerName"].ToString());

                    string curLPHeader = "-";
                    string curSpez = "-";
                    string curObProg = "-";
                    string curHeader = "-";
                    string curCountry = "-";

                    ++counter;

                    string LP = profession;
                    string LPCode = professionCode;
                    if (curLPHeader != LP)
                    {
                        td.AddRow(1);
                        curRow++;
                        td[0, curRow] = string.Format("{3}\tпо {0} {1} \"{2}\"", naprspecRod, LPCode, LP, curObProg == "-" ? "" : "\r\n");
                        curLPHeader = LP;
                    }

                    string obProg = r["ObrazProgram"].ToString();
                    string obProgCode = r["ObrazProgramCrypt"].ToString();
                    if (obProg != curObProg)
                    {
                        if (!string.IsNullOrEmpty(obProg))
                        {
                            td.AddRow(1);
                            curRow++;
                            td[0, curRow] = string.Format("\tпо {0} {1} \"{2}\"", naprobProgRod, obProgCode, obProg);
                        }

                        string spez = r["ProfileName"].ToString();

                        if (!string.IsNullOrEmpty(spez) && spez != "нет")
                        {
                            td.AddRow(1);
                            curRow++;
                            td[0, curRow] = string.Format("\t {0} \"{1}\"", profspec, spez);
                        }

                        curSpez = spez;

                        curObProg = obProg;
                    }
                    else
                    {
                        string spez = r["ProfileName"].ToString();
                        if (spez != curSpez)
                        {
                            if (!string.IsNullOrEmpty(spez) && spez != "нет")
                            {
                                td.AddRow(1);
                                curRow++;
                                td[0, curRow] = string.Format("\t {0} \"{1}\"", profspec, spez);
                            }

                            curSpez = spez;
                        }
                    }

                    if (!isRus)
                    {
                        string country = r["NameRod"].ToString();
                        if (country != curCountry)
                        {
                            td.AddRow(1);
                            curRow++;
                            td[0, curRow] = string.Format("\r\n граждан {0}:", country);

                            curCountry = country;
                        }
                    }

                    string header = r["EntryHeaderName"].ToString();
                    if (header != curHeader)
                    {
                        td.AddRow(1);
                        curRow++;
                        td[0, curRow] = string.Format("\t{0}:", header);

                        curHeader = header;
                    }

                    string balls ="";
                    if ((r["CompetitionId"].ToString() == "8") || (r["CompetitionId"].ToString() == "1"))
                        balls = "";
                    else
                        balls = r["TotalSumFiveGrade"].ToString();
                    string ballToStr = " балл";

                    if (balls.Length == 0)
                        ballToStr = "";
                    else if (balls.EndsWith("1"))
                    {
                        if (balls.EndsWith("1"))
                            ballToStr += "ов";
                        else
                            ballToStr += ""; 

                    }
                    else if (balls.EndsWith("2") || balls.EndsWith("3") || balls.EndsWith("4"))
                    {
                        if (balls.EndsWith("2") || balls.EndsWith("3") || balls.EndsWith("4"))
                            ballToStr += "ов";
                        else
                            ballToStr += "а";
                    }
                    else
                        ballToStr += "ов";

                    td.AddRow(1);
                    curRow++;
                    td[0, curRow] = string.Format("\t\t{0} {1}", r["ФИО"].ToString(), balls + ballToStr);

                    if (basisId != "2" && formId != "2")
                    {
                        td.AddRow(1);
                        curRow++;
                        td[0, curRow] = "\r\n2.      Назначить указанным лицам стипендию в размере 1200 рублей ежемесячно до 31 января 2013 г.";
                        td[0, curRow] = "\r\n2.  Назначить лицам, указанным в п. 1 настоящего приказа, стипендию в размере  " + stipendia + @" рублей ежемесячно с " + DateStart + " по " + DateOfFinish + ".";

                    }
                }

            }
            catch (WordException we)
            {
                WinFormsServ.Error(we);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
        }

        public static void PrintDisEntryOrder(string protocolId, bool isRus)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\DisEntryOrder.dot", MainClass.dirTemplates));
                TableDoc td = wd.Tables[0];

                string query = @"SELECT ed.extAbitAspirant.Id as Id, ed.extAbitAspirant.RegNum as Рег_Номер, 
                    ed.extPersonAspirant.PersonNum as 'Ид. номер', ed.extAbitMarksSum.TotalSum, 
                    ed.extPersonAspirant.FIO  as ФИО, 
                    ed.extAbitAspirant.LicenseProgramName, ed.extAbitAspirant.ProfileName as Specialization, ed.Country.NameRod 
                     FROM ed.extAbitAspirant 
                     INNER JOIN ed.extDisEntryView ON ed.extDisEntryView.AbiturientId=ed.extAbitAspirant.Id 
                     INNER JOIN ed.extPersonAspirant ON ed.extPersonAspirant.Id = ed.extAbitAspirant.PersonId 
                     INNER JOIN ed.Country ON ed.extPersonAspirant.NationalityId = ed.Country.Id
                     INNER JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId 
                     LEFT JOIN ed.extAbitMarksSum ON ed.extAbitAspirant.Id = ed.extAbitMarksSum.Id";

                string where = " WHERE ed.extDisEntryView.Id = @protocolId ";
                where += " AND ed.extPersonAspirant.NationalityId" + (isRus ? "=1 " : "<>1 ");
                string orderby = " ORDER BY ed.extAbitAspirant.ProfileName, NameRod ,ФИО ";

                SortedList<string, object> slDel = new SortedList<string, object>();

                slDel.Add("@protocolId", protocolId);

                DataSet ds = MainClass.Bdc.GetDataSet(query + where + orderby, slDel);

                string entryProtocolId = MainClass.Bdc.GetStringValue("SELECT ed.extProtocol.Id FROM ed.extDisEntryView INNER JOIN ed.extProtocol ON ed.extDisEntryView.AbiturientId=ed.extProtocol.AbiturientId WHERE ed.extDisEntryView.Id=@protocolId AND ed.extProtocol.ProtocolTypeId=4 AND ed.extprotocol.isold=0 ", slDel);

                string docNum = "НОМЕР";
                string docDate = "ДАТА";
                DateTime tempDate;
                if (isRus)
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNum FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId));
                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDate FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }
                else
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNumFor FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId));

                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDateFor FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }

                string formId = MainClass.Bdc.GetStringValue("SELECT StudyForm.Id FROM ed.Protocol INNER JOIN StudyForm ON Protocol.StudyFormId=StudyForm.Id WHERE Protocol.Id= @protocolId", slDel);
                string facDat = MainClass.Bdc.GetStringValue("SELECT SP_Faculty.DatName FROM ed.Protocol INNER JOIN SP_Faculty ON Protocol.FacultyId=SP_Faculty.Id WHERE Protocol.Id= @protocolId", slDel);

                string basisId = MainClass.Bdc.GetStringValue("SELECT StudyBasis.Id FROM ed.Protocol INNER JOIN StudyBasis ON Protocol.StudyBasisId=StudyBasis.Id WHERE Protocol.Id= @protocolId", slDel);
                string basis = string.Empty;
                string form = string.Empty;
                string form2 = string.Empty;

                bool? isSec = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsSecond FROM ed.Protocol  WHERE Protocol.Id= '{0}'", protocolId));
                bool? isReduced = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsReduced FROM ed.Protocol  WHERE Protocol.Id= '{0}'", protocolId));
                bool? isList = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsListener FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));

                string list = string.Empty, sec = string.Empty;

                if (isList.HasValue && isList.Value)
                    list = " в качестве слушателя";

                if (isReduced.HasValue && isReduced.Value)
                    sec = " (сокращенной)";

                if (isSec.HasValue && isSec.Value)
                    sec = " (для лиц с высшим образованием)";

                string LicenseProgramName = MainClass.Bdc.GetStringValue("SELECT qEntry.LicenseProgramName FROM ed.qEntry INNER JOIN ed.extDisEntryView ON qEntry.LicenseProgramId=extDisEntryView.LicenseProgramId WHERE extDisEntryView.Id= @protocolId AND extDisEntryView.StudyLevelGroupId=@StudyLevelGroupId", slDel);
                string LicenseProgramCode = MainClass.Bdc.GetStringValue("SELECT qEntry.LicenseProgramCode FROM ed.qEntry INNER JOIN ed.extDisEntryView ON qEntry.LicenseProgramId=extDisEntryView.LicenseProgramId WHERE extDisEntryView.Id= @protocolId AND extDisEntryView.StudyLevelGroupId=@StudyLevelGroupId", slDel);

                switch (basisId)
                {
                    case "1":
                        basis = "обучение за счет средств федерального бюджета";
                        break;
                    case "2":
                        basis = string.Format("по договорам оказания государственной услуги по обучению по основной{0} образовательной программе высшего профессионального образования", sec);
                        break;
                }

                switch (formId)
                {
                    case "1":
                        form = "очная форма обучения";
                        form2 = "по очной форме";
                        break;
                    case "2":
                        form = "очно-заочная (вечерняя) форма обучения";
                        form2 = "по очно-заочной (вечерней) форме";
                        break;
                }

                wd.SetFields("Граждан", isRus ? "граждан РФ" : "иностранных граждан");
                wd.SetFields("Граждан2", isRus ? "граждан Российской Федерации" : "");
                wd.SetFields("Стипендия", (basisId == "2" || formId == "2") ? "" : "\r\nи назначении стипендии");
                wd.SetFields("Стипендия2", (basisId == "2" || formId == "2") ? "" : " и назначении стипендии");
                wd.SetFields("Факультет", facDat);
                wd.SetFields("Форма", form);
                wd.SetFields("Основа", basis);
                wd.SetFields("БакСпец", "аспиранта");
                wd.SetFields("НапрСпец", string.Format(" направлению {0} «{1}»", LicenseProgramCode, LicenseProgramName));
                wd.SetFields("ПриказОт", docDate);
                wd.SetFields("ПриказНомер", docNum);
                wd.SetFields("ПриказОт2", docDate);
                wd.SetFields("ПриказНомер2", docNum);
                wd.SetFields("Сокращ", sec);

                int curRow = 4;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    td.AddRow(1);
                    curRow++;
                    td[0, curRow] = string.Format("\t\tп. № {0} {1} - исключить.", r["ФИО"].ToString(), r["TotalSum"]);
                }
            }
            catch (WordException we)
            {
                WinFormsServ.Error(we);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
        }

        public static void PrintDisEntryView(string protocolId)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\DisEntryView.dot", MainClass.dirTemplates));
                TableDoc td = wd.Tables[0];

                string query = @"SELECT ed.extAbitAspirant.Id as Id, ed.extAbitAspirant.RegNum as Рег_Номер, 
                    ed.extPersonAspirant.PersonNum as 'Ид. номер', ed.extAbitMarksSum.TotalSum, 
                    ed.extPersonAspirant.FIO  as ФИО, 
                    ed.extAbitAspirant.LicenseProgramName, ed.extAbitAspirant.ProfileName as Specialization, ed.Country.NameRod 
                     FROM ed.extAbitAspirant 
                     INNER JOIN ed.extDisEntryView ON ed.extDisEntryView.AbiturientId=ed.extAbitAspirant.Id 
                     INNER JOIN ed.extPersonAspirant ON ed.extPersonAspirant.Id = ed.extAbitAspirant.PersonId 
                     INNER JOIN ed.Country ON ed.extPersonAspirant.NationalityId = ed.Country.Id
                     INNER JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId 
                     LEFT JOIN ed.extAbitMarksSum ON ed.extAbitAspirant.Id = ed.extAbitMarksSum.Id";

                string where = " WHERE extDisEntryView.Id = @protocolId";
                string orderby = " ORDER BY extAbitAspirant.ProfileName, NameRod, ФИО ";

                DateTime protocolDate = (DateTime)MainClass.Bdc.GetValue(string.Format("SELECT Protocol.Date FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));
                string protocolNum = MainClass.Bdc.GetStringValue(string.Format("SELECT Protocol.Number FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));

                SortedList<string, object> slDel = new SortedList<string, object>();

                slDel.Add("@protocolId", protocolId);

                DataSet ds = MainClass.Bdc.GetDataSet(query + where + orderby, slDel);

                bool isRus = "1" == MainClass.Bdc.GetStringValue(" SELECT NationalityId FROM ed.extPersonAspirant INNER JOIN ed.Abiturient on Abiturient.personid=person.id INNER JOIN ed.extDisEntryView ON extDisEntryView.AbiturientId=Abiturient.Id WHERE extDisEntryView.Id=@protocolId", slDel);

                string entryProtocolId = MainClass.Bdc.GetStringValue("SELECT extProtocol.Id FROM ed.extDisEntryView INNER JOIN ed.extProtocol ON ed.extDisEntryView.AbiturientId=extProtocol.AbiturientId WHERE extProtocol.isOld = 0 and extDisEntryView.Id=@protocolId AND extProtocol.ProtocolTypeId=4 ", slDel);

                string docNum = "НОМЕР";
                string docDate = "ДАТА";
                DateTime tempDate;
                if (isRus)
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNum FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId));
                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDate FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }
                else
                {
                    docNum = MainClass.Bdc.GetStringValue(string.Format("SELECT OrderNumFor FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId));

                    DateTime.TryParse(MainClass.Bdc.GetStringValue(string.Format("SELECT OrderDateFor FROM ed.OrderNUmbers WHERE ProtocolId='{0}'", entryProtocolId)), out tempDate);

                    docDate = tempDate.ToShortDateString();
                }

                string formId = MainClass.Bdc.GetStringValue("SELECT StudyForm.Id FROM ed.Protocol INNER JOIN ed.StudyForm ON Protocol.StudyFormId=StudyForm.Id WHERE Protocol.Id= @protocolId", slDel);
                string facDat = MainClass.Bdc.GetStringValue("SELECT SP_Faculty.DatName FROM ed.Protocol INNER JOIN ed.SP_Faculty ON Protocol.FacultyId=SP_Faculty.Id WHERE Protocol.Id= @protocolId", slDel);

                string basisId = MainClass.Bdc.GetStringValue("SELECT StudyBasis.Id FROM ed.Protocol INNER JOIN ed.StudyBasis ON Protocol.StudyBasisId=StudyBasis.Id WHERE Protocol.Id= @protocolId", slDel);
                string basis = string.Empty;
                string form = string.Empty;
                string form2 = string.Empty;

                bool? isSec = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsSecond FROM ed.Protocol WHERE Protocol.Id= '{0}'", protocolId));
                bool? isList = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsListener FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));
                bool? isReduced = (bool?)MainClass.Bdc.GetValue(string.Format("SELECT IsReduced FROM ed.Protocol WHERE Protocol.Id='{0}'", protocolId));

                string list = string.Empty, sec = string.Empty;

                if (isList.HasValue && isList.Value)
                    list = " в качестве слушателя";

                if (isSec.HasValue && isSec.Value)
                    sec = " (для лиц с ВО)";

                if (isReduced.HasValue && isReduced.Value)
                    sec = " (сокращенной)";


                string LicenseProgramName = MainClass.Bdc.GetStringValue("SELECT TOP 1 qEntry.LicenseProgramName FROM ed.qEntry INNER JOIN ed.extDisEntryView ON qEntry.LicenseProgramId=extDisEntryView.LicenseProgramId WHERE extDisEntryView.Id= @protocolId AND extDisEntryView.StudyLevelGroupId=@StudyLevelGroupId", slDel);
                string LicenseProgramCode = MainClass.Bdc.GetStringValue("SELECT TOP 1 qEntry.LicenseProgramCode FROM ed.qEntry INNER JOIN ed.extDisEntryView ON qEntry.LicenseProgramId=extDisEntryView.LicenseProgramId WHERE extDisEntryView.Id= @protocolId AND extDisEntryView.StudyLevelGroupId=@StudyLevelGroupId", slDel);
                
                switch (basisId)
                {
                    case "1":
                        basis = "обучение за счет средств федерального бюджета";
                        break;
                    case "2":
                        basis = string.Format("по договорам оказания государственной услуги по обучению по основной{0} образовательной программе высшего профессионального образования", sec);
                        break;
                }

                switch (formId)
                {
                    case "1":
                        form = "очная форма обучения";
                        form2 = "по очной форме";
                        break;
                    case "2":
                        form = "очно-заочная (вечерняя) форма обучения";
                        form2 = "по очно-заочной (вечерней) форме";
                        break;
                }

                wd.SetFields("Граждан", isRus ? "граждан РФ" : "иностранных граждан");
                wd.SetFields("Граждан2", isRus ? "граждан Российской Федерации" : "");
                wd.SetFields("Стипендия", basisId == "2" ? "" : "и назначении стипендии");
                wd.SetFields("Стипендия2", basisId == "2" ? "" : "и назначении стипендии");
                wd.SetFields("Факультет", facDat);
                wd.SetFields("Форма", form);
                wd.SetFields("Основа", basis);
                wd.SetFields("БакСпец", "аспиранта");
                wd.SetFields("НапрСпец", string.Format(" направлению {0} «{1}»", LicenseProgramCode, LicenseProgramName));
                wd.SetFields("ПриказОт", docDate);
                wd.SetFields("ПриказНомер", docNum);
                wd.SetFields("ПриказОт2", docDate);
                wd.SetFields("ПриказНомер2", docNum);
                wd.SetFields("ПредставлениеОт", protocolDate.ToShortDateString());
                wd.SetFields("ПредставлениеНомер", protocolNum);
                wd.SetFields("Сокращ", sec);

                int curRow = 4;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    td.AddRow(1);
                    curRow++;
                    td[0, curRow] = string.Format("\t\tп. № {0}, {1} - исключить.", r["ФИО"].ToString(), r["TotalSum"]);
                }
            }
            catch (WordException we)
            {
                WinFormsServ.Error(we);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error(exc);
            }
        }
        public static string[] GetSplittedStrings(string sourceStr, int firstStrLen, int strLen, int numOfStrings)
        {
            sourceStr = sourceStr ?? "";
            string[] retStr = new string[numOfStrings];
            int index = 0, startindex = 0;
            for (int i = 0; i < numOfStrings; i++)
            {
                if (sourceStr.Length > startindex && startindex >= 0)
                {
                    int rowLength = firstStrLen;//длина первой строки
                    if (i > 1) //длина остальных строк одинакова
                        rowLength = strLen;
                    index = startindex + rowLength;
                    if (index < sourceStr.Length)
                    {
                        index = sourceStr.IndexOf(" ", index);
                        string val = index > 0 ? sourceStr.Substring(startindex, index - startindex) : sourceStr.Substring(startindex);
                        retStr[i] = val;
                    }
                    else
                        retStr[i] = sourceStr.Substring(startindex);
                }
                startindex = index;
            }

            return retStr;
        }
    }
}
