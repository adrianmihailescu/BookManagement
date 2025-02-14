using System;

using System.Collections;

using System.Data;
using System.Data.SqlClient;

using System.Drawing;

using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

using System.Linq;

using System.IO;
using System.Net;
using System.Net.Mail;

using BookManagement.db;
using BookManagement.BusinessObjects;

namespace BookManagement
{
    public partial class Details : System.Web.UI.Page
    {
        protected MetaTable table;
        protected BookManagementEntities context = new BookManagementEntities();

        #region page methods
        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);

            FormView1.SetMetaTable(table);
            DetailsDataSource.EntityTypeFilter = table.EntityType.Name;
        }

        /// <summary>
        /// called when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;
            DetailsDataSource.Include = table.ForeignKeyColumnsNames;

            string i = String.Empty;
            string strType = table.Name.ToLower();

            // ((LinkButton)FormView1.FindControl("LinkButtonDelete")).OnClientClick = String.Format(ConfigurationManager.AppSettings["EntityDeleteConfirmation"], table.Name.ToLower());

            if ((table.Name.ToLower() == "book") || (table.Name.ToLower() == "user")) // display the book preview only if we are viewing a book
            {
                if ((Request.QueryString["IDBook"] != null) || (Request.QueryString["IDUser"] != null))
                {
                    if (strType == "book")
                    {
                        i = Request.QueryString["IDBook"];
                    }

                    else if (strType == "user")
                    {
                        i = Request.QueryString["IDUser"];
                    }

                    imgShowPreview.ImageUrl = "~/UI/Handlers/ImageHandler.ashx?id=" + i + "&type=" + strType;
                    imgShowPreview.AlternateText = (String.Format(ConfigurationManager.AppSettings["NoImageAvailableLine1"], table.Name.ToLower()));
                }

                ///////
                int currentIndex = Convert.ToInt32(i);

                // var foundLeaseTemp = Utils.GetBookLeaseListByBookId(currentBookIndex);
                var foundLease = Utils.GetBookLeaseInstanceByBookId(currentIndex);

                if (table.Name.ToLower().Equals("user"))
                {
                    foundLease = Utils.GetBookLeaseInstanceByUserId(currentIndex);
                }

                // var foundLease = foundLeaseTemp.OrderBy(p => p.ScheduledReturnDate).FirstOrDefault();

                string strEarliestReturnDate = "";
                string strEarliestReturnUser = "";
                string strEarliestReturnCopies = "";

                if (foundLease != null)
                {
                    ShowNearestDateReturnInformation(foundLease, ref strEarliestReturnDate, ref strEarliestReturnUser, ref strEarliestReturnCopies);

                }

                else if (table.Name.ToLower() == "book")
                {
                    lblBookScheduledToBeReturned.Text = ConfigurationManager.AppSettings["BookHasntBeenLeasedYet"];
                }
                ///////
            }
        }

        /// <summary>
        /// gets the default date parameter's value 03/13/2013
        /// </summary>
        /// <returns></returns>
        public DateTime GetDefaultParameterReportingValue()
        {
            if (SqlDataSource1.SelectParameters["data"].DefaultValue == null)
                return DateTime.Now;

            return Convert.ToDateTime(SqlDataSource1.SelectParameters["data"].DefaultValue);
        }

        /// <summary>
        /// checks if an user has books scheduled to be returned for a specific date
        /// </summary>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        protected int CheckIfUserHasScheduledBooksForToday(DateTime data, int IDUser)
        {
            // setez valorile parametrilor
            DateTime selectedDateValue = data;
            int selectedIDUser = IDUser;

            ArrayList alListaParametri = new ArrayList();
            alListaParametri.Add("@IDUser");
            alListaParametri.Add("@data");

            ArrayList alValoriParametri = new ArrayList();
            alValoriParametri.Add(selectedIDUser);
            alValoriParametri.Add(selectedDateValue);

            string strProcedureName = "GetScheduledBooksForTodayByUser";
            int operationResult = Utils.GenericOperation(strProcedureName, alListaParametri, alValoriParametri);

            return operationResult;
        }       

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            ShowMessageSuccess(showOperationResult, lblOperationResult, ConfigurationManager.AppSettings["TheRecordHasBeenDeleted"], table.Name);

            if (table.Name.ToLower().Equals("lease"))
            {
                int strIDLease = Convert.ToInt32(Utils.GetStringValue(e.Keys["IDLease"]));


                // get the values from joined tables

                BookLeaseInstance foundLease = Utils.GetBookLeaseInstanceByIDLease(strIDLease);

                BuildMailMessageFromLease(foundLease, ConfigurationManager.AppSettings["SmtpMailSubjectDelete"], ConfigurationManager.AppSettings["YourBookLeaseHasBeenDeletedOn"]);
            }
        }

        protected void FormView1_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            if (table.Name.ToLower().Equals("lease"))
            {
                int strIDLease = Convert.ToInt32(Utils.GetStringValue(e.Keys["IDLease"]));

                int addingBookLeaseStock = Convert.ToInt32(e.Values["Copies"]);

                // get the values from joined tables

                BookLeaseInstance foundLease = Utils.GetBookLeaseInstanceByIDLease(strIDLease);

                if (foundLease != null)
                {
                    int strFoundLeaseStatus = Convert.ToInt32(foundLease.FlagDel);

                    if (strFoundLeaseStatus == 1) // if the user tries to borrow more copies than the existing stock in library
                    {
                        e.Cancel = true;

                        string strText = ConfigurationManager.AppSettings["ThisLeaseHasAlreadyBeenDeleted"];
                        ShowErrorMessage(lblOperationResult, strText);
                    }

                    else
                    {
                        BuildMailMessageFromLease(foundLease, ConfigurationManager.AppSettings["SmtpMailSubjectDelete"], ConfigurationManager.AppSettings["YourBookLeaseHasBeenDeletedOn"]);
                    }
                }
            }

        }
        #endregion page methods

        #region messages

        /// <summary>
        /// builds a mail message from the selected book lease
        /// </summary>
        /// <param name="foundLease"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailHeader"></param>
        protected void BuildMailMessageFromLease(BookLeaseInstance foundLease, string mailSubject, string mailHeader)
        {
            try
            {
                // book details
                string foundBookName = foundLease.BookName;
                string foundISBN = foundLease.ISBN;
                string foundHasDisk = foundLease.HasDisk.ToString();

                // author details
                string foundAuthorName = foundLease.AuthorName;

                // user details
                string foundUserName = foundLease.UserName;
                string foundUserFullName = foundLease.FullName;
                string foundEmail = foundLease.Email;
                string strLeaseDate = foundLease.LeaseDate.ToString();
                string strScheduledReturnDate = foundLease.ScheduledReturnDate.ToString();
                string strActualReturnDate = foundLease.ActualReturnDate.ToString();
                string strCopies = foundLease.Copies.ToString();
                string strRemarks = foundLease.Remarks;


                string strSubject = mailSubject;

                string strBody = Utils.BuildMailMessageBody(
                    "<b>" + mailHeader + "</b>: "
                    , foundUserName
                    , foundUserFullName
                    , foundEmail
                    , foundBookName
                    , foundISBN
                    , foundHasDisk
                    , strLeaseDate
                    , strScheduledReturnDate
                    , strActualReturnDate
                    , strCopies
                    , strRemarks);

                ////// send the email
                Utils.SendMailToUser(foundEmail, strSubject, strBody);
                // the e-mail notification has been sent
                showEmailNotificationResult.CssClass = "divInvisible";
            }

            catch
            {
                // no e-mail notification has been sent
                showEmailNotificationResult.CssClass = "divVisible";
                // throw ex;
            }
        }

        /// <summary>
        /// displays a success operation panel
        /// </summary>
        /// <param name="pnlToShow"></param>
        /// <param name="lblToShow"></param>
        /// <param name="strText"></param>
        /// <param name="tableName"></param>
        protected void ShowMessageSuccess(Panel pnlToShow, Label lblToShow, string strText, string tableName)
        {
            pnlToShow.CssClass = "divVisibleHeader";

            strText = String.Format(strText, tableName);
            ShowInformationMessage(lblToShow, strText);
        }

        /// <summary>
        /// shows an error message
        /// </summary>
        /// <param name="lblToShow"></param>
        /// <param name="strText"></param>
        protected void ShowErrorMessage(Label lblToShow, string strText)
        {
            lblToShow.ForeColor = System.Drawing.Color.Red;
            lblToShow.Text = strText;

            showOperationResult.CssClass = "divVisibleHeader";
            imgOperationResultInformation.CssClass = "divInvisibleHeaderSmall";
            imgOperationResultError.CssClass = "divVisibleHeaderSmall";
        }

        /// <summary>
        /// shows an information messaghe
        /// </summary>
        /// <param name="lblToShow"></param>
        /// <param name="strText"></param>
        protected void ShowInformationMessage(Label lblToShow, string strText)
        {
            lblToShow.ForeColor = System.Drawing.Color.Green;
            lblToShow.Text = strText;

            showOperationResult.CssClass = "divVisibleHeader";
            imgOperationResultInformation.CssClass = "divVisibleHeaderSmall";
            imgOperationResultError.CssClass = "divInvisibleHeaderSmall";
        }
        #endregion messages

        #region calendar
        /// <summary>
        /// renders a day of calendar according to the number of book leases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void userCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            HyperLink lnkNavigateToLease = new HyperLink();

            try
            {
                if (Request.QueryString["IDUser"] != null)
                {
                    int idUser = Convert.ToInt32(Request.QueryString["IDUser"]);
                    DateTime selectedDate = e.Day.Date;

                    int result = CheckIfUserHasScheduledBooksForToday(selectedDate, idUser);

                    if (result > 0)
                    {
                        // Label b = new Label();

                        lnkNavigateToLease.NavigateUrl = String.Format(String.Format(ConfigurationManager.AppSettings["EntityUserNavigartLeaseIDUser"], idUser));
                        lnkNavigateToLease.Text = "(" + result + ")";
                        lnkNavigateToLease.Target = "_blank";

                        e.Cell.Controls.Add(lnkNavigateToLease);

                        e.Cell.BackColor = Color.DarkOrange;
                        e.Cell.ForeColor = Color.White;
                        // b.Dispose();
                    }
                }
            }

            catch
            {
                throw;
            }

            finally
            {
                // dispose controls
                lnkNavigateToLease.Dispose();
            }
        }
        #endregion calendar

        #region additional info
        /// <summary>
        /// displays informations about the nearest user to return the book and the nearest day
        /// </summary>
        /// <param name="foundLease"></param>
        /// <param name="strEarliestReturnDate"></param>
        /// <param name="strEarliestReturnUser"></param>
        /// <param name="strEarliestReturnCopies"></param>
        private void ShowNearestDateReturnInformation(BookLeaseInstance foundLease, ref string strEarliestReturnDate, ref string strEarliestReturnUser, ref string strEarliestReturnCopies)
        {
            strEarliestReturnDate = foundLease.ScheduledReturnDate.ToString(ConfigurationManager.AppSettings["DateFormatDisplay"]);
            strEarliestReturnUser = foundLease.FullName.ToString();
            strEarliestReturnCopies = foundLease.Copies.ToString();

            if (table.Name.ToLower() == "book")
            {
                lblBookScheduledToBeReturned.Text =
                    ConfigurationManager.AppSettings["BookIsScheduledToBeReturnedOn"]
                    + "<br />" + strEarliestReturnUser
                    + "<br /><br />on: " + strEarliestReturnDate
                    + "<br />copies: " + strEarliestReturnCopies;

                pnlAdvancedInformations.ToolTip = lblBookScheduledToBeReturned.Text.Replace("<br />", "\r\n");
            }
        }
        #endregion additional info
    }
}
