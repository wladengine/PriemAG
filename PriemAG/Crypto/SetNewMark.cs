using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using EducServLib;
//using BDClassLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class SetNewMark : BaseFormEx
    {
        private LoadMarks owner;
        private DBPriem bdc; 

        private string examId;
        private DateTime passDate;

        public SortedList<string, string> slAbits;

        private string sQuery = @"SELECT DISTINCT ed.extAbitAspirant.Id, ed.extPersonAspirant.PersonNum as Ид_номер, RegNum as Рег_Номер, ed.extPersonAspirant.FIO as ФИО, 
                                    ed.extAbitAspirant.ObrazProgramName + ' ' +(Case when ed.extAbitAspirant.ProfileId IS NULL then '' else ed.extAbitAspirant.ProfileName end) as Направление, 
                                    ed.qMark.Value AS Старая_оценка 
                                    FROM ed.extAbitAspirant LEFT JOIN ed.extPersonAspirant ON ed.extAbitAspirant.PersonId = ed.extPersonAspirant.Id 
                                    INNER JOIN ed.qMark ON ed.qMark.AbiturientId = ed.extAbitAspirant.Id 
                                    INNER JOIN ed.extExamInEntry ON ed.qMark.ExamInEntryId = ed.extExamInEntry.Id 
                                    WHERE 1=1 ";

        private string sOrderby = " ORDER BY ФИО ";

        public SetNewMark(LoadMarks owner, string examId, DateTime date, SortedList<string, string> slValues)
        {            
            InitializeComponent();
            
            this.owner = owner;
            bdc = MainClass.Bdc;
           
            this.passDate = date;
            this.examId = examId;

            this.slAbits = slValues;           
            InitControls();
        }

        //дополнительная инициализация
        protected virtual void InitControls()
        {           
            this.CenterToParent();
            InitFocusHandlers();

            Dgv = dgvRight;

            tbExam.Text = bdc.GetStringValue(string.Format("SELECT DISTINCT ExamName FROM ed.extExamInEntry WHERE ExamId = '{0}'", examId));
            tbDate.Text = passDate.ToShortDateString();
            
            
            //заполнение гридов            
            InitGrid(dgvLeft);
            FillGridRight();            
        }

        private void FillGridRight()
        {
            string query = string.Empty;
            if (slAbits.Keys.Count == 0)
                return;           
            
            string ids = "'" + string.Join("', '", new List<string>(slAbits.Keys).ToArray()) + "'";
            string flt_InList = string.Format(" AND ed.extAbitAspirant.Id IN ({0}) ", ids);


            query = sQuery + flt_InList + " AND ed.extExamInEntry.ExamId = " + examId;

            FillGrid(dgvRight, query, "", sOrderby);

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
            dgv.Columns.Add("RegNum", "Рег_номер");
            dgv.Columns.Add("FIO", "ФИО");
            dgv.Columns.Add("OldMark", "Старая_оценка");
            dgv.Columns.Add("NewMark", "Новая_оценка");
            dgv.Columns.Add("Spec", "Направление");

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
                    r.CreateCells(dgv, false, dr["Id"].ToString(), dr["Ид_номер"].ToString(), dr["Рег_номер"].ToString(), dr["ФИО"].ToString(), dr["Старая_оценка"].ToString(), slAbits[dr["Id"].ToString()], dr["Направление"].ToString());
                    dgv.Rows.Add(r);
                }
               
                //блокируем редактирование
                for (int i = 1; i < dgv.ColumnCount; i++)
                    dgv.Columns[i].ReadOnly = true;

                dgv.Update();

                lblCountRight.Text = "Всего: " + dgvRight.RowCount.ToString();                
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении грида данными протокола: ", ex);
            }
        }

        private bool Save()
        {
            try
            {
                foreach (DataGridViewRow r in dgvLeft.Rows)
                {
                    try
                    {
                        string id = r.Cells["Id"].Value.ToString();                        
                        owner.slReplaceMark.Add(id, slAbits[id]);                        
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }

            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при записи в коллекцию: ", ex);
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
        private void SetNewMark_Resize(object sender, EventArgs e)
        {
            int width = (this.Width - 90 - 11 * 2) / 2;
            dgvLeft.Width = dgvRight.Width = width;
            dgvLeft.Location = new Point(12, 59);
            dgvRight.Location = new Point(90 + width, 59);

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

            lblCountRight.Text = "Всего: " + dgvRight.RowCount.ToString();
            lblCountLeft.Text = "Всего: " + dgvLeft.RowCount.ToString();
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
        
        private void dgvRight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string abId = dgvRight.Rows[dgvRight.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (abId != "")
                {
                    MainClass.OpenCardAbit(abId, this, dgvRight.CurrentRow.Index);
                }
            }
        }        
    }
}
