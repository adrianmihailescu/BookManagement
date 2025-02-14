using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement.DynamicData.EntityTemplates
{
    public partial class Picture : System.Web.DynamicData.FieldTemplateUserControl
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
                string valueToReturn = "";

                if (base.FieldValue != null)
                    valueToReturn = "<img class=\"imgHeaderSmall\" src=\"" + "../DynamicData/Content/Images/ui/ok.png" + "\" />";
                else
                    valueToReturn = "";
                
                return valueToReturn;
            }
        }
    }
}