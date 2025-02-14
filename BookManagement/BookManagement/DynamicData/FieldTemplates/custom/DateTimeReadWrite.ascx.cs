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
    public partial class DateTimeReadWriteField : System.Web.DynamicData.FieldTemplateUserControl
    {
        public override Control DataControl
        {
            get
            {
                return Literal1;
            }
        }

        public override string FieldValueString
        {
            get
            {
                object valueToDisplay = base.FieldValue;
                // object valueToDisplay = null;

                if (valueToDisplay != null)
                    {
                        return Convert.ToDateTime(valueToDisplay).ToString(ConfigurationManager.AppSettings["DateFormatDisplay"]);
                    }

                return ""; // format dates
            }
        }

    }
}
