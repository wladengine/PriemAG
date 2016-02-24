using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Objects;

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class CardAbit : CardFromList
    {
        private DataRefreshHandler _drh;

        private Guid? _personId;
        private bool inEnableProtocol;
        private bool inEntryView;
        private bool lockHasOrigin;
        private int? abitBarcode;
        private int? persBarcode;
        private bool IsGosLine
        {
            get { return chbIsGosLine.Checked; }
            set { chbIsGosLine.Checked = value; }
        }

        // конструктор нового заявления для человека
        public CardAbit(Guid? personId)
        {
            InitializeComponent();
            _Id = null;
            _personId = personId;
            formOwner = null;
            tcCard = tabCard;

            InitControls();
        }

        public CardAbit(string abId, int? rowInd, BaseFormEx formOwner)
        {
            InitializeComponent();
            _Id = abId;
            _personId = null;
            tcCard = tabCard;

            this.formOwner = formOwner;
            if (rowInd.HasValue)
                ownerRowIndex = rowInd.Value;

            InitControls();
        }

        protected void InitControls()
        {
            InitFocusHandlers();

            ExtraInit();
            FillCard();
            InitHandlers();

            if (_Id == null)
            {
                _isModified = true;
                btnSaveChange.Text = Constants.BTN_SAVE_TITLE;
                this.Text = Constants.CARD_TITLE + _title + Constants.CARD_MODIFIED;
            }
            else
            {
                _isModified = false;
                ReadOnlyCard();
            }

            SetReadOnlyFieldsAfterFill();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            _tableName = "ed.Abiturient";

            _drh = new DataRefreshHandler(UpdateFIO);
            MainClass.AddHandler(_drh);

            tcCard = tabCard;
            abitBarcode = 0;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    if (_Id != null && _personId == null)
                        _personId = (from ab in context.qAbiturient
                                     where ab.Id == GuidId
                                     select ab.PersonId).FirstOrDefault();

                    FillLicenseProgram();
                    FillObrazProgram();
                    FillProfile();
                    FillFaculty();
                    FillStudyForm();
                    FillStudyBasis();
                    FillCompetition();

                    ComboServ.FillCombo(cbLanguage, HelpClass.GetComboListByTable("ed.Language"), true, false);
                    ComboServ.FillCombo(cbCelCompetition, HelpClass.GetComboListByTable("ed.CelCompetition"), true, false);

                    cbOtherCompetition.Visible = false;
                    lblOtherCompetition.Visible = false;
                    cbCelCompetition.Visible = false;
                    lblCelCompetition.Visible = false;
                    tbCelCompetitionText.Visible = false;

                    dtBackDocDate.Enabled = false;
                    lblCompFromOlymp.Visible = false;
                    chbHasOriginals.Checked = false;
                    lockHasOrigin = false;
                    lblOtherOriginals.Visible = false;
                    chbChecked.Checked = false;
                    chbChecked.Enabled = false;
                    chbNotEnabled.Checked = false;
                    dtDocInsertDate.Enabled = false;
                    btnDocInventory.Visible = false;

                    btnDocs.Visible = true;
                    gbSecondType.Visible = false;
                    btnDocInventory.Visible = true;

                    if (MainClass.IsPasha())
                    {
                        btnDeleteMark.Visible = btnDeleteMark.Enabled = true;
                        btnAddFix.Enabled = btnAddFix.Visible = true;
                        btnAddGreen.Enabled = btnAddGreen.Visible = true;
                    }
                    else
                    {
                        btnDeleteMark.Visible = btnDeleteMark.Enabled = false;
                        btnAddFix.Enabled = btnAddFix.Visible = false;
                        btnAddGreen.Enabled = btnAddGreen.Visible = false;
                    }

                    cbPrint.Items.Clear();
                    cbPrint.Items.Add("Заявление");
                    cbPrint.Items.Add("Наклейка для каждого заявления");
                    cbPrint.Items.Add("Наклейка для всех заявлений");
                    cbPrint.Items.Add("Справка");
                    if (MainClass.RightsFacMain())
                        cbPrint.Items.Add("Экзам.лист");
                    cbPrint.SelectedIndex = 0;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }
        }

        protected override void FillCard()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    FillLockOrigin(context);

                    if (_Id == null)
                    {
                        extPersonAspirant pers = (from per in context.extPersonAspirant
                                          where per.Id == _personId
                                          select per).FirstOrDefault();

                        int? lanId = pers.LanguageId;
                        ComboServ.SetComboId(cbLanguage, lanId);
                        dtDocInsertDate.Value = DateTime.Now;

                        return;
                    }

                    UpdateFIO();
                    persBarcode = context.Person.Where(x => x.Id == _personId).Select(x => x.Barcode).FirstOrDefault();

                    qAbiturient abit = (from ab in context.qAbiturient
                                        where ab.Id == GuidId
                                        select ab).FirstOrDefault();

                    string PersonNum = context.extPersonAspirant.Where(x => x.Id == abit.PersonId).Select(x => x.PersonNum).FirstOrDefault();

                    tbRegNum.Text = MainClass.GetAbitNum(abit.RegNum, PersonNum);

                    IsSecond = abit.IsSecond;
                    IsReduced = abit.IsReduced;
                    IsParallel = abit.IsParallel;
                    FillLicenseProgram();

                    LicenseProgramId = abit.LicenseProgramId;
                    FillObrazProgram();

                    ObrazProgramId = abit.ObrazProgramId;
                    FillProfile();

                    ProfileId = abit.ProfileId;
                    FillFaculty();

                    FacultyId = abit.FacultyId;
                    FillStudyForm();

                    StudyFormId = abit.StudyFormId;
                    FillStudyBasis();

                    StudyBasisId = abit.StudyBasisId;
                    FillCompetition();

                    IsGosLine = abit.IsGosLine;
                    CompetitionId = abit.CompetitionId;
                    OtherCompetitionId = abit.OtherCompetitionId;
                    CelCompetitionId = abit.CelCompetitionId;
                    CelCompetitionText = abit.CelCompetitionText;
                    CompFromOlymp = abit.CompFromOlymp;
                    IsListener = abit.IsListener;
                    IsPaid = abit.IsPaid;
                    BackDoc = abit.BackDoc;
                    if (abit.BackDoc)
                    {
                        GetWhoSetBackDoc(context);
                    }
                    else
                    {
                        lblWhoBackDoc.Visible = false;
                        lblWhoBackDoc.Text = "";
                    }

                    BackDocDate = abit.BackDocDate;
                    DocDate = abit.DocDate;
                    DocInsertDate = abit.DocInsertDate;
                    Checked = abit.Checked;
                    NotEnabled = abit.NotEnabled;
                    Coefficient = abit.Coefficient;
                    LanguageId = abit.LanguageId;
                    HasOriginals = abit.HasOriginals;
                    IsCommonRussianCompetition = abit.IsCommonRussianCompetition;

                    if (StudyBasisId == 2)
                    {
                        chbIsCommonRussianCompetition.Visible = false;
                        IsCommonRussianCompetition = abit.IsCommonRussianCompetition;
                    }
                    else
                    {
                        chbIsCommonRussianCompetition.Visible = false;
                        IsCommonRussianCompetition = true;
                    }

                    if (abit.HasOriginals)
                    {
                        GetWhoSetHasOriginals(context);
                    }
                    else
                    {
                        lblHasOriginalsUser.Visible = false;
                        lblHasOriginalsUser.Text = "";
                    }

                    Priority = abit.Priority;
                    abitBarcode = abit.Barcode;

                    FillProtocols(context);
                    UpdateDataGridOlymp();

                    FillExams();
                    Sum = GetAbitSum(_Id);

                    inEnableProtocol = GetInEnableProtocol(context);
                    inEntryView = GetInEntryView(context);

                    context.Abiturient_UpdateIsViewed(GuidId);

                    if (MainClass.dbType == PriemType.PriemAspirant)
                    {
                        chbHasMotivationLetter.Checked = GetHasMotivationLetter();
                        chbHasEssay.Checked = GetHasEssay();
                    }
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + ex.Message);
            }
        }

        private string GetAbitSum(string abitId)
        {
            if (string.IsNullOrEmpty(abitId))
                return null;

            using (PriemEntities context = new PriemEntities())
            {
                return context.ExecuteStoreQuery<int?>(string.Format("SELECT TotalSum FROM ed.extAbitMarksSum WHERE Id = '{0}'", abitId)).FirstOrDefault().ToString();
            }
        }
        private void GetWhoSetHasOriginals(PriemEntities context)
        {
            var _who = context.qAbiturient_WhoSetHasOriginals.Where(x => x.Id == GuidId).FirstOrDefault();
            if (_who == null)
                return;

            string who = _who.UserId;
            string whoFac = _who.FacultyName;
            string whoDate = _who.ActionTime.ToShortDateString() + " " + _who.ActionTime.ToShortTimeString();
            who = MainClass.GetADUserName(who);

            if (!string.IsNullOrEmpty(who))
            {
                lblHasOriginalsUser.Text = "Проставлено: " + who + " (" + whoDate + " " + whoFac + ")";
                lblHasOriginalsUser.Visible = true;
            }
        }
        private void GetWhoSetBackDoc(PriemEntities context)
        {
            var _who = context.qAbiturient_WhoSetBackDoc.Where(x => x.Id == GuidId).FirstOrDefault();
            if (_who == null)
                return;

            string who = _who.UserId;
            string whoFac = _who.FacultyName;
            string whoDate = _who.ActionTime.ToShortDateString() + " " + _who.ActionTime.ToShortTimeString();
            who = MainClass.GetADUserName(who);

            if (!string.IsNullOrEmpty(who))
            {
                lblWhoBackDoc.Text = "Проставлен отказ: " + who + " (" + whoDate + " " + whoFac + ")";
                lblWhoBackDoc.Visible = true;
            }
        }

        // если подал подлинники на одно заявления - то писать об этом
        private void FillLockOrigin(PriemEntities context)
        {
            string queryForLock = string.Format(@"SELECT Case when Exists 
                (SELECT Id FROM ed.Abiturient 
                WHERE ed.Abiturient.PersonId = '{0}' 
                {1} 
                AND ed.Abiturient.HasOriginals > 0) then 'true' else 'false' end ", _personId.ToString(), _Id == null ? "" : string.Format(" AND ed.Abiturient.Id <> '{0}'", _Id));
            lockHasOrigin = bool.Parse(context.ExecuteStoreQuery<string>(queryForLock).FirstOrDefault());


            if (lockHasOrigin)
            {
                lblOtherOriginals.Visible = true;
                chbHasOriginals.Enabled = false;
            }
            else
            {
                lblOtherOriginals.Visible = false;
                chbHasOriginals.Enabled = true;
            }
        }

        private bool GetHasMotivationLetter()
        {
            BDClassLib.SQLClass InetDB = new BDClassLib.SQLClass();
            InetDB.OpenDatabase(MainClass.connStringOnline);

            string query = "SELECT COUNT(*) FROM qAbitFiles_OnlyEssayMotivLetter WHERE (ApplicationBarcode=@ApplicationBarcode OR PersonBarcode=@PersonBarcode) AND FileTypeId=2";
            int cnt = (int)InetDB.GetValue(query, new SortedList<string, object>() 
            {
                { "@ApplicationBarcode", QueryServ.ToNullDB(abitBarcode) }, 
                { "@PersonBarcode", QueryServ.ToNullDB(persBarcode) } 
            });

            return (cnt > 0);
        }
        private bool GetHasEssay()
        {
            BDClassLib.SQLClass InetDB = new BDClassLib.SQLClass();
            InetDB.OpenDatabase(MainClass.connStringOnline);

            string query = "SELECT COUNT(*) FROM qAbitFiles_OnlyEssayMotivLetter WHERE (ApplicationBarcode=@ApplicationBarcode OR PersonBarcode=@PersonBarcode) AND FileTypeId=3";
            int cnt = (int)InetDB.GetValue(query, new SortedList<string, object>() 
            {
                { "@ApplicationBarcode", QueryServ.ToNullDB(abitBarcode) }, 
                { "@PersonBarcode", QueryServ.ToNullDB(persBarcode) }  
            });

            return (cnt > 0);
        }

        // возвращает, есть ли человек в протоколе о допуске
        private bool GetInEnableProtocol(PriemEntities context)
        {
            int cntProt = (from ph in context.extProtocol
                           where ph.ProtocolTypeId == 1 && !ph.IsOld && !ph.Excluded && ph.AbiturientId == GuidId
                           select ph).Count();

            if (cntProt > 0)
                return true;
            else
                return false;
        }

        // возвращает, есть ли человек в представлении к зачислению
        private bool GetInEntryView(PriemEntities context)
        {
            int cntProt = (from ph in context.extEntryView
                           where ph.AbiturientId == GuidId
                           select ph).Count();

            if (cntProt > 0)
                return true;
            else
                return false;
        }

        private void FillProtocols(PriemEntities context)
        {
            try
            {
                tbEnabledProtocol.Text = Util.ToStr((from ph in context.extProtocol
                                                     where ph.ProtocolTypeId == 1 && !ph.IsOld && !ph.Excluded && ph.AbiturientId == GuidId
                                                     select ph.Number).FirstOrDefault());

                tbEntryProtocol.Text = Util.ToStr((from ph in context.extEntryView
                                                   where ph.AbiturientId == GuidId
                                                   select ph.Number).FirstOrDefault());
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Данное заявление находится одновременно в нескольких протоколах \n " + exc.Message);
            }
        }

        #region ReadOnly

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();

            //if (_Id == null)
            //    chbHasOriginals.Enabled = false;

            // магистратура!
            if (MainClass.dbType == PriemType.PriemAspirant)
                tpOlymps.Parent = null;
        }

        protected override void SetAllFieldsNotEnabled()
        {
            base.SetAllFieldsNotEnabled();

            btnCardPerson.Enabled = true;

            if (StudyBasisId == 2)
                btnPaidData.Enabled = true;
            gbPrint.Enabled = true;
            btnPrint.Enabled = true;

            btnDocs.Enabled = true;

            if (MainClass.dbType != PriemType.PriemAspirant)
            {
                WinFormsServ.SetSubControlsEnabled(gbOlymps, true);
                btnAddO.Enabled = false;
                btnRemoveO.Enabled = false;
            }

            if (MainClass.IsPasha())
            {
                dgvExams.Enabled = btnDeleteMark.Enabled = true;
                btnAddFix.Enabled = btnAddFix.Visible = true;
                btnAddGreen.Enabled = btnAddGreen.Visible = true;
            }

            btnDocInventory.Enabled = true;
        }

        protected override void SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();

            tbPriority.Enabled = true;
            tbRegNum.Enabled = false;
            if (StudyBasisId == 2)
                btnPaidData.Enabled = true;

            btnDocs.Enabled = true;

            tbEnabledProtocol.Enabled = false;
            tbEntryProtocol.Enabled = false;
            dtDocInsertDate.Enabled = false;
            tbSum.Enabled = false;
            btnDeleteMark.Enabled = false;

            if (MainClass.dbType != PriemType.PriemAspirant)
                WinFormsServ.SetSubControlsEnabled(gbOlymps, true);

            cbFaculty.Enabled = false;
            btnDocInventory.Enabled = true;
        }

        // закрытие части полей в зависимости от прав
        protected override void SetReadOnlyFields()
        {
            SetAllFieldsNotEnabled();

            if (!chbBackDoc.Checked)
                chbBackDoc.Enabled = true;

            chbIsPaid.Enabled = true;
            cbLanguage.Enabled = true;
            cbCelCompetition.Enabled = true;
            tbCelCompetitionText.Enabled = true;
            chbIsListener.Enabled = true;
            if (!chbHasOriginals.Checked)
                chbHasOriginals.Enabled = true;
            tbPriority.Enabled = true;
            tbCoefficient.Enabled = true;

            btnAddO.Enabled = true;

            btnClose.Enabled = true;
            btnSaveChange.Enabled = true;

            chbIsCommonRussianCompetition.Enabled = true;

            if (MainClass.IsFacMain())
            {
                if (chbBackDoc.Checked)
                    chbBackDoc.Enabled = false;
                if (chbHasOriginals.Checked)
                    chbHasOriginals.Enabled = false;

                cbCompetition.Enabled = true;
                cbOtherCompetition.Enabled = true;
                chbChecked.Enabled = true;
                chbNotEnabled.Enabled = true;

                cbLicenseProgram.Enabled = true;
                cbObrazProgram.Enabled = true;
                cbProfile.Enabled = true;
                cbFaculty.Enabled = true;
                gbSecondType.Enabled = true;
                cbStudyForm.Enabled = true;
                cbStudyBasis.Enabled = true;

                if (CompetitionId == 1 || CompetitionId == 2 || CompetitionId == 7 || CompetitionId == 8)
                    chbChecked.Enabled = false;
            }

            if (MainClass.RightsSov_SovMain())
            {
                cbCompetition.Enabled = true;
                cbOtherCompetition.Enabled = true;
                chbChecked.Enabled = true;
                chbNotEnabled.Enabled = true;

                cbLicenseProgram.Enabled = true;
                cbObrazProgram.Enabled = true;
                cbProfile.Enabled = true;
                cbFaculty.Enabled = true;
                gbSecondType.Enabled = true;
                cbStudyForm.Enabled = true;
                cbStudyBasis.Enabled = true;

                if (chbBackDoc.Checked)
                    chbBackDoc.Enabled = true;

                //уточнить, кто может снимать эту галочку! 
                if (chbHasOriginals.Checked)
                    chbHasOriginals.Enabled = true;
            }

            if (MainClass.IsPasha())
            {
                chbHasOriginals.Enabled = true;
                chbBackDoc.Enabled = true;
            }

            if (MainClass.IsPasha() || (MainClass.RightsSov_SovMain_FacMain() && !inEnableProtocol))
                btnRemoveO.Enabled = (dgvOlimps.RowCount == 0 ? false : true);

            if (inEnableProtocol)
            {
                chbChecked.Enabled = false;
                chbNotEnabled.Enabled = false;
                dtDocDate.Enabled = false;
                cbCompetition.Enabled = false;

                cbLicenseProgram.Enabled = false;
                cbObrazProgram.Enabled = false;
                cbProfile.Enabled = false;
                cbFaculty.Enabled = false;
                gbSecondType.Enabled = false;
                cbStudyForm.Enabled = false;
                cbStudyBasis.Enabled = false;

                if (MainClass.RightsFaculty())
                    btnAddO.Enabled = false;

                if (MainClass.IsPasha())
                    btnRemoveO.Enabled = true;
            }

            // больше нельзя изменять конкурс
            cbLicenseProgram.Enabled = false;
            cbObrazProgram.Enabled = false;
            cbProfile.Enabled = false;
            cbFaculty.Enabled = false;
            gbSecondType.Enabled = false;
            cbStudyForm.Enabled = false;
            cbStudyBasis.Enabled = false;

            if (inEntryView)
            {
                tbCoefficient.Enabled = false;

                chbChecked.Enabled = false;
                chbNotEnabled.Enabled = false;
                dtDocDate.Enabled = false;
                cbCompetition.Enabled = false;

                chbHasOriginals.Enabled = false;
                chbBackDoc.Enabled = false;
                dtBackDocDate.Enabled = false;

                cbLicenseProgram.Enabled = false;
                cbObrazProgram.Enabled = false;
                cbProfile.Enabled = false;
                cbFaculty.Enabled = false;
                gbSecondType.Enabled = false;
                cbStudyForm.Enabled = false;
                cbStudyBasis.Enabled = false;

                btnAddO.Enabled = false;
                btnRemoveO.Enabled = false;
                cbLanguage.Enabled = false;
            }

            if (MainClass.IsPasha())
                dtDocDate.Enabled = true;

            if (lockHasOrigin)
                if (!chbHasOriginals.Checked)
                    chbHasOriginals.Enabled = false;

            // закрываем для изменения кроме ограниченного набора
            //if (!MainClass.HasAddRightsForPriem(FacultyId, ProfessionId, ObrazProgramId, SpecializationId, StudyFormId, StudyBasisId))
            //{
            //    tbCoefficient.Enabled = false;

            //    chbChecked.Enabled = false;
            //    chbNotEnabled.Enabled = false;
            //    dtDocDate.Enabled = false;
            //    cbCompetition.Enabled = false;

            //    cbLicenseProgram.Enabled = false;
            //    cbObrazProgram.Enabled = false;
            //    cbProfile.Enabled = false;
            //    cbFaculty.Enabled = false;
            //    cbSecondType.Enabled = false;
            //    cbStudyForm.Enabled = false;
            //    cbStudyBasis.Enabled = false; 
            //    btnAddO.Enabled = false;
            //    btnRemoveO.Enabled = false;

            //    cbLanguage.Enabled = false;
            //}
        }

        #endregion

        #region Handlers

        //инициализация обработчиков мегакомбов
        protected override void InitHandlers()
        {
            chbIsReduced.CheckedChanged += new EventHandler(chbIsReduced_CheckedChanged);
            chbIsParallel.CheckedChanged += new EventHandler(chbIsParallel_CheckedChanged);
            chbIsSecond.CheckedChanged += new EventHandler(chbIsSecond_CheckedChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbObrazProgram.SelectedIndexChanged += new EventHandler(cbObrazProgram_SelectedIndexChanged);
            cbProfile.SelectedIndexChanged += new EventHandler(cbProfile_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbCompetition.SelectedIndexChanged += new EventHandler(cbCompetition_SelectedIndexChanged);
            chbHasOriginals.CheckedChanged += new System.EventHandler(chbHasOriginals_CheckedChanged);

        }
        protected override void NullHandlers()
        {
            chbIsReduced.CheckedChanged -= new EventHandler(chbIsReduced_CheckedChanged);
            chbIsParallel.CheckedChanged -= new EventHandler(chbIsParallel_CheckedChanged);
            chbIsSecond.CheckedChanged -= new EventHandler(chbIsSecond_CheckedChanged);
            cbLicenseProgram.SelectedIndexChanged -= new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbObrazProgram.SelectedIndexChanged -= new EventHandler(cbObrazProgram_SelectedIndexChanged);
            cbProfile.SelectedIndexChanged -= new EventHandler(cbProfile_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged -= new EventHandler(cbStudyForm_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged -= new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbCompetition.SelectedIndexChanged -= new EventHandler(cbCompetition_SelectedIndexChanged);
            chbHasOriginals.CheckedChanged -= new System.EventHandler(chbHasOriginals_CheckedChanged);
        }

        void chbIsSecond_CheckedChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }
        void chbIsParallel_CheckedChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }
        void chbIsReduced_CheckedChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }
        void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
        }
        void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProfile();
            FillFaculty();
            FillStudyForm();
            FillStudyBasis();
        }
        void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFaculty();
            FillStudyForm();
            FillStudyBasis();
        }
        void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyBasis();
        }
        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCompetition();
        }
        void cbCompetition_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAfterCompetition();
        }


        private IEnumerable<qEntry> GetEntry(PriemEntities context)
        {
            IEnumerable<qEntry> entry = MainClass.GetEntry(context);

            entry = entry.Where(c => c.IsReduced == IsReduced);
            entry = entry.Where(c => c.IsParallel == IsParallel);
            entry = entry.Where(c => c.IsSecond == IsSecond);

            return entry;
        }

        private void FillLicenseProgram()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               orderby ent.LicenseProgramName
                                                               select new
                                                               {
                                                                   Id = ent.LicenseProgramId,
                                                                   Name = ent.LicenseProgramName,
                                                                   Code = ent.LicenseProgramCode
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Code)).ToList();

                    ComboServ.FillCombo(cbLicenseProgram, lst, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillLicenseProgram" + exc.Message);
            }
        }
        private void FillObrazProgram()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               where ent.LicenseProgramId == LicenseProgramId
                                                               orderby ent.ObrazProgramName
                                                               select new
                                                               {
                                                                   Id = ent.ObrazProgramId,
                                                                   Name = ent.ObrazProgramName,
                                                                   Crypt = ent.ObrazProgramCrypt
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Crypt)).ToList();

                    ComboServ.FillCombo(cbObrazProgram, lst, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillObrazProgram" + exc.Message);
            }
        }
        private void FillProfile()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               where ent.LicenseProgramId == LicenseProgramId && ent.ObrazProgramId == ObrazProgramId && ent.ProfileId != null
                                                               orderby ent.ProfileName
                                                               select new
                                                               {
                                                                   Id = ent.ProfileId,
                                                                   Name = ent.ProfileName
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    if (lst.Count() > 0)
                    {
                        if (ObrazProgramId == 39)
                            ComboServ.FillCombo(cbProfile, lst, true, false);
                        else
                            ComboServ.FillCombo(cbProfile, lst, false, false);
                        cbProfile.Enabled = true;
                    }
                    else
                    {
                        ComboServ.FillCombo(cbProfile, new List<KeyValuePair<string, string>>(), true, false);
                        cbProfile.Enabled = false;
                    }
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillProfile" + exc.Message);
            }
        }
        private void FillFaculty()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               where ent.LicenseProgramId == LicenseProgramId
                                                               && ent.ObrazProgramId == ObrazProgramId
                                                               && (ProfileId == null ? ent.ProfileId == null : ent.ProfileId == ProfileId)
                                                               select new
                                                               {
                                                                   Id = ent.FacultyId,
                                                                   Name = ent.FacultyName
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    ComboServ.FillCombo(cbFaculty, lst, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillFaculty" + exc.Message);
            }
        }
        private void FillStudyForm()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               where
                                                               ent.LicenseProgramId == LicenseProgramId
                                                               && ent.ObrazProgramId == ObrazProgramId
                                                               && (ProfileId == null ? ent.ProfileId == null : ent.ProfileId == ProfileId)
                                                               && ent.FacultyId == FacultyId
                                                               orderby ent.StudyFormName
                                                               select new
                                                               {
                                                                   Id = ent.StudyFormId,
                                                                   Name = ent.StudyFormName
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    ComboServ.FillCombo(cbStudyForm, lst, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillStudyForm" + exc.Message);
            }
        }
        private void FillStudyBasis()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from ent in GetEntry(context)
                                                               where ent.LicenseProgramId == LicenseProgramId
                                                               && ent.ObrazProgramId == ObrazProgramId
                                                               && (ProfileId == null ? ent.ProfileId == null : ent.ProfileId == ProfileId)
                                                               && ent.FacultyId == FacultyId
                                                               && ent.StudyFormId == StudyFormId
                                                               orderby ent.StudyBasisName
                                                               select new
                                                               {
                                                                   Id = ent.StudyBasisId,
                                                                   Name = ent.StudyBasisName
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    ComboServ.FillCombo(cbStudyBasis, lst, false, false);
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillStudyBasis" + exc.Message);
            }
        }
        private void FillCompetition()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = ((from cp in context.Competition
                                                               where cp.StudyBasisId == StudyBasisId
                                                               orderby cp.Name
                                                               select new
                                                               {
                                                                   cp.Id,
                                                                   cp.Name
                                                               }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    ComboServ.FillCombo(cbCompetition, lst, false, false);

                    lst = ((from cp in context.Competition
                            where cp.StudyBasisId == StudyBasisId && (cp.Id < 6 || cp.Id == 9)
                            select new
                            {
                                cp.Id,
                                cp.Name
                            }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();

                    ComboServ.FillCombo(cbOtherCompetition, lst, true, false);

                    if (StudyBasisId == 1)
                    {
                        chbIsListener.Checked = false;
                        chbIsListener.Enabled = false;
                        chbIsPaid.Checked = false;
                        chbIsPaid.Enabled = false;
                        btnPaidData.Enabled = false;
                        ComboServ.SetComboId(cbCompetition, 4);
                        chbIsCommonRussianCompetition.Visible = false;
                    }
                    else
                    {
                        chbIsListener.Enabled = true;
                        chbIsPaid.Enabled = true;
                        btnPaidData.Enabled = true;
                        ComboServ.SetComboId(cbCompetition, 3);
                        chbIsCommonRussianCompetition.Visible = true;
                    }
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы FillCompetition" + exc.Message);
            }
        }
        private void UpdateAfterCompetition()
        {
            if (CompetitionId == 1 || CompetitionId == 2 || CompetitionId == 7 || CompetitionId == 8)
            {
                chbChecked.Checked = false;
                chbChecked.Enabled = false;
            }

            if (CompetitionId == 6)
            {
                if (_Id != null)
                    chbChecked.Enabled = true;

                cbOtherCompetition.SelectedIndex = 0;
                cbCelCompetition.SelectedIndex = 0;
                lblOtherCompetition.Visible = true;
                cbOtherCompetition.Visible = true;

                lblCelCompetition.Visible = true;
                cbCelCompetition.Visible = true;
                tbCelCompetitionText.Visible = true;
            }
            else
            {
                if (_Id != null)
                    chbChecked.Enabled = true;

                cbOtherCompetition.SelectedIndex = 0;
                lblOtherCompetition.Visible = false;
                cbOtherCompetition.Visible = false;
                tbCelCompetitionText.Text = "";
                tbCelCompetitionText.Visible = false;

                lblCelCompetition.Visible = false;
                cbCelCompetition.Visible = false;
                cbCelCompetition.SelectedIndex = 0;
            }
        }

        // строка с ФИО если поменялись данные личной карточки
        private void UpdateFIO()
        {
            try
            {
                if (_personId == null)
                    lblFIO.Text = string.Empty;
                else
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        lblFIO.Text = (from per in context.extPersonAll
                                       where per.Id == _personId
                                       select per.FIO).FirstOrDefault();

                    }
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при обращении к базе" + ex.Message);
            }
        }

        private void chbHasOriginals_CheckedChanged(object sender, EventArgs e)
        {
            if (_isModified)
            {
                if (chbHasOriginals.Checked)
                {
                    if (MessageBox.Show("Я подтверждаю что все документы подлинные", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //chbEgeDocOriginal.Checked = true;
                        chbHasOriginals.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        chbHasOriginals.Checked = false;
                    }
                }
                else
                {
                    chbHasOriginals.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        private void chbBackDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (_isModified)
            {
                if (chbBackDoc.Checked)
                {
                    if (MessageBox.Show(string.Format("Вы уверены, что абитуриент отказался от участия в конкурсе на образовательную программу \"{0}\", форму обучения \"{1}\", основу обучения \"{2}\"?????", cbObrazProgram.Text, cbStudyForm.Text, cbStudyBasis.Text), "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        chbBackDoc.ForeColor = System.Drawing.Color.Red;
                        dtBackDocDate.Enabled = true;
                        chbHasOriginals.Checked = false;
                    }
                    else
                    {
                        chbBackDoc.Checked = false;
                    }
                }
                else
                {
                    chbBackDoc.ForeColor = System.Drawing.Color.Black;
                    dtBackDocDate.Enabled = false;
                }
            }
        }
        private void chbChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (chbChecked.Checked)
                chbChecked.ForeColor = System.Drawing.Color.Black;
            else
                chbChecked.ForeColor = System.Drawing.Color.Red;
        }
        private void chbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chbNotEnabled.Checked)
                chbNotEnabled.ForeColor = System.Drawing.Color.Red;
            else
                chbNotEnabled.ForeColor = System.Drawing.Color.Black;
        }


        #endregion

        #region Save

        protected override bool CheckFields()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    if (LicenseProgramId == null || ObrazProgramId == null || FacultyId == null || StudyFormId == null || StudyBasisId == null)
                    {
                        epErrorInput.SetError(cbLicenseProgram, "Прием документов на данную программу не осуществляется!");
                        tabCard.SelectedIndex = 0;
                        return false;
                    }
                    else
                        epErrorInput.Clear();

                    if (EntryId == null)
                    {
                        epErrorInput.SetError(cbLicenseProgram, "Прием документов на данную программу не осуществляется!");
                        tabCard.SelectedIndex = 0;
                        return false;
                    }
                    else
                        epErrorInput.Clear();


                    if (_Id == null)
                    {
                        if (!CheckIsClosed(context))
                        {
                            epErrorInput.SetError(cbLicenseProgram, "Прием документов на данную программу закрыт!");
                            tabCard.SelectedIndex = 0;
                            return false;
                        }
                        else
                            epErrorInput.Clear();

                        //if (!_bdc.HasAddRightsForPriem(FacultyId, ProfessionId, ObrazProgramId, SpecializationId, StudyFormId, StudyBasisId))               
                        //{
                        //    epErrorInput.SetError(cbFaculty, "Прием документов на данную программу закрыт (по кц)!");
                        //    tabCard.SelectedIndex = 0;
                        //    return false;
                        //}
                        //else
                        //    epErrorInput.Clear();
                    }

                    if ((CompetitionId != 3 && CompetitionId != 4 /* б/э + цел + дог-б/э + в/к + дог-в/к + общ-преим */) && !HasOriginals && !BackDoc)
                    {
                        WinFormsServ.Error("Для данного типа конкурса требуется обязательная подача оригиналов документов об образовании");
                        return false;
                    }

                    if (!CheckIdent(context))
                    {
                        WinFormsServ.Error("У абитуриента уже существует заявление на данный факультет, направление, профиль, форму и основу обучения!");
                        return false;
                    }

                    if (!CheckThreeAbits(context))
                    {
                        WinFormsServ.Error("У абитуриента уже существует 3 заявления на различные образовательные программы!");
                        return false;
                    }

                    if (DocDate > DateTime.Now)
                    {
                        epErrorInput.SetError(dtDocDate, "Неправильная дата");
                        tabCard.SelectedIndex = 1;
                        return false;
                    }
                    else
                        epErrorInput.Clear();

                    //if (cbCompetition.Id != CheckCompetition(context))
                    //{
                    //    DialogResult res = MessageBox.Show(string.Format("Тип конкурса не соответствует льготам абитуриента? Установить тип конкурса {0}?", _bdc.GetStringValue(string.Format("SELECT Competition.Name FROM Competition WHERE Competition.Id = {0}", CheckCompetition()))), "Предупреждение", MessageBoxButtons.YesNoCancel);
                    //    if (res == DialogResult.Yes)
                    //        cbCompetition.SetItem(CheckCompetition());
                    //} 
                    return true;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при CheckFields" + exc.Message);
                return false;
            }
        }

        private bool CheckIsClosed(PriemEntities context)
        {
            bool isClosed = (from ent in context.qEntry
                             where ent.Id == EntryId
                             select ent.IsClosed).FirstOrDefault();
            return !isClosed;
        }

        private string CheckCompetition(PriemEntities context)
        {
            if (StudyBasisId == 2)
                return "3";
            // проверка на олимпиады 

            int cntOl = context.ExecuteStoreQuery<int>(string.Format("SELECT Count(Olympiads.Id) FROM Olympiads WHERE Olympiads.AbiturientId = '{0}' AND ((Olympiads.OlympLevelId = 2 AND Olympiads.OlympValueId IN (1,2,3)) OR Olympiads.OlympLevelId = 1)", _Id)).FirstOrDefault();
            if (cntOl > 0)
                return "1";

            //проверка на льготы
            //if (chbRebSir.Checked || chbSir.Checked || chbInv.Checked || chbPSir.Checked || chbVoen.Checked || chbBoev.Checked || chbCher.Checked)
            //    return "2";   
            return "";
        }

        // проверка на уникальность заявления
        private bool CheckIdent(PriemEntities context)
        {
            ObjectParameter boolPar = new ObjectParameter("result", typeof(bool));

            if (_Id == null)
                context.CheckAbitIdentWithGosLine(_personId, EntryId, IsGosLine, boolPar);
            else
                context.CheckAbitIdentWithIdAndGosLine(GuidId, _personId, EntryId, IsGosLine, boolPar);

            return Convert.ToBoolean(boolPar.Value);
        }

        private bool CheckThreeAbits(PriemEntities context)
        {
            return SomeMethodsClass.CheckThreeAbits(context, _personId, LicenseProgramId);
        }

        protected override void InsertRec(PriemEntities context, ObjectParameter idParam)
        {
            context.Abiturient_Insert(_personId, EntryId, CompetitionId, IsListener, false, IsPaid, BackDoc, BackDocDate, DocDate, DateTime.Now,
                Checked, NotEnabled, Coefficient, OtherCompetitionId, CelCompetitionId, CelCompetitionText, LanguageId, HasOriginals,
                Priority, abitBarcode, null, null, IsGosLine, idParam);
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            context.Abiturient_UpdateWithoutEntry(CompetitionId, IsListener, false, IsPaid, BackDoc, BackDocDate, DocDate,
                  Checked, NotEnabled, Coefficient, OtherCompetitionId, CelCompetitionId, CelCompetitionText, LanguageId, HasOriginals,
                  Priority, id);

            //если есть права на изменение конкурса 
            context.Abiturient_UpdateEntry(EntryId, id);
        }

        protected override void OnSave()
        {
            MainClass.DataRefresh();
        }

        protected override void OnSaveNew()
        {
            UpdateApplications();
            btnPrint.Enabled = true;

            using (PriemEntities context = new PriemEntities())
            {
                string num = (from pr in context.qAbiturient
                              where pr.Id == GuidId
                              select pr.RegNum).FirstOrDefault().ToString();

                string personNum = (from pr in context.extAbitAspirant
                                    where pr.Id == GuidId
                                    select pr.PersonNum).FirstOrDefault().ToString();

                tbRegNum.Text = MainClass.GetAbitNum(num, personNum);
            }
        }

        #endregion

        // Грид Олимпиады
        #region Olymps

        // обновление грида олимпиад
        public void UpdateDataGridOlymp()
        {
            if (MainClass.dbType == PriemType.PriemAspirant)
                return;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    var source = from ec in context.extOlympiads
                                 where ec.AbiturientId == GuidId
                                 select new
                                 {
                                     ec.Id,
                                     Вид = ec.OlympTypeName,
                                     Уровень = ec.OlympLevelId == null ? "нет" : ec.OlympLevelName,
                                     Название = ec.OlympNameId == null ? ec.OlympName : ec.OlympSubjectName,
                                     Предмет = ec.OlympSubjectName ?? "",
                                     Диплом = ec.OlympValueName
                                 };

                    dgvOlimps.DataSource = source;
                    dgvOlimps.Columns["Id"].Visible = false;

                    btnCardO.Enabled = dgvOlimps.RowCount != 0;
                    if (MainClass.IsPasha() || (MainClass.RightsSov_SovMain_FacMain() && !inEnableProtocol))
                        btnRemoveO.Enabled = dgvOlimps.RowCount != 0;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка  заполения грида Ege: " + exc.Message);
            }
        }

        private void btnAddO_Click(object sender, EventArgs e)
        {
            if (_Id == null)
            {
                if (MessageBox.Show("Данное действие приведет к сохранению записи, продолжить?", "Сохранить", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (SaveClick())
                        {
                            OlympCard crd = new OlympCard(GuidId);
                            crd.ToUpdateList += new UpdateListHandler(UpdateDataGridOlymp);
                            crd.ShowDialog();
                        }
                    }
                    catch (Exception exc)
                    {
                        WinFormsServ.Error("Ошибка сохранения данных" + exc.Message);
                    }
                }
            }
            else
            {
                OlympCard crd = new OlympCard(GuidId);
                crd.ToUpdateList += new UpdateListHandler(UpdateDataGridOlymp);
                crd.ShowDialog();
            }
        }

        private void btnCardO_Click(object sender, EventArgs e)
        {
            OpenCardOlymp();
        }

        private void OpenCardOlymp()
        {
            if (dgvOlimps.CurrentCell != null && dgvOlimps.CurrentCell.RowIndex > -1)
            {
                string olId = dgvOlimps.Rows[dgvOlimps.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (olId != "")
                {
                    OlympCard crd = new OlympCard(olId, GuidId, GetReadOnlyOlymps());
                    crd.ToUpdateList += new UpdateListHandler(UpdateDataGridOlymp);
                    crd.ShowDialog();
                }
            }
        }

        private bool GetReadOnlyOlymps()
        {
            if (!_isModified)
                return true;

            if (MainClass.RightsFaculty())
                return true;

            if (inEntryView)
                return true;

            //// закрываем уже всем на изменение кроме огр набора            
            //if (!MainClass.HasAddRightsForPriem(FacultyId, ProfessionId, ObrazProgramId, SpecializationId, StudyFormId, StudyBasisId))
            //    return true;

            return false;
        }

        private void dgvOlimps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCardOlymp();
        }

        private void btnRemoveO_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Guid sId = (Guid)dgvOlimps.CurrentRow.Cells["Id"].Value;
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        context.Olympiads_Delete(sId);
                    }
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка удаления данных" + ex.Message);
                }
                UpdateDataGridOlymp();
            }
        }

        #endregion

        // Грид Экзамены
        private void FillExams()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    DataTable examTable = new DataTable();

                    DataColumn clm;
                    clm = new DataColumn();
                    clm.ColumnName = "ExamInEntryId";
                    clm.DataType = typeof(int);
                    examTable.Columns.Add(clm);

                    clm = new DataColumn();
                    clm.ColumnName = "Id";
                    clm.DataType = typeof(Guid);
                    examTable.Columns.Add(clm);

                    clm = new DataColumn();
                    clm.ColumnName = "Экзамен";
                    examTable.Columns.Add(clm);

                    clm = new DataColumn();
                    clm.ColumnName = "Оценка";
                    examTable.Columns.Add(clm);

                    clm = new DataColumn();
                    clm.ColumnName = "Примечание";
                    examTable.Columns.Add(clm);

                    IEnumerable<qMark> marks = from mrk in context.qMark
                                               where mrk.AbiturientId == GuidId
                                               select mrk;

                    //sQuery = string.Format("SELECT qMark.Id, qMark.Value AS Mark, ExamName.Name AS 'Экзамен', ExamName.IsAdditional, qMark.ExamInProgramId, qMark.IsFromOlymp, 
                    //qMark.IsFromEge, qMark.IsManual, qMark.ExamVedId 
                    //    FROM qMark LEFT JOIN (ExamInProgram LEFT JOIN ExamName ON ExamInProgram.ExamNameId = ExamName.Id) ON qMark.ExamInProgramId = ExamInProgram.Id 
                    //WHERE qMark.AbiturientId = '{0}' ", _Id);

                    foreach (qMark abMark in marks)
                    {
                        DataRow newRow;
                        newRow = examTable.NewRow();
                        newRow["Экзамен"] = abMark.ExamName;
                        newRow["Id"] = abMark.Id;
                        if (abMark != null && abMark.Value.ToString() != "")
                            newRow["Оценка"] = abMark.Value.ToString();
                        if (abMark.IsFromEge)
                            newRow["Примечание"] = "Из ЕГЭ ";
                        else if (abMark.IsFromOlymp)
                            newRow["Примечание"] = "Олимпиада";
                        else if (abMark.IsManual)
                            newRow["Примечание"] = "Ручной ввод";
                        else if (abMark.ExamVedId != null && MainClass.IsPasha())
                        {
                            string vedNum = _bdc.GetStringValue(string.Format("SELECT ed.extExamsVed.Number FROM ed.extExamsVed WHERE Id = '{0}'", abMark.ExamVedId.ToString()));
                            newRow["Примечание"] = "Ведомость № " + vedNum;
                        }

                        newRow["ExamInEntryId"] = abMark.ExamInEntryId;
                        examTable.Rows.Add(newRow);
                    }

                    DataView dv = new DataView(examTable);
                    dv.AllowNew = false;

                    dgvExams.DataSource = dv;
                    dgvExams.ReadOnly = true;
                    dgvExams.Columns["ExamInEntryId"].Visible = false;
                    dgvExams.Columns["Id"].Visible = false;
                    dgvExams.Update();
                }
            }
            catch (DataException de)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + de.Message);
            }
        }

        // Печать документов
        #region Print

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Guid? AbitId = Guid.Parse(_Id);
            switch (cbPrint.SelectedIndex)
            {
                case 0:
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = lblFIO.Text + " - Заявление.pdf";
                    sfd.Filter = "ADOBE Pdf files|*.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                        Print.PrintApplication(AbitId, chbPrint.Checked, sfd.FileName);
                    break;
                case 1:
                    Print.PrintStikerOne(AbitId, chbPrint.Checked);
                    break;
                case 2:
                    Print.PrintStikerAll(_personId, AbitId, chbPrint.Checked);
                    break;
                case 3:
                    Print.PrintSprav(AbitId, chbPrint.Checked);
                    break;
                case 4:
                    PrintExamList();
                    break;
            }
        }

        public void PrintExamList()
        {
            if (MainClass.RightsFacMain())
            {
                Guid? AbitId = Guid.Parse(_Id);

                if (tbEnabledProtocol.Text.Trim() != string.Empty)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = lblFIO.Text + " - Экзаменационный лист.pdf";
                    sfd.Filter = "ADOBE Pdf files|*.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                        Print.PrintExamList(AbitId, chbPrint.Checked, sfd.FileName);
                }
                else
                    WinFormsServ.Error("Невозможно создание экзаменационного листа, абитуриент не внесен в протокол о допуске");
            }
        }

        #endregion

        private void UpdateApplications()
        {
            foreach (Form frmChild in MainClass.mainform.MdiChildren)
            {
                if (frmChild is CardPerson)
                    if (((CardPerson)frmChild).Id.CompareTo(_personId.ToString()) == 0)
                        ((CardPerson)frmChild).FillApplications();
            }
        }

        private void tabCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D1)
                this.tabCard.SelectedIndex = 0;
            if (e.Control && e.KeyCode == Keys.D2)
                this.tabCard.SelectedIndex = 1;
            if (e.Control && e.KeyCode == Keys.D3)
                this.tabCard.SelectedIndex = 2;
        }

        private void CardAbit_Click(object sender, EventArgs e)
        {
            this.Activate();
        }

        protected override void OnClosed()
        {
            MainClass.RemoveHandler(_drh);
        }

        private void btnCardPerson_Click(object sender, EventArgs e)
        {
            MainClassCards.OpenCardPerson(_personId.ToString(), null, -1);
        }

        //данные по оплате
        private void btnPaidData_Click(object sender, EventArgs e)
        {
            CardPaidData pd;

            if (_Id == null)
            {
                if (MessageBox.Show("Данное действие приведет к сохранению записи, продолжить?", "Сохранить", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (SaveClick())
                    {
                        pd = new CardPaidData(GuidId, false);
                        pd.Show();
                    }
                }
            }
            else
            {
                pd = new CardPaidData(GuidId, !_isModified);
                pd.Show();
            }
        }

        protected override void btnNext_Click(object sender, EventArgs e)
        {
            _personId = null;
            base.btnNext_Click(sender, e);
        }

        protected override void btnPrev_Click(object sender, EventArgs e)
        {
            _personId = null;
            base.btnPrev_Click(sender, e);
        }

        private void btnDeleteMark_Click(object sender, EventArgs e)
        {
            if (MainClass.IsPasha() || MainClass.IsOwner())
            {
                if (MessageBox.Show("Удалить выбранную оценку?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        foreach (DataGridViewRow dgvr in dgvExams.SelectedRows)
                        {
                            Guid? markId = new Guid(dgvr.Cells["Id"].Value.ToString());

                            try
                            {
                                context.Mark_Delete(markId);
                            }
                            catch (Exception ex)
                            {
                                WinFormsServ.Error("Ошибка удаления данных" + ex.Message);
                                goto Next;
                            }
                        Next: ;
                        }
                        FillExams();
                    }
                }
            }
        }

        private void btnAddFix_Click(object sender, EventArgs e)
        {
            if (!MainClass.IsPasha())
                return;

            AddtoFixieren();
        }

        private void AddtoFixieren()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var abit = (from ab in context.extAbitAspirant
                            where ab.Id == GuidId
                            select ab).FirstOrDefault();

                context.Fixieren_DELETE(GuidId);

                Guid? fixViewId = (from fv in context.FixierenView
                                   where fv.StudyLevelGroupId == abit.StudyLevelGroupId && fv.IsReduced == abit.IsReduced && fv.IsParallel == abit.IsParallel && fv.IsSecond == abit.IsSecond
                                   && fv.FacultyId == abit.FacultyId && fv.LicenseProgramId == abit.LicenseProgramId
                                   && fv.ObrazProgramId == abit.ObrazProgramId
                                   && (abit.ProfileId == null ? fv.ProfileId == null : fv.ProfileId == abit.ProfileId)
                                   && fv.StudyFormId == abit.StudyFormId
                                   && fv.StudyBasisId == abit.StudyBasisId
                                   && fv.IsCel == (abit.CompetitionId == 6)
                                   select fv.Id).FirstOrDefault();

                int cnt = (from fx in context.Fixieren
                           where fx.AbiturientId == GuidId
                           select fx.Number).Count();

                if (cnt == 0)
                    context.Fixieren_Insert(66666, GuidId, fixViewId);
            }

            MessageBox.Show("DONE!");
        }

        private void btnAddGreen_Click(object sender, EventArgs e)
        {
            if (!MainClass.IsPasha())
                return;

            using (PriemEntities context = new PriemEntities())
            {
                context.FirstWaveGreen_DeleteByAbId(GuidId);
                context.FirstWaveGreen_INSERT(GuidId, true);

                MessageBox.Show("DONE!");
            }
        }

        private void btnAddToSite_Click(object sender, EventArgs e)
        {
            if (!MainClass.IsPasha())
                return;

            using (PriemEntities context = new PriemEntities())
            {
                int? fixnum = (from fx in context.Fixieren
                               where fx.AbiturientId == GuidId
                               select fx.Number).FirstOrDefault();

                if (fixnum == null)
                    return;

                context.FirstWave_INSERT(GuidId, fixnum);
                MessageBox.Show("DONE!");
            }
        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            if (_Id == null)
                return;

            if (abitBarcode == null || abitBarcode == 0)
                return;

            using (PriemEntities context = new PriemEntities())
            {
                int? persBarcode = (from ab in context.extAbitAspirant
                                    join pers in context.Person
                                    on ab.PersonId equals pers.Id
                                    where ab.Id == GuidId
                                    select pers.Barcode).FirstOrDefault();

                if (persBarcode == null || persBarcode == 0)
                    return;

                new DocCard(persBarcode.Value, abitBarcode.Value).Show();
            }
        }

        private void btnDocInventory_Click(object sender, EventArgs e)
        {
            new CardDocInventory(GuidId, !_isModified).Show();
        }
    }
}
