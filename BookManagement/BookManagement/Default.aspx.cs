using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookManagement
{
    public partial class _Default : System.Web.UI.Page
    {
        protected MetaTable table;
        
        int numberOfRowsInGrid = 0;

        #region page methods
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.SelectParameters["data"].DefaultValue = DateTime.Now.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }

        #endregion page methods        


        #region grid methods
        
        /// <summary>
        /// gets the actual sort direction of a column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        
        /// <summary>
        /// called when exporting the grid rows to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Utils.ExportGridToExcel(GridView1, ConfigurationManager.AppSettings["XlsExportName"], "Books with return due today");

        }

        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            Utils.SetGridRowHighlight(e);
        }

        /// <summary>
        /// called when sorting the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_Sorting(object sender, EventArgs e)
        {
            LinkButton foundControl = ((LinkButton)sender);
            // Response.Write(s);

            string columnName = foundControl.CommandName.Replace("lnkSort", "");
            string sortDirection = GetSortDirection(columnName);

            if (sortDirection == "ASC")
            {
                GridView1.Sort(columnName, SortDirection.Ascending);
            }
            else
            {
                GridView1.Sort(columnName, SortDirection.Descending);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Utils.GridView1_PageIndexChanging(GridView1, e);
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            numberOfRowsInGrid = e.AffectedRows;
        }

        /// <summary>
        /// returns the number of rows in grid
        /// </summary>
        /// <returns></returns>
        protected int GetNumberOfRowsInGrid()
        {
            return numberOfRowsInGrid;
        }

        /// <summary>
        /// gets the default date parameter's value
        /// </summary>
        /// <returns></returns>
        public DateTime GetDefaultParameterReportingValue()
        {
            if (SqlDataSource1.SelectParameters["data"].DefaultValue == null)
                return DateTime.Now;

            return Convert.ToDateTime(SqlDataSource1.SelectParameters["data"].DefaultValue);
        }

        #endregion grid methods
    }
}
