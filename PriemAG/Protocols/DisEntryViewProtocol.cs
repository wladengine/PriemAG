using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

//using BDClassLib;
using EducServLib;
using WordOut;
using PriemLib; 

namespace Priem
{
    public partial class DisEntryViewProtocol : ProtocolCard
    { 
        public DisEntryViewProtocol(ProtocolList owner, int sFac, int sSection, int sForm, int? sProf, bool? isSec, bool? isReduced, bool? isParal, bool? isList)
            : this(owner, sFac, sSection, sForm, sProf, isSec, isReduced, isParal, isList, null)
        {
        }

        //конструктор 
        public DisEntryViewProtocol(ProtocolList owner, int sFac, int sSection, int sForm, int? sProf, bool? isSec, bool? isReduced, bool? isParal, bool? isList, Guid? sProtocol)
            : base(owner,sFac,sSection,sForm,sProf, isSec, isReduced, isParal, isList, sProtocol)
        {
            _type = ProtocolTypes.DisEntryView;                      
        }

        //дополнительная инициализация
        protected override void  InitControls()
        {
            sQuery = string.Format("SELECT DISTINCT ed.extAbitMarksSum.TotalSum as Sum, ed.extPersonAspirant.AttestatSeries, ed.extPersonAspirant.AttestatNum, ed.extAbitAspirant.Id as Id, ed.extAbitAspirant.BAckDoc as backdoc, " +
            " 'false' as Red, ed.extAbitAspirant.RegNum as Рег_Номер, " +
            " ed.extPersonAspirant.FIO as ФИО, " +
            " (case when ed.extPersonAspirant.SchoolTypeId = 1 then ed.extPersonAspirant.AttestatRegion + ' ' + ed.extPersonAspirant.AttestatSeries + '  №' + ed.extPersonAspirant.AttestatNum else ed.extPersonAspirant.DiplomSeries + '  №' + ed.extPersonAspirant.DiplomNum end) as Документ_об_образовании, " +
            " ed.extPersonAspirant.PassportSeries + ' №' + ed.extPersonAspirant.PassportNumber as Паспорт, " +
            " ed.extAbitAspirant.ObrazProgramName + ' ' +(Case when ed.extAbitAspirant.ProfileName IS NULL then '' else ed.extAbitAspirant.ProfileName end) as Направление, " +
            " ed.Competition.NAme as Конкурс, ed.extAbitAspirant.BackDoc " +
            " FROM ed.extAbitAspirant INNER JOIN ed.extPersonAspirant ON ed.extAbitAspirant.PErsonId = ed.extPersonAspirant.Id " +
            " INNER JOIN ed.extEnableProtocol ON ed.extAbitAspirant.Id=ed.extEnableProtocol.AbiturientId " +
            " INNER JOIN ed.extEntryView ON ed.extAbitAspirant.Id=ed.extEntryView.AbiturientId " +
            " INNER JOIN ed.extAbitMarksSum ON ed.extAbitAspirant.Id=ed.extAbitMarksSum.Id " +
            " LEFT JOIN ed.Competition ON ed.Competition.Id = ed.extAbitAspirant.CompetitionId ");


            string q = string.Format("SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, Number as Name FROM ed.extEntryView WHERE StudyLevelGroupId=4 AND Excluded=0 AND IsOld=0 AND FacultyId ={0} AND StudyFormId = {1} AND StudyBasisId = {2} AND IsSecond = {3} AND IsReduced = {4} AND IsParallel = {5} AND IsListener = {6}", _facultyId, _studyFormId, _studyBasisId, QueryServ.StringParseFromBool(_isSecond.Value), QueryServ.StringParseFromBool(_isReduced.Value), QueryServ.StringParseFromBool(_isParallel.Value), QueryServ.StringParseFromBool(_isListener.Value));
            ComboServ.FillCombo(cbHeaders, HelpClass.GetComboListByQuery(q), false, false);
      
            cbHeaders.Visible = true;
            lblHeaderText.Text = "Из представления к зачислению №";            
            chbInostr.Visible = true;

            cbHeaders.SelectedIndexChanged += new EventHandler(cbHeaders_SelectedIndexChanged);           
            chbInostr.CheckedChanged += new System.EventHandler(UpdateGrids);  
            
            base.InitControls();

            this.Text = "Приказ об исключении ";
            this.chbEnable.Text = "Добавить всех выбранных слева абитуриентов в приказ об исключении";

            this.chbFilter.Visible = false;
        }

        void cbHeaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGrids();
        }

        public string HeaderId
        {
            get { return ComboServ.GetComboId(cbHeaders); }
            set { ComboServ.SetComboId(cbHeaders, value); }
        }   

        protected override void InitAndFillGrids()
        {
            base.InitAndFillGrids();

            UpdateRight();
            string sFilter = string.Empty;
                        
            //заполнили левый
            if (_id!=null)
            {
                sFilter = string.Format(" WHERE ed.extAbitAspirant.Id IN (SELECT AbiturientId FROM ed.qProtocolHistory WHERE ProtocolId = '{0}')", _id);
                FillGrid(dgvLeft, sQuery, sFilter, sOrderby);
            }
            else //новый
            {
                InitGrid(dgvLeft);
            }                    
        }

        private void UpdateGrids(object sender, EventArgs e)
        {
            UpdateGrids();
        }

        void UpdateGrids()
        {
            UpdateRight();
            InitGrid(dgvLeft);
        }

        void UpdateRight()
        {
            if (HeaderId == null)
            {
                dgvRight.DataSource = null;
                return;
            }

            string sFilter = string.Empty;
            sFilter = string.Format(" AND ed.extEntryView.Id = '{0}' {1}", HeaderId, chbInostr.Checked ? " AND ed.extPersonAspirant.NationalityId <> 1 " : " AND ed.extPersonAspirant.NationalityId = 1 ");
            FillGrid(dgvRight, sQuery, GetWhereClause("ed.extAbitAspirant") + sFilter, sOrderby);
        }        

        //подготовка нужного грида
        protected override void InitGrid(DataGridView dgv)
        {
            base.InitGrid(dgv);

            dgv.Columns["Pasport"].Visible = false;
            dgv.Columns["Attestat"].Visible = false;
        }
   }
}