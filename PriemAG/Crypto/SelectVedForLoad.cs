using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class SelectVedForLoad : Form
    {
        private bool _forAppeal;       

        public SelectVedForLoad(bool forAppeal)
        {
            InitializeComponent();
            _forAppeal = forAppeal;
            InitControls();
        }

        private class VedClass
        {
            string vedId; 
            string vedName;
            bool isLoad;

            public VedClass(string _vedId, string _vedName, bool _isLoad) 
            {
                this.vedId = _vedId;
                this.vedName = _vedName;
                this.isLoad = _isLoad; 
            } 
            
            public override string ToString() 
            {
                return this.vedName; 
            } 
            
            public string VedId 
            { 
                get 
                {
                    return this.vedId; 
                } 
            }

            public string VedName
            {
                get
                {
                    return this.vedName;
                }
            }

            public bool IsLoad
            {
                get
                {
                    return this.isLoad;
                }
            } 
        }

        private void InitControls()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.SP_Faculty"), false, false);
            ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis"), false, true);
                        
            cbVed.DrawMode = DrawMode.OwnerDrawFixed;
            cbVed.DrawItem += new DrawItemEventHandler(cb_DrawItem);
            
            UpdateVedList();

            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);            
            cbFaculty.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);              
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVedList();
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVedList();
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

        //обновление списка 
        public void UpdateVedList()
        {
            try
            {
                string flt_appeal = "";
                if (_forAppeal)
                    flt_appeal = " AND ed.extExamsVed.IsLoad = 1";

                string query = string.Empty;

                query = string.Format(@"SELECT DISTINCT ed.extExamsVed.Id, '[' + CONVERT(nvarchar, ed.extExamsVed.Number) + '] ' + ed.extExamsVed.ExamName + ' ' + Convert(nvarchar, Day(ed.extExamsVed.Date)) + '/' + Convert(nvarchar, Month(ed.extExamsVed.Date)) + '/' + Convert(nvarchar, Year(ed.extExamsVed.Date)) + 
                (Case When StudyBasisId IS NULL then '' else (case when StudyBasisId = 1 then ' г/б' else ' дог' end) end) + 
                (Case When IsAddVed = 1 then ' дополнительная' + (case when Convert(nvarchar, AddCount) = '1' then '' else '(' + Convert(nvarchar, AddCount) + ')' end) 
                else '' end) as Name, IsLoad  
                FROM ed.extExamsVed WHERE ed.extExamsVed.IsLocked = 1 AND ed.extExamsVed.StudyLevelGroupId IN ({2}) AND ed.extExamsVed.FacultyId = {0} {1} ORDER BY IsLoad, Name ", FacultyId, flt_appeal, Util.BuildStringWithCollection(MainClass.lstStudyLevelGroupId));
                
                DataTable dt = MainClass.Bdc.GetDataSet(query).Tables[0];

                DataRow r = dt.NewRow();
                r[0] = DBNull.Value;
                r[1] = "нет";
                r[2] = false;
                dt.Rows.InsertAt(r, 0);

                cbVed.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    cbVed.Items.Add(new VedClass(dr["Id"].ToString(), dr["Name"].ToString(), (bool)dr["IsLoad"]));
                }                

                cbVed.SelectedIndex = 0; 
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при обновлении списка ведомостей: ", ex);
            }
        }

        private void cb_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            ComboBox combo = (ComboBox)sender;

            string VedText = combo.Items[e.Index].ToString();
            bool isLoad = ((VedClass)combo.Items[e.Index]).IsLoad;

            Color fontColor;

            Graphics g = e.Graphics;

            if (isLoad)           
                fontColor = Color.Red;            
            else            
                fontColor = Color.Black;              
            
            e.Graphics.DrawString(VedText, new Font("Microsoft San Serif", 8, FontStyle.Regular), new SolidBrush(fontColor), e.Bounds);
            e.DrawFocusRectangle();

            //e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);
            //e.Graphics.DrawString(VedText, combo.Font, new SolidBrush(combo.ForeColor), e.Bounds);
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            string text = cbVed.SelectedItem.ToString();
            string vedId = ((VedClass)cbVed.SelectedItem).VedId;

            if (vedId != "" && vedId != "0" && vedId != null)
            {
                Guid exVedId = new Guid(vedId);
                
                if (_forAppeal)
                    new AppealMarks(exVedId).ShowDialog();
                else
                    new LoadMarks(exVedId).ShowDialog();

                UpdateVedList();
            }
        }        
    }
}
