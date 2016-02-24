using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EducServLib;
using BaseFormsLib;
using WordOut;
using PriemLib;

namespace Priem
{
    /*Список Person для общежитий*/
    public partial class ListHostel : BookList
    {
        //конструктор
        public ListHostel()
        {
            InitializeComponent();

            Dgv = dgvAbiturients;
            _tableName = "ed.qAbiturient";
            _title = "Список для общежитий";         

            InitControls();
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            
            this.Width = 812;
            using (PriemEntities context = new PriemEntities())
            {
                ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("ed.qFaculty", "ORDER BY Acronym"), false, true);                
            }
        }

        public override void InitHandlers()
        {
            cbFaculty.SelectedIndexChanged += new EventHandler(cbFaculty_SelectedIndexChanged); 
        }

        void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        //поле поиска
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgvAbiturients, "ФИО", tbSearch.Text);            
        }

        //обновление грида       
        protected override void GetSource()
        {
            string sFilters = " WHERE HostelAbit = 1 ";
            sFilters += MainClass.GetStLevelFilter("ed.qAbiturient");
                      
            int? facId = ComboServ.GetComboIdInt(cbFaculty);
                if (facId != null)
                    sFilters += string.Format(" AND HasAssignToHostel = 1 AND HostelFacultyId = {0}", facId);


            _sQuery = @"SELECT ed.extPersonAspirant.Id, PersonNum as Ид_номер, FIO  as ФИО, 
            (Case When (CountryId = 1 AND NOT RegionId IS NULL) then RegionName else CountryName end) AS Место_жительства, 
            (Case when HostelFacultyId IS NULL then 'не выдано' else HostelFacultyName end) AS Факультет_выдавший_направление, 
            PassportData AS Паспортные_данные 
            FROM ed.qAbiturient INNER JOIN ed.extPersonAspirant ON ed.qAbiturient.PersonId = ed.extPersonAspirant.Id ";

            HelpClass.FillDataGrid(Dgv, _bdc, _sQuery, sFilters, " ORDER BY ФИО");

            dgvAbiturients.Columns[1].Width = 74;
            dgvAbiturients.Columns[2].Width = 234;
            dgvAbiturients.Columns[3].Width = 266; 
        }

        protected override void OpenCard(string itemId, BaseFormEx formOwner, int? index)
        {
            MainClass.OpenCardPerson(itemId, this, dgvAbiturients.CurrentRow.Index);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {               
                WordDoc wd = new WordDoc(string.Format(@"{0}\ListPerson.dot", MainClass.dirTemplates));

                int k = dgvAbiturients.Columns.Count;
                wd.AddNewTable(dgvAbiturients.Rows.Count + 1, k);
                TableDoc td = wd.Tables[0];

                int? facId = ComboServ.GetComboIdInt(cbFaculty);
                string sFac = cbFaculty.Text.ToLower();
                if (sFac.CompareTo("все") == 0)
                    sFac = "";
                else
                {
                    sFac = "получивших направления на послеление от ";                   
                    if (facId == 3)
                        sFac += "высшей школы менеджмента ";
                    else
                        sFac += sFac.Replace("кий", "кого ").Replace("ый", "ого ") + " факультета ";
                }
               
                wd.Fields["Faculty"].Text = sFac;
                wd.Fields["Info"].Text = "для общежитий";


                int i = 0;

                td[0, 0] = "№ п/п";
                for (int j = 0; j < k-1; j++)
                    td[j + 1, 0] = dgvAbiturients.Columns[j + 1].HeaderText;

                // печать из грида
                foreach (DataGridViewRow dgvr in dgvAbiturients.Rows)
                {
                    td[0, i + 1] = (i + 1).ToString();
                    for (int j = 0; j < k-1; j++)
                        td[j + 1, i + 1] = dgvAbiturients.Rows[i].Cells[j + 1].Value.ToString();

                    i++;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
        }
    }
}