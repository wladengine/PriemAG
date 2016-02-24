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
    public class EnableProtocolList : ProtocolList
    {
        //constructor
        public EnableProtocolList()
            : base(ProtocolTypes.EnableProtocol)
        {

        }

        protected override void InitControls()
        {
            base.InitControls();

            cbPrint.Items.Add("о допуске");
            this.Text = "Протокол о допуске к вступительным экзаменам";
            cbPrint.SelectedIndex = 0;
        }

        protected override void InitGrid()
        {
            base.InitGrid();

            dgvProtocols.Columns["Sum"].Visible = false;
        }

        //печать протокола о допуске
        protected override void PrintProtocol()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = cbProtocolNum.Text + " - Протокол о допуске (" + ProtocolDate.ToShortDateString() + ")";
            sfd.Filter = "ADOBE Pdf files|*.pdf";
            if (sfd.ShowDialog() == DialogResult.OK)
                PriemLib.Print.PrintEnableProtocol(ComboServ.GetComboId(cbProtocolNum), false, sfd.FileName);
        }
    }
}
