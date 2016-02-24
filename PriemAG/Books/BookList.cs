using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseFormsLib;
using EducServLib;
using BDClassLib;
using PriemLib;

namespace Priem
{
    public partial class BookList : BaseList
    {
        protected string _orderBy;
        protected MainClass.DataRefreshHandler _drh;
        protected DBPriem _bdc; 

        public BookList()
        {
            InitializeComponent();
        }

        protected override bool IsForReadOnly()
        {
            return MainClass.IsReadOnly();
        }

        protected override void ExtraInit()
        {
            this.CenterToParent();
            this.MdiParent = MainClass.mainform;           
            _bdc = MainClass.Bdc;

            _drh = new MainClass.DataRefreshHandler(UpdateDataGrid);
            MainClass.AddHandler(_drh);           

            Dgv.ReadOnly = true;
            Dgv.AllowUserToAddRows = Dgv.AllowUserToDeleteRows = false;
            Dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(Dgv_CellDoubleClick);
            Dgv.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(Dgv_ColumnHeaderMouseClick);          

            if (MainClass.IsOwner())
                btnRemove.Visible = true;
            else
                btnRemove.Visible = false; 
        }

        protected virtual void Dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex <= 0)
                _orderBy = null;
            else
                _orderBy = "it." + Dgv.Columns[e.ColumnIndex].Name;

            UpdateDataGrid();
        }

        public override void UpdateDataGrid()
        {
            GetSource();

            if (Dgv.Columns.Contains("Id"))
                Dgv.Columns["Id"].Visible = false;
                   
            lblCount.Text = "Всего: " + Dgv.Rows.Count.ToString();
            btnCard.Enabled = !(Dgv.RowCount == 0);
        }        

        protected virtual void GetSource()
        {
            throw new NotImplementedException();
        }

        protected void SetVisibleColumnsAndNameColumns(string ColumnName, string ColumnTitle)
        {           
            if (Dgv.Columns.Contains(ColumnName))
            {
                Dgv.Columns[ColumnName].Visible = true;
                Dgv.Columns[ColumnName].HeaderText = ColumnTitle;
            }
        }

        protected virtual void SetVisibleColumnsAndNameColumnsOrdered(string columnName, string columnTitle, int? index)
        {
            if (Dgv.Columns.Contains(columnName))
            {
                Dgv.Columns[columnName].Visible = true;
                Dgv.Columns[columnName].HeaderText = columnTitle;

                if (index != null)
                    Dgv.Columns[columnName].DisplayIndex = index.Value;
            }
        }

        protected virtual void SetVisibleColumnsAndNameColumns()
        {
            foreach (DataGridViewColumn col in Dgv.Columns)
            {
                col.Visible = false;
            }

            SetVisibleColumnsAndNameColumns("Name", "Название");
        }

        protected override void OnClosed()
        {
            MainClass.RemoveHandler(_drh);
        }         
      
        protected override void btnRemove_Click(object sender, EventArgs e)
        {
            if (MainClass.IsPasha())
            {
                if (MessageBox.Show("Удалить записи?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow dgvr in Dgv.SelectedRows)
                    {
                        string itemId = dgvr.Cells["Id"].Value.ToString();
                        try
                        {
                            DeleteSelectedRows(itemId);
                        }
                        catch (Exception ex)
                        {
                            WinFormsServ.Error("Ошибка удаления данных" + ex.Message);
                            goto Next;
                        }
                    Next: ;
                    }
                    MainClass.DataRefresh();
                }
            }
        }
               
        protected virtual void DeleteSelectedRows(string sId)
        {
            WinFormsServ.Error("Удаление недоступно");
        }
    }
}
