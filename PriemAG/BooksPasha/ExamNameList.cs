using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using BaseFormsLib;
using EducServLib;

namespace Priem
{
    public partial class ExamNameList : SimpleBook
    {
        public ExamNameList()
        {
            InitializeComponent();
            Title = "Экзамены";
            this.Text = Title + BaseFormsLib.Constants.CARD_READ_ONLY;

            //btnSave.Enabled = false;
        }

        protected override DataTable GetSource()
        {
            return _bdc.GetExamName();
        }

        protected override void UpdateSource(DataTable table)
        {
            _bdc.SetExamName(table);
        }

        protected override void BindGrid(DataTable table)
        {
            dataTable = table;
            dgv.DataSource = table;
            dgv.Columns["Id"].Visible = false;
            dgv.Update();
        }
    }
}
