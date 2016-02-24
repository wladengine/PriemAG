using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EducServLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class CardBenefitDocument : CardFromList
    {
        public int BenefitDocumentTypeId
        {
            get { return ComboServ.GetComboIdInt(cbBenefitDocumentType).Value; }
            set { ComboServ.SetComboId(cbBenefitDocumentType, value); }
        }
        public int DisabilityTypeId
        {
            get { return ComboServ.GetComboIdInt(cbDisabilityType).Value; }
            set { ComboServ.SetComboId(cbDisabilityType, value); }
        }
        public string Series
        {
            get { return tbSeries.Text.Trim(); }
            set { tbSeries.Text = value;}
        }
        public string Number
        {
            get { return tbNumber.Text.Trim(); }
            set { tbNumber.Text = value; }
        }
        public string Author
        {
            get { return tbAuthor.Text.Trim(); }
            set { tbAuthor.Text = value; }
        }
        public DateTime? Date
        {
            get 
            {
                if (dtpDate.Checked)
                    return null;
                else
                    return dtpDate.Value;
            }
            set 
            {
                dtpDate.Checked = value.HasValue;
                if (value.HasValue)
                    dtpDate.Value = value.Value;
            }
        }
        public bool HasOriginals
        {
            get { return chbHasOriginals.Checked; }
            set { chbHasOriginals.Checked = value; }
        }

        private Guid PersonId;

        public CardBenefitDocument(string id, Guid _personId, int? rowInd, BaseFormEx formOwner)
        {
            InitializeComponent();
            _Id = id;
            PersonId = _personId;
            this.formOwner = formOwner;
            if(rowInd.HasValue)
                this.ownerRowIndex = rowInd.Value;

            InitControls();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            ComboServ.FillCombo(cbBenefitDocumentType, HelpClass.GetComboListByTable("ed.BenefitDocumentType"), false, false);
            ComboServ.FillCombo(cbDisabilityType, HelpClass.GetComboListByTable("ed.DisabilityType"), false, false);
        }

        protected override void InitFocusHandlers()
        {
            //base.InitFocusHandlers();
        }

        protected override void SetAllFieldsNotEnabled()
        {
            foreach (Control crl in this.Controls)
                crl.Enabled = false;
        }

        protected override void FillCard()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var Person = context.Person.Where(x => x.Id == PersonId).FirstOrDefault();
                lblFIO.Text = Person.Surname + " " + Person.Name + " " + Person.SecondName;

                if (_Id == null)
                    return;

                var value = context.PersonBenefitDocument.Where(x => x.Id == GuidId).FirstOrDefault();
                if (value == null)
                    WinFormsServ.Error("Не удалось загрузить из PersonBenefitDocument");

                BenefitDocumentTypeId = value.BenefitDocumentTypeId;
                Series = value.Series;
                Number = value.Number;
                Date = value.Date;
                Author = value.Author;
                HasOriginals = value.HasOriginals;

                //если инвалид, то вводить группу инвалидности
                if (value.DisabilityTypeId.HasValue)
                    DisabilityTypeId = value.DisabilityTypeId.Value;
            }
        }

        protected override void InsertRec(PriemEntities context, System.Data.Objects.ObjectParameter idParam)
        {
            context.PersonBenefitDocument_insert(PersonId, BenefitDocumentTypeId, Series, Number, Date, Author, HasOriginals, DisabilityTypeId, idParam);
            _Id = idParam.Value.ToString();
        }
        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            context.PersonBenefitDocument_update(BenefitDocumentTypeId, Series, Number, Date, Author, HasOriginals, DisabilityTypeId, GuidId);
        }

        private void cbBenefitDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDisabilityType.Visible = (BenefitDocumentTypeId == 1);
            lblDisabilityType.Visible = (BenefitDocumentTypeId == 1);
        }
    }
}
