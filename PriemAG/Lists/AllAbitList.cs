using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity.Core.Objects;
using System.Linq;

using EducServLib;
using WordOut;
using BaseFormsLib;
using PriemLib;
namespace Priem
{
    public partial class AllAbitList : BookList
    { 
        //конструктор
        public AllAbitList()
        {            
            InitializeComponent();
            
            Dgv = dgvAbitList;
            _tableName = "ed.qAbiturient";
            _title = "Список абитуриентов с заявлениями на другие факультеты";          

            InitControls();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            
            Dgv.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(Dgv_CellDoubleClick);
            UpdateDataGrid();           
        }  

        //обновление грида
        protected override void GetSource()
        {
            _sQuery = @"SELECT ed.qAbitAll.Id, PersonNum as Ид_номер, 
                     FIO as ФИО, 
                     RegNum as Рег_номер, FacultyName as Факультет, ObrazProgramCrypt as Код, 
                     LicenseProgramName as Направление, ProfileName as Профиль, 
                     StudyFormName as Форма, StudyBasisName as Основа, Priority AS Приоритет 
                     FROM ed.qAbitAll INNER JOIN ed.extPersonAspirant ON ed.qAbitAll.PersonId =  ed.extPersonAspirant.Id              
                     WHERE personId in (SELECT distinct personId FROM ed.qAbiturient) ";

            string filter = MainClass.GetStLevelFilter("ed.qAbitAll");

            HelpClass.FillDataGrid(Dgv, _bdc, _sQuery, filter, " ORDER BY ФИО, Рег_номер");
        }

        //поиск по номеру
        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbitList, "Ид_номер", tbNumber.Text);
        }

        //поиск по фио
        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbitList, "ФИО", tbFIO.Text);
        }

        protected override void OpenCard(string itemId, BaseFormEx formOwner, int? index)
        {
            return;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintClass.PrintAllToExcel(this);
        }
    }
}