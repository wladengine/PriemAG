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
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class SelectExamCrypto : PriemLib.SelectExamCrypto
    {
        protected ExamsVedList owner;

        public SelectExamCrypto(ExamsVedList owner, int studyLevelGroupId, int? facId, int? basisId, DateTime passDate, int? examId)
            : base(owner, studyLevelGroupId, facId, basisId, passDate, examId)
        {
            this.owner = owner;
        }

        public SelectExamCrypto(ExamsVedList owner, int studyLevelId, int? facId, int? basisId)
            : base(owner, studyLevelId, facId, basisId)
        {
            this.owner = owner;
        }

        protected override List<KeyValuePair<string, string>> GetSourceExam()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = Exams.GetExamsWithFiltersStudyLevelId(context, iStudyLevelGroupId, iFacultyId, null, null, null, null, iStudyBasisId, null, null, null);
                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.ExamId.ToString(), u.ExamName)).Distinct().ToList();

                return lst;
            }
        }

        protected override void btnOk_Click(object sender, EventArgs e)
        {
            if (!CheckFields())
                return;

            ExamsVedCard frm = new ExamsVedCard(owner, iStudyLevelGroupId, iFacultyId.Value, ExamId.Value, ExamDate, iStudyBasisId, isAdditional);
                //= new ExamsVedCard(owner, iStudyLevelGroupId, iFacultyId.Value, ExamId.Value, ExamDate, iStudyBasisId, isAdditional);
            frm.Show();
            this.Close();
        }     
    }
}