using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseFormsLib;

namespace Priem
{
    public partial class CardFromList : BookCard
    {      
        protected TabControl tcCard;       
        protected BaseFormEx formOwner;
        protected int ownerRowIndex = 0;
        
        public CardFromList()
            : base()
        {
            InitializeComponent();  
        }

        public CardFromList(string id)
            : base(id)
        {
            InitializeComponent();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();

            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            
            if (formOwner != null)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }                             
        }

        protected override void InitFocusHandlers()
        {
            base.InitFocusHandlers();

            foreach (Control control in tcCard.Controls)
            {
                control.Click += new EventHandler(FocusForm);
                foreach (Control crl in control.Controls)
                    crl.Click += new EventHandler(FocusForm);
            }
        }    

        // карточка открывается в режиме read-only
        protected override void  SetAllFieldsNotEnabled()
        {
            base.SetAllFieldsNotEnabled();
            tcCard.Enabled = true;

            foreach (Control control in tcCard.Controls)
            {
                foreach (Control crl in control.Controls)
                    crl.Enabled = false;
            }

            if (formOwner != null)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }            
        }

        //убрать режим read-only
        protected override void  SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();

            foreach (Control control in tcCard.Controls)
            {
                control.Enabled = true;
                foreach (Control crl in control.Controls)
                    crl.Enabled = true;
            }

            btnPrev.Enabled = false;
            btnNext.Enabled = false;
        }

        protected virtual void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string cardId = formOwner.OpenNextCard(ref ownerRowIndex);
                if (cardId != string.Empty && cardId != null)
                {
                    _Id = cardId;
                    NullHandlers();
                    InitControls();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при загрузке следующей карточки: " + exc.Message + (exc.InnerException == null ? "" : "\nВнутреннее исключение: " + exc.InnerException.Message));
            }
        }

        protected virtual void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                string cardId = formOwner.OpenPrevCard(ref ownerRowIndex);
                if (cardId != string.Empty && cardId != null)
                {
                    _Id = cardId;
                    NullHandlers();
                    InitControls();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при загрузке предыдущей карточки: " + exc.Message + (exc.InnerException == null ? "" : "\nВнутреннее исключение: " + exc.InnerException.Message));
            }
        }
    }
}
