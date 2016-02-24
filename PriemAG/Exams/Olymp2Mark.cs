using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Transactions;

using EducServLib;
//using BDClassLib;
using WordOut;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class Olymp2Mark : BookList
    {         
        //конструктор
        public Olymp2Mark()
        {
            InitializeComponent();
            Dgv = dgvAbitList;
            _title = "Зачет результатов олимпиад как 100 баллов за экзамен";           

            InitControls();           
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            Dgv.ReadOnly = false;         
            btnRemove.Visible = btnAdd.Visible = false;

            //this.Width = 774;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, false);
                    ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name"), false, true);

                    cbStudyBasis.SelectedIndex = 0;
                    FillStudyForm();
                    FillLicenseProgram();
                    FillObrazProgram();                   
                   
                    FillExams();
                                       
                    ComboServ.FillCombo(cbOlympType, HelpClass.GetComboListByTable("ed.OlympType", " ORDER BY Id"), false, true);
                    ComboServ.FillCombo(cbOlympName, HelpClass.GetComboListByTable("ed.OlympName"), false, true);
                    ComboServ.FillCombo(cbOlympSubject, HelpClass.GetComboListByTable("ed.OlympSubject"), false, true);
                    ComboServ.FillCombo(cbOlympValue, HelpClass.GetComboListByTable("ed.OlympValue"), false, true);
                    ComboServ.FillCombo(cbOlympLevel, HelpClass.GetComboListByTable("ed.OlympLevel"), false, true);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы ", exc);
            }
        }

        #region Handlers

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

        public int? ExamId
        {
            get { return ComboServ.GetComboIdInt(cbExam); }
            set { ComboServ.SetComboId(cbExam, value); }
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

        private void FillStudyForm()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = MainClass.GetEntry(context).Where(c => c.FacultyId == FacultyId);

                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.StudyFormId.ToString(), u.StudyFormName)).Distinct().ToList();

                ComboServ.FillCombo(cbStudyForm, lst, false, true);
                cbStudyForm.SelectedIndex = 0;
            }
        }

        private void FillLicenseProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = MainClass.GetEntry(context).Where(c => c.FacultyId == FacultyId);

                if (StudyFormId != null)
                    ent = ent.Where(c => c.StudyFormId == StudyFormId);

                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.LicenseProgramId.ToString(), u.LicenseProgramName)).Distinct().ToList();

                ComboServ.FillCombo(cbLicenseProgram, lst, false, true);
                cbLicenseProgram.SelectedIndex = 0;
            }
        }

        private void FillObrazProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = MainClass.GetEntry(context).Where(c => c.FacultyId == FacultyId);

                if (StudyFormId != null)
                    ent = ent.Where(c => c.StudyFormId == StudyFormId);
                if (LicenseProgramId != null)
                    ent = ent.Where(c => c.LicenseProgramId == LicenseProgramId);

                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.ObrazProgramId.ToString(), u.ObrazProgramName + ' ' + u.ObrazProgramCrypt)).Distinct().ToList();

                ComboServ.FillCombo(cbObrazProgram, lst, false, true);
            }
        }
        
        private void FillExams()
        {            
            int? curExamId = ExamId;

            using (PriemEntities context = new PriemEntities())
            {
                var ent = Exams.GetExamsWithFilters(context, MainClass.lstStudyLevelGroupId, FacultyId, LicenseProgramId, ObrazProgramId, null, StudyFormId, StudyBasisId, null, null, null);
                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.ExamId.ToString(), u.ExamName)).Distinct().ToList();
                ComboServ.FillCombo(cbExam, lst, false, false);
            }

            foreach (KeyValuePair<string, string> ex in cbExam.Items)
            {
                if (ex.Key == curExamId.ToString())
                    ComboServ.SetComboId(cbExam, curExamId);
            }                      
        }

        //инициализация обработчиков мегакомбов
        public override void InitHandlers()
        {
            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbObrazProgram.SelectedIndexChanged += new EventHandler(cbObrazProgram_SelectedIndexChanged);          
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
           
            cbExam.SelectedIndexChanged += new EventHandler(cbExam_SelectedIndexChanged);

            cbOlympType.SelectedIndexChanged += new EventHandler(cbOlympType_SelectedIndexChanged);
            cbOlympName.SelectedIndexChanged += new EventHandler(cbOlympName_SelectedIndexChanged);
            cbOlympSubject.SelectedIndexChanged += new EventHandler(cbOlympSubject_SelectedIndexChanged);
            cbOlympValue.SelectedIndexChanged += new EventHandler(cbOlympValue_SelectedIndexChanged);
            cbOlympLevel.SelectedIndexChanged += new EventHandler(cbOlympLevel_SelectedIndexChanged);
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }

        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
        }

        void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {            
            FillExams();
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExams();
        }

        void cbExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
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

        #endregion

        //обновление грида
        public override void UpdateDataGrid()
        {
            if (ExamId == null)
            {
                dgvAbitList.DataSource = null;
                lblCount.Text = string.Empty;
                return;
            }

            DataTable examTable = new DataTable();
            DataSet ds;

            DataColumn clm;
            clm = new DataColumn();
            clm.ColumnName = "Id";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "OlympiadId";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Рег_номер";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "ФИО";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Тип_конкурса";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);           

            clm = new DataColumn();
            clm.ColumnName = "Вид";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Название";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);
            
            clm = new DataColumn();
            clm.ColumnName = "Предмет";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Уровень";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Степень";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Перезачесть";
            clm.Caption = "Set";
            clm.ReadOnly = false;
            clm.DataType = typeof(bool);
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "ExamInEntryId";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            ds = _bdc.GetDataSet(GetQuery());

            lblCount.Text = ds.Tables[0].Rows.Count.ToString();

            foreach (DataRow dsRow in ds.Tables[0].Rows)
            {
                DataRow newRow;
                newRow = examTable.NewRow();
                newRow["Рег_номер"] = dsRow["Рег_Номер"].ToString();
                newRow["ФИО"] = dsRow["ФИО"].ToString();
                newRow["Тип_конкурса"] = dsRow["Тип_конкурса"].ToString();
                newRow["Вид"] = dsRow["Вид"].ToString();
                newRow["Название"] = dsRow["Вид"].ToString();
                newRow["Предмет"] = dsRow["Предмет"].ToString();
                newRow["Уровень"] = dsRow["Уровень"].ToString();                
                newRow["Степень"] = dsRow["Степень"].ToString();
                newRow["Id"] = dsRow["Id"].ToString();
                newRow["OlympiadId"] = dsRow["OlympiadId"].ToString();
                newRow["ExamInEntryId"] = dsRow["ExamInEntryId"].ToString();

                examTable.Rows.Add(newRow);
            }

            foreach (DataColumn dc in examTable.Columns)
                if (dc.Caption == "Set")
                    dc.ReadOnly = false;
                else
                    dc.ReadOnly = true;

            DataView dv = new DataView(examTable);
            dv.AllowNew = false;

            string sortedColumn = string.Empty;
            ListSortDirection order = ListSortDirection.Ascending;
            bool sorted = false;
            int index = 0;

            if (dgvAbitList.SortOrder != SortOrder.None)
            {
                sorted = true;
                sortedColumn = dgvAbitList.SortedColumn.Name;
                order = dgvAbitList.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                index = dgvAbitList.CurrentRow == null ? -1 : dgvAbitList.CurrentRow.Index;
            }

            dgvAbitList.DataSource = dv;
            dgvAbitList.Columns["Id"].Visible = false;
            dgvAbitList.Columns["ExamInEntryId"].Visible = false;
            dgvAbitList.Columns["OlympiadId"].Visible = false;
            dgvAbitList.Update();

            if (dgvAbitList.Rows.Count > 0)
            {
                if (sorted && dgvAbitList.Columns.Contains(sortedColumn))
                    dgvAbitList.Sort(dgvAbitList.Columns[sortedColumn], order);
                if (index >= 0 && index <= dgvAbitList.Rows.Count)
                    dgvAbitList.CurrentCell = dgvAbitList[2, index];
            }
        }

        private string GetFilters()
        {
            string s1 = string.Empty;

            s1 += " AND ed.qAbiturient.StudyLevelGroupId IN (" + Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId) + ")";

            //обработали форму обучения  
            if (StudyFormId != null)
                s1 += " AND ed.qAbiturient.StudyFormId = " + StudyFormId;

            //обработали основу обучения  
            if (StudyBasisId != null)
                s1 += " AND ed.qAbiturient.StudyBasisId = " + StudyBasisId;

            //обработали факультет
            if (FacultyId != null)
                s1 += " AND ed.qAbiturient.FacultyId = " + FacultyId;

            //обработали Направление
            if (LicenseProgramId != null)
                s1 += " AND ed.qAbiturient.LicenseProgramId = " + LicenseProgramId;

            //обработали Образ программу
            if (ObrazProgramId != null)
                s1 += " AND ed.qAbiturient.ObrazProgramId = " + ObrazProgramId;  

            return s1;
        }

        //сбор фильтров 
        private string GetQuery()
        {
            string sQuery = @"SELECT extOlympiads.Id AS OlympiadId, ed.qAbiturient.Id as Id, ed.qAbiturient.RegNum as Рег_Номер, ed.extPersonAspirant.FIO as ФИО, 
                        ed.Competition.Name as Тип_конкурса, OlympTypeName as Вид, OlympName AS Название, OlympLevelName AS Уровень, OlympSubjectName as Предмет,
                        OlympValueName as Степень, ed.extExamInEntry.Id AS ExamInEntryId 
                        FROM ed.qAbiturient LEFT JOIN ed.extPersonAspirant ON ed.qAbiturient.PersonId = ed.extPersonAspirant.Id INNER JOIN ed.extOlympiads ON ed.extOlympiads.AbiturientId = ed.qAbiturient.Id 
                        LEFT JOIN ed.Competition ON ed.Competition.Id = ed.qAbiturient.CompetitionId 
                        INNER JOIN ed.extExamInEntry ON qAbiturient.EntryId = ed.extExamInEntry.EntryId ";

            sQuery += " WHERE ed.qAbiturient.BackDoc=0 " + GetFilters();
            sQuery += string.Format(" AND ed.extExamInEntry.ExamId = {0}", ExamId);
            //sQuery += string.Format(" AND NOT Exists (Select qMark.id FROM qMark INNER JOIN qAbiturient as abit ON abit.Id=qMark.AbiturientId INNER JOIN extExamInProgram ON qMark.ExamInProgramId = extExamInProgram.Id WHERE abit.id=qAbiturient.Id AND extExamInProgram.ProgramId = qAbiturient.ProgramId AND extExamInProgram.ExamNameId={0} ANd qMark.IsFromOlymp = 1) ", cbExams.Id);
            sQuery += " AND NOT Exists (SELECT ed.qMark.Id FROM ed.qMark INNER JOIN ed.qAbiturient as abit ON abit.Id=ed.qMark.AbiturientId WHERE abit.id=ed.qAbiturient.Id AND ed.extExamInEntry.Id = ed.qMark.ExamInEntryBlockUnitId AND ed.qMark.IsFromOlymp = 1)";
            string sOl = string.Empty;

            //обработали вид            
            if (OlympTypeId != null)
                sOl += " AND ed.extOlympiads.OlympTypeId = " + OlympTypeId;

            //обработали уровень            
            if (OlympLevelId != null)
                sOl += " AND ed.extOlympiads.OlympLevelId = " + OlympLevelId;

            //обработали предмет            
            if (OlympSubjectId != null)
                sOl += " AND ed.extOlympiads.OlympSubjectId = " + OlympSubjectId;

            //обработали значение            
            if (OlympValueId != null)
                sOl += " AND ed.extOlympiads.OlympValueId  = " + OlympValueId;

            //обработали название            
            if (OlympNameId != null)
                sOl += " AND ed.extOlympiads.OlympNameId  = " + OlympNameId;

            sQuery += sOl;

            return sQuery;
        }

        //поиск по фио
        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbitList, "ФИО", tbFIO.Text);
        }

        private void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvAbitList.Rows)
            {
                if (chbAll.Checked)
                    dgvr.Cells["Перезачесть"].Value = true;
                else
                    dgvr.Cells["Перезачесть"].Value = false;
            }
        }

        protected override void OpenCard(string id, BaseFormsLib.BaseFormEx formOwner, int? index)
        {
            MainClass.OpenCardAbit(id, this, dgvAbitList.CurrentRow.Index);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            Guid? exId;
            Guid? abId;
            List<Guid?> lstIds = new List<Guid?>();

            foreach (DataGridViewRow row in dgvAbitList.Rows)
            {
                if (row.Cells["Перезачесть"].Value.ToString() == "True")
                {
                    abId = new Guid(row.Cells["Id"].Value.ToString());
                    lstIds.Add(abId);
                }
            }

            if (lstIds.Count == 0)
                return;

            DialogResult res = MessageBox.Show("Подтвердить зачет олимпиад у выбранных заявлений как 100 баллов за экзамен?", "Зачет олимпиад", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            foreach (DataGridViewRow row in dgvAbitList.Rows)
                            {
                                if (row.Cells["Перезачесть"].Value.ToString() == "True")
                                {
                                    exId = Guid.Parse(row.Cells["ExamInEntryId"].Value.ToString());
                                    abId = new Guid(row.Cells["Id"].Value.ToString());

                                    int cnt;

                                    cnt = (from mrk in context.qMark
                                           where mrk.AbiturientId == abId && mrk.ExamInEntryBlockUnitId == exId && mrk.IsFromEge
                                           select mrk).Count();

                                    if (cnt > 0)
                                    {
                                        if (MessageBox.Show("Данное действие перекроет оценку, зачтенную из ЕГЭ. Заменить?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                                            continue;
                                    }

                                    cnt = (from mrk in context.qMark
                                           where mrk.AbiturientId == abId && mrk.ExamInEntryBlockUnitId == exId
                                           select mrk).Count();
                                    
                                    if (cnt > 0)                                   
                                        context.Mark_DeleteByAbitExamId(abId, exId);

                                    Guid OlympiadId = new Guid(row.Cells["OlympiadId"].Value.ToString());

                                    context.Mark_Insert(abId, exId, 100, DateTime.Now, false, true, false, null, OlympiadId, null);
                                }
                            }

                            transaction.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка при сохранении перезачета оценок. Оценки перезачтены не будут. ", ex);
                }

                UpdateDataGrid();
            }
        }

        //печать
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string s1 = string.Empty;

            //обработали факультет
            if (FacultyId != null)
                s1 += " AND ed.extAbitAspirant.FacultyId = " + FacultyId;

            //обработали Направление
            if (LicenseProgramId != null)
                s1 += " AND ed.extAbitAspirant.LicenseProgramId = " + LicenseProgramId;

            //обработали Образ программу
            if (ObrazProgramId != null)
                s1 += " AND ed.extAbitAspirant.ObrazProgramId = " + ObrazProgramId;

            string query = string.Format(@"SELECT ed.extPersonAspirant.PersonNum AS PerNum, ed.extAbitAspirant.RegNum,  LicenseProgramName, ObrazProgramName, 
                    StudyFormName, ed.extAbitAspirant.FIO as fio, ed.qMark.Value as markval 
                    FROM ed.extAbitAspirant INNER JOIN ed.qMark ON ed.qMark.AbiturientId = ed.extAbitAspirant.Id 
                    INNER JOIN ed.extExamInEntry ON ed.qMark.ExamInEntryBlockUnitId = ed.extExamInEntry.Id 
                    WHERE ed.extExamInEntry.ExamId={0} AND ed.qMark.IsFromOlymp>0 {1} ORDER BY 1", ExamId, s1);
            try
            {
                DataSet ds = _bdc.GetDataSet(query);
                if (ds.Tables[0].Rows.Count == 0)
                    return;

                WordDoc wd = new WordDoc(string.Format(@"{0}\Olymp2Mark.dot", MainClass.dirTemplates), true);
                TableDoc td = wd.Tables[0];

                wd.Fields["Предмет"].Text = cbExam.Text;
                wd.Fields["Факультет"].Text = cbFaculty.Text;
                wd.Fields["Специальность"].Text = cbLicenseProgram.Text;
                wd.Fields["ОбразовательнаяПрограмма"].Text = cbObrazProgram.Text;

                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    td.AddRow(1);
                    td[0, i] = i.ToString();
                    td[1, i] = dr["PerNum"].ToString();
                    td[2, i] = dr["RegNum"].ToString();
                    td[3, i] = dr["LicenseProgramName"].ToString();
                    td[4, i] = dr["ObrazProgramName"].ToString();
                    td[5, i] = dr["StudyFormName"].ToString();
                    td[6, i] = dr["fio"].ToString();
                    td[7, i] = "100";
                    i++;
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при выводе в Word: ", ex);
            }
        }
    }
}