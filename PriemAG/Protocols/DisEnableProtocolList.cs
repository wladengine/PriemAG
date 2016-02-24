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
    public class DisEnableProtocolList : ProtocolList
    {
        //constructor
        public DisEnableProtocolList()
            : base(ProtocolTypes.DisEnableProtocol)
        {

        }

        protected override void InitControls()
        {
            base.InitControls();

            cbPrint.Items.Add("об исключении из допуска");
            this.Text = "Протокол об исключении из протокола о допуске к вступительным экзаменам";
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
            sfd.Filter = "ADOBE Pdf files|*.pdf";
            if (sfd.ShowDialog() == DialogResult.OK)
                PriemLib.Print.PrintDisEnableProtocol(ComboServ.GetComboId(cbProtocolNum), false, sfd.FileName);
        }
    }
}
