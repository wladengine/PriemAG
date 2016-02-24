using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priem
{
    public class ChangeAbitClass
    {
        private int _Barcode;
        private string _Priority;       
        private bool _BackDoc;

        public ChangeAbitClass(int Barcode, string Priority, bool BackDoc)
        {
            _Barcode = Barcode;
            _Priority = Priority;
            _BackDoc = BackDoc;
        }

        public int Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }

        public string Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public bool BackDoc
        {
            get { return _BackDoc; }
            set { _BackDoc = value; }
        }        
    }
}
