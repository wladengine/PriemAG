using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using BDClassLib;
using EducServLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class PersonChangesList : BaseFormEx
    {
        private DBPriem _bdc;
        private DataRefreshHandler _drh;

        public PersonChangesList()
        {
            InitializeComponent();
            InitControls();            
        }

        private void InitControls()
        {
            this.CenterToParent();
            this.MdiParent = MainClass.mainform;
            _bdc = MainClass.Bdc;

            _drh = new DataRefreshHandler(UpdateDataGrid);
            MainClass.AddHandler(_drh);

            Dgv = dgvChanges;

            UpdateDataGrid();
            InitHandlers();
        }

        // обработка фильтров
        private void InitHandlers()
        {
            // фильтры для вкладки Входящая документация
            dtDateFrom.ValueChanged += new EventHandler(UpdateDataGrid);
            dtDateTo.ValueChanged += new EventHandler(UpdateDataGrid);
        }

        private void FillDataGrid(string filters)
        {
            string defQuery = @"SELECT DISTINCT ed.PersonChanges.PersonId AS Id, ed.extPersonAspirant.Surname + ' ' + ed.extPersonAspirant.Name + ' ' + ed.extPersonAspirant.SecondName AS ФИО,                  
                ed.extPersonAspirant.PersonNum AS Ид_номер, ed.PersonChanges.FieldName AS Измененное_поле, ed.PersonChanges.OldValue AS Старое_значение, 
                ed.PersonChanges.NewValue AS Новое_значение, ed.PersonChanges.Date AS Дата_изменения, 
                ed.PersonChanges.Owner AS Автор_изменения, 
                Case When ed.PersonChanges.FacultyId = 0 then 'администратор' else ed.SP_Faculty.Acronym end AS Факультет
                FROM ed.PersonChanges LEFT JOIN ed.SP_Faculty ON ed.PersonChanges.FacultyId = ed.SP_Faculty.Id 
                LEFT JOIN ed.extPersonAspirant ON ed.PersonChanges.PersonId = ed.extPersonAspirant.Id INNER JOIN ed.qAbiturient ON ed.qAbiturient.PersonId = ed.extPersonAspirant.Id WHERE 0=0";                

            HelpClass.FillDataGrid(dgvChanges, _bdc, defQuery, filters, " ORDER BY Date");            
        }

        private void UpdateDataGrid(object o, EventArgs ev)
        {
            UpdateDataGrid();
        }
                
        // собирание фильтров для запроса  
        private string GetFilters()
        {
            string flt = string.Empty;
            
            if (dtDateFrom.Checked)
            {
                flt += string.Format(" AND ed.PersonChanges.Date >= " + _bdc.BuildData(dtDateFrom.Value.Year.ToString(), dtDateFrom.Value.Month.ToString(), dtDateFrom.Value.Day.ToString()));
            }
            if (dtDateTo.Checked)
            {
                flt += string.Format(" AND ed.PersonChanges.Date <= " + _bdc.BuildData(dtDateTo.Value.Year.ToString(), dtDateTo.Value.Month.ToString(), dtDateTo.Value.Day.ToString()));
            }   
            return flt;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvChanges_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                OpenCard();
        }

        //открытие карточки
        private void OpenCard()
        {
            if (dgvChanges.CurrentCell != null && dgvChanges.CurrentCell.RowIndex > -1)
            {
                string perId = dgvChanges.Rows[dgvChanges.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
                if (perId != "")
                {
                    MainClass.OpenCardPerson(perId, this, dgvChanges.CurrentRow.Index);
                }
            }
        }

        private void PersonChanges_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainClass.RemoveHandler(_drh);
        }

        private void UpdateDataGrid()
        {
            FillDataGrid(GetFilters());
        }
    }
}
