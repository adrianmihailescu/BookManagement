using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Configuration;

namespace BookManagement
{
    public partial class ForeignKeyFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        #region page methods
        private const string NullValueString = "[null]";
        private new MetaForeignKeyColumn Column
        {
            get
            {
                return (MetaForeignKeyColumn)base.Column;
            }
        }

        public override Control FilterControl
        {
            get
            {
                return Utils.SortedListBox(ListBox1);
                // return SortedDropDownList(DropDownList1);
            }
        }

        protected string GetTableName()
        {
            return Column.Name;
        }

        protected string GetPrimaryKeyColumnName()
        {
            return Column.ParentTable.PrimaryKeyColumns[0].Name;
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                ListBox1.Items.Clear();
                PopulateListControl(ListBox1);
                // PopulateListControl(SortedDropDownList(DropDownList1));

                //if (!Column.IsRequired)
                //{
                // ListBox1.Items.Add(new ListItem("[Not Set]", NullValueString));
                AddPleaseSelectItem();
                //}

                // Set the initial value if there is one
                string initialValue = DefaultValue;

                if (!String.IsNullOrEmpty(initialValue))
                {
                    ListBox1.SelectedValue = initialValue;
                }

                // add a hyperlink to the drop down
                //HyperLink lnkEntity = new HyperLink();
                lnkEntityNew.Text = ConfigurationManager.AppSettings["ForeignKeyTextNew"];

                string columnNameToAdd = "";
                if (Column.Name.Contains("Departure") || Column.Name.Contains("Arrival"))
                {
                    columnNameToAdd = Column.Name.Replace("Departure", "").Replace("Arrival", "");
                }

                else
                {
                    columnNameToAdd = Column.Name;
                }

                // lnkEntityNew.NavigateUrl = "../../" + columnNameToAdd + "/Insert.aspx";
                lnkEntityNew.NavigateUrl = String.Format(ConfigurationManager.AppSettings["ForeignKeyLinkInsert"], columnNameToAdd);
                lnkEntityDetails.Text = ConfigurationManager.AppSettings["ForeignKeyTextDetails"]; ;

                //ddlContainer.Controls.Add(lnkEntity);
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }

        /// <summary>
        /// adds a default item to the list
        /// </summary>
        protected void AddPleaseSelectItem()
        {
            ListItem defaultListItem = new ListItem(ConfigurationManager.AppSettings["ListPleaseSelect"], "");
            defaultListItem.Selected = true;

            ListBox1.Items.Add(defaultListItem);
        }

        /// <summary>
        /// refreshes the list with values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEntityRefresh_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            PopulateListControl(ListBox1);

            AddPleaseSelectItem();

            // PopulateListControl(SortedDropDownList(DropDownList1));
        }
        #endregion page methods

        #region query methods
        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = ListBox1.SelectedValue;

            if (String.IsNullOrEmpty(selectedValue))
            {
                return source;
            }

            if (selectedValue == NullValueString)
            {
                return ApplyEqualityFilter(source, Column.Name, null);
            }

            IDictionary dict = new Hashtable();
            Column.ExtractForeignKey(dict, selectedValue);
            foreach (DictionaryEntry entry in dict)
            {
                string key = (string)entry.Key;
                if (DefaultValues != null)
                {
                    DefaultValues[key] = entry.Value;
                }
                source = ApplyEqualityFilter(source, Column.GetFilterExpression(key), entry.Value);
            }
            return source;
        }
        #endregion query methods


    }
}
