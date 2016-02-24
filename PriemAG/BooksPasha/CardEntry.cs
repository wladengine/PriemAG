using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;

using EducServLib;
using PriemLib;
namespace Priem
{
    public partial class CardEntry : BookCard
    {
        public CardEntry(string id)
            : base(id)
        {
            InitializeComponent();            
            InitControls();            
        }
      
        protected override void ExtraInit()
        {
            base.ExtraInit();
            _title = "Прием";
            _tableName = "ed.[Entry]";              
        }

        protected override void SetIsOpen()
        {
            return;
        }
        protected override void DeleteIsOpen()
        {
            return;
        }
        protected override bool GetIsOpen()
        {
            return false;
        }     

        protected override void FillCard()
        {
            if (_Id == null)
                return;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    qEntry ent = (from ex in context.qEntry
                                 where ex.Id == GuidId
                                       select ex).FirstOrDefault();


                    tbStudyLevel.Text = ent.StudyLevelName;
                    tbFaculty.Text = ent.FacultyName;
                    tbLicenseProgram.Text = ent.LicenseProgramName + " (" + ent.LicenseProgramCode + ")";
                    tbObrazProgram.Text = ent.ObrazProgramName + " (" + ent.ObrazProgramCrypt + ")";
                    tbProfile.Text = ent.ProfileName;
                    tbStudyForm.Text = ent.StudyFormName;
                    tbStudyBasis.Text = ent.StudyBasisName;
                    tbStudyPlan.Text = ent.StudyPlanNumber;
                    tbKC.Text = ent.KCP.ToString();
                    string real = ent.IsSecond ? " для лиц с ВО" : "";
                    real += ent.IsReduced ? " сокращенная" : "";
                    real += ent.IsParallel ? " параллельная" : "";
                    tbSecond.Text = real.Length == 0 ? "нет" : real;
                    tbKCPCel.Text = ent.KCPCel.ToString();

                    UpdateExams();
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + exc.Message);
            }
        }        

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();
            if(_Id == null)
                btnSaveChange.Enabled = false;
            if (!MainClass.IsEntryChanger())
                btnSaveChange.Enabled = false;
        }

        protected override void SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();

            foreach (Control control in tcCard.Controls)
            {
                control.Enabled = true;
                foreach (Control crl in control.Controls)
                    crl.Enabled = true;
            }

            WinFormsServ.SetSubControlsEnabled(gbEntry, false);
            tbKCPCel.Enabled = true;
        }

        protected override void SetAllFieldsNotEnabled()
        {
            base.SetAllFieldsNotEnabled();
            tcCard.Enabled = true;

            foreach (Control control in tcCard.Controls)
            {
                foreach (Control crl in control.Controls)
                    crl.Enabled = false;
            }            
        }

        protected override string  Save()
        {
            using (PriemEntities context = new PriemEntities())
            {
                int? KCPCel;
                int j;
                if (int.TryParse(tbKCPCel.Text.Trim(), out j))
                    KCPCel = j;
                else
                    KCPCel = null;

                context.Entry_UpdateCEl(GuidId, KCPCel);
            }
            return "true";
        }

        #region Exams

        public void UpdateExams()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var query = (from exEntry in context.extExamInEntry                             
                             where exEntry.EntryId == GuidId
                             orderby exEntry.IsProfil descending, exEntry.ExamName
                             select new 
                             { 
                                 exEntry.Id, 
                                 Name = exEntry.ExamName,
                                 IsProfil = exEntry.IsProfil ? "да" : "нет",
                                 exEntry.EgeMin
                             });

                dgvExams.DataSource = query;
                dgvExams.Columns["Id"].Visible = false;

                dgvExams.Columns["Name"].HeaderText = "Название"; 
                dgvExams.Columns["IsProfil"].HeaderText = "Профильный"; 
                dgvExams.Columns["EgeMin"].HeaderText = "Мин. ЕГЭ"; 
            }
        }

        private void OpenCardExam(Guid? entryId, string id, bool isForModified)
        {
            CardExamInEntry crd = new CardExamInEntry(entryId, id, isForModified);
            crd.ToUpdateList += new UpdateListHandler(UpdateExams);
            crd.Show();
        }


        private void btnOpenExam_Click(object sender, EventArgs e)
        {
            if (dgvExams.CurrentCell != null && dgvExams.CurrentCell.RowIndex > -1)
            {
                string itemId = dgvExams.Rows[dgvExams.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (!string.IsNullOrEmpty(itemId))
                    OpenCardExam(GuidId, itemId, _isModified);
            }
        }

        private void btnAddExam_Click(object sender, EventArgs e)
        {
            OpenCardExam(GuidId, null, true);
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            if (dgvExams.CurrentCell != null && dgvExams.CurrentCell.RowIndex > -1)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string itemId = dgvExams.CurrentRow.Cells["Id"].Value.ToString();
                    try
                    {
                        using (PriemEntities context = new PriemEntities())
                        {
                            int? id = int.Parse(itemId);
                            context.ExamInEntry_Delete(id);
                        }
                    }
                    catch (Exception ex)
                    {
                        WinFormsServ.Error("Каскадное удаление запрещено: " + ex.Message);
                    }

                    UpdateExams();
                }
            }
        }

        private void dgvExams_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOpenExam_Click(null, null);
        }

        #endregion   
    }
}
