using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EducServLib;
using BDClassLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class CardPaidData : BookCard
    {
        private Guid? _abitId;//айди абитуриента  
        private bool _isReadOnly;
        List<string> QualificationList = new List<string>();
       
        public CardPaidData(Guid? abitId, bool isReadOnly)
        {
            InitializeComponent();
           
            _abitId = abitId;
            _isReadOnly = isReadOnly;

            if (_abitId == null)
                this.Close();          
           
            // аспирантура
            QualificationList = new List<string> { { "диплом" } };
            InitControls();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
          
            _title = "Данные по договору";
            _tableName = "ed.PaidData";
            this.MdiParent = null;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    _Id = (from pd in context.PaidData
                           where pd.AbiturientId == _abitId
                           orderby pd.DogovorDate descending
                           select pd.Id).FirstOrDefault().ToString();
                    
                    if (string.IsNullOrEmpty(_Id) || _Id == Guid.Empty.ToString())
                        _Id = null;

                    ComboServ.FillCombo(cbDogovorType, HelpClass.GetComboListByTable("ed.DogovorType", "ORDER BY Id"), false, false);
                    UpdateAfterDogovorType();
                    ComboServ.FillCombo(cbProrektor, HelpClass.GetComboListByTable("ed.Prorektor"), false, false);
                    ComboServ.FillCombo(cbPayPeriod, HelpClass.GetComboListByTable("ed.PayPeriod"), false, false);
                    ComboServ.FillCombo(cbQualification, QualificationList, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }    
        }

        private string CreateDogNum()
        {
            using (PriemEntities context = new PriemEntities())
            {
                string curYear = DateTime.Now.Year.ToString();
                string dogNum = (from ab in context.qAbitAll
                                 join sl in context.StudyLevel
                                 on ab.StudyLevelId equals sl.Id
                                 where ab.Id == _abitId
                                 select sl.Acronym + "-" + ab.RegNum + "-" + curYear).FirstOrDefault();
                return dogNum;
            }
        }

        protected override void FillCard()
        {  
            if (_Id == null)
            {
                FillPaidData();

                btnPrint.Enabled = false;
                dtDogovorDate.Value = DateTime.Now.Date;
                tbDogovorNum.Text = CreateDogNum();
                tbDogovorNum.Enabled = false;
                return;
            }
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    PaidData paiddata = (from ec in context.PaidData
                                         where ec.Id == GuidId
                                         select ec).FirstOrDefault();

                    if (paiddata == null)
                        return;

                    DogovorNum = paiddata.DogovorNum;
                    DogovorDate = paiddata.DogovorDate;
                    DogovorTypeId = paiddata.DogovorTypeId;
                    //Qualification = paiddata.Qualification;
                    if (QualificationList.Contains(paiddata.Qualification))
                        Qualification = paiddata.Qualification;
                    else
                        Qualification = QualificationList.First();
                    Srok = paiddata.Srok;
                    SrokIndividual = paiddata.SrokIndividual;
                    DateStart = paiddata.DateStart;
                    DateFinish = paiddata.DateFinish;
                    ProrektorId = paiddata.ProrektorId;
                    SumFirstYear = paiddata.SumFirstYear;
                    PayPeriodId = paiddata.PayPeriodId;
                    SumTotal = paiddata.SumTotal;
                    SumFirstPeriod = paiddata.SumFirstPeriod;
                    AbitParent = paiddata.Parent;
                    AbitFIORod = paiddata.AbitFIORod;
                    Customer = paiddata.Customer;
                    CustomerLico = paiddata.CustomerLico;
                    CustomerAddress = paiddata.CustomerAddress;
                    CustomerPassport = paiddata.CustomerPassport;
                    CustomerPassportAuthor = paiddata.CustomerPassportAuthor;
                    CustomerReason = paiddata.CustomerReason;
                    CustomerINN = paiddata.CustomerINN;
                    CustomerRS = paiddata.CustomerRS;
                }
            }
            catch (DataException de)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + de.Message);
            }            
        }

        public void FillPaidData()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    PayDataEntry payData = (from ab in context.qAbiturient
                                            join pde in context.PayDataEntry
                                            on ab.EntryId equals pde.EntryId
                                            where ab.Id == _abitId
                                            select pde).FirstOrDefault();

                    if (payData == null)
                        return;
                    
                    //Qualification = payData.Qualification;
                    if (QualificationList.Contains(payData.Qualification))
                        Qualification = payData.Qualification;
                    else
                        Qualification = QualificationList.First();
                    Srok = payData.EducPeriod;
                    DateStart = payData.DateStart;
                    DateFinish = payData.DateFinish;
                    ProrektorId = payData.ProrektorId;                   
                }
            }
            catch (DataException de)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + de.Message);
            }
        }

        #region ReadOnly

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();            

            if (_isReadOnly)
            {
                ReadOnlyCard();
                btnSaveChange.Enabled = false;
            }          
        }

        protected override void SetAllFieldsNotEnabled()
        {
            base.SetAllFieldsNotEnabled(); 

            if (_Id != null)
                btnPrint.Enabled = true;              
            
        }

        protected override void SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();
            tbDogovorNum.Enabled = false;           
        }
        #endregion

        //инициализация обработчиков мегакомбов
        protected override void InitHandlers()
        {
            cbDogovorType.SelectedIndexChanged += new EventHandler(cbDogovorType_SelectedIndexChanged);                   
        }

        void cbDogovorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAfterDogovorType();
        }

        private void UpdateAfterDogovorType()
        {
            switch (DogovorTypeId)
            {
                case 1:
                    {
                        tpSponsor.Parent = null;
                        break;
                    }
                case 2:
                    {
                        tpSponsor.Parent = tcDogovor;
                        gbType2.Visible = true;
                        gbType3.Visible = false;
                        break;
                    }
                case 3:
                    {
                        tpSponsor.Parent = tcDogovor;
                        gbType2.Visible = false;
                        gbType3.Visible = true;
                        break;
                    }
                case 4:
                    {
                        tpSponsor.Parent = tcDogovor;
                        gbType2.Visible = true;
                        gbType3.Visible = false;
                        break;
                    }
            }
        }

        private bool CheckFields()
        {
            SetFielsOfDogovorType();
            
            //if (DogovorDate > DateTime.Now)
            //{
            //    epError.SetError(dtDogovorDate, "Неправильная дата");
            //    tcDogovor.SelectedIndex = 0;
            //    return false;
            //}
            //else
            //    epError.Clear();
          
            return true;
        }

        private void SetFielsOfDogovorType()
        {            
            switch (DogovorTypeId)
            {
                case 1:
                    {
                        Customer = string.Empty;                        
                        CustomerAddress = string.Empty;
                        CustomerPassport = string.Empty;
                        CustomerPassportAuthor = string.Empty;
                        CustomerLico = string.Empty;
                        CustomerReason = string.Empty;
                        CustomerINN = string.Empty;
                        CustomerRS = string.Empty;
                        
                        break;
                    }
                case 2:
                    {                        
                        CustomerLico = string.Empty;
                        CustomerReason = string.Empty;
                        CustomerINN = string.Empty;
                        CustomerRS = string.Empty;

                        break;
                    }
                case 3:
                    {
                        CustomerPassport = string.Empty;
                        CustomerPassportAuthor = string.Empty;

                        break;
                    }
            }
        }

        protected override void InsertRec(PriemEntities context, System.Data.Objects.ObjectParameter idParam)
        {
            context.PaidData_Insert(DogovorNum, DogovorDate, DogovorTypeId, ProrektorId, Qualification, Srok, 
                DateStart, DateFinish, SumFirstYear, PayPeriodId, SumFirstPeriod, AbitFIORod, AbitParent, null, 
                Customer, CustomerAddress, CustomerPassport, CustomerPassportAuthor, CustomerLico, CustomerReason, 
                CustomerINN, CustomerRS, _abitId, SumTotal, SrokIndividual, idParam);
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            context.PaidData_Update(DogovorNum, DogovorDate, DogovorTypeId, ProrektorId, Qualification, Srok,
                DateStart, DateFinish, SumFirstYear, PayPeriodId, SumFirstPeriod, AbitFIORod, AbitParent, null,
                Customer, CustomerAddress, CustomerPassport, CustomerPassportAuthor, CustomerLico, CustomerReason,
                CustomerINN, CustomerRS, SumTotal, SrokIndividual, id);
        }

        protected override void OnSaveNew()
        {            
            btnPrint.Enabled = true;
            FillCard();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_Id == null)
                return;
            if (_abitId == null)
                return;
            Guid dogovorId;
            if (!Guid.TryParse(_Id, out dogovorId))
                return;

            Print.PrintDogovor(dogovorId, _abitId.Value, chbPrint.Checked);
        }  
    }
}
