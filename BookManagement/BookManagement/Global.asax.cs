using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.DynamicData;
using System.Web.Routing;

using BookManagement.db;

using System.Collections;

namespace BookManagement
{
    public class Global : System.Web.HttpApplication
    {
        private static MetaModel s_defaultModel = new MetaModel();
        public static MetaModel DefaultModel
        {
            get
            {
                return s_defaultModel;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            //                    IMPORTANT: DATA MODEL REGISTRATION 
            // Uncomment this line to register an ADO.NET Entity Framework model for ASP.NET Dynamic Data.
            // Set ScaffoldAllTables = true only if you are sure that you want all tables in the
            // data model to support a scaffold (i.e. templates) view. To control scaffolding for
            // individual tables, create a partial class for the table and apply the
            // [ScaffoldTable(true)] attribute to the partial class.
            // Note: Make sure that you change "YourDataContextType" to the name of the data context
            // class in your application.
            DefaultModel.RegisterContext(typeof(BookManagementEntities), new ContextConfiguration() { ScaffoldAllTables = true });

            // The following statement supports separate-page mode, where the List, Detail, Insert, and 
            // Update tasks are performed by using separate pages. To enable this mode, uncomment the following 
            // route definition, and comment out the route definitions in the combined-page mode section that follows.
            routes.Add(new DynamicDataRoute("{table}/{action}.aspx")
            {
                Constraints = new RouteValueDictionary(new { action = "List|Details|Edit|Insert" }),
                Model = DefaultModel
            });

            // The following statements support combined-page mode, where the List, Detail, Insert, and
            // Update tasks are performed by using the same page. To enable this mode, uncomment the
            // following routes and comment out the route definition in the separate-page mode section above.
            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.List,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.Details,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});
        }

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // setez valorile parametrilor

            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                // parameters value
                DateTime selectedDateValue = DateTime.Now;

                HttpContext con = HttpContext.Current;
                string url = con.Request.Url.ToString();

                string message = ex.Message;

                string innerException = "";
                if (ex.InnerException != null)
                {
                    innerException = ex.InnerException.Message;
                }
                string stackTrace = ex.StackTrace;
                string source = ex.Source;

                ArrayList alListaParametri = new ArrayList();
                alListaParametri.Add("@Date");
                alListaParametri.Add("@Url");
                alListaParametri.Add("@Message");
                alListaParametri.Add("@InnerException");
                alListaParametri.Add("@StackTrace");
                alListaParametri.Add("@Source");

                ArrayList alValoriParametri = new ArrayList();
                alValoriParametri.Add(selectedDateValue);
                alValoriParametri.Add(url);
                alValoriParametri.Add(message);
                alValoriParametri.Add(innerException);
                alValoriParametri.Add(stackTrace);
                alValoriParametri.Add(source);

                string strProcedureName = "LogError";
                int operationResult = Utils.GenericOperation(strProcedureName, alListaParametri, alValoriParametri);
            }
        }

    }
}
