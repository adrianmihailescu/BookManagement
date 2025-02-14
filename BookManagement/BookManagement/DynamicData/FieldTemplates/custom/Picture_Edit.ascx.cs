using System;
using System.Collections.Generic;

using System.Collections.Specialized;

using System.Data.Linq;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using System.IO;

namespace BookManagement.DynamicData.FieldTemplates
{
    public partial class Picture_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // dictionary[Column.Name] = ConvertEditedValue(fuBuckImage.PostedFile.FileName);
            if (fuBookImage.HasFile)
            {
                // Get the bytes from the uploaded file
                byte[] fileData = new byte[fuBookImage.PostedFile.InputStream.Length];
                fuBookImage.PostedFile.InputStream.Read(fileData, 0, fileData.Length);

                dictionary[Column.Name] = fileData;
            }

        }

        public override Control DataControl
        {
            get
            {
                return fuBookImage;
            }
        }

    }
}