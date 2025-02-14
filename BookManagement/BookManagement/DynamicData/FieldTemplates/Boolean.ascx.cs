using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement
{
    public partial class BooleanField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            /*
            object val = FieldValue;
            if (val != null)
            {
                CheckBox1.Checked = (bool)val;
            }
            */
        }

        public override Control DataControl
        {            
            get
            {
                // return CheckBox1;
                return Literal1;
            }
        }

        public override string FieldValueString
        {
            get
            {
                string valueToReturn = "";

                if ((base.FieldValue != null) && (Convert.ToUInt32(base.FieldValue) != 0))
                    valueToReturn = "<img id=\"imgHasFieldValue\" class=\"imgHeaderSmall\" src=\"" + "../DynamicData/Content/Images/ui/ok.png" + "\" />";
                else
                    valueToReturn = "";

                return valueToReturn;
            }
        }

    }
}
