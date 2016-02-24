using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priem
{
    public partial class EgeCard
    {        
        public string Number
        {
            get { return tbNumber.Text.Trim(); }
            set { tbNumber.Text = value; }
        }

        public string PrintNumber
        {
            get { return tbPrintNumber.Enabled ? tbPrintNumber.Text.Trim() : ""; }
            set { tbPrintNumber.Text = value; }
        }

        public string Year
        {
            get { return tbYear.Text.Trim(); }
            set { tbYear.Text = value; }
        }

        public string NewFIO
        {
            get { return chbNewFIO.Checked ? tbSurname.Text.Trim() + "%" + tbName.Text.Trim() + "%" + tbSecondName.Text.Trim() : ""; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    chbNewFIO.Checked = true;
                    char[] sep = { '%' };
                    string[] FIO = value.Split(sep, 3);
                    tbSurname.Text = FIO[0];
                    tbName.Text = FIO[1];
                    tbSecondName.Text = FIO[2];
                }
            }
        }

        public string FBSStatus
        {
            get { return tbFBSStatus.Text.Trim(); }
            set { tbFBSStatus.Text = value; }
        }

        public string FBSComment
        {
            get { return tbFBSComment.Text.Trim(); }
            set { tbFBSComment.Text = value; }
        }

        public bool IsImported
        {
            get { return lblIsImported.Visible; }
            set { lblIsImported.Visible = value; }
        }

        public bool NoNumber
        {
            get { /*return chbNoNumber.Checked;*/ return false; }
            set { /*chbNoNumber.Checked = value;*/ }
        }
    }
}
