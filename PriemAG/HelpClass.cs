using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using EducServLib;
using BDClassLib;
using PriemLib;
namespace Priem
{
    public static class HelpClass
    {
        public static List<KeyValuePair<string, string>> GetComboListByTable(string tableName)
        {
            return GetComboListByTable(tableName, null);
        }

        public static List<KeyValuePair<string, string>> GetComboListByTable(string tableName, string orderBy)
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();

                    foreach (ListItem ob in context.ExecuteStoreQuery<ListItem>(string.Format("SELECT CONVERT(varchar(100), Id) AS Id, Name FROM {0} {1}", tableName, string.IsNullOrEmpty(orderBy) ? "ORDER BY 2" : orderBy)))
                    {
                        lst.Add(new KeyValuePair<string, string>(ob.Id, ob.Name));
                    }                   

                    return lst;
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при запросе " + exc.Message);
                return null;
            }
        }

        public static List<KeyValuePair<string, string>> GetComboListByQuery(string query)
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
                                        
                    foreach (ListItem ob in context.ExecuteStoreQuery<ListItem>(query))
                    {
                        lst.Add(new KeyValuePair<string, string>(ob.Id, ob.Name));
                    }

                    return lst;                    
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при запросе " + exc.Message);
                return null;
            }
        }

        // заполнение DataGrid
        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters)
        {
            FillDataGrid(grid, bdc, query, filters, "");
        }

        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters, string orderby)
        {
            FillDataGrid(grid, bdc, query, filters, orderby, false);
        }

        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters, string orderby, bool saveOrder)
        {
            string sortedColumn = string.Empty;
            ListSortDirection order = ListSortDirection.Ascending;
            bool sorted = false;
            int index = 0;

            if (saveOrder && grid.SortOrder != SortOrder.None)
            {
                sorted = true;
                sortedColumn = grid.SortedColumn.Name;
                order = grid.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                index = grid.CurrentRow == null ? -1 : grid.CurrentRow.Index;
            }

            DataSet ds;
            DataTable dt;

            try
            {
                if (query != "")
                {
                    ds = bdc.GetDataSet(query + " " + filters + " " + orderby);
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                }

                DataView dv = new DataView(dt);
                dv.AllowNew = false;

                grid.DataSource = dv;
                grid.Columns["Id"].Visible = false;
                grid.Update();

                if (saveOrder && grid.Rows.Count > 0)
                {
                    if (sorted && grid.Columns.Contains(sortedColumn))
                        grid.Sort(grid.Columns[sortedColumn], order);
                    if (index >= 0 && index <= grid.Rows.Count)
                        grid.CurrentCell = grid[1, index];
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка сервера: " + ex.Message);
            }
        }

        public static void FillDataGrid(DataGridView grid, SQLClass bdc, string query, string filters, string orderby, bool saveOrder)
        {
            string sortedColumn = string.Empty;
            ListSortDirection order = ListSortDirection.Ascending;
            bool sorted = false;
            int index = 0;


            if (saveOrder && grid.SortOrder != SortOrder.None)
            {
                sorted = true;
                sortedColumn = grid.SortedColumn.Name;
                order = grid.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                index = grid.CurrentRow == null ? -1 : grid.CurrentRow.Index;
            }

            DataSet ds;
            DataTable dt;

            try
            {
                if (query != "")
                {
                    ds = bdc.GetDataSet(query + " " + filters + " " + orderby);
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                }

                DataView dv = new DataView(dt);
                dv.AllowNew = false;

                grid.DataSource = dv;
                grid.Columns["Id"].Visible = false;
                grid.Update();

                if (saveOrder && grid.Rows.Count > 0)
                {
                    if (sorted && grid.Columns.Contains(sortedColumn))
                        grid.Sort(grid.Columns[sortedColumn], order);
                    if (index >= 0 && index <= grid.Rows.Count)
                        grid.CurrentCell = grid[1, index];
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка сервера: " + ex.Message);
            }
        }
    }
}
