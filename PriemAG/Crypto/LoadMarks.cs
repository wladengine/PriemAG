using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using System.Transactions;

using WordOut;
using EducServLib;
using BDClassLib;
using PriemLib;
using BaseFormsLib;

namespace Priem
{
    public partial class LoadMarks : BaseForm
    {
        private DBPriem bdc;
        private Guid? _vedId;        
        private string _examId;
        private DateTime _dateExam;
        private string _facultyId;
        private string _studybasisId;       
        private bool _isLoad;
        private bool _isAdditional;

        private string _vedNum;
        
        private List<int?> lstNumbers;
        public SortedList<string, string> slReplaceMark;
        public SortedList<string, string> slNewMark;

        public LoadMarks(Guid? vedId)
        {
            InitializeComponent();
            this._vedId = vedId;            
            _isLoad = false;
         
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

            slReplaceMark = new SortedList<string, string>();
            slNewMark = new SortedList<string, string>();
                        
            dgvMarks.ReadOnly = true;

            lblAdd.Text = string.Empty;

            using (PriemEntities context = new PriemEntities())
            {
                extExamsVed exVed = (from ev in context.extExamsVed
                                     where ev.Id == _vedId
                                     select ev).FirstOrDefault();

                _isAdditional = exVed.IsAddVed;
                _examId = exVed.ExamId.ToString();
                _dateExam = exVed.Date;
                _facultyId = exVed.FacultyId.ToString();
                _studybasisId = exVed.StudyBasisId.ToString();
                _vedNum = exVed.Number.ToString();

                lblFaculty.Text += exVed.FacultyName;

                if (exVed.StudyBasisId == null)
                    lblStudyBasis.Text += "все";
                else
                    lblStudyBasis.Text += exVed.StudyBasisName;

                lblExam.Text += exVed.ExamName;
                lblDate.Text += exVed.Date.ToShortDateString();
                if (exVed.IsAddVed)
                    lblAdd.Text += "дополнительная (" + exVed.AddCount.ToString() + ")";              

                btnPrintVed.Enabled = false;

                if (exVed.IsLoad)
                {
                    _isLoad = true;
                    dgvMarks.ReadOnly = true;
                    lblIsLoad.Text = "Загружена";
                    btnPrintVed.Enabled = true;
                }

                if ((MainClass.IsCryptoMain() || MainClass.IsPasha()) && !exVed.IsLoad)
                    btnLoad.Visible = true;
                else
                    btnLoad.Visible = false;

                if (MainClass.IsPasha())
                    btnPrintVed.Enabled = true;

                btnDoubleLanguage.Visible = false;//MainClass.IsPasha();
            }
        }

