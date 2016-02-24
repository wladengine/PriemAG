using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using EducServLib;
using BDClassLib;
using WordOut;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class OlympAbitList : BookList
    {
        //конструктор
        public OlympAbitList()
        {
            InitializeComponent(); 
            
            Dgv = dgvAbitList;
            _title = "Список абитуриентов с олимпиадами";
           
            InitControls();
            InitHandlers(); 
        }

        protected override void ExtraInit()             
        {
            base.ExtraInit();

            btnAdd.Visible = btnRemove.Visible = false;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, false);
                    ComboServ.FillCombo(cbStudyForm, HelpClass.GetComboListByTable("ed.StudyForm", "ORDER BY Id"), false, false);

                    FillLicenseProgram();
                    FillObrazProgram();    

                    ComboServ.FillCombo(cbLanguage, HelpClass.GetComboListByTable("ed.Language"), false, true);
                    ComboServ.FillCombo(cbOlympType, HelpClass.GetComboListByTable("ed.OlympType", " ORDER BY Id"), false, true);
                    ComboServ.FillCombo(cbOlympName, HelpClass.GetComboListByTable("ed.OlympName"), false, true);
                    ComboServ.FillCombo(cbOlympSubject, HelpClass.GetComboListByTable("ed.OlympSubject"), false, true);
                    ComboServ.FillCombo(cbOlympValue, HelpClass.GetComboListByTable("ed.OlympValue"), false, true);
                    ComboServ.FillCombo(cbOlympLevel, HelpClass.GetComboListByTable("ed.OlympLevel"), false, true);

                    UpdateDataGrid();                    
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы ", exc);
            }           
        }

        //инициализация обработчиков мегакомбов
        public override void  InitHandlers()
        {
            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbObrazProgram.SelectedIndexChanged += new EventHandler(cbObrazProgram_SelectedIndexChanged);           
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);

            cbLanguage.SelectedIndexChanged += new EventHandler(cbLanguage_SelectedIndexChanged);
            cbOlympType.SelectedIndexChanged += new EventHandler(cbOlympType_SelectedIndexChanged);
            cbOlympName.SelectedIndexChanged += new EventHandler(cbOlympName_SelectedIndexChanged);
            cbOlympSubject.SelectedIndexChanged += new EventHandler(cbOlympSubject_SelectedIndexChanged);
            cbOlympValue.SelectedIndexChanged += new EventHandler(cbOlympValue_SelectedIndexChanged);
            cbOlympLevel.SelectedIndexChanged += new EventHandler(cbOlympLevel_SelectedIndexChanged);
        }

        void cbOlympLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbOlympValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbOlympSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbOlympName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbOlympType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
            UpdateDataGrid();
        }

        void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
            UpdateDataGrid();
        }

        void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {           
            UpdateDataGrid();
        }

        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }

        public int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }

        public int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        public int? StudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm); }
            set { ComboServ.SetComboId(cbStudyForm, value); }
        }

        public int? OlympTypeId
        {
            get { return ComboServ.GetComboIdInt(cbOlympType); }
            set { ComboServ.SetComboId(cbOlympType, value); }
        }

        public int? OlympSubjectId
        {
            get { return ComboServ.GetComboIdInt(cbOlympSubject); }
            set { ComboServ.SetComboId(cbOlympSubject, value); }
        }

        public int? OlympLevelId
        {
            get { return ComboServ.GetComboIdInt(cbOlympLevel); }
            set { ComboServ.SetComboId(cbOlympLevel, value); }
        }

        public int? OlympValueId
        {
            get { return ComboServ.GetComboIdInt(cbOlympValue); }
            set { ComboServ.SetComboId(cbOlympValue, value); }
        }

        public int? OlympNameId
        {
            get { return ComboServ.GetComboIdInt(cbOlympName); }
            set { ComboServ.SetComboId(cbOlympName, value); }
        }

        public int? LanguageId
        {
            get { return ComboServ.GetComboIdInt(cbLanguage); }
            set { ComboServ.SetComboId(cbLanguage, value); }
        }

        private void FillLicenseProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<KeyValuePair<string, string>> lst = ((from ent in MainClass.GetEntry(context)
                                                           where ent.FacultyId == FacultyId
                                                           select new
                                                           {
                                                               Id = ent.LicenseProgramId,
                                                               Name = ent.LicenseProgramName
                                                           }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                ComboServ.FillCombo(cbLicenseProgram, lst, false, false);
                cbLicenseProgram.SelectedIndex = 0;
            }
        }

        private void FillObrazProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<KeyValuePair<string, string>> lst = ((from ent in MainClass.GetEntry(context)
                                                           where ent.FacultyId == FacultyId && ent.LicenseProgramId == LicenseProgramId
                                                           select new
                                                           {
                                                               Id = ent.ObrazProgramId,
                                                               Name = ent.ObrazProgramName,
                                                               Crypt = ent.ObrazProgramCrypt
                                                           }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Crypt)).ToList();

                ComboServ.FillCombo(cbObrazProgram, lst, false, false);
            }
        }

        protected override void GetSource()
        {
            _sQuery = string.Format(@"SELECT ed.extAbitAspirant.Id as Id, RegNum as Рег_Номер, ed.Person.Surname + ' ' + ed.Person.Name + ' ' + ed.Person.SecondName as ФИО,
                     ed.Competition.Name as Тип_конкурса, OlympTypeName as Вид, OlympName AS Название, OlympLevelName AS Уровень, OlympSubjectName as Предмет,
                     OlympValueName as Степень, StudyFormName as Форма_обучения, ed.Person.BirthDate as Дата_рождения, PassportData as Паспорт                      
                     FROM ed.extAbitAspirant 
                     INNER JOIN ed.Person ON ed.extAbitAspirant.PersonId = ed.Person.Id
                     INNER JOIN ed.extOlympiads ON ed.extOlympiads.AbiturientId = ed.extAbitAspirant.Id                     
                     LEFT JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId ");

            string sFilters = GetFilterString();

            HelpClass.FillDataGrid(Dgv, _bdc, _sQuery, sFilters, " ORDER BY FIO");            
        }

        //сбор фильтров 
        private string GetFilterString()
        {
            string s = " WHERE 1=1 ";

            //обработали форму обучения           
            if (StudyFormId != null)
                s += " AND ed.extAbitAspirant.StudyFormId = " + StudyFormId;

            //обработали факультет            
            if (FacultyId != null)
                s += " AND ed.extAbitAspirant.FacultyId = " + FacultyId;

            //обработали вид            
            if (OlympTypeId != null)
                s += " AND ed.extOlympiads.OlympTypeId = " + OlympTypeId;

            //обработали уровень            
            if (OlympLevelId != null)
                s += " AND ed.extOlympiads.OlympLevelId = " + OlympLevelId;

            //обработали предмет            
            if (OlympSubjectId != null)
                s += " AND ed.extOlympiads.OlympSubjectId = " + OlympSubjectId;

            //обработали значение            
            if (OlympValueId != null)
                s += " AND ed.extOlympiads.OlympValueId  = " + OlympValueId;

            //обработали название            
            if (OlympNameId != null)
                s += " AND ed.extOlympiads.OlympNameId  = " + OlympNameId;

            //обработали Направление            
            if (LicenseProgramId != null)
                s += " AND ed.extAbitAspirant.LicenseProgramId = " + LicenseProgramId;

            //обработали Образ программу             
            if (ObrazProgramId != null)
                s += " AND ed.extAbitAspirant.ObrazProgramId = " + ObrazProgramId;

            //обработали язык          
            if (LanguageId != null)
                s += " AND ed.extAbitAspirant.LanguageId = " + LanguageId;   
            
            return s;
        }

        //поиск по номеру
        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbitList, "Рег_Номер", tbNumber.Text);
        }

        //поиск по фио
        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbitList, "ФИО", tbFIO.Text);
        }

        //печать
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int k = 7;
                WordDoc wd = new WordDoc(string.Format(@"{0}\ListAbit.dot", MainClass.dirTemplates));

                wd.AddNewTable(dgvAbitList.Rows.Count + 1, k+1);
                TableDoc td = wd.Tables[0];

                //переменные
                string sFac = cbFaculty.Text.ToLower();
                if (sFac.CompareTo("все") == 0)
                    sFac = "всех факультетов ";
                else
                { 
                    if (FacultyId == 3)
                        sFac = "высшей школы менеджмента ";
                    else
                        sFac = sFac.Replace("кий", "кого ").Replace("ый", "ого ") + " факультета ";
                }

                string sForm = cbStudyForm.Text.ToLower();
                if (sForm.CompareTo("все") == 0)
                    sForm = " всех форм обучения ";
                else
                    sForm = sForm.Replace("ая", "ой").Replace("яя", "ей") + " формы обучения ";
                wd.Fields["Faculty"].Text = sFac;
                wd.Fields["Section"].Text = sForm;

                int i = 0;

                td[0, 0] = "№ п/п";
                for (int j = 0; j < k; j++)
                    td[j + 1, 0] = dgvAbitList.Columns[j+1].HeaderText;

                // печать из грида
                foreach (DataGridViewRow dgvr in dgvAbitList.Rows)
                {
                    td[0, i + 1] = (i + 1).ToString();
                    for (int j = 0; j < k; j++)
                        td[j + 1, i + 1] = dgvAbitList.Rows[i].Cells[j+1].Value.ToString();

                    i++;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
        }        

        protected override void OpenCard(string id, BaseFormEx formOwner, int? index)
        {
            MainClass.OpenCardAbit(id, this, dgvAbitList.CurrentRow.Index);
        }        
    }
}