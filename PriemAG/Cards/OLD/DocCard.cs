using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseFormsLib;
using BDClassLib;

namespace Priem
{
    public partial class DocCard : BaseForm
    {
        private DocsClass _docs;
        private int _personBarc;
        private int? _abitBarc;

        public DocCard(int perBarcode, int? abitBarcode)
        {
            InitializeComponent();
            _personBarc = perBarcode;
            _abitBarc = abitBarcode;
            _docs = new DocsClass(_personBarc, _abitBarc);

            InitControls();
        }

        private void InitControls()
        {
            InitFocusHandlers();

            this.CenterToParent();

            List<KeyValuePair<string, string>> lstFiles = _docs.UpdateFiles();
            if (lstFiles == null || lstFiles.Count == 0)
                return;

            chlbFile.DataSource = new BindingSource(lstFiles, null);
            chlbFile.ValueMember = "Key";
            chlbFile.DisplayMember = "Value";
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstFiles = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> file in chlbFile.CheckedItems)
            {
                lstFiles.Add(file);
            }

            _docs.OpenFile(lstFiles);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DocCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_docs != null)
            {
                _docs.BDCInet.ExecuteQuery(string.Format("UPDATE Person SET DateReviewDocs = '{0}' WHERE Person.Barcode = {1}", DateTime.Now.ToString(), _personBarc));
                if(_abitBarc != null)
                    _docs.BDCInet.ExecuteQuery(string.Format("UPDATE Application SET DateReviewDocs = '{0}' WHERE Application.Barcode = {1}", DateTime.Now.ToString(), _abitBarc));
                
                _docs.CloseDB();
            }
        }
    }
}
