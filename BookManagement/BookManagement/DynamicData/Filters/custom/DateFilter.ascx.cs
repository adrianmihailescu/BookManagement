using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement.DynamicData.Filters
{
    public partial class DateFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        private const string NullValueString = "[null]";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = DatePicker1.Text;
            if (String.IsNullOrEmpty(selectedValue))
            {
                return source;
            }

            object value = selectedValue;
            if (selectedValue == NullValueString)
            {
                value = null;
            }
            if (DefaultValues != null)
            {
                DefaultValues[Column.Name] = value;
            }

            return ApplyEqualityFilter(source, Column.Name, value);

        }

        protected void DatePicker1_TextChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }
    }
}