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
using System.Data.Entity.Core.Objects;

using EducServLib;
using BDClassLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class ExamsVedRoomCard : BaseFormEx
    {
        private ExamsVedRoomList owner;
        private DBPriem bdc;

        private Guid? vedId;
        private Guid? vedRoomId;
        private bool isAdditional;
        private int? examId;   
        private int? facultyId;
        private int? studyBasisId;
        private DateTime passDate;
        private bool isAddVed;
        private int addCount;

        private string sQuery = @"SELECT DISTINCT ed.extPersonAspirant.Id, ed.extPersonAspirant.PersonNum as Ид_Номер, ed.extPersonAspirant.FIO as ФИО, 
                                ed.extPersonAspirant.EducDocument as Документ_об_Образовании, 
                                ed.extPersonAspirant.PassportData as Паспорт, ed.qAbiturient.FacultyId 
                                FROM ed.qAbiturient INNER JOIN ed.extPersonAspirant ON ed.qAbiturient.PersonId = ed.extPersonAspirant.Id ";

        private string sOrderby = " ORDER BY ФИО ";

        public ExamsVedRoomCard(ExamsVedRoomList owner, Guid? vedId)
        {
            InitializeComponent();
            
            this.vedId = vedId;
            this.owner = owner;
            this.vedRoomId = null;
            bdc = MainClass.Bdc;
            
            InitControls();       
        }

        public ExamsVedRoomCard(ExamsVedRoomList owner, Guid? vedId, Guid? vedRoomId)
        {
            InitializeComponent();

            this.vedId = vedId;
            this.owner = owner;
            this.vedRoomId = vedRoomId;
            bdc = MainClass.Bdc;           

            InitControls();
        }        

        //дополнительная инициализация
        protected virtual void InitControls()
        {
            this.MdiParent = MainClass.mainform;
            this.CenterToParent();
            InitFocusHandlers();

            using (PriemEntities context = new PriemEntities())
            {
                extExamsVed ved = (from vd in context.extExamsVed
                                   where vd.Id == vedId
                                   select vd).FirstOrDefault();

                this.facultyId = ved.FacultyId;
                this.passDate = ved.Date.Date;
                this.isAdditional = ved.IsAdditional;
                this.examId = ved.ExamId;
                this.studyBasisId = ved.StudyBasisId;
                this.isAddVed = false;
                this.addCount = 0;

                Dgv = dgvRight;
                dtPassDate.Enabled = false;

                tbExam.Text = ved.ExamName;  
                dtPassDate.Value = passDate;
                if (isAddVed)
                {
                    dtPassDate.Enabled = false;
                    lblAdd.Visible = true;
                    if (addCount == 0)
                        lblAddCount.Text = "";
                    else
                        lblAddCount.Text = "(" + addCount + ")";
                }
                if (vedRoomId != null)
                    tbRoomNumber.Enabled = false;

                dtPassDate.Enabled = false;
             
                
                if (studyBasisId == null)
                    ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name"), false, true);
                else
                    ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByQuery(string.Format("SELECT CONVERT(varchar(100), Id) AS Id, Name FROM ed.StudyBasis WHERE Id = {0} ORDER BY Name", studyBasisId)), false, false);

                ComboServ.FillCombo(cbStudyForm, HelpClass.GetComboListByQuery(string.Format("SELECT DISTINCT CONVERT(varchar(100), StudyFormId) AS Id, StudyFormName AS Name FROM ed.qEntry WHERE StudyLevelGroupId IN ({0}) AND FacultyId = {1} ORDER BY Name", Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId), facultyId)), false, true);
                                
                FillObrazProgram();

                //заполнение гридов            
                FillGridLeft();
                UpdateRightGrid();

                cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
                cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
                cbObrazProgram.SelectedIndexChanged += new EventHandler(cbObrazProgram_SelectedIndexChanged);
            }           
        }

        public int? cbStudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public int? cbStudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm); }
            set { ComboServ.SetComboId(cbStudyForm, value); }
        }

        public int? cbObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRightGrid();
        }

        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
            UpdateRightGrid();
        }

        void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRightGrid();
        }

        private void FillObrazProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<KeyValuePair<string, string>> lst = ((from ent in MainClass.GetEntry(context)
                                                           where ent.FacultyId == facultyId
                                                           && (cbStudyBasisId != null ? ent.StudyBasisId == cbStudyBasisId : true == true)
                                                           && (cbStudyFormId != null ? ent.StudyBasisId == cbStudyFormId : true == true)
                                                           select new
                                                           {
                                                               Id = ent.ObrazProgramId,
                                                               Name = ent.ObrazProgramName,
                                                               Crypt = ent.ObrazProgramCrypt
                                                           }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Crypt)).ToList();

                ComboServ.FillCombo(cbObrazProgram, lst, false, true);
            }
        } 

        private void UpdateRightGrid()
        {
            string rez = string.Empty;

            //обработали форму обучения  
            if (cbStudyFormId != null)
                rez += " AND ed.qAbiturient.StudyFormId = " + cbStudyFormId;

            //обработали основу обучения 
            if (cbStudyBasisId != null)
                rez += " AND ed.qAbiturient.StudyBasisId = " + cbStudyBasisId; 
            
            //Направление
            if (cbObrazProgramId != null)
                rez += " AND ed.qAbiturient.ObrazProgramId = " + cbObrazProgramId;

            List<string> lstIds = new List<string>();
            foreach(DataGridViewRow dgrw in dgvLeft.Rows)
            {
                lstIds.Add(string.Format("'{0}'", dgrw.Cells["Id"].Value.ToString()));
            }

            if (lstIds.Count > 0)
                rez += string.Format(" AND ed.extPersonAspirant.Id NOT IN ({0}) ", Util.BuildStringWithCollection(lstIds));

            FillGridRight(rez);                        
        }

        private void FillGridRight(string flt_prof)
        {
            string flt_where = string.Format(" WHERE ed.qAbiturient.FacultyId = {0} AND ed.qAbiturient.StudyLevelGroupId IN ({1}) ", facultyId, Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId)) + flt_prof;

            flt_where += string.Format(@" AND ed.extPersonAspirant.Id IN (SELECT PersonId FROM ed.ExamsVedHistory WHERE ExamsVedId = '{0}') 
            AND ed.extPersonAspirant.Id NOT IN (SELECT PersonId FROM ed.ExamsVedRoomHistory INNER JOIN ed.ExamsVedRoom 
            ON ed.ExamsVedRoomHistory.ExamsVedRoomId = ed.ExamsVedRoom.Id WHERE ed.ExamsVedRoom.ExamsVedId = '{0}')", vedId);
            
            FillGrid(dgvRight, sQuery + flt_where, "", sOrderby);
        }

        private void FillGridLeft()
        {
            string flt_where = string.Format(" WHERE ed.qAbiturient.FacultyId = {0} AND ed.qAbiturient.StudyLevelGroupId IN ({1}) ", facultyId, Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId));
            
            //заполнили левый
            if (vedRoomId != null)
            {
                string sFilter = string.Format(" AND ed.extPersonAspirant.Id IN (Select ed.ExamsVedRoomHistory.PersonId FROM ed.ExamsVedRoomHistory WHERE ed.ExamsVedRoomHistory.ExamsVedRoomId = '{0}') ", vedRoomId);
                FillGrid(dgvLeft, sQuery + flt_where, sFilter, sOrderby);
            }
            else //новый
            {
                InitGrid(dgvLeft);
            }
        }

        //подготовка нужного грида
        private void InitGrid(DataGridView dgv)
        {
            dgv.Columns.Clear();

            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Width = 20;
            column.ReadOnly = false;
            column.Resizable = DataGridViewTriState.False;
            dgv.Columns.Add(column);
            dgv.Columns.Add("Id", "Id");
            dgv.Columns.Add("Num", "Ид_номер");
            dgv.Columns.Add("FIO", "ФИО");
            dgv.Columns.Add("Attestat", "Документ_об_образовании");
            dgv.Columns.Add("Pasport", "Паспорт");

            dgv.Columns["Id"].Visible = false;

            dgv.Update();
        }

        //функция заполнения грида
        private void FillGrid(DataGridView dgv, string sQuery, string sWhere, string sOrderby)
        {
            try
            {
                //подготовили грид
                InitGrid(dgv);
                DataSet ds = bdc.GetDataSet(sQuery + sWhere + sOrderby);

                //заполняем строки
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                   
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(dgv, false, dr["Id"].ToString(), dr["Ид_номер"].ToString(), dr["ФИО"].ToString(), dr["Документ_об_образовании"].ToString(), dr["Паспорт"].ToString());                   
                    dgv.Rows.Add(r);
                }

                //блокируем редактирование
                for (int i = 1; i < dgv.ColumnCount; i++)
                    dgv.Columns[i].ReadOnly = true;
                
                dgv.Update();               
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении грида данными протокола: ", ex);
            }
        }

        private string RoomNumber
        {
            get { return tbRoomNumber.Text.Trim(); }
            set { tbRoomNumber.Text = value; }
        }

        private bool Save()
        {
            if (dgvLeft.Rows.Count == 0)
            {
                MessageBox.Show("Нельзя создать пустую ведомость!", "Внимание");
                return false;
            }

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        if (vedRoomId == null)
                        {
                            if (RoomNumber == string.Empty)
                            {
                                MessageBox.Show("Укажите помещение!", "Внимание");
                                return false;
                            }

                            int cnt = (from evr in context.ExamsVedRoom
                                       where evr.ExamsVedId == vedId && evr.RoomNumber == RoomNumber
                                       select evr).Count();

                            if (cnt > 0)
                            {
                                MessageBox.Show("Уже существует ведомость на данное помещение!", "Внимание");
                                return false;
                            }

                            int num = (from evr in context.ExamsVedRoom
                                       where evr.ExamsVedId == vedId
                                       select evr).Count();
                            num++;
                            
                            ObjectParameter vedParId = new ObjectParameter("id", typeof(Guid));
                            context.ExamsVedRoom_Insert(vedId, RoomNumber, num, vedParId);
                            vedRoomId = (Guid)vedParId.Value;                           
                        }
                        else
                            context.ExamsVedRoomHistory_DeleteByVedRoomId(vedRoomId); 

                        //записи в ведомостьхистори
                        foreach (DataGridViewRow r in dgvLeft.Rows)
                        {
                            Guid? persId = new Guid(r.Cells["Id"].Value.ToString());
                            context.ExamsVedRoomHistory_InsertToVed(vedRoomId, persId);                            
                        }

                        transaction.Complete();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при создании новой ведомости: ", ex);
                return false;
            }
        }
        
        //отмена
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //все хорошо
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(Save())
                this.Close();
        }

        //ресайз
        private void VedExamCrypto_Resize(object sender, EventArgs e)
        {
            int width = (this.Width - 90 - 11 * 2) / 2;
            dgvLeft.Width = dgvRight.Width = width;
            dgvLeft.Location = new Point(12, 112);
            dgvRight.Location = new Point(90 + width, 112);

            btnLeft.Left = 11 + width + 11;
            btnLeftAll.Left = 11 + width + 11;

            btnRight.Left = 90 + width - btnRight.Width - 11;
            btnRightAll.Left = 90 + width - btnRightAll.Width - 11;
        }               

        //перенос строк
        private void MoveRows(DataGridView from, DataGridView to, bool isAll)
        {
            for (int i = from.Rows.Count - 1; i >= 0; i--) 
            {
                DataGridViewRow dr = from.Rows[i];               

                if (isAll || (bool)dr.Cells[0].Value)
                {
                    dr.Cells[0].Value = false;
                    from.Rows.Remove(dr);
                    to.Rows.Add(dr);
                }
            }
        }
                
        //убрать влево
        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, false);
        }

        //убрать вправо
        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, false);
        }

        //все влево
        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, true);
        }

        //все вправо
        private void btnRightAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, true);
        }

        private void ExamsVed_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.UpdateVedRoomList();
            owner.SelectVedRoom(vedRoomId);
        }

        private void dgvRight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string abId = dgvRight.Rows[dgvRight.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (abId != "")
                {
                    MainClass.OpenCardPerson(abId, this, dgvRight.CurrentRow.Index);
                }
            }                
        }        
    }
}
