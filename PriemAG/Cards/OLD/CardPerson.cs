using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Objects;
using System.Transactions;

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class CardPerson : CardFromList
    { 
        private int? personBarc;
     
        private bool inEnableProtocol;
        private bool inEntryView;        

        // конструктор формы
        public CardPerson(string id, int? rowInd, BaseFormEx formOwner)
        {
            InitializeComponent();
            _Id = id;             
            tcCard = tabCard;
            
            this.formOwner = formOwner;
            if(rowInd.HasValue)
                this.ownerRowIndex = rowInd.Value; 

            InitControls();     
        }
        
        public CardPerson()
            : this(null, null, null)
        {
        }

        // конструктор формы открытие и создание в нашей базе        
        public CardPerson(string id)
            : this(id, null, null)
        {                        
        }

        protected override void ExtraInit()
        { 
            base.ExtraInit();                        
            _tableName = "ed.Person";
            
            Dgv = dgvApplications;
            personBarc = 0;  

            rbMale.Checked = true;

            gbAtt.Visible = true;
            gbDipl.Visible = false;
            chbEkvivEduc.Visible = false;
            
            chbHostelAbitYes.Checked = false;
            chbHostelAbitNo.Checked = false;

            lblHasAssignToHostel.Visible = false;
            lblHasExamPass.Visible = false;
            btnPrintHostel.Enabled = false;
            btnPrintExamPass.Enabled = false;
            btnGetAssignToHostel.Enabled = false;
            btnGetExamPass.Enabled = false; 
            
            tbNum.Enabled = false;
            
            btnAddAbit.Enabled = false;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ComboServ.FillCombo(cbPassportType, HelpClass.GetComboListByTable("ed.PassportType", " ORDER BY Id "), false, false);

                    ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("ed.Country", "ORDER BY Distance, Name"), false, false);
                    ComboServ.FillCombo(cbNationality, HelpClass.GetComboListByTable("ed.Country", "ORDER BY Distance, Name"), false, false);
                    ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("ed.Region", "ORDER BY Distance, Name"), true, false);
                    ComboServ.FillCombo(cbRegionEduc, HelpClass.GetComboListByTable("ed.Region", "ORDER BY Distance, Name"), true, false);
                    ComboServ.FillCombo(cbLanguage, HelpClass.GetComboListByTable("ed.Language"), false, false);
                    ComboServ.FillCombo(cbCountryEduc, HelpClass.GetComboListByTable("ed.Country", "ORDER BY Distance, Name"), false, false);
                    ComboServ.FillCombo(cbHEStudyForm, HelpClass.GetComboListByTable("ed.StudyForm"), true, false);

                    cbSchoolCity.DataSource = context.ExecuteStoreQuery<string>("SELECT DISTINCT ed.Person_EducationInfo.SchoolCity AS Name FROM ed.Person_EducationInfo WHERE ed.Person_EducationInfo.SchoolCity > '' ORDER BY 1");
                    cbAttestatSeries.DataSource = context.ExecuteStoreQuery<string>("SELECT DISTINCT ed.Person_EducationInfo.AttestatSeries AS Name FROM ed.Person_EducationInfo WHERE ed.Person_EducationInfo.AttestatSeries > '' ORDER BY 1");
                    cbHEQualification.DataSource = context.ExecuteStoreQuery<string>("SELECT DISTINCT ed.Person_EducationInfo.HEQualification AS Name FROM ed.Person_EducationInfo WHERE NOT ed.Person_EducationInfo.HEQualification IS NULL /*AND ed.Person_EducationInfo.HEQualification > ''*/ ORDER BY 1");

                    cbAttestatSeries.SelectedIndex = -1;
                    cbSchoolCity.SelectedIndex = -1;
                    cbHEQualification.SelectedIndex = -1;
                }

                
                btnDocs.Visible = true;

                ComboServ.FillCombo(cbSchoolType, HelpClass.GetComboListByQuery("SELECT Cast(ed.SchoolType.Id as nvarchar(100)) AS Id, ed.SchoolType.Name FROM ed.SchoolType WHERE ed.SchoolType.Id = 4 ORDER BY 1"), false, false);
                tbSchoolNum.Visible = false;
                tbSchoolName.Width = 200;
                lblSchoolNum.Visible = false;
                gbAtt.Visible = false;
                gbDipl.Visible = true;
                chbIsExcellent.Text = "Диплом с отличием";
                btnAttMarks.Visible = false;
                gbSchool.Visible = false;                    

                gbEduc.Location = new Point(11, 7);
                gbFinishStudy.Location = new Point(11, 222);
                
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }
        }

        protected override bool IsForReadOnly()
        {
            return !MainClass.RightsToEditCards();
        }

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();                  
    
            if(_Id == null)
                btnDocs.Enabled = false;
        }
        
        #region handlers
               

        protected override void InitHandlers()
        {
            cbSchoolType.SelectedIndexChanged += new EventHandler(UpdateAfterSchool);
            cbCountry.SelectedIndexChanged += new EventHandler(UpdateAfterCountry);
            cbCountryEduc.SelectedIndexChanged += new EventHandler(UpdateAfterCountryEduc);
        }

        protected override void NullHandlers()
        {
            cbSchoolType.SelectedIndexChanged -= new EventHandler(UpdateAfterSchool);
            cbCountry.SelectedIndexChanged -= new EventHandler(UpdateAfterCountry);
            cbCountryEduc.SelectedIndexChanged -= new EventHandler(UpdateAfterCountryEduc);
        }        

        private void UpdateAfterSchool(object sender, EventArgs e)
        {
            if (SchoolTypeId == MainClass.educSchoolId)
            {
                gbAtt.Visible = true;
                gbDipl.Visible = false;
                tbSchoolName.Width = 217;
            }               
            else
            {
                if (SchoolTypeId == 4)
                    tbSchoolName.Width = 281;
                else
                    tbSchoolName.Width = 217;
                gbAtt.Visible = false;
                gbDipl.Visible = true;
            }
        }

        private void UpdateAfterCountry(object sender, EventArgs e)
        {
            if (CountryId == MainClass.countryRussiaId)
            {
                cbRegion.Enabled = true;
                cbRegion.SelectedItem = "нет";
            }
            else
            {
                cbRegion.Enabled = false;
                cbRegion.SelectedItem = "нет";
            }
        }

        private void UpdateAfterCountryEduc(object sender, EventArgs e)
        {
            if (CountryEducId == MainClass.countryRussiaId)           
                chbEkvivEduc.Visible = false;
            else
                chbEkvivEduc.Visible = true;
        }

        private void chbHostelAbitYes_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelAbitNo.Checked = !chbHostelAbitYes.Checked;
            if (_Id != null)
                btnGetAssignToHostel.Enabled = chbHostelAbitYes.Checked;
        }

        private void chbHostelAbitNo_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelAbitYes.Checked = !chbHostelAbitNo.Checked;
            if (_Id != null)
                btnGetAssignToHostel.Enabled = !chbHostelAbitNo.Checked;
        }

        private void chbHostelEducYes_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducNo.Checked = !chbHostelEducYes.Checked;
        }

        private void chbHostelEducNo_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducYes.Checked = !chbHostelEducNo.Checked;
        }
        #endregion

        private void FillPersonData(ref extPersonAll person)
        {
            CardTitle = Util.GetFIO(person.Surname, person.Name, person.SecondName);
            PersonName = person.Name;
            SecondName = person.SecondName;
            Surname = person.Surname;
            BirthDate = person.BirthDate;
            BirthPlace = person.BirthPlace;
            PassportTypeId = person.PassportTypeId;
            PassportSeries = person.PassportSeries;
            PassportNumber = person.PassportNumber;
            PassportAuthor = person.PassportAuthor;
            PassportDate = person.PassportDate;
            PassportCode = person.PassportCode;
            PersonalCode = person.PersonalCode;
            Sex = person.Sex;
            CountryId = person.CountryId;
            NationalityId = person.NationalityId;
            RegionId = person.RegionId;
            Phone = person.Phone;
            Mobiles = person.Mobiles;
            Email = person.Email;
            Code = person.Code;
            City = person.City;
            Street = person.Street;
            House = person.House;
            Korpus = person.Korpus;
            Flat = person.Flat;
            CodeReal = person.CodeReal;
            CityReal = person.CityReal;
            StreetReal = person.StreetReal;
            HouseReal = person.HouseReal;
            KorpusReal = person.KorpusReal;
            FlatReal = person.FlatReal;
            HostelAbit = person.HostelAbit ?? false;
            HostelEduc = person.HostelEduc ?? false;
            HasAssignToHostel = person.HasAssignToHostel ?? false;
            HostelFacultyId = person.HostelFacultyId;
            HasExamPass = person.HasExamPass ?? false;
            ExamPassFacultyId = person.ExamPassFacultyId;
            IsExcellent = person.IsExcellent ?? false;
            LanguageId = person.LanguageId;
            SchoolCity = person.SchoolCity;
            SchoolTypeId = person.SchoolTypeId;
            SchoolName = person.SchoolName;
            SchoolNum = person.SchoolNum;
            SchoolExitYear = person.SchoolExitYear;
            CountryEducId = person.CountryEducId;
            RegionEducId = person.RegionEducId;
            IsEqual = person.IsEqual ?? false;
            AttestatRegion = person.AttestatRegion;
            AttestatSeries = person.AttestatSeries;
            AttestatNum = person.AttestatNum;
            DiplomSeries = person.DiplomSeries;
            DiplomNum = person.DiplomNum;
            SchoolAVG = person.SchoolAVG;
            HighEducation = person.HighEducation;
            HEProfession = person.HEProfession;
            HEQualification = person.HEQualification;
            HEEntryYear = person.HEEntryYear;
            HEExitYear = person.HEExitYear;
            HEWork = person.HEWork;
            HEStudyFormId = person.HEStudyFormId;
            Stag = person.Stag;
            WorkPlace = person.WorkPlace;
            ExtraInfo = person.ExtraInfo;
            PersonInfo = person.PersonInfo;
            ScienceWork = person.ScienceWork;
            personBarc = person.Barcode;

            HasGrant = person.HasGrant ?? false;
            VAKPublCount = person.VAKPublCount;
            TotalPublCount = person.TotalPublCount;
            CompetitionWinner = person.CompetitionWinner ?? false;
        }

        //данные из нашей базы
        protected override void  FillCard()
        {
            if (_Id == null)
                return;                   
                        
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    extPersonAll person = (from pr in context.extPersonAll
                                     where pr.Id == GuidId
                                     select pr).FirstOrDefault();

                    tbNum.Text = person.PersonNum;
                    FillPersonData(ref person);

                    FillApplications();

                    inEnableProtocol = GetInEnableProtocol(context);
                    inEntryView = GetInEntryView(context);
                }
            }
            catch (DataException de)
            {
                WinFormsServ.Error("Ошибка при заполнении формы (DataException)" + de.Message);
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + ex.Message);
            }
        }
           
        public void FillApplications()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    string queryOwn = string.Format("SELECT extAbitAspirant.Id AS Id, extAbitAspirant.FacultyAcr AS Факультет, extAbitAspirant.ProfessionCode + ' ' + extAbitAspirant.Profession AS Направление, " +
                                      "extAbitAspirant.ObrazProgram AS Образ_программа, extAbitAspirant.Specialization AS Профиль, " +
                                      "extAbitAspirant.StudyFormAcr AS Форма_обучения, extAbitAspirant.StudyBasisAcr AS Основа_обучения " +
                                      "FROM extAbitAspirant WHERE extAbitAspirant.BackDoc = 0 AND extAbitAspirant.PersonId = '{0}' ORDER BY 2, 3", _Id);


                    string queryAll = string.Format("SELECT AllAbits.Id AS Id, AllAbits.FacultyAcr AS Факультет, AllAbits.ProfessionCode + ' ' + AllAbits.Profession AS Направление, " +
                                      "AllAbits.ObrazProgram AS Образ_программа, AllAbits.Specialization AS Профиль, " +
                                      "AllAbits.StudyFormAcr AS Форма_обучения, AllAbits.StudyBasisAcr AS Основа_обучения " +
                                      "FROM AllAbits WHERE AllAbits.BackDoc = 0 AND AllAbits.PersonId = '{0}' " +
                                      "EXCEPT {1} ORDER BY 2, 3", _Id, queryOwn);

                    var sourceOwn = from abit in context.qAbiturient
                                    where !abit.BackDoc && abit.PersonId == GuidId
                                    orderby abit.FacultyAcr, abit.ObrazProgramCrypt
                                    select new
                                    {
                                        abit.Id,
                                        Факультет = abit.FacultyAcr,
                                        Направление = abit.LicenseProgramName,
                                        Образ_программа = abit.ObrazProgramCrypt,
                                        Образ_программа_шифр = abit.ObrazProgramName,
                                        Профиль = abit.ProfileName,
                                        Форма_обучения = abit.StudyBasisName,
                                        Основа_обучения = abit.StudyFormName
                                    };

                    var sourceAll = (from abit in context.qAbitAll
                                    where !abit.BackDoc && abit.PersonId == GuidId
                                    orderby abit.FacultyAcr, abit.LicenseProgramName
                                    select new
                                    {
                                        abit.Id,
                                        Факультет = abit.FacultyAcr,
                                        Направление = abit.LicenseProgramName,
                                        Образ_программа = abit.ObrazProgramCrypt,
                                        Образ_программа_шифр = abit.ObrazProgramName,
                                        Профиль = abit.ProfileName,
                                        Форма_обучения = abit.StudyBasisName,
                                        Основа_обучения = abit.StudyFormName
                                    }).Except
                                    (from abit in context.qAbiturient
                                     where !abit.BackDoc && abit.PersonId == GuidId
                                     orderby abit.FacultyAcr, abit.ObrazProgramCrypt
                                     select new
                                     {
                                         abit.Id,
                                         Факультет = abit.FacultyAcr,
                                         Направление = abit.LicenseProgramName,
                                         Образ_программа = abit.ObrazProgramCrypt,
                                         Образ_программа_шифр = abit.ObrazProgramName,
                                         Профиль = abit.ProfileName,
                                         Форма_обучения = abit.StudyBasisName,
                                         Основа_обучения = abit.StudyFormName
                                     });

                    dgvApplications.DataSource = sourceOwn;
                    dgvApplications.Columns["Id"].Visible = false;
                    dgvOtherAppl.DataSource = sourceAll;
                    dgvOtherAppl.Columns["Id"].Visible = false;

                    // после зачисления раскомментить
                    var entries = (from ev in context.extEntryProtocols
                                  join ab in context.extAbitAspirant
                                  on ev.AbiturientId equals ab.Id
                                  where !ab.BackDoc && ab.PersonId == GuidId
                                  select ab).FirstOrDefault();

                    if(entries == null)                    
                        gbEnter.Visible = false;
                    else
                    {
                        gbEnter.Visible = true;
                        lblFaculty.Text = entries.FacultyName;
                        lblStudyForm.Text = entries.StudyFormName;
                        lblStudyBasis.Text = entries.StudyBasisName;
                        lblProfession.Text = entries.LicenseProgramCode + " " + entries.LicenseProgramName;
                        lblObrazProgram.Text = entries.ObrazProgramCrypt + " " + entries.ObrazProgramName;
                        lblProfile.Text = entries.ProfileId == null ? "" : entries.ProfileName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        // возвращает, есть ли человек в протоколе о допуске
        private bool GetInEnableProtocol(PriemEntities context)
        {  
            List<Guid> lstAbits = (from ab in context.Abiturient
                                  where ab.PersonId == GuidId
                                  select ab.Id).ToList();

            int cntProt = (from ph in context.extProtocol
                          where ph.ProtocolTypeId == 1 && !ph.IsOld && !ph.Excluded && lstAbits.Contains(ph.AbiturientId)
                          select ph.AbiturientId).Count();
            if (cntProt > 0)
                return true;
            else
                return false;     
        }
        
        // возвращает, есть ли человек в представлении к зачислению
        private bool GetInEntryView(PriemEntities context)
        {
            List<Guid> lstAbits = (from ab in context.Abiturient
                                   where ab.PersonId == GuidId
                                   select ab.Id).ToList();

            int cntProt = (from ph in context.extEntryView
                           where lstAbits.Contains(ph.AbiturientId)
                           select ph.AbiturientId).Count();
            
            if (cntProt > 0)
                return true;
            else
                return false;
        }

        private bool GetInEntryViewAspirant()
        {
            using (PriemEntities context = new PriemEntities())
            {
                List<Guid> lstAbits = (from ab in context.Abiturient
                                       where ab.PersonId == GuidId
                                       select ab.Id).ToList();

                int cntProt = (from ph in context.extEntryView
                               where lstAbits.Contains(ph.AbiturientId) && ph.StudyLevelGroupId == MainClass.studyLevelGroupId
                               select ph.AbiturientId).Count();

                if (cntProt > 0)
                    return true;
                else
                    return false;
            }
        }

        #region ReadOnly & IsOpen

        // карточка открывается в режиме read-only
        protected override void  SetAllFieldsNotEnabled()
        {
            base.SetAllFieldsNotEnabled();
            
            dgvApplications.Enabled = true;
            dgvOtherAppl.Enabled = true;
            
            btnAttMarks.Enabled = true;           
                        
            if (HasAssignToHostel && MainClass.RightsFacMain() && MainClass.HasRightsForFaculty(HostelFacultyId))
                SetBtnPrintHostelEnabled();            
            
            if (HasExamPass && MainClass.RightsFacMain() && MainClass.HasRightsForFaculty(ExamPassFacultyId))
                SetBtnPrintExamPassEnabled();

            if (!IsForReadOnly() && !inEntryView)
                btnAddAbit.Enabled = true;            

            btnDocs.Enabled = true;
        }

        //убрать режим read-only
        protected override void SetAllFieldsEnabled()
        {
            base.SetAllFieldsEnabled();
            
            btnPrintHostel.Enabled = false;
            btnPrintExamPass.Enabled = false;

            if (HasAssignToHostel)
            {
                chbHostelAbitYes.Enabled = chbHostelAbitNo.Enabled = false;
                btnGetAssignToHostel.Enabled = false;

                if (MainClass.RightsFacMain() && MainClass.HasRightsForFaculty(HostelFacultyId))
                    btnPrintHostel.Enabled = true;
            }
            else
            {
                if(chbHostelAbitYes.Checked)
                    btnGetAssignToHostel.Enabled = true;
                else
                    btnGetAssignToHostel.Enabled = false;
            }           

            if (HasExamPass)
            {
                btnGetExamPass.Enabled = false;

                if (MainClass.RightsFacMain() && MainClass.HasRightsForFaculty(ExamPassFacultyId))
                    btnPrintExamPass.Enabled = true;                    
            }
            else            
                btnGetExamPass.Enabled = true;              
          
            btnAttMarks.Enabled = true;            

            tbNum.Enabled = false;
            gbEnter.Enabled = false;
        }

        // закрытие части полей в зависимости от прав
        protected override void SetReadOnlyFields()
        {
            if (MainClass.RightsFaculty())
            {
                tbName.Enabled = false;
                tbSurname.Enabled = false;
                tbSecondName.Enabled = false;

                dtBirthDate.Enabled = false;

                cbPassportType.Enabled = false;
                tbPassportAuthor.Enabled = false;
                tbPassportNumber.Enabled = false;
                tbPassportSeries.Enabled = false;
                dtPassportDate.Enabled = false;

                tbAttestatRegion.Enabled = false;
                tbAttestatNum.Enabled = false;
                cbAttestatSeries.Enabled = false;

                btnAttMarks.Enabled = true;
            }

            if (inEnableProtocol && MainClass.RightsFaculty())
            {
                SetAllFieldsNotEnabled();

                tbMobiles.Enabled = true;
                gbStag.Enabled = true;
                gbPersonInfo.Enabled = true;

                tbDiplomNum.Enabled = true;
                tbDiplomSeries.Enabled = true;
                
                btnSaveChange.Enabled = true;
                btnClose.Enabled = true;
                btnAddAbit.Enabled = true;

                //попросили, чтобы можно было добавлять даже зачисленным в протокол о допуске
                gbEduc.Enabled = true;
                btnAttMarks.Enabled = true;                
            }

            if (inEnableProtocol && MainClass.RightsSov_SovMain_FacMain())
            {
                tbName.Enabled = false;
                tbSurname.Enabled = false;
                tbSecondName.Enabled = false;

                dtBirthDate.Enabled = false;

                cbPassportType.Enabled = false;
                tbPassportAuthor.Enabled = false;
                tbPassportNumber.Enabled = false;
                tbPassportSeries.Enabled = false;
                dtPassportDate.Enabled = false;

                tbAttestatRegion.Enabled = false;
                tbAttestatNum.Enabled = false;
                cbAttestatSeries.Enabled = false;
            }            

            // закрываем для создания новых для уже зачисленных
            if (inEntryView)
            {
                //если это зачисленные в аспирантуру, то не трогать, иначе разрешить
                if (GetInEntryViewAspirant())
                    btnAddAbit.Enabled = false;
                //btnAddAbit.Enabled = false;
                chbIsExcellent.Enabled = false;
                tbSchoolAVG.Enabled = false;
            }
        }        

        private void SetBtnPrintHostelEnabled()
        {
            gbHostel.Enabled = true;
            btnGetAssignToHostel.Enabled = false;
            btnPrintHostel.Enabled = true;
        }

        private void SetBtnPrintExamPassEnabled()
        {
            gbExamPass.Enabled = true;
            btnGetExamPass.Enabled = false;
            btnPrintExamPass.Enabled = true;
        }
        
        #endregion

        #region Save

        // проверка на уникальность абитуриента
        private bool CheckIdent()
        {
            using (PriemEntities context = new PriemEntities())
            {
                ObjectParameter boolPar = new ObjectParameter("result", typeof(bool));

                if(_Id == null)
                    context.CheckPersonIdent(Surname, Name, SecondName, BirthDate, PassportSeries, PassportNumber, AttestatRegion, AttestatSeries, AttestatNum, boolPar);
                else
                    context.CheckPersonIdentWithId(Surname, Name, SecondName, BirthDate, PassportSeries, PassportNumber, AttestatRegion, AttestatSeries, AttestatNum, GuidId, boolPar);

                return Convert.ToBoolean(boolPar.Value);
            }
        }

        protected override bool  CheckFields()
        { 
            // проверка на уникальность номера
            
            if (Surname.Length <= 0)
            {
                epErrorInput.SetError(tbSurname, "Отсутствует фамилия абитуриента");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();
            
            if (PersonName.Length <= 0)
            {
                epErrorInput.SetError(tbName, "Отсутствует имя абитуриента");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            //Для О'Коннор сделал добавку в регулярное выражение: \'
            if (!Regex.IsMatch(Surname, @"^[А-Яа-яёЁ\-\'\s]+$"))
            {
                epErrorInput.SetError(tbSurname, "Неправильный формат");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (!Regex.IsMatch(PersonName, @"^[А-Яа-яёЁ\-\s]+$"))
            {
                epErrorInput.SetError(tbName, "Неправильный формат");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (!Regex.IsMatch(SecondName, @"^[А-Яа-яёЁ\-\s]*$"))
            {
                epErrorInput.SetError(tbSecondName, "Неправильный формат");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (SecondName.StartsWith("-"))
            {
                SecondName = SecondName.Replace("-", "");                
            }           
            
            // проверка на англ. буквы
            if (!Util.IsRussianString(PersonName))
            {
                epErrorInput.SetError(tbName, "Имя содержит английские символы, используйте только русскую раскладку");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (!Util.IsRussianString(Surname))
            {
                epErrorInput.SetError(tbSurname, "Фамилия содержит английские символы, используйте только русскую раскладку");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (!Util.IsRussianString(SecondName))
            {
                epErrorInput.SetError(tbSecondName, "Отчество содержит английские символы, используйте только русскую раскладку");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();           
                       
            if (BirthDate == null)
            {
                epErrorInput.SetError(dtBirthDate, "Неправильно указана дата");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            int checkYear = DateTime.Now.Year - 12;
            if (BirthDate.Value.Year > checkYear || BirthDate.Value.Year < 1920)            
            {
                epErrorInput.SetError(dtBirthDate, "Неправильно указана дата");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (PassportDate.Value.Year > DateTime.Now.Year || PassportDate.Value.Year < 1970)
            {
                epErrorInput.SetError(dtPassportDate, "Неправильно указана дата");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();
            
            if (PassportTypeId == MainClass.pasptypeRFId)
            {
                if (!(PassportSeries.Length == 4))
                {
                    epErrorInput.SetError(tbPassportSeries, "Неправильно введена серия паспорта абитуриента");
                    tabCard.SelectedIndex = 0;
                    return false;
                }
                else
                    epErrorInput.Clear();

                if (!(PassportNumber.Length == 6))
                {
                    epErrorInput.SetError(tbPassportNumber, "Неправильно введен номер паспорта абитуриента");
                    tabCard.SelectedIndex = 0;
                    return false;
                }
                else
                    epErrorInput.Clear();
            }
            
            if (NationalityId == MainClass.countryRussiaId)
            {
                if (PassportSeries.Length <= 0)
                {
                    epErrorInput.SetError(tbPassportSeries, "Отсутствует серия паспорта абитуриента");
                    tabCard.SelectedIndex = 0;
                    return false;
                }
                else
                    epErrorInput.Clear();

                if (PassportNumber.Length <= 0)
                {
                    epErrorInput.SetError(tbPassportNumber, "Отсутствует номер паспорта абитуриента");
                    tabCard.SelectedIndex = 0;
                    return false;
                }
                else
                    epErrorInput.Clear();
            }
              
            if (PassportSeries.Length > 10)
            {
                epErrorInput.SetError(tbPassportSeries, "Слишком длинное значение серии паспорта абитуриента");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();
           

            if (PassportNumber.Length > 20)
            {
                epErrorInput.SetError(tbPassportNumber, "Слишком длинное значение номера паспорта абитуриента");
                tabCard.SelectedIndex = 0;
                return false;
            }
            else
                epErrorInput.Clear();

            if (!chbHostelAbitYes.Checked && !chbHostelAbitNo.Checked)
            {
                epErrorInput.SetError(chbHostelAbitNo, "Не указаны данные о предоставлении общежития");
                tabCard.SelectedIndex = 1;
                return false;
            }
            else
                epErrorInput.Clear();                

            if (!Regex.IsMatch(SchoolExitYear.ToString(), @"^\d{0,4}$"))
            {
                epErrorInput.SetError(tbSchoolExitYear, "Неправильно указан год");
                tabCard.SelectedIndex = 2;
                return false;
            }
            else
                epErrorInput.Clear();

            // проверка региона аттестата - нужна ли?
            if (CountryEducId == 1)
            {
                //if (gbAtt.Visible && !Regex.IsMatch(tbAttestatRegion.Text.Trim(), @"^\w{2}$"))
                //{
                //    epErrorInput.SetError(tbAttestatRegion, "Неправильно указан регион аттестата");
                //    tabCard.SelectedIndex = 2;
                //    return false;
                //}
                //else
                //    epErrorInput.Clear();
               

                if (gbAtt.Visible && AttestatSeries.Length <= 0)
                {
                    epErrorInput.SetError(cbAttestatSeries, "Отсутствует серия аттестата абитуриента");
                    tabCard.SelectedIndex = 2;
                    return false;
                }
                else
                    epErrorInput.Clear();
            }

            if (gbAtt.Visible && AttestatNum.Length <= 0)
            {
                epErrorInput.SetError(tbAttestatNum, "Отсутствует номер аттестата абитуриента");
                tabCard.SelectedIndex = 2;
                return false;
            }
            else
                epErrorInput.Clear();

            double d = 0;
            if (tbSchoolAVG.Text.Trim() != "")
            {
                if (!double.TryParse(tbSchoolAVG.Text.Trim().Replace(".", ","), out d))
                {
                    epErrorInput.SetError(tbSchoolAVG, "Неправильный формат");
                    tabCard.SelectedIndex = 2;
                    return false;
                }
                else
                    epErrorInput.Clear();

            }

            if (MainClass.dbType == PriemType.Priem)
            {
                //if (gbDipl.Visible && tbDiplomNum.Text.Trim().Length <= 0)
                //{
                //    epErrorInput.SetError(tbDiplomNum, "Отсутствует номер диплома абитуриента");
                //    tabCard.SelectedIndex = 2;
                //    return false;
                //}
                //else
                //    epErrorInput.Clear();
            }
            
            if (!CheckIdent())
            {
                WinFormsServ.Error("В базе уже существует абитуриент с такими же либо ФИО, либо данными паспорта, либо данными аттестата!");
                return false;
            }

            return true;
        }

        protected override void InsertRec(PriemEntities context, ObjectParameter idParam)
        {
            context.Person_insert(personBarc, PersonName, SecondName, Surname, BirthDate, BirthPlace, PassportTypeId, PassportSeries, PassportNumber,
                PassportAuthor, PassportDate, Sex, CountryId, NationalityId, RegionId, Phone, Mobiles, Email,
                Code, City, Street, House, Korpus, Flat, CodeReal, CityReal, StreetReal, HouseReal, KorpusReal, FlatReal, KladrCode, HostelAbit, HostelEduc, HasAssignToHostel,
                HostelFacultyId, HasExamPass, ExamPassFacultyId, IsExcellent, LanguageId, SchoolCity, SchoolTypeId, SchoolName, SchoolNum, SchoolExitYear,
                SchoolAVG, CountryEducId, RegionEducId, IsEqual, AttestatRegion, AttestatSeries, AttestatNum, DiplomSeries, DiplomNum, HighEducation, HEProfession,
                HEQualification, HEEntryYear, HEExitYear, HEStudyFormId, HEWork, Stag, WorkPlace, null, null, null, 0, PassportCode,
                PersonalCode, PersonInfo, ExtraInfo, ScienceWork, false, null, false, SNILS, idParam);

            Guid PersonId = (Guid)idParam.Value;

            context.Person_AspirantAddInfo_update(VAKPublCount, TotalPublCount, CompetitionWinner, HasGrant, PersonId);
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
            context.Person_UpdateWithoutMain(BirthPlace, Sex, CountryId, NationalityId, RegionId, Phone, Mobiles, Email,
                Code, City, Street, House, Korpus, Flat, CodeReal, CityReal, StreetReal, HouseReal, KorpusReal, FlatReal, KladrCode, HostelAbit, HostelEduc, HasAssignToHostel,
                HostelFacultyId, HasExamPass, ExamPassFacultyId, IsExcellent, LanguageId, SchoolCity, SchoolTypeId, SchoolName, SchoolNum, SchoolExitYear,
                SchoolAVG, CountryEducId, RegionEducId, IsEqual, DiplomSeries, DiplomNum, HighEducation, HEProfession,
                HEQualification, HEEntryYear, HEExitYear, HEStudyFormId, HEWork, Stag, WorkPlace, null, null, null, PassportCode,
                PersonalCode, PersonInfo, ExtraInfo, ScienceWork, false, null, false, id);

            if (MainClass.RightsSov_SovMain_FacMain() || MainClass.IsPasha())
                context.Person_UpdateMain(PersonName, SecondName, Surname, BirthDate, PassportTypeId, PassportSeries, PassportNumber,
                PassportAuthor, PassportDate, AttestatRegion, AttestatSeries, AttestatNum, 0, SNILS, id);

            //уточнить у Паши, кто может расставлять данные по приоритетам аспирантов!!!!
            context.Person_AspirantAddInfo_update(VAKPublCount, TotalPublCount, CompetitionWinner, HasGrant, id);
        }                    
                 
        protected override void OnSave()
        {
            CardTitle = Util.GetFIO(Surname, PersonName, SecondName);          
            btnAddAbit.Enabled = true;
           
            MainClass.DataRefresh();
        }

        protected override void  OnSaveNew()
        {
            using (PriemEntities context = new PriemEntities())
            {
                string num = (from pr in context.extPersonAspirant
                             where pr.Id == GuidId
                             select pr.PersonNum).FirstOrDefault().ToString();

                tbNum.Text = num;
            }
        }

        #endregion 

        private void tabCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D1)
                this.tabCard.SelectedIndex = 0;
            if (e.Control && e.KeyCode == Keys.D2)
                this.tabCard.SelectedIndex = 1;
            if (e.Control && e.KeyCode == Keys.D3)
                this.tabCard.SelectedIndex = 2;
            if (e.Control && e.KeyCode == Keys.D4)
                this.tabCard.SelectedIndex = 3;
            if (e.Control && e.KeyCode == Keys.D5)
                this.tabCard.SelectedIndex = 4;
            if (e.Control && e.KeyCode == Keys.D6)
                this.tabCard.SelectedIndex = 5;

            if (e.Control && e.KeyCode == Keys.S)
                SaveClick();                
            if (e.Control && e.KeyCode == Keys.N)
                AddAbitClick();
        }        

        private void CardPerson_Click(object sender, EventArgs e)
        {
            this.Activate();
        }

        #region Abit

        private void dgvApplications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                OpenCardAbit();
        }

        private void OpenCardAbit()
        {
            if (dgvApplications.CurrentCell != null && dgvApplications.CurrentCell.RowIndex > -1)
            {
                string abId = dgvApplications.Rows[dgvApplications.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (abId != "")
                {
                    MainClassCards.OpenCardAbit(abId, this, dgvApplications.CurrentRow.Index);
                }
            }
        }

        private void AddAbitClick()
        {
            if (btnAddAbit.Visible && btnAddAbit.Enabled)
            {
                CardAbit crd = new CardAbit(GuidId);
                crd.Show();
            }
        }

        private void btnAddAbit_Click(object sender, EventArgs e)
        {
            AddAbitClick();
        }

        #endregion

        private void btnAttMarks_Click(object sender, EventArgs e)
        {
            CardAttMarks am;

            if (_Id == null)
            {              
                if (MessageBox.Show("Данное действие приведет к сохранению записи, продолжить?", "Сохранить", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (SaveClick())
                        {
                            am = new CardAttMarks(GuidId, !_isModified);
                            am.ShowDialog();
                        }                     
                    }
                    catch (Exception de)
                    {
                        WinFormsServ.Error("Ошибка сохранения данных" + de.Message);                        
                    }
                }
            }
            else
            {
                am = new CardAttMarks(GuidId, !_isModified);
                am.ShowDialog();
            }
        }     
               
        private void btnDocs_Click(object sender, EventArgs e)
        {
            if (_Id == null)
                return;

            if (personBarc == 0)
            {
                MessageBox.Show("Даная анкета была заведена вручную");
                return;
            }

            if(personBarc != null)
                new DocCard(personBarc.Value, null).Show();
        }

        private void btnGetAssignToHostel_Click(object sender, EventArgs e)
        {
            using (PriemEntities context = new PriemEntities())
            {
                if (_Id == null)
                    return;

                if (HasAssignToHostel)
                    return;

                int facId = MainClass.GetFacultyIdInt();

                string facName = (from fac in context.qFaculty
                                  where fac.Id == facId
                                  select fac.Name).FirstOrDefault();
               
                if (MessageBox.Show(string.Format("Будет выдано направление на поселение. Факультет: {0}, продолжить?", facName), "Сохранить", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    context.Person_UpdateHostel(true, facId, GuidId);                   
                    HasAssignToHostel = true;
                    HostelFacultyId = facId;

                    btnGetAssignToHostel.Enabled = false;

                    if (MainClass.RightsFacMain())
                        btnPrintHostel.Enabled = true;
                    
                    btnPrintHostel_Click(null, null);
                }
            }
        }

        private void btnGetExamPass_Click(object sender, EventArgs e)
        {
            using (PriemEntities context = new PriemEntities())
            {
                if (_Id == null)
                    return;

                if (HasExamPass)
                    return;

                int facId = MainClass.GetFacultyIdInt();

                string facName = (from fac in context.qFaculty
                                  where fac.Id == facId
                                  select fac.Name).FirstOrDefault();                
               
                if (MessageBox.Show(string.Format("Будет выдан экзаменационный пропуск. Факультет: {0}, продолжить?", facName), "Сохранить", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                    
                    context.Person_UpdateExamPass(true, facId, GuidId);                   
                    HasExamPass = true;
                    ExamPassFacultyId = facId;

                    btnGetExamPass.Enabled = false;

                    if (MainClass.RightsFacMain())
                        btnPrintExamPass.Enabled = true;
                    btnPrintExamPass_Click(null, null);                    
                }
            }
        }      

        private void btnPrintHostel_Click(object sender, EventArgs e)
        {
            if (_Id == null)
                return;

            sfdPrint.FileName = string.Format("{0} - направление на поселение.pdf", tbSurname.Text);
            sfdPrint.Filter = "ADOBE Pdf files|*.pdf";
            if (sfdPrint.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Print.PrintHostelDirection(GuidId, chbPrint.Checked, sfdPrint.FileName);
        }

        private void btnPrintExamPass_Click(object sender, EventArgs e)
        {
            sfdPrint.FileName = string.Format("{0} - ЭкзПропуск.pdf", tbSurname.Text);
            sfdPrint.Filter = "ADOBE Pdf files|*.pdf";
            if (sfdPrint.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Print.PrintExamPass(GuidId, sfdPrint.FileName, chbPrint.Checked);
        }
    }
}
