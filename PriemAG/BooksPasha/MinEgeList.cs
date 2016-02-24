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
    public partial class MinEgeList : SimpleBook
    {
        public MinEgeList()
        {
            InitializeComponent();
            Title = "Минимальные планки";
            this.Text = Title + BaseFormsLib.Constants.CARD_READ_ONLY;
            this.Width = 800;
        }

        protected override void SetReadOnly(bool val)
        {
            if (val)
                dgv.ReadOnly = true;
            else
            {
                dgv.ReadOnly = false;
                dgv.Columns["Факультет"].ReadOnly = true;
                dgv.Columns["Код"].ReadOnly = true;               
                dgv.Columns["Направление"].ReadOnly = true;
                dgv.Columns["Образовательная_программа"].ReadOnly = true; 
                dgv.Columns["Профиль"].ReadOnly = true;
                dgv.Columns["Форма обучения"].ReadOnly = true;
                dgv.Columns["Основа обучения"].ReadOnly = true;
                dgv.Columns["Экзамен"].ReadOnly = true;
            }
            dgv.AllowUserToAddRows =
                dgv.AllowUserToDeleteRows = false;

            return;
        }

        protected override DataTable GetSource()
        {
            return _bdc.GetMinEge();
        }

        protected override void UpdateSource(DataTable table)
        {
            _bdc.SetMinEge(table);
        }

        protected override void BindGrid(DataTable table)
        {
            dataTable = table;
            dgv.DataSource = dataTable;
            dgv.Columns["Id"].Visible = false;           

            dgv.Update();
        }
    }
}
