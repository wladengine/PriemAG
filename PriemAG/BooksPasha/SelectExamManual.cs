using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseFormsLib;
using EducServLib;
using PriemLib;
using PriemLib;

namespace Priem
{
    public partial class SelectExamManual : BaseForm
    {
        private DBPriem bdc;

        public SelectExamManual()
        {
            InitializeComponent();
            InitControls();            
        }  

        //дополнительная инициализация контролов
        private void InitControls()
        {
            this.CenterToParent();
            InitFocusHandlers();
            this.MdiParent = MainClass.mainform;
            bdc = MainClass.Bdc;


            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.SP_Faculty"), false, false);
            ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis"), false, true);
            
            FillExams();

            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);      
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillExams();
        }

        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }

        public int? ExamId
        {
            get { return ComboServ.GetComboIdInt(cbExam); }
            set { ComboServ.SetComboId(cbExam, value); }
        }

        private void FillExams()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = Exams.GetExamsWithFilters(context, FacultyId, null, null, null, null, StudyBasisId, null, null, null);
                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.ExamId.ToString(), u.ExamName)).Distinct().ToList();
                ComboServ.FillCombo(cbExam, lst, false, true);
            }             
        } 

        private void btnOk_Click(object sender, EventArgs e)
        {
            new EnterMarksManual(ExamId, FacultyId, StudyBasisId).Show();            
            this.Close();
        }                     
    }
}