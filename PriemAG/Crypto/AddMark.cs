using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Priem
{
    public partial class AddMark : Form
    {
        private EnterMarks _owner;
        private string _number;

        public AddMark(EnterMarks owner, string number)
        {
            InitializeComponent();
            _owner = owner;
            _number = number;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetMark();
        }

        private void tbMark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)            
                SetMark();
            
        }

        private void SetMark()
        {
            DataGridView dgv = _owner.dgvMarks;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["Номер"].Value.ToString() == _number)
                    {
                        dgv["Баллы", i].Value = tbMark.Text;
                        break;
                    }
                }
                this.Close();
        }
    }
}
