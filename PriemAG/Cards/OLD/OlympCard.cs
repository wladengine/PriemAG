using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using EducServLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class OlympCard : BookCard
    {
        private Guid? _abitId;
        private bool _isReadOnly;    
        
        // конструктор пустой формы
        public OlympCard(Guid? abitId)
            : this(null, abitId, false)
        {
        }  

        // конструктор формы для изменения
        public OlympCard(string olId, Guid? abitId, bool isReadOnly)
        {
            InitializeComponent();                      
            _abitId = abitId;
            _Id = olId;
            _isReadOnly = isReadOnly;           

            InitControls();
        }       

        protected override void ExtraInit()
        {
            base.ExtraInit();
            
            _title = "Диплом олимпиады";
            _tableName = "ed.Olympiads";
            this.MdiParent = null;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbOlympType, HelpClass.GetComboListByTable("ed.OlympType", "ORDER BY Id"), false, false);
                    UpdateAfterType();
                    ComboServ.FillCombo(cbOlympValue, HelpClass.GetComboListByTable("ed.OlympValue"), false, false);                    
                }                
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }
        }

        protected override void InitHandlers()
        {
            cbOlympType.SelectedIndexChanged += new EventHandler(cbOlympType_SelectedIndexChanged);
            cbOlympName.SelectedIndexChanged += new EventHandler(cbOlympName_SelectedIndexChanged);
            cbOlympSubject.SelectedIndexChanged += new EventHandler(cbOlympSubject_SelectedIndexChanged);
        }

        void cbOlympSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAfterOlympSubject();
        }

        void cbOlympName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAfterOlympName();
        }

        void cbOlympType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAfterType();
        }

        private void UpdateAfterType()
        {
            if (OlympTypeId == 1 || OlympTypeId == 2)
            {
                ComboServ.FillCombo(cbOlympName, new List<KeyValuePair<string, string>>(), true, false);
                cbOlympName.SelectedIndex = 0;
                cbOlympName.Enabled = false;
                                
                ComboServ.FillCombo(cbOlympSubject, HelpClass.GetComboListByTable("ed.OlympSubject"), false, false);
                cbOlympSubject.SelectedIndex = 0;
                cbOlympSubject.Enabled = true;

                ComboServ.FillCombo(cbOlympLevel, new List<KeyValuePair<string, string>>(), true, false); 
                cbOlympLevel.SelectedIndex = 0;
                cbOlympLevel.Enabled = false;
            }
            else
            {   
                using(PriemEntities context = new PriemEntities())
                {                    
                    List<KeyValuePair<string, string>> lst = ((from ob in context.extOlympBook
                                                             where ob.OlympTypeId == OlympTypeId
                                                             select new
                                                             {
                                                                 Id = ob.OlympNameId,
                                                                 Name = ob.OlympNameName
                                                             }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    cbOlympName.Enabled = true;
                    ComboServ.FillCombo(cbOlympName, lst, false, false);
                    cbOlympName.SelectedIndex = 0;

                    FillAfterOlympName();
                    FillAfterOlympSubject();
                }
            }
        }

        private void FillAfterOlympName()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<KeyValuePair<string, string>> lst = ((from ob in context.extOlympBook
                        where ob.OlympTypeId == OlympTypeId && ob.OlympNameId == OlympNameId
                        select new
                        {
                            Id = ob.OlympSubjectId,
                            Name = ob.OlympSubjectName
                        }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                cbOlympSubject.Enabled = true;
                ComboServ.FillCombo(cbOlympSubject, lst, false, false);
                cbOlympSubject.SelectedIndex = 0;                
            }
        }
        
        private void FillAfterOlympSubject()
        {
            using (PriemEntities context = new PriemEntities())
            {                
                List<KeyValuePair<string, string>> lst = ((from ob in context.extOlympBook
                                                           where ob.OlympTypeId == OlympTypeId && ob.OlympNameId == OlympNameId && ob.OlympSubjectId == OlympSubjectId
                                                           select new
                                                           {
                                                               Id = ob.OlympLevelId,
                                                               Name = ob.OlympLevelName
                                                           }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                cbOlympLevel.Enabled = true;
                ComboServ.FillCombo(cbOlympLevel, lst, false, false);
                cbOlympLevel.SelectedIndex = 0;
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
                    Olympiads olymp = (from ec in context.Olympiads
                                       where ec.Id == GuidId
                                       select ec).FirstOrDefault();

                    if (olymp == null)
                        return;                    

                    OlympTypeId = olymp.OlympTypeId;
                    if (OlympTypeId != 1 || OlympTypeId != 2)
                        OlympNameId = olymp.OlympNameId;
                    OlympSubjectId = olymp.OlympSubjectId;
                    if (OlympTypeId != 1 || OlympTypeId != 2)
                        OlympLevelId = olymp.OlympLevelId;
                    OlympValueId = olymp.OlympValueId;
                    OriginDoc = olymp.OriginDoc;

                    DocumentSeries = olymp.DocumentSeries;
                    DocumentNumber = olymp.DocumentNumber;
                    DocumentDate = olymp.DocumentDate;
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

            if (_isReadOnly)
                btnSaveChange.Enabled = false;
        }

        protected override void  SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();

            if (OlympTypeId == MainClass.olympSpbguId)
            {               
                cbOlympSubject.Enabled = false;
                cbOlympLevel.Enabled = false;
            }
            else
                cbOlympName.Enabled = false;           
        }

        protected override bool CheckFields()
        {
            if (!OriginDoc)
                return true;

            using (PriemEntities context = new PriemEntities())
            {
                Guid? persId = (from ab in context.extAbitAspirant
                                where ab.Id == _abitId
                                select ab.PersonId).FirstOrDefault();
                int cnt;

                if (_Id == null)
                    cnt = (from ol in context.extOlympiadsAll
                           where ol.OlympLevelId == OlympLevelId && ol.OlympTypeId == OlympTypeId
                           && ol.OlympNameId == OlympNameId && ol.OlympSubjectId == OlympSubjectId
                           && ol.OlympValueId == OlympValueId
                           && ol.PersonId == persId && ol.OriginDoc
                           select ol).Count();
                else
                    cnt = (from ol in context.extOlympiadsAll
                           where ol.OlympLevelId == OlympLevelId && ol.OlympTypeId == OlympTypeId
                           && ol.OlympNameId == OlympNameId && ol.OlympSubjectId == OlympSubjectId
                           && ol.OlympValueId == OlympValueId
                           && ol.PersonId == persId && ol.Id != GuidId && ol.OriginDoc
                           select ol).Count();

                if (cnt > 0)
                {
                    epError.SetError(chbOriginDoc, "Подан подлинник диплома олимпиады на другое заявление!");                   
                    return false;
                }
                else
                    epError.Clear();

                return true;
            }
        }

        protected override void InsertRec(PriemEntities context, System.Data.Objects.ObjectParameter idParam)
        {
            context.Olympiads_Insert(OlympTypeId, OlympNameId, OlympSubjectId, OlympLevelId, OlympValueId, OriginDoc, _abitId, DocumentSeries, DocumentNumber, DocumentDate, idParam);
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            context.Olympiads_Update(OlympTypeId, OlympNameId, OlympSubjectId, OlympLevelId, OlympValueId, OriginDoc, DocumentSeries, DocumentNumber, DocumentDate, id);
        }

        protected override void OnSave()
        {
            base.OnSave();
            MainClass.DataRefresh();            
        }

        protected override void CloseCardAfterSave()
        {
            if (!_isModified)
                this.Close();
        }
    }
}