using System;

using System.Configuration;

using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement
{
    public partial class ForeignKey_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {


        #region page methods
        protected void Page_Load(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            PopulateListControl(ListBox1);

            //if (ListBox1.Items.Count == 0)
            //{
            if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
            {
                AddPleaseSelectItem();

                // ListBox1.Enabled = false;
            }

            //}

            lnkEntity.Text = ConfigurationManager.AppSettings["ForeignKeyTextNew"];

            string columnNameToAdd = "";
            if (Column.Name.Contains("Departure") || Column.Name.Contains("Arrival"))
            {
                columnNameToAdd = Column.Name.Replace("Departure", "").Replace("Arrival", "");
            }

            else
            {
                columnNameToAdd = Column.Name;
            }

            // lnkEntity.NavigateUrl = "../../" + columnNameToAdd + "/Insert.aspx";
            lnkEntity.NavigateUrl = String.Format(ConfigurationManager.AppSettings["ForeignKeyLinkInsert"], columnNameToAdd);

            lnkEntityDetails.Text = ConfigurationManager.AppSettings["ForeignKeyTextDetails"];

            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(DynamicValidator1);
        }

        private new MetaForeignKeyColumn Column
        {
            get
            {
                return (MetaForeignKeyColumn)base.Column;
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

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            string selectedValueString = GetSelectedValueString();
            ListItem item = ListBox1.Items.FindByValue(selectedValueString);
            if (item != null)
            {
                ListBox1.SelectedValue = selectedValueString;
            }

        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // If it's an empty string, change it to null
            string value = ListBox1.SelectedValue;
            if (String.IsNullOrEmpty(value))
            {
                value = null;
            }

            ExtractForeignKey(dictionary, value);
        }

        public override Control DataControl
        {
            get
            {
                return Utils.SortedListBox(ListBox1);
            }
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
        }
        #endregion page methods

    }
}
