using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class CardOrderNumbers : BaseForm
    {
        private DBPriem _bdc;
        private bool forUpdate;
       
        //конструктор
        public CardOrderNumbers()
        {
            InitializeComponent();  

            this.CenterToParent();
            this.MdiParent = MainClass.mainform;
           
            InitControls();            
        }

        //дополнительная инициализация контролов
        private void InitControls()
        {
            InitFocusHandlers();
            _bdc = MainClass.Bdc;
            forUpdate = false;

            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, false);
            ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name"), false, false);
            ComboServ.FillCombo(cbSigner, HelpClass.GetComboListByTable("ed.Signer", "ORDER BY Name"), false, false);

            cbStudyBasis.SelectedIndex = 0;
            FillStudyForm();
            FillLicenseProgram();

            UpdateDataGrid();

            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged); 
           
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }

        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }

        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }
        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }
        public int? StudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm); }
            set { ComboServ.SetComboId(cbStudyForm, value); }
        }

        public int? SignerId
        {
            get { return ComboServ.GetComboIdInt(cbSigner); }
            set { ComboServ.SetComboId(cbSigner, value); }
        }
        public string ComissionNumber
        {
            get { return tbComissionNumber.Text.Trim(); }
            set { tbComissionNumber.Text = value; }
        }
        public DateTime? ComissionDate
        {
            get 
            { 
                return dtpComissionDate.Value.Date; 
            }
            set 
            {
                if (value.HasValue)
                {
                    try
                    {
                        dtpComissionDate.Value = value.Value;
                    }
                    catch { }
                }
            }
        }

        public bool IsSecond
        {
            get { return chbIsSecond.Checked; }
            set { chbIsSecond.Checked = value; }
        }
        public bool IsReduced
        {
            get { return chbIsReduced.Checked; }
            set { chbIsReduced.Checked = value; }
        }
        public bool IsParallel
        {
            get { return chbIsParallel.Checked; }
            set { chbIsParallel.Checked = value; }
        }
        public bool IsListener
        {
            get { return chbIsListener.Checked; }
            set { chbIsListener.Checked = value; }
        }

        private void FillStudyForm()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = MainClass.GetEntry(context).Where(c => c.FacultyId == FacultyId).Where(c => c.StudyBasisId == StudyBasisId);

                ent = ent.Where(c => c.IsSecond == IsSecond && c.IsReduced == IsReduced && c.IsParallel == IsParallel);

                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.StudyFormId.ToString(), u.StudyFormName)).Distinct().ToList();

                ComboServ.FillCombo(cbStudyForm, lst, false, false);
            }
        }

        private void FillSigner()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var lst = context.Signer.Select(x => new { x.Id, x.Name }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();
                ComboServ.FillCombo(cbSigner, lst, false, false);
            }
        }

        private void FillLicenseProgram()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var ent = MainClass.GetEntry(context).Where(c => c.FacultyId == FacultyId);

                ent = ent.Where(c => c.IsSecond == IsSecond && c.IsReduced == IsReduced && c.IsParallel == IsParallel);

                if (StudyBasisId != null)
                    ent = ent.Where(c => c.StudyBasisId == StudyBasisId);
                if (StudyFormId != null)
                    ent = ent.Where(c => c.StudyFormId == StudyFormId);

                List<KeyValuePair<string, string>> lst = ent.ToList().Select(u => new KeyValuePair<string, string>(u.LicenseProgramId.ToString(), u.LicenseProgramCode + " " + u.LicenseProgramName)).Distinct().ToList();

                ComboServ.FillCombo(cbLicenseProgram, lst, false, true);
            }
        }        

        private void UpdateDataGrid()
        {
            gbOrders.Visible = true;
            gbOrdersFor.Visible = true;

            if (StudyFormId == null || StudyBasisId == null)
            {
                dgvViews.DataSource = null;
                gbOrders.Visible = false;
                gbOrdersFor.Visible = false;
                return;
            }

            string query = string.Format(@"SELECT DISTINCT Id, Number as 'Номер представления' FROM ed.extEntryView 
WHERE StudyFormId={0} AND StudyBasisId={1} AND FacultyId= {2} {3} AND IsListener = {4} AND IsSecond = {5} AND IsReduced = {6} AND IsParallel = {7} order by 2", 
StudyFormId, StudyBasisId, FacultyId, LicenseProgramId.HasValue ? "AND LicenseProgramId = " + LicenseProgramId : "", 
QueryServ.StringParseFromBool(IsListener), QueryServ.StringParseFromBool(IsSecond), QueryServ.StringParseFromBool(IsReduced), QueryServ.StringParseFromBool(IsParallel));
            HelpClass.FillDataGrid(dgvViews, _bdc, query, "");  

            dgvViews.Columns["Номер представления"].Width = 149;
            dgvViews.Update();

            if (dgvViews.RowCount == 0)
            {
                gbOrders.Visible = false;
                gbOrdersFor.Visible = false;
            }           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvViews_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex < 0)
            {
                gbOrders.Visible = false;
                gbOrdersFor.Visible = false;                
                return;
            }
            else
            {
                gbOrders.Visible = true;
                gbOrdersFor.Visible = true;
                tbOrderNum.Text = "";
                tbOrderNumFor.Text = "";
                dtOrderDate.Value = DateTime.Now;
                dtOrderDateFor.Value = DateTime.Now;
                
                string protId = dgvViews.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                FillOrders(protId);
            }
        }

        private void FillOrders(string protId)
        {
            DataSet ds = _bdc.GetDataSet(string.Format(@"SELECT ed.OrderNumbers.OrderDateFor, ed.OrderNumbers.OrderNumFor, ed.OrderNumbers.OrderDate, ed.OrderNumbers.OrderNum, SignerId, ComissionDate, ComissionNumber 
                                                        FROM ed.OrderNumbers WHERE ed.OrderNumbers.ProtocolId = '{0}'", protId));
                       
            if (ds.Tables[0].Rows.Count == 0)
            {
                forUpdate = false;
                DeleteReadOnly();                
            }                
            else
            {
                forUpdate = true;

                DataRow rw = ds.Tables[0].Rows[0];
                if ((rw["OrderDate"].ToString()).Length > 0)
                    dtOrderDate.Value = DateTime.Parse(rw["OrderDate"].ToString());
                if ((rw["OrderDateFor"].ToString()).Length > 0)
                    dtOrderDateFor.Value = DateTime.Parse(rw["OrderDateFor"].ToString());
                if ((rw["ComissionDate"].ToString()).Length > 0)
                    dtpComissionDate.Value = DateTime.Parse(rw["ComissionDate"].ToString());

                int tmp = 0;
                if (int.TryParse(rw["SignerId"].ToString(), out tmp))
                    SignerId = tmp;
                
                tbOrderNum.Text = rw["OrderNum"].ToString();
                tbOrderNumFor.Text = rw["OrderNumFor"].ToString();
                tbComissionNumber.Text = rw["ComissionNumber"].ToString();


                SetReadOnly();
            } 
        }

        private void SetReadOnly()
        {
            dtOrderDate.Enabled = false;
            dtOrderDateFor.Enabled = false;
            tbOrderNum.Enabled = false;
            tbOrderNumFor.Enabled = false;
            btnChange.Enabled = true;
            btnSave.Enabled = false;
        }
        private void DeleteReadOnly()
        {
            dtOrderDate.Enabled = true;
            dtOrderDateFor.Enabled = true;
            tbOrderNum.Enabled = true;
            tbOrderNumFor.Enabled = true;
            btnChange.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DeleteReadOnly();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MainClass.IsPasha())
            {
                using (PriemEntities context = new PriemEntities())
                {
                    Guid? protId = new Guid(dgvViews.CurrentRow.Cells["Id"].Value.ToString());

                    if (forUpdate)                   
                        context.OrderNumbers_Update(protId, dtOrderDate.Value.Date, tbOrderNum.Text.Trim(), dtOrderDateFor.Value.Date, tbOrderNumFor.Text.Trim(), SignerId, ComissionDate, ComissionNumber);                    
                    else
                        context.OrderNumbers_Insert(protId, dtOrderDate.Value.Date, tbOrderNum.Text.Trim(), dtOrderDateFor.Value.Date, tbOrderNumFor.Text.Trim(), SignerId, ComissionDate, ComissionNumber);
                   
                    forUpdate = true;
                    SetReadOnly();
                }
            }
        }

        private void chbIsListener_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }
        private void chbIsSecond_CheckedChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }
        private void chbIsReduced_CheckedChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }
        private void chbIsParallel_CheckedChanged(object sender, EventArgs e)
        {
            FillStudyForm();
        }
    }
}