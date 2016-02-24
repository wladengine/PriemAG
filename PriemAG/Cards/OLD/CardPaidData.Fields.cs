using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EducServLib;

namespace Priem
{
    public partial class CardPaidData
    {
        public string DogovorNum
        {
            get { return tbDogovorNum.Text.Trim(); }
            set { tbDogovorNum.Text = value; }
        }

        public DateTime? DogovorDate
        {
            get { return dtDogovorDate.Value.Date; }
            set 
            {
                if (value.HasValue)
                    dtDogovorDate.Value = value.Value;                 
            }
        }

        public int? DogovorTypeId
        {
            get { return ComboServ.GetComboIdInt(cbDogovorType); }
            set { ComboServ.SetComboId(cbDogovorType, value); }            
        } 

        public int? ProrektorId
        {
            get { return ComboServ.GetComboIdInt(cbProrektor); }
            set { ComboServ.SetComboId(cbProrektor, value); } 
        }

        public string Qualification
        {
            // get { return tbQualification.Text.Trim(); }
            //set { tbQualification.Text = value; }

            get { return cbQualification.SelectedItem.ToString(); }
            set { cbQualification.SelectedItem = value; }
        }

        public string Srok
        {
            get { return tbSrok.Text.Trim(); }
            set { tbSrok.Text = value; }
        }
        public string SrokIndividual
        {
            get { return tbSrokIndividual.Text.Trim(); }
            set { tbSrokIndividual.Text = value; }
        }

        public DateTime? DateStart
        {
            get { return dtDateStart.Value.Date; }
            set 
            {
                if (value.HasValue)
                    dtDateStart.Value = value.Value;                 
            }
        }

        public DateTime? DateFinish
        {
            get { return dtDateFinish.Value.Date; }
            set 
            {
                if (value.HasValue)
                    dtDateFinish.Value = value.Value;                
            }
        }
        public string SumTotal
        {
            get { return tbSumTotal.Text.Trim(); }
            set { tbSumTotal.Text = value; }
        }
        public string SumFirstYear
        {
            get { return tbSumFirstYear.Text.Trim(); }
            set { tbSumFirstYear.Text = value; }
        }

        public int? PayPeriodId
        {
            get { return ComboServ.GetComboIdInt(cbPayPeriod); }
            set { ComboServ.SetComboId(cbPayPeriod, value); }              
        }         

        public string SumFirstPeriod
        {
            get { return tbSumFirstPeriod.Text.Trim(); }
            set { tbSumFirstPeriod.Text = value; }
        }

        public string AbitParent
        {
            get { return tbParent.Text.Trim(); }
            set { tbParent.Text = value; }
        }

        public string AbitFIORod
        {
            get { return tbAbitFIORod.Text.Trim(); }
            set { tbAbitFIORod.Text = value; }
        }

        public string Customer
        {
            get { return tbCustomer.Text.Trim(); }
            set { tbCustomer.Text = value; }
        }

        public string CustomerLico
        {
            get { return tbCustomerLico.Text.Trim(); }
            set { tbCustomerLico.Text = value; }
        }

        public string CustomerAddress
        {
            get { return tbCustomerAddress.Text.Trim(); }
            set { tbCustomerAddress.Text = value; }
        }

        public string CustomerPassport
        {
            get { return tbCustomerPassport.Text.Trim(); }
            set { tbCustomerPassport.Text = value; }
        }

        public string CustomerPassportAuthor
        {
            get { return tbCustomerPassportAuthor.Text.Trim(); }
            set { tbCustomerPassportAuthor.Text = value; }
        }

        public string CustomerReason
        {
            get { return tbCustomerReason.Text.Trim(); }
            set { tbCustomerReason.Text = value; }
        }

        public string CustomerINN
        {
            get { return tbCustomerINN.Text.Trim(); }
            set { tbCustomerINN.Text = value; }
        }

        public string CustomerRS
        {
            get { return tbCustomerRS.Text.Trim(); }
            set { tbCustomerRS.Text = value; }
        }        
    }
}