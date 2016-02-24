using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using EducServLib;
//using BDClassLib;
using WordOut;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class VedExamLists : BookList
    {       
        //конструктор
        public VedExamLists()
        {
            InitializeComponent();
          
            Dgv = dgvVed;        
           _title = "Ведомость выдачи экзаменационных листов";

            InitControls();                     
            InitHandlers();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            Dgv.ReadOnly = false;

            using (PriemEntities context = new PriemEntities())
            {
                ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, false);
                ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name"), false, true);
                cbStudyBasis.SelectedIndex = 0;

                ComboServ.FillCombo(cbStudyForm, HelpClass.GetComboListByTable("ed.StudyForm", "ORDER BY Id"), false, true);
                cbStudyForm.SelectedIndex = 0;

                FillLicenseProgram();

                ComboServ.FillCombo(cbCompetition, HelpClass.GetComboListByTable("ed.Competition"), false, true);

                UpdateDataGrid();

                if (MainClass.IsFacMain() || MainClass.IsOwner())
                    btnLists.Enabled = true;
                else
                    btnLists.Enabled = false;
            }
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

        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public int? StudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm); }
            set { ComboServ.SetComboId(cbStudyForm, value); }
        }

        public int? CompetitionId
        {
            get { return ComboServ.GetComboIdInt(cbCompetition); }
            set { ComboServ.SetComboId(cbCompetition, value); }
        }
       
       
        private void FillLicenseProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<KeyValuePair<string, string>> lst = ((from ent in MainClass.GetEntry(context)
                                                           where ent.FacultyId == FacultyId && ent.StudyFormId == StudyFormId
                                                           select new
                                                           {
                                                               Id = ent.LicenseProgramId,
                                                               Name = ent.LicenseProgramName
                                                           }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                ComboServ.FillCombo(cbLicenseProgram, lst, false, false);
                cbLicenseProgram.SelectedIndex = 0;
            }
        }
        
        //инициализация обработчиков мегакомбов
        public override void  InitHandlers() 
        {
            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbCompetition.SelectedIndexChanged += new EventHandler(cbCompetition_SelectedIndexChanged);                             
        }

        void cbCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();            
        }

        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
            UpdateDataGrid();
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
            UpdateDataGrid();
        }       

        protected override void OpenCard(string id, BaseFormEx formOwner, int? index)
        {
            MainClass.OpenCardAbit(id, this, dgvVed.CurrentRow.Index);
        }

        //обновление грида
        public override void UpdateDataGrid()
        {
            string sFilters = GetFilterString();

            string _query = @"SELECT ed.extAbitAspirant.Id as Id, RegNum as Рег_Номер, 
                        FIO as ФИО, 
                        ed.Competition.Name AS 'Тип конкурса', ed.extEnableProtocol.Number AS 'Протокол_о_допуске' 
                        FROM ed.extAbitAspirant LEFT JOIN ed.extEnableProtocol ON ed.extEnableProtocol.AbiturientId = ed.extAbitAspirant.Id 
                        LEFT JOIN ed.Competition ON ed.extAbitAspirant.CompetitionId = ed.Competition.Id 
                        WHERE ed.extAbitAspirant.BackDoc = 0 AND ed.extAbitAspirant.Id IN (SELECT ed.extProtocol.AbiturientId FROM ed.extProtocol 
                        WHERE ed.extProtocol.ProtocolTypeId = 1 AND ed.extProtocol.IsOld = 0)";           

            DataSet ds = _bdc.GetDataSet(_query + " " + sFilters + " ORDER BY ФИО");
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
           
            DataColumn clm;
            clm = new DataColumn();
            clm.ColumnName = "Печать";
            clm.DataType = typeof(bool);
            dt.Columns.Add(clm);
                       
            DataView dv = new DataView(dt);
            dv.AllowNew = false;

            dgvVed.DataSource = dv;
            dgvVed.Columns["Печать"].DisplayIndex = 0;
            dgvVed.Columns["Id"].Visible = false;
            dgvVed.ReadOnly = false;
            dgvVed.Columns["ФИО"].ReadOnly = true; 
            dgvVed.Columns["Тип конкурса"].ReadOnly = true;
            dgvVed.Columns["Рег_Номер"].ReadOnly = true; 
            dgvVed.Columns["Печать"].ReadOnly = false;
            dgvVed.Update();

            lblCount.Text = dgvVed.RowCount.ToString();
        }        

        private string GetFilterString()
        {
            string s = MainClass.GetStLevelFilter("ed.extAbitAspirant");
            
            //обработали факультет       
            if (FacultyId != null)
                s += string.Format(" AND ed.extAbitAspirant.FacultyId = {0}", FacultyId);           
            
            //обработали форму обучения           
            if (StudyFormId != null)
                s += string.Format(" AND ed.extAbitAspirant.StudyFormId = {0}", StudyFormId);

            //обработали основу обучения 
            if (StudyBasisId != null)
                s += string.Format(" AND ed.extAbitAspirant.StudyBasisId = {0}", StudyBasisId);

            //обработали тип конкурса            
            if (CompetitionId != null)
                s += string.Format(" AND ed.extAbitAspirant.CompetitionId = {0}", CompetitionId);  

            //обработали Направление
            if (LicenseProgramId != null)
                s += string.Format(" AND ed.extAbitAspirant.LicenseProgramId = {0}", LicenseProgramId); 

            return s;
        } 
       
        private void VedExamLists_Activated(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        // Кнопка Печать
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\ExamVed.dot", MainClass.dirTemplates));
                TableDoc td = wd.Tables[0];

                //переменные
                string sFac = cbFaculty.Text.ToString().ToLower();
                if (sFac.CompareTo("все") == 0)
                    sFac = "всех факультетов ";
                else
                {                   
                    if (FacultyId == 3)
                        sFac = "высшей школы менеджмента ";
                    else
                        sFac = sFac.Replace("кий", "кого ").Replace("ый", "ого ").Replace("акультет", "акультета");
                }

                string sForm = cbStudyForm.Text.ToString().ToLower();
                if (sForm.CompareTo("все") == 0)
                    sForm = " всех форм обучения ";
                else
                    sForm = sForm.Replace("ая", "ой").Replace("яя", "ей") + " формы обучения ";
                wd.Fields["Faculty"].Text = sFac;
                wd.Fields["Section"].Text = sForm;

                int i = 1;

                // печать из грида
                foreach (DataGridViewRow dgvr in dgvVed.Rows)
                {
                    if (chbPrintChecked.Checked)
                    {
                        if (dgvr.Cells["Печать"].Value.ToString() == "True")
                        {
                            td[0, i] = i.ToString();
                            td[1, i] = dgvr.Cells["Рег_Номер"].Value.ToString();
                            td[2, i] = dgvr.Cells["ФИО"].Value.ToString();
                            td[3, i] = dgvr.Cells["Тип конкурса"].Value.ToString();
                            td.AddRow(1);
                            i++;
                        }
                    }
                    else
                    {
                        td[0, i] = i.ToString();
                        td[1, i] = dgvr.Cells["Рег_Номер"].Value.ToString();
                        td[2, i] = dgvr.Cells["ФИО"].Value.ToString();
                        td[3, i] = dgvr.Cells["Тип конкурса"].Value.ToString();
                        td.AddRow(1);
                        i++;
                    }                    
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
        }

        private void btnLists_Click(object sender, EventArgs e)
        {
            if (MainClass.RightsFacMain())
            {
                foreach (DataGridViewRow dgvr in dgvVed.Rows)
                {
                    if (dgvr.Cells["Печать"].Value.ToString() == "True")
                    {
                        Guid abitId = new Guid(dgvr.Cells["Id"].Value.ToString());
                        PriemLib.Print.PrintExamListWord(abitId, false/*true*/);
                    }
                }
            }
            else
                WinFormsServ.Error("Невозможно создание экзаменационных листов, недостаточно прав");            

        }        

        private void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvVed.Rows)
            {
                if (chbAll.Checked)
                    dgvr.Cells["Печать"].Value = true;
                else
                    dgvr.Cells["Печать"].Value = false;                   
            }               
        }          
    }  
}
