using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Transactions;
using System.Linq;
using PriemLib;
namespace Priem
{
    class ChangeCompBEProtocol : ProtocolCard
    {
        //поскольку сейчас любые льготники обязанны положить аттестат вместе со льготами, то при перекладывании аттестата тип конкурса должен автоматически переходить на "общий"
        //для этой цели был придуман протокол "о смене конкурса с "льготного" на общий"
        public ChangeCompBEProtocol(ProtocolList owner, int iFacultyId, int iStudyBasisId, int iStudyFormId)
            : this(owner, iFacultyId, iStudyBasisId, iStudyFormId, null)
        {
        }

        public ChangeCompBEProtocol(ProtocolList owner, int iFacultyId, int iStudyBasisId, int iStudyFormId, Guid? ProtocolId)
            : base(owner, iFacultyId, iStudyBasisId, iStudyFormId, ProtocolId)
        {
            _type = ProtocolTypes.ChangeCompBEProtocol;
            base.sQuery = this.sQuery;
        }

        //дополнительная инициализация
        protected override void InitControls()
        {
            sQuery = @"SELECT DISTINCT ed.extAbitAspirant.Sum, ed.extPersonAspirant.AttestatSeries, ed.extPersonAspirant.AttestatNum, ed.extAbitAspirant.Id as Id, ed.extAbitAspirant.BAckDoc as backdoc, 
             (ed.extAbitAspirant.BAckDoc | ed.extAbitAspirant.NotEnabled) as Red, ed.extAbitAspirant.RegNum as Рег_Номер, 
             ed.extPersonAspirant.FIO as ФИО, 
             ed.extPersonAspirant.EducDocument as Документ_об_образовании, 
             ed.extPersonAspirant.PassportSeries + ' №' + ed.extPersonAspirant.PassportNumber as Паспорт, 
             extAbitAspirant.ObrazProgramNameEx + ' ' + (Case when extAbitAspirant.ProfileId IS NULL then '' else extAbitAspirant.ProfileName end) as Направление, 
             Competition.NAme as Конкурс, extAbitAspirant.BackDoc 
             FROM ed.extAbitAspirant INNER JOIN ed.extPersonAspirant ON ed.extAbitAspirant.PersonId = ed.extPersonAspirant.Id                
             LEFT JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId
             LEFT JOIN ed.qProtocolHistory ON ed.qProtocolHistory.AbiturientId = ed.extAbitAspirant.Id";

            base.InitControls();

            this.Text = "Протокол об изменении типа конкурса с \"льготного\" на общий";
            this.chbEnable.Text = "Изменить всем выбранным слева абитуриентам тип конкурса на общий";

            this.chbFilter.Text = "Отфильтровать абитуриентов с проверенными данными";
            this.chbFilter.Visible = false;
        }

        protected override void InitAndFillGrids()
        {
            base.InitAndFillGrids();

            string sFilter = string.Empty;
            sFilter = " AND ed.extAbitAspirant.CompetitionId NOT IN (3, 4, 6) ";//не общ-дог-цел

            FillGrid(dgvRight, sQuery, GetWhereClause("ed.extAbitAspirant") + sFilter, sOrderby);

            //заполнили левый
            if (_id != null)
            {
                sFilter = string.Format(" WHERE ed.extAbitAspirant.Id IN (SELECT AbiturientId FROM ed.qProtocolHistory WHERE ProtocolId = '{0}')", _id.ToString());
                FillGrid(dgvLeft, sQuery, sFilter, sOrderby);
            }
            else //новый
            {
                InitGrid(dgvLeft);
            }
        }

        //подготовка нужного грида
        protected override void InitGrid(DataGridView dgv)
        {
            base.InitGrid(dgv);
        }

        protected override bool Save()
        {
            if (!base.Save())
            {
                MessageBox.Show("Не удалось сохранить протокол.\nИзменения не сохранены");
                return false;
            }
            try
            {
                ArrayList alQueries = new ArrayList();

                using (PriemEntities context = new PriemEntities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        try
                        {
                            //меняет тип конкурса + снимает отметку
                            foreach (DataGridViewRow r in dgvLeft.Rows)
                            {
                                SortedList slVals = new SortedList();
                                Guid? abitId = new Guid(r.Cells["Id"].Value.ToString());
                                int? compNew = 4;

                                context.Abiturient_UpdateCompetititon(compNew, null, false, abitId);
                                context.Abiturient_UpdateHasOriginals(false, abitId);//оригиналы больше "не поданы"
                            }

                            transaction.Complete();
                        }
                        catch (Exception exc)
                        {
                            throw new Exception("Ошибка при сохранении данных: " + exc.Message);
                        }
                    }
                }
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении типа конкурса: " + ex.Message);
                return false;
            }
        }
    }
}
