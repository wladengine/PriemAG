using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Collections;
using System.Data.Objects;

using BaseFormsLib;
using EducServLib;
using PriemLib;

namespace Priem
{
    public partial class BackUpFix : BaseForm
    {
        private DBPriem bdc;

        public BackUpFix()
        {
            InitializeComponent();
            InitControls();            
        }  

        //дополнительная инициализация контролов
        private void InitControls()
        {
            this.CenterToParent();
            InitFocusHandlers();
            this.MdiParent = MainClass.mainform;
            bdc = MainClass.Bdc;


            ComboServ.FillCombo(cbStudyLevelGroup, HelpClass.GetComboListByTable("ed.StudyLevelGroup"), false, false);
            ComboServ.FillCombo(cbStudyBasis, HelpClass.GetComboListByTable("ed.StudyBasis"), false, false);                 
        }

        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public int? StudyLevelGroupId
        {
            get { return ComboServ.GetComboIdInt(cbStudyLevelGroup); }
            set { ComboServ.SetComboId(cbStudyLevelGroup, value); }
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!MainClass.IsPasha())
                return;

            using (PriemEntities context = new PriemEntities())
            {
                if (MessageBox.Show(string.Format("Создать бэкапы для таблиц и удалить рейтинг из текущих таблиц для \n {0}, {1}?", cbStudyLevelGroup.Text, cbStudyBasis.Text), "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NewWatch wc;

                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromHours(1)))
                    {
                        if (StudyBasisId == 2)
                        {
                            List<Guid> lstEntry = (from ev in context.extEntryView
                                                  select ev.AbiturientId).ToList<Guid>();

                            //Fixieren
                            IEnumerable<Fixieren> fixs = from fx in context.Fixieren
                                                         join ab in context.extAbitAspirant
                                                         on fx.AbiturientId equals ab.Id  
                                                         where ab.StudyLevelGroupId == 1 && ab.StudyBasisId == 2
                                                         && (lstEntry.Contains(ab.Id) || ab.BackDoc)
                                                         select fx;
                            wc = new NewWatch();
                            wc.Show();
                            wc.SetText("Идет процесс. BackUp Fixieren. Общее число - " + fixs.Count());
                            wc.SetMax(fixs.Count());

                            foreach (Fixieren fix in fixs)
                            {
                                context.ExecuteStoreCommand("INSERT INTO ed.FixierenBackUp2(Number, AbiturientId, FixierenViewId) VALUES({0}, {1}, {2})", fix.Number, fix.AbiturientId, fix.FixierenViewId);
                                context.Fixieren_DELETE(fix.AbiturientId);

                                wc.PerformStep();
                            }                            

                            //_FirstWave
                            IEnumerable<C_FirstWave> firstW = from fx in context.C_FirstWave
                                                              join ab in context.extAbitAspirant
                                                              on fx.AbiturientId equals ab.Id
                                                              where ab.StudyLevelGroupId == 1 && ab.StudyBasisId == 2
                                                              && (lstEntry.Contains(ab.Id) || ab.BackDoc)
                                                              select fx;

                            wc.SetText("Идет процесс. BackUp C_FirstWave. Общее число - " + firstW.Count());
                            wc.SetMax(firstW.Count());

                            foreach (C_FirstWave fix in firstW)
                            {
                                context.ExecuteStoreCommand("INSERT INTO ed._FirstWaveBackUp2(AbiturientId, SortNum) VALUES({0}, {1})", fix.AbiturientId, fix.SortNum);

                                context.FirstWave_DeleteByAbId(fix.AbiturientId);
                                wc.PerformStep();
                            }

                        }
                        else
                        {
                            //Fixieren
                            IEnumerable<Fixieren> fixs = from fx in context.Fixieren
                                                         join ab in context.extAbitAspirant
                                                         on fx.AbiturientId equals ab.Id
                                                         where ab.StudyLevelGroupId == StudyLevelGroupId && ab.StudyBasisId == StudyBasisId
                                                         select fx;

                            wc = new NewWatch();
                            wc.Show();
                            wc.SetText("Идет процесс. BackUp Fixieren. Общее число - " + fixs.Count());
                            wc.SetMax(fixs.Count());

                            foreach (Fixieren fix in fixs)
                            {
                                context.ExecuteStoreCommand("INSERT INTO ed.FixierenBackUp2(Number, AbiturientId, FixierenViewId) VALUES({0}, {1}, {2})", fix.Number, fix.AbiturientId, fix.FixierenViewId);
                                context.Fixieren_DELETE(fix.AbiturientId);

                                wc.PerformStep();
                            }

                            //FixierenView
                            IEnumerable<FixierenView> fixViews = from fx in context.FixierenView
                                                                 where fx.StudyLevelGroupId == StudyLevelGroupId && fx.StudyBasisId == StudyBasisId
                                                                 select fx;

                            wc.SetText("Идет процесс. BackUp FixierenView. Общее число - " + fixViews.Count());
                            wc.SetMax(fixViews.Count());

                            foreach (FixierenView fix in fixViews)
                            {
                                context.FixierenViewBackup2_Insert(fix.Id, fix.StudyLevelGroupId, fix.FacultyId, fix.LicenseProgramId, fix.ObrazProgramId,
                                    fix.ProfileId, fix.StudyBasisId, fix.StudyFormId, fix.IsSecond, fix.IsReduced, fix.IsParallel,
                                    fix.IsCel, fix.DocNum, fix.Locked);

                                context.FixierenView_Delete(fix.Id);
                                wc.PerformStep();
                            }

                            //_FirstWave
                            IEnumerable<C_FirstWave> firstW = from fx in context.C_FirstWave
                                                              join ab in context.extAbitAspirant
                                                              on fx.AbiturientId equals ab.Id
                                                              where ab.StudyLevelGroupId == StudyLevelGroupId && ab.StudyBasisId == StudyBasisId
                                                              select fx;

                            wc.SetText("Идет процесс. BackUp C_FirstWave. Общее число - " + firstW.Count());
                            wc.SetMax(firstW.Count());

                            foreach (C_FirstWave fix in firstW)
                            {
                                context.ExecuteStoreCommand("INSERT INTO ed._FirstWaveBackUp2(AbiturientId, SortNum) VALUES({0}, {1})", fix.AbiturientId, fix.SortNum);

                                context.FirstWave_DeleteByAbId(fix.AbiturientId);
                                wc.PerformStep();
                            }

                            //_FirstWaveGreen
                            IEnumerable<C_FirstWaveGreen> firstWGr = from fx in context.C_FirstWaveGreen
                                                                     join ab in context.extAbitAspirant
                                                                     on fx.AbiturientId equals ab.Id
                                                                     where ab.StudyLevelGroupId == StudyLevelGroupId && ab.StudyBasisId == StudyBasisId
                                                                     select fx;

                            wc.SetText("Идет процесс. BackUp C_FirstWaveGreen. Общее число - " + firstWGr.Count());
                            wc.SetMax(firstWGr.Count());

                            foreach (C_FirstWaveGreen fix in firstWGr)
                            {
                                context.ExecuteStoreCommand("INSERT INTO ed._FirstWaveGreenBackUp2(AbiturientId, IsNew) VALUES({0}, {1})", fix.AbiturientId, fix.IsNew);

                                context.FirstWaveGreen_DeleteByAbId(fix.AbiturientId);
                                wc.PerformStep();
                            }
                        }

                        transaction.Complete();
                        wc.Close();
                    }                   
                }
            }
        }                     
    }
}