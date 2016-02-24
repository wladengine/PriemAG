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
using PriemLib;


namespace Priem
{
    public partial class SimpleBook : BaseForm
    {
        protected bool _modified = false;
        protected DataTable dataTable;
        protected DBPriem _bdc;
        protected string Title = "Name";
        
        /// <summary>
        /// constructor
        /// </summary>
        public SimpleBook()
        {
            InitializeComponent();
            
            InitControls();            
        }

        protected virtual void InitControls()
        {
            InitFocusHandlers();

            this.Text = Title + BaseFormsLib.Constants.CARD_READ_ONLY;
            _bdc = MainClass.Bdc;
            this.MdiParent = MainClass.mainform;

            BindGrid(GetSource());
            SetReadOnly(true);

            ExtraInit();
            
            return; 
        }

        protected virtual void ExtraInit()
        {
            if (_bdc != null)
                if (!MainClass.IsEntryChanger())
                    btnSave.Enabled = false;
        }

        protected virtual DataTable GetSource()
        {
            return null;
        }

        protected virtual void UpdateSource(DataTable table)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// lock/unlock datagrid
        /// </summary>
        /// <param name="val"></param>
        protected virtual void SetReadOnly(bool val)
        {
            dgv.ReadOnly = val;
            dgv.AllowUserToAddRows = 
                dgv.AllowUserToDeleteRows = !val;

            return;
        }

        /// <summary>
        /// bind data to grid
        /// </summary>
        /// <param name="table"></param>
        protected virtual void BindGrid(DataTable table)
        {
            return;
        }

        /// <summary>
        /// on close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        protected virtual void OnClose()
        {
            this.Close();
        }

        /// <summary>
        /// on save or modify
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_modified==false)
            {
                btnSave.Text = BaseFormsLib.Constants.BTN_SAVE_TITLE;
                this.Text = Title + BaseFormsLib.Constants.CARD_MODIFIED;
                SetReadOnly(false);
                _modified = true;
            }
            else
            {
                try
                {
                    DataTable tableChanges = dataTable.GetChanges();
                    if (tableChanges != null)
                    {
                        UpdateSource(tableChanges);
                        dataTable.AcceptChanges();

                        BindGrid(GetSource());
                    }
                    btnSave.Text = BaseFormsLib.Constants.BTN_CHANGE_TITLE;
                    this.Text = Title + BaseFormsLib.Constants.CARD_READ_ONLY;
                    SetReadOnly(true);
                    _modified = false;
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка при сохранении данных:" + ex.Message);
                }
            }

            return;
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Неверные данные";
            e.Cancel = true;            
        }

        private void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
        }      

    }
}
