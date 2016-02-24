using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriemLib;
namespace Priem
{
    public partial class Decriptor : Form
    {
        public Decriptor()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbVedNum_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyValue == 189)
            {
                if (!tbVedNum.Text.Contains("=="))
                    return;

                int vedNum = 0;
                string t = tbVedNum.Text.Trim();
                t = t.Substring(0, t.IndexOf("=="));
                int.TryParse(t, out vedNum);

                string num = tbVedNum.Text.Trim();
                num = num.Substring(num.IndexOf("==") + 2);

                DataSet ds = MainClass.Bdc.GetDataSet(string.Format("SELECT ed.extExamsVed.Id, ed.extExamsVed.IsLoad FROM ed.extExamsVed WHERE ed.extExamsVed.Number = {0}", vedNum));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    tbPersonName.Text = "Ведомость не найдена";
                    return;
                }

                DataRow dr = ds.Tables[0].Rows[0];
                if ((bool)dr["IsLoad"])
                {
                    DataSet dsPerson = MainClass.Bdc.GetDataSet(string.Format("SELECT DISTINCT ed.ExamsVedHistory.PersonId, ed.ExamsVedHistory.PersonVedNumber, " +
                                   "ed.extPersonAspirant.PersonNum as Ид_номер, ed.extPersonAspirant.FIO AS FIO " +
                                   "FROM ed.ExamsVedHistory INNER JOIN ed.extPersonAspirant ON ed.ExamsVedHistory.PersonId = ed.extPersonAspirant.Id WHERE ed.ExamsVedHistory.PersonVedNumber = {0} AND ed.ExamsVedHistory.ExamsVedId = '{1}'", num, dr["Id"].ToString()));

                    if (dsPerson.Tables[0].Rows.Count == 0)
                        tbPersonName.Text = "";
                    else
                    {
                        DataRow drr = dsPerson.Tables[0].Rows[0];
                        tbPersonName.Text = drr["FIO"].ToString() + " " + drr["Ид_номер"].ToString() + "\r\n" + drr["PersonVedNumber"].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("Данная ведомость еще не загружена!");
                }               
            }

        }
    }
}
