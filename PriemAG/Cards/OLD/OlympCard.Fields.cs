using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EducServLib;

namespace Priem
{
    public partial class OlympCard
    {
        public int? OlympTypeId
        {            
            get { return ComboServ.GetComboIdInt(cbOlympType); }
            set { ComboServ.SetComboId(cbOlympType, value); }
        }

        public int? OlympNameId
        {
            get { return ComboServ.GetComboIdInt(cbOlympName); }
            set { ComboServ.SetComboId(cbOlympName, value); }     
        }

        public int? OlympSubjectId
        {
            get { return ComboServ.GetComboIdInt(cbOlympSubject); }
            set { ComboServ.SetComboId(cbOlympSubject, value); }            
        }

        public int? OlympLevelId
        {
            get { return ComboServ.GetComboIdInt(cbOlympLevel); }
            set { ComboServ.SetComboId(cbOlympLevel, value); }   
        }

        public int? OlympValueId
        {
            get { return ComboServ.GetComboIdInt(cbOlympValue); }
            set { ComboServ.SetComboId(cbOlympValue, value); }            
        }

        public bool OriginDoc
        {
            get { return chbOriginDoc.Checked; }
            set { chbOriginDoc.Checked = value; }
        }

        public string DocumentSeries
        {
            get { return tbSeries.Text.Trim(); }
            set { tbSeries.Text = value; }
        }

        public string DocumentNumber
        {
            get { return tbNumber.Text.Trim(); }
            set { tbNumber.Text = value; }
        }

        public DateTime? DocumentDate
        {
            get 
            {
                if (dtpDate.Checked)
                    return dtpDate.Value;
                else
                    return null;
            }
            set 
            {
                dtpDate.Checked = value.HasValue;
                if (value.HasValue)
                    dtpDate.Value = value.Value;
            }
        }
    }
}
