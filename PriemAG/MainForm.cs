using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class MainForm : Form
    {
        private DBPriem _bdc;
        private string _titleString;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            SetDB();

            try
            {
                if (string.IsNullOrEmpty(MainClass.connString))
                    return;

                MainClass.Init(this);

                _bdc = MainClass.Bdc;
                OpenHelp(string.Format("{0}; Пользователь: {1}", _titleString, MainClass.GetADUserName(System.Environment.UserName)));
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Не удалось подключиться под вашей учетной записью  ", exc);
                msMainMenu.Enabled = false;
            }
        }

        private void SetDB()
        {
            string dbName = ConfigurationManager.AppSettings["Priem"];
            MainClass.connString = DBConstants.CS_PRIEM;
            MainClass.connStringOnline = DBConstants.CS_PriemONLINE;

            switch (dbName)
            {
                case "Priem":
                    _titleString = " на первый курс";
                    MainClass.dbType = PriemType.Priem;
                    break;
                
                case "PriemMAG":
                    _titleString = " в магистратуру";
                    MainClass.dbType = PriemType.PriemMag;
                    break;

                case "PriemASP":
                    _titleString = " в аспирантуру";
                    MainClass.dbType = PriemType.PriemAspirant;
                    break;

                case "Priem_FAC":
                    _titleString = " рабочая 1 курс superman";
                    MainClass.connString = DBConstants.CS_PRIEM_FAC;
                    MainClass.dbType = PriemType.Priem;
                    break;

                case "PriemMAG_FAC":
                    _titleString = " рабочая магистратура superman";
                    MainClass.connString = DBConstants.CS_PRIEM_FAC;
                    MainClass.dbType = PriemType.PriemMag;
                    break;

                case "PriemAsp_FAC":
                    _titleString = " рабочая аспирантура superman";
                    MainClass.connString = DBConstants.CS_PRIEM_FAC;
                    MainClass.dbType = PriemType.PriemAspirant;
                    break;
                case "PriemAG":
                    _titleString = " рабочая академическая гимназия"; 
                    MainClass.dbType = PriemType.PriemAG;

                    break;
                default:
                    WinFormsServ.Error("Проверьте параметры конфиг-файла!");
                    this.Text = "ОШИБКА";
                    return;
            }

            if (MainClass.connString.ToLower().Contains("test;integrated"))
                _titleString += " ТЕСТОВАЯ";
            if (MainClass.connString.Contains("Educ;Integrated"))
                _titleString += " ДЛЯ ОБУЧЕНИЯ";

            this.Text = "ПРИЕМ " + MainClass.sPriemYear + _titleString;
        }

        /// <summary>
        /// extra information for open - what smi are enabled
        /// </summary>
        /// <param name="path"></param>
        public void OpenHelp(string path)
        {
            try
            {
                // убирает все IsOpen для данного пользователя                
                MainClass.DeleteAllOpenByHolder();

                tsslMain.Text = string.Format("Открыта база: Прием в СПбГУ {0} {1}; ", MainClass.sPriemYear, path);
                MainClass.dirTemplates = string.Format(@"{0}\Templates", Application.StartupPath);

                MainClass.InitQueryBuilder();

                ShowProtocolWarning();

                //предупреждение об рабочем режиме базы
                //MessageBox.Show("Уважаемые пользователи!\nСистема находится в рабочем режиме.\nВведение тестовых записей не допускается.", "Внимание");

                if (MainClass.IsOwner())
                    return;

                // магистратура!
                if (MainClass.dbType == PriemType.PriemAspirant)
                {
                    smiOlympAbitList.Visible = false;
                    smiOlymps.Visible = false;
                    smiOlymp2Competition.Visible = false;
                    smiOlymp2Mark.Visible = false;
                }
                else
                {
                    smiOnlineChanges.Visible = false;
                    smiLoad.Visible = false;
                }                
                
                smiRatingList.Visible = false;
                smiOrderNumbers.Visible = false;
                smiOlymps.Visible = false;
                smiCreateVed.Visible = false;
                smiBooks.Visible = false;
                smiCrypto.Visible = false;                
                smiExport.Visible = false;
                smiImport.Visible = false;
                smiExamsVedRoomList.Visible = false;
                //smiProblemSolver.Visible = false;
                smiEntryView.Visible = false;
                smiDisEntryView.Visible = false;

                smiEGEStatistics.Visible = false;
                smiDynamics.Visible = false;
                smiFormA.Visible = false;
                smiForm2.Visible = false;

                smiAbitFacultyIntesection.Visible = false;
                smiRegionStat.Visible = false;
                smiOlympStatistics.Visible = false;

                // Разделение видимости меню
                if (MainClass.IsFacMain())
                {
                    smiOlymps.Visible = true;
                    smiCreateVed.Visible = true;
                    smiExamsVedRoomList.Visible = true;
                    smiRatingList.Visible = true;
                    smiEntryView.Visible = true;
                    smiDisEntryView.Visible = true;
                    smiAbitFacultyIntesection.Visible = true;
                    smiExport.Visible = true;
                }

                if (MainClass.IsEntryChanger())
                {
                    smiBooks.Visible = true;
                    smiEnterManual.Visible = false;
                    smiRatingListPasha.Visible = false;
                    smiRatingList.Visible = true;
                    smiExport.Visible = true;
                }

                if (MainClass.IsPasha())
                {
                    smiCrypto.Visible = true;
                    smiBooks.Visible = true;
                    smiRatingList.Visible = true;
                    smiOrderNumbers.Visible = true;
                    smiExport.Visible = true;
                    smiEntryView.Visible = true;
                    smiDisEntryView.Visible = true;
                    smiEnterManual.Visible = true;
                    smiAppeal.Visible = true;
                    smiDecryptor.Visible = true;                    

                    //Паша попросил добавить себе
                    smiCreateVed.Visible = true;
                    smiExamsVedRoomList.Visible = true;

                    smiRatingListPasha.Visible = true;

                    smiEGEStatistics.Visible = true;
                    smiDynamics.Visible = true;
                    smiFormA.Visible = true;
                    smiForm2.Visible = true;

                    smiAbitFacultyIntesection.Visible = true;
                    smiRegionStat.Visible = true;
                    smiOlympStatistics.Visible = true;
                }

                if (MainClass.IsRectorat())
                {
                    smiEGEStatistics.Visible = true;
                    smiFormA.Visible = true;

                    smiExport.Visible = true;

                    smiAbitFacultyIntesection.Visible = true;
                    smiRegionStat.Visible = true;
                    smiOlympStatistics.Visible = true;
                }

                if (MainClass.IsSovetnik() || MainClass.IsSovetnikMain())
                {
                    smiAbitFacultyIntesection.Visible = true;
                }

                if (MainClass.IsCrypto())
                {
                    smiCrypto.Visible = true;
                    smiExamsVedRoomList.Visible = true;
                    smiAppeal.Visible = false;
                    smiDecryptor.Visible = false;
                    smiLoadMarks.Visible = false;
                }

                if (MainClass.IsCryptoMain())
                {
                    smiCrypto.Visible = true;
                    smiAppeal.Visible = true;
                    smiExamsVedRoomList.Visible = true;

                    //глава шифровалки тоже хочет создавать ведомости
                    smiCreateVed.Visible = true;
                   
                    smiDecryptor.Visible = false;
                    smiLoadMarks.Visible = false;
                }

                if (MainClass.IsPrintOrder())
                    smiEntryView.Visible = true;

                //временно                
                smiImport.Visible = false;                
                
                Form frm;
                if (MainClass._config.ValuesList.Keys.Contains("lstAbitDef"))
                {
                    bool lstAbitDef = bool.Parse(MainClass._config.ValuesList["lstAbitDef"]);

                    if (lstAbitDef)
                    {
                        frm = new ListAbit(this);
                        smiListAbit.Checked = true;
                        smiListPerson.Checked = false;
                    }
                    else
                    {
                        if (MainClass.dbType == PriemType.PriemAspirant)
                            frm = new ApplicationInetList();
                        else
                            frm = new PersonInetList();

                        smiListPerson.Checked = true;
                        smiListAbit.Checked = false;
                    }
                }
                else
                    frm = new PersonInetList();

                ShowProtocolWarning();
                
                frm.Show();
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка инициализации ", exc);
            }
        }

        private void ShowProtocolWarning()
        {
            if (MainClass.dbType == PriemType.Priem && !MainClass.b1kCheckProtocolsEnabled)
                return;
            if (MainClass.dbType == PriemType.PriemAspirant && !MainClass.bMagCheckProtocolsEnabled)
                return;

            DateTime dtNow = DateTime.Now.Date;
            DateTime dtYesterday = DateTime.Now.AddDays(-1).Date;

            using (PriemEntities context = new PriemEntities())
            {
                int cntProts = (from prot in context.qProtocol
                                where prot.Date >= dtYesterday
                                select prot).Count();

                if (cntProts == 0)
                    MessageBox.Show("Уважаемые пользователи!\nВами не создан протокол о допуске!\nСоздайте срочно!\nУправление по организации приема.", "Внимание");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //сохраняем параметры
            try
            {
                if (MainClass._config != null)
                {
                    MainClass._config.AddValue("lstAbitDef", smiListAbit.Checked.ToString());
                    MainClass._config.SaveConfig();
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при чтении параметров из файла: ", ex);
            }

            try
            {
                MainClass.DeleteAllOpenByHolder();
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка записи в базу: ", ex);
            }

            MainClass.DeleteTempFiles();
        }

        //реакция на смену mdi-окна
        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            Form f = this.ActiveMdiChild;

            if (f is FormFilter)
                smiFilters.Visible = true;
            else
                smiFilters.Visible = false;
        }

        private void smiEntry_Click(object sender, EventArgs e)
        {
            new EntryList().Show();
        }

        private void smiLoad_Click(object sender, EventArgs e)
        {
            if (MainClass.dbType == PriemType.PriemAspirant || MainClass.dbType == PriemType.PriemAG)
                new ApplicationInetList().Show();
            else
                new PersonInetList().Show();
        }

        private void smiAbits_Click(object sender, EventArgs e)
        {
            new ListAbit(this).Show();
        }

        private void smiPersons_Click(object sender, EventArgs e)
        {
            new ListPersonFilter(this).Show();
        }

        private void smiAllAbitList_Click(object sender, EventArgs e)
        {
            new AllAbitList().Show();
        }

        private void smiListHostel_Click(object sender, EventArgs e)
        {
            new ListHostel().Show();
        }

        private void smiVedExamList_Click(object sender, EventArgs e)
        {
            new VedExamLists().Show();
        }

        private void smiOlymBook_Click(object sender, EventArgs e)
        {
            new OlympBookList().Show();
        }

        private void smiEnableProtocol_Click(object sender, EventArgs e)
        {
            new EnableProtocolList().Show();
        }

        private void smiDisEnableProtocol_Click(object sender, EventArgs e)
        {
            new DisEnableProtocolList().Show();
        }

        private void smiPersonsSPBGU_Click(object sender, EventArgs e)
        {
            new PersonInetList().Show();
        }

        private void smiOnlineChanges_Click(object sender, EventArgs e)
        {
            new OnlineChangesList().Show();
        }

        private void smiOlympAbitList_Click(object sender, EventArgs e)
        {
            new OlympAbitList().Show();
        }

        private void smiExamName_Click(object sender, EventArgs e)
        {
            new ExamNameList().Show();
        }

        private void smiEGE_Click(object sender, EventArgs e)
        {
            new EgeExamList().Show();
        }

        private void smiExam_Click(object sender, EventArgs e)
        {
            new ExamList().Show();
        }

        private void smiChanges_Click(object sender, EventArgs e)
        {
            new PersonChangesList().Show();
        }

        private void smiCPK1_Click(object sender, EventArgs e)
        {
            new CPK1().Show();
        }

        #region Настройки

        //настройки по умолачнию - открывается список абитуриентов
        private void smiListPerson_Click(object sender, EventArgs e)
        {
            smiListAbit.Checked = false;
        }

        //настройки по умолачнию - открывается список заявлений
        private void smiListAbit_Click(object sender, EventArgs e)
        {
            smiListPerson.Checked = false;
        }

        //сохранить фильтр
        private void smiFiltersSave_Click(object sender, EventArgs e)
        {
            FilterList f = new FilterList(this.ActiveMdiChild as FormFilter, true);
            f.ShowDialog();
        }

        //выбрать фильтр
        private void smiFiltersChoose_Click(object sender, EventArgs e)
        {
            FilterList f = new FilterList(this.ActiveMdiChild as FormFilter, false);
            f.ShowDialog();
        }

        #endregion

        private void импортОлимпиадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SomeMethodsClass.FillOlymps();
        }

        private void smiChangeCompCel_Click(object sender, EventArgs e)
        {
            new ChangeCompCelProtocolList().Show();
        }

        private void smiExams_Click(object sender, EventArgs e)
        {
            new ExamResults().Show();
        }

        private void smiOlymp2Mark_Click(object sender, EventArgs e)
        {
            new Olymp2Mark().Show();
        }

        private void smiOlymp2Competition_Click(object sender, EventArgs e)
        {
            new Olymp2Competition().Show();
        }

        private void smiCreateVed_Click(object sender, EventArgs e)
        {
            new ExamsVedList().Show();
        }

        private void smiExamsVedRoomList_Click(object sender, EventArgs e)
        {
            new ExamsVedRoomList().Show();
        }

        private void smiMinEge_Click(object sender, EventArgs e)
        {
            new MinEgeList().Show();
        }

        private void smiImportMags_Click(object sender, EventArgs e)
        {
            
        }

        private void smiHelp_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format(@"{0}\Templates\Help.doc", Application.StartupPath));
        }

        private void smiChangeCompBE_Click(object sender, EventArgs e)
        {
            new ChangeCompBEProtocolList().Show();
        }

        private void smiFormA_Click(object sender, EventArgs e)
        {
            new FormA().Show();
        }

        private void smiDynamics_Click(object sender, EventArgs e)
        {
            new CountAbitStatistics().Show();
        }

        private void smiEGEStatistics_Click(object sender, EventArgs e)
        {
            //EGE Stat
            new EgeStatistics().Show();
        }

        private void smiEnterMarks_Click(object sender, EventArgs e)
        {
            new SelectVed().Show();
        }

        private void smiLoadMarks_Click(object sender, EventArgs e)
        {
            new SelectVedForLoad(false).Show();
        }        

        private void smiEnterManual_Click(object sender, EventArgs e)
        {
            new SelectExamManual().Show();
        }

        private void smiForm2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void smiAppeal_Click(object sender, EventArgs e)
        {
            new SelectVedForLoad(true).Show();
        }

        private void smiDecryptor_Click(object sender, EventArgs e)
        {
            new Decriptor().Show();
        }

        private void smiRatingList_Click(object sender, EventArgs e)
        {
            new RatingList(false).Show();
        }                          

        private void smiAbitFacultyIntesection_Click(object sender, EventArgs e)
        {
            new AbitFacultyIntersection().Show();
        }

        private void smiRegionAbitsStat_Click(object sender, EventArgs e)
        {
            new RegionAbitStatistics().Show();
        }

        private void smiRegionStatMarks_Click(object sender, EventArgs e)
        {
            new AbitEgeMarksStatistics().Show();
        }

        private void smiRatingListPasha_Click(object sender, EventArgs e)
        {
            new RatingList(true).Show();
        }

        private void smiRegionFacultyAbitCount_Click(object sender, EventArgs e)
        {
            new RegionFacultyAbitCountStatistics().Show();
        }

        private void smiEntryView_Click(object sender, EventArgs e)
        {
            new EntryViewList().Show();
        }

        private void smiDisEntryView_Click(object sender, EventArgs e)
        {
            new DisEntryViewList().Show();
        }

        private void smiOrderNumbers_Click(object sender, EventArgs e)
        {
            new CardOrderNumbers().Show();
        }

        private void smiRatingBackUp_Click(object sender, EventArgs e)
        {
            new BackUpFix().Show();
        }

        private void smiMakeBackDoc_Click(object sender, EventArgs e)
        {
            SomeMethodsClass.SetBackDocForBudgetInEntryView();
        }

        private void smiDeleteDog_Click(object sender, EventArgs e)
        {
            SomeMethodsClass.DeleteDogFromFirstWave();
        }

        private void smiVTB_Click(object sender, EventArgs e)
        {
            ExportClass.ExportVTB();
        }

        private void smiSberbank_Click(object sender, EventArgs e)
        {
            ExportClass.ExportSber();
        }

        private void номераЗачетокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportClass.SetStudyNumbers();
        }

        private void smiExportStudent_Click(object sender, EventArgs e)
        {
            new Migrator().Show();
        }

        private void smiOlympSubjectByRegion_Click(object sender, EventArgs e)
        {
            new OlympSubjectByRegion().Show();
        }

        private void smiOlympRegionBySubject_Click(object sender, EventArgs e)
        {
            new OlympRegionBySubject().Show();
        }

        private void smiOlympAbitBallsAndRatings_Click(object sender, EventArgs e)
        {
            new OlympAbitBallsAndRatings().Show();
        }

        private void региональноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OlympLevelAbitRating().Show();
        }

        private void smiRegionAbitStat_Rev_Click(object sender, EventArgs e)
        {
            new RegionAbitStatstics_Reversed().Show();
        }

        private void smiRegionAbitEGEMarksStatistics_Click(object sender, EventArgs e)
        {
           new RegionAbitEGEMarksStatistics().Show();

            // тестовая запись
           MessageBox.Show("");
        }

        private void smiOlympCheckList_Click(object sender, EventArgs e)
        {
            new OlympCheckList().Show();
        }

        private void smiReSetMarksForPaid_Click(object sender, EventArgs e)
        {
            ExportClass.ReSetMarksForPaid();
        }

        private void smiADAccounts_Click(object sender, EventArgs e)
        {
            //ExportClass.ImportLoginsFromCSV();
        }

        private void smiExamRegList_Click(object sender, EventArgs e)
        {
            new CardExamTimeTableAbitList().Show();
        }
    }
}