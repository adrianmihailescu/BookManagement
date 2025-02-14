using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement.DynamicData.FieldTemplates.custom
{
    public partial class DateTimeNonEditable : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

                DateTime dtValueToDisplay = Convert.ToDateTime(valueToDisplay);

                if ((dtValueToDisplay == DateTime.MinValue) || (dtValueToDisplay == DateTime.MaxValue))
                {
                    dtValueToDisplay = DateTime.Now;
                }

                return (Convert.ToDateTime(dtValueToDisplay).ToString(ConfigurationManager.AppSettings["DateFormatDisplay"]));
            }
        }
    }
}