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
    public partial class CardOlympBook : BookCard
    {
        public CardOlympBook()
        {
            InitializeComponent();
            InitControls();
        }

        public CardOlympBook(string olId)
        {
            InitializeComponent();
            _Id = olId;                  

            InitControls();
        }

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

        protected override void ExtraInit()
        {
            base.ExtraInit();
            
            _title = "Олимпиада";
            _tableName = "ed.OlympBook";
            this.MdiParent = null;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbOlympType, HelpClass.GetComboListByTable("ed.OlympType", "ORDER BY Id "), false, false);
                    ComboServ.FillCombo(cbOlympName, HelpClass.GetComboListByTable("ed.OlympName", "ORDER BY Number, Name"), false, false);
                    ComboServ.FillCombo(cbOlympSubject, HelpClass.GetComboListByTable("ed.OlympSubject", "ORDER BY Name"), false, false);
                    ComboServ.FillCombo(cbOlympLevel, HelpClass.GetComboListByTable("ed.OlympLevel", "ORDER BY Name"), false, false);                               
                }                
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }
        }

        protected override void FillCard()
        {
            if (_Id == null)
                return;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    OlympBook olymp = (from ec in context.OlympBook
                                       where ec.Id == GuidId
                                       select ec).FirstOrDefault();

                    if (olymp == null)
                        return;                    

                    OlympTypeId = olymp.OlympTypeId;
                    OlympNameId = olymp.OlympNameId;
                    OlympSubjectId = olymp.OlympSubjectId;
                    OlympLevelId = olymp.OlympLevelId;                   
                }              
            }
            catch (DataException de)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + de.Message);
            }
        }

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();

            if (!MainClass.IsEntryChanger())
                btnSaveChange.Enabled = false;
        }        

        protected override void InsertRec(PriemEntities context, System.Data.Objects.ObjectParameter idParam)
        {
            context.OlympBook_Insert(OlympTypeId, OlympNameId, OlympSubjectId, OlympLevelId, idParam);
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            return;
        }

        protected override void OnSave()
        {           
            MainClass.DataRefresh();            
        }

        protected override void CloseCardAfterSave()
        {
            if (!_isModified)
                this.Close();
        }
    }    
}
