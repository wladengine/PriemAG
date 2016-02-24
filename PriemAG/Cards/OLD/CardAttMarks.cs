using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;

using EducServLib;
using BaseFormsLib;
using PriemLib;
namespace Priem
{
    public partial class CardAttMarks : BaseForm
    {
        private Guid? _personId;
        private List<string> _lMarks;

        public CardAttMarks(Guid? personId, bool isReadOnly)
        {
            InitializeComponent();
            InitFocusHandlers();

            this.CenterToParent();
            _personId = personId;
           
            _lMarks = new List<string>();
      
            ComboServ.FillCombo(cbSubjects, HelpClass.GetComboListByTable("ed.AttSubject"), true, false);

            UpdateDataGrid();

            if (isReadOnly)
            {
                cbSubjects.Enabled = false;
                tbMark.Enabled = false;
                btnAdd.Enabled = false;
                dgvExams.Columns["Delete"].Visible = false;
            }
        }

        //закрытие
        private void btnClose_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        //добавить 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((!Regex.IsMatch(tbMark.Text.Trim(), @"^[2-5]{0,1}$")))
            {
                WinFormsServ.Error("Неправильный формат оценки");
                return;
            }

            int mark;
            if(!int.TryParse(tbMark.Text.Trim(), out mark))
            {
                WinFormsServ.Error("Введите оценку");
                return;
            }

            //если вдруг сломается комбобокс, выводя все предметы
            if (_lMarks.Contains(cbSubjects.Text))
            {
                MessageBox.Show("Оценка по данному предмету уже есть!");
                return;
            }

            int? subjId = ComboServ.GetComboIdInt(cbSubjects);
            if(subjId == null)
            {
                WinFormsServ.Error("Не выбран предмет");
                return;
            }
            
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    context.AttMarks_Insert(_personId, subjId, mark);                    
                    UpdateDataGrid();
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при добавлении оценки: "+ ex.Message);
            }
        }

        private void UpdateDataGrid()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var query = from am in context.AttMarks
                            join sj in context.AttSubject
                            on am.AttSubjectId equals sj.Id
                            where am.PersonId == _personId
                            orderby sj.Name
                            select new
                            {
                                am.Id,
                                Subject = sj.Name,
                                Mark = am.Value
                            };


                dgvExams.DataSource = query;

                dgvExams.Columns["Id"].Visible = false;
                dgvExams.Columns["Subject"].HeaderText = "Предмет";
                dgvExams.Columns["Mark"].HeaderText = "Оценка";            

                if (!dgvExams.Columns.Contains("Delete"))
                {
                    DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                    col.Name = "Delete";
                    col.HeaderText = "Удалить оценку";
                    col.Text = "Удалить";
                    col.UseColumnTextForButtonValue = true;

                    dgvExams.Columns.Add(col);
                    dgvExams.Update();
                }

                List<float> lAvg = new List<float>();
                //делаем проверочку 
                _lMarks.Clear();
                foreach (DataGridViewRow row in dgvExams.Rows)
                {
                    _lMarks.Add(row.Cells["Subject"].Value.ToString());
                    lAvg.Add(float.Parse(row.Cells["Mark"].Value.ToString()));
                }

                float avg = 0;
                foreach (float i in lAvg)
                    avg += i;
                if (avg != 0 && lAvg.Count != 0)//division by zero, lol
                {
                    string avgstr = (avg / (float)lAvg.Count).ToString();
                    if (avgstr.Length >= 4)
                        tbAvg.Text = avgstr.Substring(0, 4);
                    else
                        tbAvg.Text = avgstr;
                }

                string filter = "";
                foreach (string s in _lMarks)
                {
                    filter += "'" + s + "'" + ", ";
                }
                if (filter.Length != 0)
                {
                    filter = filter.Substring(0, filter.Length - 2);
                    ComboServ.FillCombo(cbSubjects, HelpClass.GetComboListByQuery(string.Format("SELECT AttSubject.Name, Cast(AttSubject.Id as nvarchar(100)) FROM AttSubject WHERE AttSubject.Name NOT IN ({0})", filter)), true, false);                    
                }
                if (cbSubjects.SelectedIndex == -1)
                    btnAdd.Enabled = false;
                else
                    btnAdd.Enabled = true;
            }

        }

        private void dgvExams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvExams.Columns[e.ColumnIndex].Name=="Delete")
            {
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        Guid markId = (Guid)dgvExams.Rows[e.RowIndex].Cells["Id"].Value;
                        context.AttMarks_Delete(markId);
                    }

                    UpdateDataGrid();
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка при удалении оценки: " + ex.Message);
                }

            }
        }
    }
}