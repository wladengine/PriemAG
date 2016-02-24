using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Transactions;

using BDClassLib;
using EducServLib;
using WordOut;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class ExamsVedRoomList : BaseFormEx
    {
        private DBPriem bdc;
        private string sQuery;        
        private string sOrderby;
        
        //конструктор
        public ExamsVedRoomList()
        {
            this.CenterToParent();
            this.MdiParent = MainClass.mainform;
                    
            this.sOrderby = " ORDER BY Фамилия ";           

            InitializeComponent();
            InitControls();
        }        

        //дополнительная инициализация контролов
        private void InitControls()
        {
            InitFocusHandlers();			
            bdc = MainClass.Bdc; 

            try
            {
                if (MainClass.IsCrypto() || MainClass.IsPasha())
                    btnCreateCsv.Visible = true;
                else
                    btnCreateCsv.Visible = false;

                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, false);
                    ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name"), false, true);

                    UpdateVedList();
                    UpdateVedRoomList();
                    
                    UpdateDataGrid();

                    cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);
                    cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
                    cbExamVed.SelectedIndexChanged += new EventHandler(cbExamVed_SelectedIndexChanged);
                    cbExamVedRoom.SelectedIndexChanged += new EventHandler(cbExamVedRoom_SelectedIndexChanged);                   
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при инициализации формы ведомостей: ", ex);
            }
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVedList();
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVedList();
        }

        void cbExamVed_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVedRoomList();
        }

        void cbExamVedRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }

        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public Guid? ExamVedId
        {
            get
            {
                string valId = ComboServ.GetComboId(cbExamVed);
                if (string.IsNullOrEmpty(valId))
                    return null;
                else
                    return new Guid(valId);
            }
            set
            {
                if (value == null)
                    ComboServ.SetComboId(cbExamVed, (string)null);
                else
                    ComboServ.SetComboId(cbExamVed, value.ToString());
            }
        }

        public Guid? ExamVedRoomId
        {
            get
            {
                string valId = ComboServ.GetComboId(cbExamVedRoom);
                if (string.IsNullOrEmpty(valId))
                    return null;
                else
                    return new Guid(valId);
            }
            set
            {
                if (value == null)
                    ComboServ.SetComboId(cbExamVedRoom, (string)null);
                else
                    ComboServ.SetComboId(cbExamVedRoom, value.ToString());
            }
        }

        //обновление списка 
        public void UpdateVedList()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst =
                        ((from ent in context.extExamsVed
                          where MainClass.lstStudyLevelGroupId.Contains(ent.StudyLevelGroupId)
                          && ent.FacultyId == FacultyId
                          && (StudyBasisId != null ? ent.StudyBasisId == StudyBasisId : true == true)
                          select new
                          {
                              ent.Id,
                              ent.ExamName,
                              ent.Date,
                              StBasis = ent.StudyBasisId == null ? "" : ent.StudyBasisAcr,
                              AddVed = ent.IsAddVed ? " дополнительная" : "",
                              ent.AddCount
                          }).Distinct()).ToList()
                          .Select(u => new KeyValuePair<string, string>(u.Id.ToString(),
                                u.ExamName + ' ' + u.Date.ToShortDateString() + ' ' + u.StBasis + u.AddVed +
                                (u.AddCount > 1 ? "(" + Convert.ToString(u.AddCount) + ")" : ""))).ToList();

                    ComboServ.FillCombo(cbExamVed, lst, true, false);    
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при обновлении списка ведомостей: " + ex);
            }
        }
               
        public void UpdateVedRoomList()
        {
            try
            {               
                if (ExamVedId == null)
                {
                    ComboServ.FillCombo(cbExamVedRoom, new List<KeyValuePair<string, string>>(), false, false);
                    cbExamVedRoom.Enabled = false;                   
                    
                }
                else
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        List<KeyValuePair<string, string>> lst = ((from ent in context.ExamsVedRoom
                                                                   where ent.ExamsVedId == ExamVedId
                                                                   orderby ent.Number, ent.RoomNumber
                                                                   select new
                                                                   {
                                                                       ent.Id,
                                                                       ent.Number,
                                                                       ent.RoomNumber                                                                      
                                                                   }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(),
                                                                       u.Number + ": " + u.RoomNumber)).ToList();

                        ComboServ.FillCombo(cbExamVedRoom, lst, true, false);
                        cbExamVedRoom.Enabled = true;        
                    }                                
                }               
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при обновлении списка ведомостей: " + ex);
            }
        }

        //обновление грида
        public virtual void UpdateDataGrid()
        {              
            string sFilters;

            btnCreateCsv.Enabled = true;
            btnCreateRooms.Enabled = true;
            btnDelete.Enabled = true;
            btnChange.Enabled = true;

            if (ExamVedRoomId != null)
            {
                sQuery = @"SELECT DISTINCT ed.extPersonAspirant.Id, ed.extPersonAspirant.PersonNum as Ид_номер, ed.extPersonAspirant.Surname AS Фамилия, ed.extPersonAspirant.Name AS Имя, 
                          ed.extPersonAspirant.SecondName AS Отчество, ed.extPersonAspirant.BirthDate AS Дата_рождения 
                          FROM ed.extPersonAspirant INNER JOIN ed.ExamsVedRoomHistory ON ed.ExamsVedRoomHistory.PersonId = ed.extPersonAspirant.Id ";

                sFilters = string.Format("WHERE ed.ExamsVedRoomHistory.ExamsVedRoomId = '{0}' ", ExamVedRoomId.ToString());

            }
            if (ExamVedId != null)
            {
                sQuery = @"SELECT DISTINCT ed.extPersonAspirant.Id, ed.extPersonAspirant.PersonNum as Ид_номер, ed.extPersonAspirant.Surname AS Фамилия, ed.extPersonAspirant.Name AS Имя, 
                          ed.extPersonAspirant.SecondName AS Отчество, ed.extPersonAspirant.BirthDate AS Дата_рождения 
                          FROM ed.extPersonAspirant INNER JOIN ed.ExamsVedHistory ON ed.ExamsVedHistory.PersonId = ed.extPersonAspirant.Id ";

                sFilters = string.Format("WHERE ed.ExamsVedHistory.ExamsVedId = '{0}' ", ExamVedId.ToString());

                btnDelete.Enabled = false;
                btnChange.Enabled = false;
            }
            else
            {
                btnCreateCsv.Enabled = false;
                btnCreateRooms.Enabled = false;
                btnDelete.Enabled = false;
                btnChange.Enabled = false;

                dgvList.DataSource = null;
                dgvList.Update();
                return;  
            }                 
            
            if (!dgvList.Columns.Contains("Number"))
            {
                dgvList.Columns.Add("Number", "№");
                dgvList.Update();
            }
            dgvList.Columns["Number"].DisplayIndex = 0; 

            HelpClass.FillDataGrid(dgvList, bdc, sQuery, sFilters, sOrderby);                       
        }
        
        //закрытие
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //изменение - только для супер
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (ExamVedId == null || ExamVedRoomId == null)
                return;

            if (MainClass.IsFacMain() || MainClass.IsPasha())
            {
                ExamsVedRoomCard p = new ExamsVedRoomCard(this, ExamVedId, ExamVedRoomId);
                p.Show();
            }
        }

        private void btnCreateRooms_Click(object sender, EventArgs e)
        {
            if (ExamVedId == null)
                return;

            ExamsVedRoomCard p = new ExamsVedRoomCard(this, ExamVedId);
            p.Show();
            
        }

        //выбрать ведомость в списке
        public void SelectVed(Guid? vedId)
        {
            if (vedId != null)
                ExamVedId = vedId;               
        }

        //выбрать ведомость в списке
        public void SelectVedRoom(Guid? vedRoomId)
        {
            if (vedRoomId != null)
                ExamVedRoomId = vedRoomId;
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ExamVedId == null || ExamVedRoomId == null)
                return;
            using (PriemEntities context = new PriemEntities())
            {

                if (MessageBox.Show("Удалить выбранную ведомость в помещении? ", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            context.ExamsVedRoomHistory_DeleteByVedRoomId(ExamVedRoomId);
                            context.ExamsVedRoom_Delete(ExamVedRoomId);

                            UpdateVedRoomList();
                        }
                    }
                    catch (Exception de)
                    {
                        WinFormsServ.Error("Ошибка удаления данных " + de);
                    }
                }
            }          
        }
        
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvList.Columns["Number"].Index)
            {
                e.Value = string.Format("{0}", e.RowIndex+1);
            }  
        }

        private void btnCreateCsv_Click(object sender, EventArgs e)
        {           
            string fName;

            string query = @"SELECT DISTINCT ed.extPersonAspirant.Id, ed.extPersonAspirant.PersonNum as regnum, ed.extPersonAspirant.Surname, ed.extPersonAspirant.Name, ed.extPersonAspirant.SecondName, 
                    CAST((STR( DAY( ed.extPersonAspirant.Birthdate ) ) + '/' +STR( MONTH( ed.extPersonAspirant.Birthdate ) ) + '/' +STR( YEAR( ed.extPersonAspirant.Birthdate )))AS DATETIME) as birthdate ";

            // если по выбрана конкр. ауд.
            if (ExamVedRoomId != null)
            {
                query += " FROM ed.extPersonAspirant INNER JOIN ed.ExamsVedRoomHistory ON ed.ExamsVedRoomHistory.PersonId = ed.extPersonAspirant.Id " +
                          string.Format("WHERE ed.ExamsVedRoomHistory.ExamsVedRoomId = '{0}' ", ExamVedRoomId.ToString());

                fName = cbExamVed.Text + ", " +cbExamVedRoom.Text;
            }

            else if (ExamVedId != null)
            {
                query += " FROM ed.extPersonAspirant INNER JOIN ed.ExamsVedHistory ON ed.ExamsVedHistory.PersonId = ed.extPersonAspirant.Id " +
                         string.Format("WHERE ed.ExamsVedHistory.ExamsVedId = '{0}' ", ExamVedId.ToString());

                fName = cbExamVed.Text;
            }
            else
                return;

            try
            { 
                fName = fName.Replace("/", "_");
                fName = fName.Replace(":", "-");

                SaveFileDialog sfd = new SaveFileDialog();                
                sfd.FileName = fName + ".csv"; 
                sfd.Filter = "CSV files|*.csv";               

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                using (StreamWriter writer = new StreamWriter(sfd.OpenFile(), Encoding.GetEncoding(1251)))
                {
                    DataSet dsExam = bdc.GetDataSet(string.Format(@"SELECT ed.extExamsVed.Id, ed.extExamsVed.Number, ed.extExamsVed.Date AS ExamDate, ed.extExamsVed.ExamName 
                                                                    FROM ed.extExamsVed WHERE ed.extExamsVed.Id = '{0}'", ExamVedId.ToString()));
                    DataRow dr = dsExam.Tables[0].Rows[0];                    
                    
                    // если есть разделение по аудиториям, то получим его
                    DataSet dsAudAbits = bdc.GetDataSet(string.Format(@"SELECT ed.ExamsVedHistory.PersonId, ed.ExamsVedRoom.RoomNumber, ed.extPersonAspirant.Num 
                                                                        FROM ed.ExamsVedHistory LEFT OUTER JOIN ed.ExamsVedRoomHistory 
                                                                        ON ed.ExamsVedRoomHistory.PersonId = ed.ExamsVedHistory.PersonId LEFT OUTER JOIN 
                                                                        ed.ExamsVedRoom ON ed.ExamsVedRoomHistory.ExamsVedRoomId = ed.ExamsVedRoom.Id 
                                                                        INNER JOIN ed.extPersonAspirant ON ed.extPersonAspirant.Id = ed.ExamsVedHistory.PersonId
                                                                        WHERE ed.ExamsVedHistory.ExamsVedId = '{0}'", ExamVedId.ToString()));

                    List<string> list = new List<string>();
                    list.Add(dr["ExamName"].ToString());
                    list.Add(dr["Number"].ToString());                                         
                    list.Add(DateTime.Parse(dr["ExamDate"].ToString()).ToString("dd/MM/yyyy"));
                    writer.WriteLine(string.Join("%", list.ToArray()));                  

                    DataSet ds = MainClass.Bdc.GetDataSet(query);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list = new List<string>();
                        
                        list.Add(row["regnum"].ToString());
                        list.Add(row["surname"].ToString());
                        list.Add(row["name"].ToString());
                        list.Add(row["secondname"].ToString());                        
                        list.Add(DateTime.Parse(row["birthdate"].ToString()).ToString("dd/MM/yyyy"));
                        
                        string audNum = (
                            from DataRow x in dsAudAbits.Tables[0].Rows
                            where x["Num"].ToString() == row["regnum"].ToString()
                            select x["RoomNumber"].ToString() == "" ? "общ" : x["RoomNumber"].ToString()
                            ).DefaultIfEmpty<string>("н.указ").ToList<string>()[0];

                        list.Add(audNum);
                       
                        writer.WriteLine(string.Join("%", list.ToArray()));
                    }
                }
            }
            catch(Exception exc)
            {
                WinFormsServ.Error("Ошибка при экспорте: ", exc);
            } 
        }       
    }
}