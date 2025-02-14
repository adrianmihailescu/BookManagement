using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement
{
    public partial class DefaultEntityTemplate : System.Web.DynamicData.EntityTemplateUserControl
    {
        private MetaColumn currentColumn;

        protected override void OnLoad(EventArgs e)
        {
            Control item = new Control();
            try
            {
                foreach (MetaColumn column in Table.GetScaffoldColumns(Mode, ContainerType))
                {
                    currentColumn = column;
                    item = new _NamingContainer();
                    EntityTemplate1.ItemTemplate.InstantiateIn(item);
                    EntityTemplate1.Controls.Add(item);
                }
            }

            catch
            {
                throw;
            }

            finally
            {
                item.Dispose();
            }

        }

        protected void Label_Init(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Text = currentColumn.DisplayName;
        }

        protected void DynamicControl_Init(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.DataField = currentColumn.Name;
        }

        public class _NamingContainer : Control, INamingContainer { }

    }
}
