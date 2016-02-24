using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriemLib;
namespace Priem
{
    public class EnableProtocol : ProtocolCard
    {
        //конструктор         
        public EnableProtocol(ProtocolList owner, int iFacultyId, int iStudyBasisId, int iStudyFormId)
            : base(owner, iFacultyId, iStudyBasisId, iStudyFormId)
        {
            _type = ProtocolTypes.EnableProtocol;
        }

        public EnableProtocol() : base()
        { 
            //InitializeComponent(); 
        }

        //дополнительная инициализация
        protected override void InitControls()
        {
            sQuery = @"SELECT DISTINCT ed.extAbitAspirant.Sum, ed.extPersonAspirant.AttestatSeries, ed.extPersonAspirant.AttestatNum, ed.extAbitAspirant.Id as Id, ed.extAbitAspirant.BAckDoc as backdoc, 
             (ed.extAbitAspirant.BAckDoc | ed.extAbitAspirant.NotEnabled | case when (NOT ed.hlpMinEgeAbiturient.Id IS NULL) then 'true' else 'false' end) as Red, ed.extAbitAspirant.RegNum as Рег_Номер, 
             ed.extPersonAspirant.FIO as ФИО, 
             ed.extPersonAspirant.EducDocument as Документ_об_образовании, 
             ed.extPersonAspirant.PassportSeries + ' №' + ed.extPersonAspirant.PassportNumber as Паспорт, 
             extAbitAspirant.ObrazProgramNameEx + ' ' + (Case when extAbitAspirant.ProfileId IS NULL then '' else extAbitAspirant.ProfileName end) as Направление, 
             Competition.NAme as Конкурс, extAbitAspirant.BackDoc 
             FROM ed.extAbitAspirant INNER JOIN ed.extPersonAspirant ON ed.extAbitAspirant.PersonId = ed.extPersonAspirant.Id   
             LEFT JOIN ed.hlpMinEgeAbiturient ON ed.hlpMinEgeAbiturient.Id = ed.extAbitAspirant.Id
             LEFT JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId";

            base.InitControls();

            this.Text = "Протокол о допуске";
            this.chbEnable.Text = "Добавить всех выбранных слева абитуриентов в протокол о допуске";

            this.chbFilter.Text = "Отфильтровать абитуриентов с проверенными данными";
            this.chbFilter.Visible = true;
        }

        protected override void InitAndFillGrids()
        {
            base.InitAndFillGrids();

            string sFilter = " AND extAbitAspirant.Id NOT IN (SELECT Id FROM ed.qAbiturientForeignApplicationsOnly) ";
            sFilter += string.Format(" AND ed.extAbitAspirant.BackDoc = 0 AND ed.extAbitAspirant.NotEnabled=0 AND ed.extAbitAspirant.Id NOT IN (SELECT AbiturientId FROM ed.qProtocolHistory WHERE Excluded=0 AND ProtocolId IN (SELECT Id FROM ed.qProtocol WHERE ISOld=0 AND ProtocolTypeId=1 AND FacultyId ={0} AND StudyFormId = {1} AND StudyBasisId = {2}))", 
                _facultyId.ToString(), _studyFormId.ToString(), _studyBasisId.ToString(), MainClass.studyLevelGroupId);

            if (chbFilter.Checked)
                sFilter += " AND ed.extAbitAspirant.Checked > 0";

            //сперва общий конкурс (не общ-преим), т.к. чернобыльцы негодуют - льготы есть, а в протокол не попасть
            FillGrid(dgvRight, sQuery, GetWhereClause("ed.extAbitAspirant") + sFilter + " AND ed.extAbitAspirant.CompetitionId NOT IN (1,2,7,8)/* AND (ed.extPersonAspirant.Privileges=0  OR ed.extAbitAspirant.CompetitionId IN (5,6))*/", sOrderby);

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

            // заполнение льготников, проверенных советниками
            string query = sQuery + GetWhereClause("ed.extAbitAspirant") + sFilter + " AND (ed.extAbitAspirant.CompetitionId IN (1,8) OR (ed.extPersonAspirant.Privileges>0 AND ed.extAbitAspirant.CompetitionId IN (2,7))) AND ed.extAbitAspirant.Checked>0 ";

            DataSet ds = MainClass.Bdc.GetDataSet(query);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgvLeft, false, dr["Id"].ToString(), dr["Red"].ToString(), true, dr["backdoc"].ToString(), dr["Рег_номер"].ToString(), dr["ФИО"].ToString(), dr["Sum"].ToString(), dr["Документ_об_образовании"].ToString(), dr["Направление"].ToString(), dr["Конкурс"].ToString(), dr["Паспорт"].ToString());
                if (bool.Parse(dr["Red"].ToString()))
                {
                    r.Cells[5].Style.ForeColor = Color.Red;
                    r.Cells[6].Style.ForeColor = Color.Red;
                }

                r.Cells[5].Style.BackColor = Color.Green;
                r.Cells[6].Style.BackColor = Color.Green;

                dgvLeft.Rows.Add(r);
            }

            //блокируем редактирование кроме первого столбца
            for (int i = 1; i < dgvLeft.ColumnCount; i++)
                dgvLeft.Columns[i].ReadOnly = true;

            dgvLeft.Update();
        }

        //подготовка нужного грида
        protected override void InitGrid(DataGridView dgv)
        {
            base.InitGrid(dgv);
        }
    }
}
