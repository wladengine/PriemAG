using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Transactions;

using BaseFormsLib;
using EducServLib;
using WordOut;
using PriemLib;

namespace Priem
{
    public partial class EnterMarksManual : BaseForm
    {
        private DBPriem bdc;        
        private bool _isModified;
        private int? _examId;
        private int? _facultyId;
        private int? _studybasisId;
        private bool _isAdditional;

        public EnterMarksManual(int? examId, int? facultyId, int? studybasisId)
        {
            InitializeComponent();
            this._examId = examId;            
            _isModified = true;
            _facultyId = facultyId;
            _studybasisId = studybasisId;
           
            InitControls();
            FillGridMarks();
        }        

        //дополнительная инициализация контролов
        private void InitControls()
        {
            InitFocusHandlers();

            this.CenterToParent();
            this.MdiParent = MainClass.mainform;
            bdc = MainClass.Bdc;

            if (!_isModified)
            {
                dgvMarks.ReadOnly = true;
                btnSave.Text = "Изменить";
            }

            using (PriemEntities context = new PriemEntities())
            {
                lblFaculty.Text += (from f in context.SP_Faculty
                                       where f.Id == _facultyId
                                       select f.Name).FirstOrDefault();

                if (_studybasisId == null)
                    lblStudyBasis.Text += "все";
                else
                    lblStudyBasis.Text += (from f in context.StudyBasis
                                           where f.Id == _studybasisId
                                           select f.Name).FirstOrDefault();

                var exam = (from e in context.Exam
                            join en in context.ExamName
                            on e.ExamNameId equals en.Id
                            where e.Id == _examId
                            select new
                            {
                                e.IsAdditional,
                                en.Name
                            }).FirstOrDefault();
                               
                _isAdditional = exam.IsAdditional;
                lblExam.Text += exam.Name;
            }
        }

        private void FillGridMarks()
        {
            dgvMarks.Columns.Clear();

            DataTable examTable = new DataTable();
            DataSet ds;
            string sQuery;

            DataColumn clm;
            clm = new DataColumn();
            clm.ColumnName = "abitId";
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
            clm.ColumnName = "Баллы";
            examTable.Columns.Add(clm);


            sQuery = @"SELECT DISTINCT ed.extAbitAspirant.Id, RegNum as Рег_номер, ed.extPersonAspirant.FIO as ФИО
                     FROM ed.extAbitAspirant LEFT JOIN ed.extPersonAspirant ON ed.extAbitAspirant.PersonId = ed.extPersonAspirant.Id ";

            string flt_where = "";
            string flt_backDoc = "";
            string flt_enable = "";
            string flt_protocol = "";
            string flt_existMark = "";
            string flt_hasExam = "";
            string flt_notInVed = "";
            string flt_notAdd = "";

            flt_backDoc = " AND ed.extAbitAspirant.BackDoc = 0 ";
            flt_enable = " AND ed.extAbitAspirant.NotEnabled = 0 ";
            flt_protocol = " AND ed.extAbitAspirant.Id IN (SELECT AbiturientId FROM ed.extProtocol WHERE ProtocolTypeId = 1 AND IsOld = 0 AND Excluded = 0) ";
            flt_existMark = string.Format(" AND ed.extAbitAspirant.Id NOT IN (SELECT ed.qMark.abiturientId FROM ed.qMark INNER JOIN ed.extExamInEntry ON ed.qMark.ExamInEntryId = ed.extExamInEntry.Id WHERE ed.extExamInEntry.ExamId = {0} AND ed.extExamInEntry.FacultyId = {1}) ", _examId, _facultyId);
            flt_hasExam = string.Format(" AND ed.extAbitAspirant.EntryId IN (SELECT ed.extExamInEntry.EntryId FROM ed.extExamInEntry WHERE ed.extExamInEntry.ExamId = {0})", _examId);

            flt_where = string.Format(" WHERE ed.extAbitAspirant.FacultyId = {0} {1}", _facultyId, (_studybasisId == null) ? "" : " AND StudyBasisId = " + _studybasisId);
                        
            if (_isAdditional)           
                flt_notAdd = Exams.GetFilterForNotAddExam(_examId, _facultyId);

            ds = bdc.GetDataSet(sQuery + flt_where + flt_backDoc + flt_enable + flt_protocol + flt_existMark + flt_hasExam + flt_notInVed + flt_notAdd + " ORDER BY ФИО "); 
            foreach (DataRow dsRow in ds.Tables[0].Rows)
            {
                DataRow newRow;
                newRow = examTable.NewRow();
                newRow["Рег_номер"] = dsRow["Рег_Номер"].ToString();
                newRow["ФИО"] = dsRow["ФИО"].ToString();
                newRow["abitId"] = dsRow["Id"].ToString();
                examTable.Rows.Add(newRow);
            }

            DataView dv = new DataView(examTable);
            dv.AllowNew = false;

            dgvMarks.DataSource = dv;
            dgvMarks.Columns["abitId"].Visible = false;
            dgvMarks.Update();

            lblCount.Text = dgvMarks.RowCount.ToString();
        }  

        private bool SaveMarks()
        {
            if (!MainClass.IsPasha() && !MainClass.IsOwner())
                return false;            
            
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        string mark = null;

                        for (int i = 0; i < dgvMarks.Rows.Count; i++)
                        {
                            Guid abId = new Guid(dgvMarks["abitId", i].Value.ToString());

                            Guid? entryId = (from ab in context.extAbitAspirant
                                             where ab.Id == abId
                                             select ab.EntryId).FirstOrDefault();

                            int examInEntryId = (from ex in context.extExamInEntry
                                                 where ex.ExamId == _examId && ex.EntryId == entryId
                                                 select ex.Id).FirstOrDefault();

                            if (dgvMarks["Баллы", i].Value != null)
                                mark = dgvMarks["Баллы", i].Value.ToString().Trim();

                            if (!string.IsNullOrEmpty(mark))
                            {
                                int mrk;
                                if (int.TryParse(mark, out mrk) && mrk >= 0 && mrk < 101)
                                    context.Mark_Insert(abId, examInEntryId, mrk, DateTime.Now, false, false, true, null, null, null);
                                else
                                {
                                    dgvMarks.CurrentCell = dgvMarks["Баллы", i];
                                    WinFormsServ.Error(dgvMarks["ФИО", i].Value.ToString() + ": неправильно введены данные");
                                    return false;
                                }
                            }                            
                        }

                        transaction.Complete();
                    }
                    
                    return true;
                }
            }

            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка сохранения данных: \n" + exc.Message);
                return false;
            }             
        }
        
        private void btnCancel_Click(object sender, EventArgs e)        
        {            
            this.Close();            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveClick())
                this.Close();
        }

        private bool SaveClick()
        {
            if (btnSave.Enabled && btnSave.Visible)
            {
                if (_isModified)
                {
                    if (SaveMarks())
                    {
                        btnSave.Text = "Изменить";
                        _isModified = false;
                        dgvMarks.ReadOnly = true;
                        return true;
                    }
                    return false;
                }
                else
                {
                    btnSave.Text = "Сохранить";
                    _isModified = true;
                    dgvMarks.ReadOnly = false;
                    dgvMarks.Columns["PersonId"].ReadOnly = true;
                    dgvMarks.Columns["ФИО"].ReadOnly = true;
                    return true;
                }                
            }
            return false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }       
        
        private void EnterMarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isModified)
            {
                DialogResult res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {                    
                    if (!SaveClick())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            } 
        }     
    }
}