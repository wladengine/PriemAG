using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions; 

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class AppealMarks : BaseForm
    {
        private DBPriem bdc;
        private Guid? _vedId;
        private string _examId;
        private DateTime _dateExam;
        private string _facultyId;
        private string _studybasisId;

        public AppealMarks(Guid? vedId)
        {
            InitializeComponent();
            this._vedId = vedId;            
           
            InitControls();
            FillGridMarks();
        }        

        //дополнительная инициализация контролов
        private void InitControls()
        {
            InitFocusHandlers();

            this.CenterToParent();
            //this.MdiParent = MainClass.mainform;
            bdc = MainClass.Bdc;

            lblAdd.Text = string.Empty;

            using (PriemEntities context = new PriemEntities())
            {
                extExamsVed exVed = (from ev in context.extExamsVed
                                     where ev.Id == _vedId
                                     select ev).FirstOrDefault();
                                
                _examId = exVed.ExamId.ToString();
                _dateExam = exVed.Date;
                _facultyId = exVed.FacultyId.ToString();
                _studybasisId = exVed.StudyBasisId.ToString();
                
                lblFaculty.Text += exVed.FacultyName;

                if (exVed.StudyBasisId == null)
                    lblStudyBasis.Text += "все";
                else
                    lblStudyBasis.Text += exVed.StudyBasisName;

                lblExam.Text += exVed.ExamName;
                lblDate.Text += exVed.Date.ToShortDateString();
                if (exVed.IsAddVed)
                    lblAdd.Text += "дополнительная (" + exVed.AddCount.ToString() + ")";

                if (MainClass.IsCryptoMain() || MainClass.IsPasha())
                    btnLoad.Visible = true;
                else
                    btnLoad.Visible = false;                
            }            
        }

        private void FillGridMarks()
        {
            dgvMarks.Columns.Clear();
           
            DataTable examTable = new DataTable();
            //DataSet ds;
            //string sQuery;

            DataColumn clm;
            clm = new DataColumn();
            clm.ColumnName = "PersonId";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "ФИО";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);
            
            clm = new DataColumn();
            clm.ColumnName = "Номер";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);           

            clm = new DataColumn();
            clm.ColumnName = "Баллы (письм)";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы(письм) по аппеляции";
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы (устные)";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы (устные) по аппеляции";
            examTable.Columns.Add(clm);

            using (PriemEntities context = new PriemEntities())
            {
                var persMark = from evh in context.ExamsVedHistory
                               join pers in context.extPerson
                               on evh.PersonId equals pers.Id
                               where evh.ExamsVedId == _vedId
                               orderby pers.FIO
                               select new
                               {
                                   evh.ExamsVedId,
                                   pers.FIO,
                                   evh.PersonId,
                                   evh.PersonVedNumber,
                                   evh.Mark,
                                   evh.AppealMark,
                                   evh.OralMark,
                                   evh.OralAppealMark
                               };

                foreach (var pm in persMark)
                {
                    DataRow newRow;
                    newRow = examTable.NewRow();
                    newRow["PersonId"] = pm.PersonId;
                    newRow["ФИО"] = pm.FIO;
                    newRow["Номер"] = pm.PersonVedNumber;
                    newRow["Баллы (письм)"] = pm.Mark;
                    newRow["Баллы(письм) по аппеляции"] = pm.AppealMark;
                    newRow["Баллы (устные)"] = pm.OralMark;
                    newRow["Баллы (устные) по аппеляции"] = pm.OralAppealMark;
                    
                    examTable.Rows.Add(newRow);
                }

                DataView dv = new DataView(examTable);
                dv.AllowNew = false;

                dgvMarks.DataSource = dv;
                dgvMarks.Columns["PersonId"].Visible = false;
                dgvMarks.Columns["ФИО"].Visible = true;              
                
                dgvMarks.Columns["ФИО"].SortMode = DataGridViewColumnSortMode.NotSortable;                
                dgvMarks.Columns["Номер"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы (письм)"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы(письм) по аппеляции"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы (устные)"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы (устные) по аппеляции"].SortMode = DataGridViewColumnSortMode.NotSortable;
                
                dgvMarks.Update();

                lblCount.Text = dgvMarks.RowCount.ToString();
            }
        }  
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();    
        }                
        
        private void EnterMarks_FormClosing(object sender, FormClosingEventArgs e)
        {            
            DialogResult res = MessageBox.Show("Закрыть без сохранения?", "Внимание", MessageBoxButtons.YesNo);                
            if (res == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }             
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (MainClass.IsCryptoMain() || MainClass.IsPasha())
            {
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            int marksCount = 0;

                            foreach (DataGridViewRow dgvr in dgvMarks.Rows)
                            {
                                string balWr = dgvr.Cells["Баллы (письм)"].Value == null ? string.Empty : dgvr.Cells["Баллы (письм)"].Value.ToString();
                                string balOr = dgvr.Cells["Баллы (устные)"].Value == null ? string.Empty : dgvr.Cells["Баллы (устные)"].Value.ToString();
                                string valWr = dgvr.Cells["Баллы(письм) по аппеляции"].Value == null ? string.Empty : dgvr.Cells["Баллы(письм) по аппеляции"].Value.ToString();
                                string valOr = dgvr.Cells["Баллы (устные) по аппеляции"].Value == null ? string.Empty : dgvr.Cells["Баллы (устные) по аппеляции"].Value.ToString();

                                if (string.IsNullOrEmpty(balWr) && string.IsNullOrEmpty(balOr))
                                    continue;

                                if (string.IsNullOrEmpty(valWr) && string.IsNullOrEmpty(valOr))
                                    continue;

                                Guid persId = new Guid(dgvr.Cells["PersonId"].Value.ToString());
                                int persNum = int.Parse(dgvr.Cells["Номер"].Value.ToString());

                                decimal mrkWrTmp;
                                decimal mrkOrTmp;

                                decimal? mrkWr;
                                decimal? mrkOr;

                                if (string.IsNullOrEmpty(valWr))
                                    mrkWr = null;
                                else if (!(decimal.TryParse(valWr, out mrkWrTmp) && mrkWrTmp >= 0 && mrkWrTmp < 101))
                                {
                                    dgvMarks.CurrentCell = dgvr.Cells["Баллы(письм) по аппеляции"];
                                    WinFormsServ.Error(dgvr.Cells["ФИО"].Value.ToString() + ": неправильно введены данные");
                                    return;
                                }
                                else
                                    mrkWr = mrkWrTmp;

                                if (string.IsNullOrEmpty(valOr))
                                    mrkOr = null;
                                else if (!(decimal.TryParse(valOr, out mrkOrTmp) && mrkOrTmp >= 0 && mrkOrTmp < 101))
                                {
                                    dgvMarks.CurrentCell = dgvr.Cells["Баллы (устные) по аппеляции"];
                                    WinFormsServ.Error(dgvr.Cells["ФИО"].Value.ToString() + ": неправильно введены данные");
                                    return;
                                }
                                else
                                    mrkOr = mrkOrTmp;
                               
                                context.ExamsVedHistory_UpdateMarkAppeal(_vedId, persId, persNum, mrkWr);                                
                                context.ExamsVedHistory_UpdateMarkOralAppeal(_vedId, persId, persNum, mrkOr);

                                DataSet ds = bdc.GetDataSet(string.Format("SELECT Id, EntryId FROM ed.qAbiturient WHERE PersonId = '{0}' {1}", persId.ToString(), _studybasisId == "" ? "" : " AND ed.qAbiturient.StudyBasisId = " + _studybasisId));
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    string examInPr = Exams.GetExamInEntryId(_examId, row["EntryId"].ToString());
                                    Guid abitId = new Guid(row["Id"].ToString());
                                    
                                    if(string.IsNullOrEmpty(examInPr))
                                        continue;
                                    
                                    int examInEntryId = int.Parse(examInPr);

                                    decimal? sumMark = 0;
                                    if (mrkWr == null && mrkOr == null)
                                        sumMark = null;
                                    else
                                        sumMark = (mrkWr ?? 0m) + (mrkOr ?? 0m); 

                                    int cnt = (from mrk in context.qMark
                                               where mrk.ExamInEntryId == examInEntryId && mrk.AbiturientId == abitId
                                               select mrk).Count();

                                    if (cnt > 0)
                                    {
                                        context.Mark_updateByAbVedId(abitId, examInEntryId, sumMark, _dateExam, _vedId);
                                        marksCount++;
                                    }
                                }
                            }
                            transaction.Complete();
                            MessageBox.Show(marksCount + " записей изменено.", "Выполнено");
                        }
                    }
                }

                catch (Exception exc)
                {
                    WinFormsServ.Error("Ошибка сохранения данных: \n", exc);
                    return;
                }
            }
        }        
    }
}