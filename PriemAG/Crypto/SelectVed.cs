using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity.Core.Objects;

using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class SelectVed : Form
    {        
        protected Guid? vedId;
        protected bool isLocked;       
        
        public SelectVed()
        {
            InitializeComponent();
            InitControls();
        }

        public bool IsOral
        {
            get 
            {
                bool h = QueryServ.ToBoolValue(ComboServ.GetComboIdInt(cbVedType));
                return h; 
            }            
        }

      
        private void InitControls()
        {
            tbVedNum.Focus();

            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string,string>("0", "Письменная часть"));
            lst.Add(new KeyValuePair<string,string>("1", "Устная часть"));
            
            ComboServ.FillCombo(cbVedType, lst, false, false);            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (vedId == null)
                return;

            if (!isLocked)
            {
                MessageBox.Show("Данная ведомость не закрыта!", "Ошибка");
                return;
            }

            new EnterMarks(vedId, IsOral).Show();
            this.Close();
        }

        private void tbVedNum_KeyDown(object sender, KeyEventArgs e)
        {               
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (vedId == null )
                    return;

                if (!isLocked)
                {
                    MessageBox.Show("Данная ведомость не закрыта!", "Ошибка");
                    return;
                }

                new EnterMarks(vedId, true).Show();
                this.Close();
            }
       
            if (e.KeyValue == 189)
            {
                if (!tbVedNum.Text.Contains("=="))
                    return;

                int vedNum = 0;
                string t = tbVedNum.Text.Trim();
                t = t.Substring(0, t.IndexOf("=="));
                int.TryParse(t, out vedNum);

                using (PriemEntities context = new PriemEntities())
                {
                    var exVed = (from ev in context.extExamsVed
                                 where ev.Number == vedNum && MainClass.lstStudyLevelGroupId.Contains(ev.StudyLevelGroupId)
                                 select ev).FirstOrDefault();

                    vedId = exVed.Id;
                    isLocked = exVed.IsLocked;

                    string g = "";
                    g =  exVed.FacultyName + " " + exVed.StudyBasisName + "\r\n" +
                         exVed.Date.Date.ToShortDateString() + " " + exVed.ExamName + "\r\n";

                    if (exVed.IsAddVed)
                        g += "дополнительная (" + exVed.AddCount.ToString() + ")";

                    tbVedName.Text = g;
                }           
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }                    
    }
}
