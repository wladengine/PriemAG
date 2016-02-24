using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Priem
{
    public partial class EgeExamList : SimpleBook
    {
        public EgeExamList()
        {
            InitializeComponent();
            Title = "ЕГЭ";
            this.Text = Title + BaseFormsLib.Constants.CARD_READ_ONLY;
        }

        protected override DataTable GetSource()
        {
            return _bdc.GetEgeExam();
        }

        protected override void UpdateSource(DataTable table)
        {
            _bdc.SetEgeExam(table);
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