        private void FillGridMarks()
        {
            dgvMarks.Columns.Clear();
            lstNumbers = new List<int?>();

            DataTable examTable = new DataTable();

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
            clm.ColumnName = "Баллы";
            examTable.Columns.Add(clm);

            using (PriemEntities context = new PriemEntities())
            {
                var persMark = from evh in context.ExamsVedHistory
                               join pers in context.extPerson
                               on evh.PersonId equals pers.Id
                               where evh.ExamsVedId == _vedId
                               select new
                               {
                                   evh.ExamsVedId,
                                   pers.FIO,
                                   evh.PersonId,
                                   evh.PersonVedNumber,
                                   evh.Mark,
                                   evh.OralMark
                               };

                foreach (var pm in persMark)
                {
                    DataRow newRow;
                    newRow = examTable.NewRow();
                    newRow["PersonId"] = pm.PersonId;
                    newRow["ФИО"] = pm.FIO;
                    newRow["Номер"] = pm.PersonVedNumber;
                    lstNumbers.Add(pm.PersonVedNumber);

                    decimal? sumMark = 0;
                    if(pm.Mark == null && pm.OralMark == null)
                        sumMark = null;
                    else                    
                        sumMark = (pm.Mark ?? 0m) + (pm.OralMark ?? 0m); 
                  
                    newRow["Баллы"] = sumMark;
                    examTable.Rows.Add(newRow);
                }

                DataView dv = new DataView(examTable);
                dv.AllowNew = false;

                dgvMarks.DataSource = dv;
                dgvMarks.Columns["PersonId"].Visible = false;
                dgvMarks.Columns["ФИО"].Visible = false;

                //if (!_forLoad && (bdc.IsCryptoMain() || bdc.IsSuperman()))
                //    dgvMarks.Columns["ФИО"].Visible = true;

                dgvMarks.Columns["Номер"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Update();

                lblCount.Text = dgvMarks.RowCount.ToString();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\Templates\EmptyTemplate.dot", Application.StartupPath));

                int colCount = 0;
                foreach (DataGridViewColumn clm in dgvMarks.Columns)
                {
                    if (clm.Visible)
                        colCount++;
                }

                wd.AddNewTable(2, colCount);
                TableDoc td = wd.Tables[0];

                // печать из грида
                int i = 0;
                foreach (DataGridViewColumn clm in dgvMarks.Columns)
                {
                    if (clm.Visible)
                    {
                        td[i, 0] = clm.HeaderText;
                        i++;
                    }
                }

                i = 1;
                int j;
                foreach (DataGridViewRow dgvr in dgvMarks.Rows)
                {
                    j = 0;
                    foreach (DataGridViewColumn clm in dgvMarks.Columns)
                    {
                        if (clm.Visible)
                        {
                            td[j, i] = dgvr.Cells[clm.Index].Value.ToString();
                            j++;
                        }
                    }
                    i++;
                    td.AddRow(1);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n" + exc);
            }
        } 

        private void btnPrintVed_Click(object sender, EventArgs e)
        {
            if (!_isLoad)
                return;

            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\Templates\CryptoEnterMarks.dot", Application.StartupPath));
                TableDoc td = wd.Tables[0];

                //переменные

                string examId;
                string examName;
                DateTime examDate;
                string facultyId;

                using (PriemEntities context = new PriemEntities())
                {
                    extExamsVed exVed = (from ev in context.extExamsVed
                                         where ev.Id == _vedId
                                         select ev).FirstOrDefault();
                   
                    examId = _examId;
                    examName = exVed.ExamName;
                    examDate = _dateExam;
                    facultyId = _facultyId;

                    wd.Fields["Faculty"].Text = exVed.FacultyName;
                    wd.Fields["Exam"].Text = examName;
                    wd.Fields["StudyBasis"].Text = exVed.StudyBasisId == null ? "все" : exVed.StudyBasisName;
                    wd.Fields["Date"].Text = examDate.ToShortDateString();
                    wd.Fields["VedNum"].Text = exVed.Number.ToString();

                    int i = 1;

                    var persMark = from evh in context.ExamsVedHistory
                               join pers in context.extPerson
                               on evh.PersonId equals pers.Id
                               where evh.ExamsVedId == _vedId
                               select new
                               {
                                   evh.ExamsVedId,
                                   pers.Surname,
                                   pers.Name,
                                   pers.SecondName,
                                   pers.BirthDate,
                                   pers.PersonNum,
                                   evh.PersonId,
                                   evh.PersonVedNumber,
                                   evh.Mark,
                                   evh.OralMark
                               };                

                    foreach (var pm in persMark)
                    {
                        td[0, i] = i.ToString();
                        td[1, i] = pm.Surname;
                        td[2, i] = pm.Name;
                        td[3, i] = pm.SecondName;
                        td[4, i] = pm.BirthDate.ToShortDateString();
                        td[5, i] = pm.PersonNum;
                        td[6, i] = facultyId;
                        td[7, i] = examName;
                        td[8, i] = examId;
                        td[9, i] = examDate.ToShortDateString();

                        decimal? sumMark = 0;
                        if (pm.Mark == null && pm.OralMark == null)
                            sumMark = null;
                        else
                            sumMark = (pm.Mark ?? 0m) + (pm.OralMark ?? 0m);
                                          
                        td[10, i] =  sumMark == null ? "" : sumMark.ToString();

                        td.AddRow(1);
                        i++;  
                    }
                    td.DeleteLastRow();
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }           
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    int marksCount = 0;

                    SortedList<string, string> slAbitsWithMark = new SortedList<string, string>();

                    string flt = "";
                    flt += " AND ed.qAbiturient.BackDoc = 0 ";
                    flt += " AND ed.qAbiturient.NotEnabled = 0 ";
                    flt += " AND ed.qAbiturient.Id IN (SELECT AbiturientId FROM ed.extProtocol WHERE ProtocolTypeId = 1 AND IsOld = 0 AND Excluded = 0) ";
                    flt += " AND ed.qAbiturient.StudyLevelGroupId IN (" + Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId) + ")";
                    flt += string.Format(" AND ed.qAbiturient.EntryId IN (SELECT EntryId FROM ed.extExamInEntry WHERE ExamId = {0})", _examId);

                    string flt_fac = "";
                    if (_isAdditional)
                        flt_fac = " AND FacultyId = " + _facultyId;

                    foreach (DataGridViewRow dgvr in dgvMarks.Rows)
                    {
                        string val = dgvr.Cells["Баллы"].Value.ToString();
                        if (dgvr.Cells["Баллы"].Value == null || val.CompareTo("") == 0)
                            continue;

                        string perId = dgvr.Cells["PersonId"].Value.ToString();

                        if (_studybasisId == "2")
                        {
                            DataSet dsAbit = bdc.GetDataSet(string.Format(@"SELECT ed.qAbiturient.Id, ed.qMark.Value FROM ed.qAbiturient 
                                LEFT JOIN (ed.qMark INNER JOIN ed.extExamInEntry ON ed.qMark.ExamInEntryId = ed.extExamInEntry.Id) 
                                ON ed.qMark.AbiturientId = ed.qAbiturient.Id AND ed.extExamInEntry.ExamId = {1} 
                                WHERE ed.qAbiturient.PersonId = '{0}' AND ed.qAbiturient.StudyBasisId = 2 {2} {3}", perId, _examId, flt, flt_fac));

                            foreach (DataRow dra in dsAbit.Tables[0].Rows)
                            {
                                if (dra["Value"] == null || dra["Value"].ToString() == "")
                                    slNewMark.Add(dra["Id"].ToString(), val);
                                else
                                    slAbitsWithMark.Add(dra["Id"].ToString(), val);
                            }
                        }
                        else
                        {
                            DataSet ds = bdc.GetDataSet(string.Format("SELECT ed.qAbiturient.Id FROM ed.qAbiturient WHERE PersonId = '{0}' {1} {2} {3}", perId, flt_fac, _studybasisId == "" ? "" : " AND qAbiturient.StudyBasisId = " + _studybasisId, flt));
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (int.Parse(bdc.GetStringValue(string.Format("SELECT Count(ed.Mark.Id) FROM ed.Mark INNER JOIN ed.extExamInEntry ON ed.Mark.ExamInEntryId = ed.extExamInEntry.Id WHERE ed.extExamInEntry.ExamId = '{0}' AND AbiturientId = '{1}'", _examId, row["Id"].ToString()))) > 0)
                                    continue;

                                slNewMark.Add(row["Id"].ToString(), val);
                            }
                        }
                    }

                    if (slAbitsWithMark.Count > 0)
                    {
                        SetNewMark frm = new SetNewMark(this, _examId, _dateExam, slAbitsWithMark);
                        frm.ShowDialog();
                    }

                    //using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    //{
                        foreach (string abId in slNewMark.Keys)
                        {
                            DataSet dss = bdc.GetDataSet(string.Format("SELECT Id, EntryId FROM ed.qAbiturient WHERE Id = '{0}' ", abId));
                            DataRow drr = dss.Tables[0].Rows[0];

                            string examInPr = Exams.GetExamInEntryId(_examId, drr["EntryId"].ToString());

                            Guid abitId = new Guid(abId);
                            int examInEntryId = int.Parse(examInPr);
                            decimal val = decimal.Parse(slNewMark[abId]);

                            int cnt = (from mrk in context.qMark
                                       where mrk.ExamInEntryId == examInEntryId && mrk.AbiturientId == abitId
                                       select mrk).Count();

                            if (cnt > 0)
                                continue;

                            List<string> list = Exams.GetExamIdsInEntry(drr["EntryId"].ToString());

                            if (list.Contains(_examId))
                            {
                                context.Mark_Insert(abitId, examInEntryId, val, _dateExam, false, false, false, _vedId, null, null);
                                marksCount++;
                            }
                        }

                        foreach (string abId in slReplaceMark.Keys)
                        {
                            DataSet dss = bdc.GetDataSet(string.Format("SELECT Id, EntryId FROM ed.qAbiturient WHERE Id = '{0}' ", abId));
                            DataRow drr = dss.Tables[0].Rows[0];

                            string examInPr = Exams.GetExamInEntryId(_examId, drr["EntryId"].ToString());
                            List<string> list = Exams.GetExamIdsInEntry(drr["EntryId"].ToString());
                            if (list.Contains(_examId))
                            {
                                Guid abitId = new Guid(abId);
                                int examInEntryId = int.Parse(examInPr);
                                decimal val = decimal.Parse(slReplaceMark[abId]);

                                context.Mark_DeleteByAbitExamId(abitId, examInEntryId);
                                context.Mark_Insert(abitId, examInEntryId, val, _dateExam, false, false, false, _vedId, null, null);

                                marksCount++;
                            }
                        }

                        context.ExamsVed_UpdateLoad(true, _vedId);

                       // transaction.Complete();

                        lblIsLoad.Text = "Загружена";
                        btnLoad.Enabled = false;
                   // }

                    MessageBox.Show(marksCount + " записей добавлено.", "Выполнено");
                }
            }
            catch(Exception exc)
            {
                WinFormsServ.Error(exc);
            }
        }

