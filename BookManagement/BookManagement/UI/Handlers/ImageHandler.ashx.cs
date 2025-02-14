using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using BookManagement.db;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Drawing;
using System.Drawing.Imaging;


namespace BookManagement.DynamicData.FieldTemplates
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if ((context.Request.QueryString["id"] != null) && (context.Request.QueryString["type"] != null))
            {
                int imgId = 0; string type = "";
                
                imgId = Convert.ToInt32(context.Request.QueryString["id"]);
                type = Convert.ToString(context.Request.QueryString["type"]);

                // MemoryStream memoryStream =new MemoryStream();
                // Image imgFromGB;
                Image imgFromDB = Utils.GetPreviewImageFromDB(context, imgId, type);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}