        private void btnDoubleLanguage_Click(object sender, EventArgs e)
        {
            //if (_facultyId != "18" && _facultyId != "7")
            //    return;

            //ArrayList alQueries = new ArrayList();
            //int marksCount = 0;

            //SortedList<string, string> slAbitsWithMark = new SortedList<string, string>();

            //string flt = "";
            //flt += " AND qAbiturient.BackDoc = 0 ";
            //flt += " AND qAbiturient.NotEnabled = 0 ";
            //flt += " AND qAbiturient.Id IN (SELECT extProtocol.AbiturientId FROM extProtocol WHERE extProtocol.ProtocolTypeId = 1 AND extProtocol.IsOld = 0 AND extProtocol.Excluded = 0) ";
            //flt += string.Format(" AND qAbiturient.ProgramId IN (SELECT ExamInProgram.ProgramId FROM ExamInProgram WHERE ExamInProgram.ExamNameId = {0})", _examNameId);

            //string flt_fac = "";
            //flt_fac = " AND FacultyId = " + _facultyId;

            //foreach (DataGridViewRow dgvr in dgvMarks.Rows)
            //{
            //    string val = dgvr.Cells["Баллы"].Value.ToString();
            //    if (dgvr.Cells["Баллы"].Value == null || val.CompareTo("") == 0)
            //        continue;

            //    string perId = dgvr.Cells["PersonId"].Value.ToString();

            //    DataSet dsAbit = bdc.GetDataSet(string.Format("SELECT qAbiturient.Id, qMark.Value FROM qAbiturient LEFT JOIN (qMark INNER JOIN ExamInProgram ON qMark.ExamInProgramId = ExamInProgram.Id) ON qMark.AbiturientId = qAbiturient.Id AND ExamInprogram.ExamNameId = '{1}' WHERE qAbiturient.PersonId = '{0}' {2} {3}", perId, _examNameId, flt, flt_fac));
            //    foreach (DataRow dra in dsAbit.Tables[0].Rows)
            //    {
            //        if (dra["Value"] == null || dra["Value"].ToString() == "")
            //            slNewMark.Add("'" + dra["Id"].ToString() + "'", val);
            //        else
            //            slAbitsWithMark.Add("'" + dra["Id"].ToString() + "'", val);
            //    }
            //}

            //if (slAbitsWithMark.Count > 0)
            //{
            //    SetNewMark frm = new SetNewMark(this, _examNameId, _dateExam, slAbitsWithMark);
            //    frm.ShowDialog();
            //}

            ////foreach (string abId in slNewMark.Keys)
            ////{
            ////    DataSet dss = bdc.GetDataSet(string.Format("SELECT Id, FacultyId, ProfessionId, ObrazProgramId, SpecializationId FROM qAbiturient WHERE Id = {0} ", abId));
            ////    DataRow drr = dss.Tables[0].Rows[0];

            ////    string examInPr = Exams.GetExamInStudyPlanId(_examNameId, drr["FacultyId"].ToString(), drr["ProfessionId"].ToString(), drr["ObrazProgramId"].ToString(), drr["SpecializationId"].ToString(), drr["StudyFormId"].ToString(), drr["StudyBasisId"].ToString());

            ////    if (int.Parse(bdc.GetStringValue(string.Format("SELECT Count(qMark.Id) FROM qMark WHERE ExamInProgramId = '{0}' AND AbiturientId = '{1}'", examInPr, drr["Id"].ToString()))) > 0)
            ////        continue;

            ////    List<string> list = Exams.GetExamNameIdsInStudyPlan(drr["FacultyId"].ToString(), drr["ProfessionId"].ToString(), drr["ObrazProgramId"].ToString(), drr["SpecializationId"].ToString(), drr["StudyFormId"].ToString(), drr["StudyBasisId"].ToString());

            ////    if (list.Contains(_examNameId))
            ////    {
            ////        alQueries.Add(string.Format("INSERT INTO qMark (Id, AbiturientId, ExamInProgramId, Value, PassDate, ExamVedId) VALUES (NewId(), '{0}',{1}, {2}, '{3}', '{4}')", drr["Id"], examInPr, slNewMark[abId], _dateExam, _vedId));
            ////        marksCount++;
            ////    }
            ////}

            //foreach (string abId in slReplaceMark.Keys)
            //{
            //    DataSet dss = bdc.GetDataSet(string.Format("SELECT Id, FacultyId, ProfessionId, ObrazProgramId, SpecializationId, StudyFormId, StudyBasisId FROM qAbiturient WHERE Id = {0} ", abId));
            //    DataRow drr = dss.Tables[0].Rows[0];

            //    string examInPr = Exams.GetExamInStudyPlanId(_examNameId, drr["FacultyId"].ToString(), drr["ProfessionId"].ToString(), drr["ObrazProgramId"].ToString(), drr["SpecializationId"].ToString(), drr["StudyFormId"].ToString(), drr["StudyBasisId"].ToString());
            //    List<string> list = Exams.GetExamNameIdsInStudyPlan(drr["FacultyId"].ToString(), drr["ProfessionId"].ToString(), drr["ObrazProgramId"].ToString(), drr["SpecializationId"].ToString(), drr["StudyFormId"].ToString(), drr["StudyBasisId"].ToString());
            //    if (list.Contains(_examNameId))
            //    {
            //        alQueries.Add(string.Format("DELETE FROM qMark WHERE ExamInProgramId = {0} AND AbiturientId = '{1}'", examInPr, drr["Id"]));
            //        alQueries.Add(string.Format("INSERT INTO qMark (Id, AbiturientId, ExamInProgramId, Value, PassDate, ExamVedId) VALUES (NewId(), '{0}',{1}, {2}, '{3}', '{4}')", drr["Id"], examInPr, slReplaceMark[abId], _dateExam, _vedId));

            //        marksCount++;
            //    }
            //}

            ////alQueries.Add(string.Format("Update ExamsVed Set IsLoad=1 WHERE Id ='{0}'", _vedId));

            //if (alQueries.Count > 0)
            //    bdc.ExecuteWithTrasaction(alQueries);

            ////lblIsLoad.Text = "Загружена";
            ////btnLoad.Enabled = false;

            //MessageBox.Show(marksCount + " записей добавлено.", "Выполнено");
        }                    
    }
}