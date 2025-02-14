using System;
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

using System.Configuration;

using BookManagement.db;
using BookManagement.BusinessObjects;


namespace BookManagement
{
    public partial class Insert : System.Web.UI.Page
    {
        protected MetaTable table;

        protected BookManagementEntities context = new BookManagementEntities();

        #region page methods
        protected void Page_Init(object sender, EventArgs e)
        {
            table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
            FormView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
            DetailsDataSource.EntityTypeFilter = table.EntityType.Name;

            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;
            // DynamicHyperLinkBack.CssClass = "displayNone";

        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == DataControlCommands.CancelCommandName)
            {
                Response.Redirect(table.ListActionPath);
            }
        }
        #endregion page methods

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            if (table.Name.ToLower().Equals("lease"))
            {
                CheckForLessOrEqualNumberOfCopies(e);
            }

            else if (table.Name.ToLower().Equals("book"))
            {
                CheckForTheSameBookISBN(e);
            }

            else if (table.Name.ToLower().Equals("category"))
            {
                CheckForTheSameBookCategory(e);
            }

            else if (table.Name.ToLower().Equals("author"))
            {
                CheckForTheSameAuthorName(e);
            }

            else if (table.Name.ToLower().Equals("user"))
            {
                CheckForTheSameUserCNP(e);
            }
        }       

        /// <summary>
        /// executed after the insert statement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            string strText = String.Empty;

            ShowMessageSuccess(showOperationResult, lblOperationResult, ConfigurationManager.AppSettings["TheNewRecordHasBeenAdded"], table.Name);
            /////
            Utils.ShowBackLinkOnEntity(DynamicHyperLinkBack, table.Name, e);

            if (table.Name.ToLower().Equals("lease"))
            {
                int strIDBook = Convert.ToInt32(Utils.GetStringValue(e.Values["IDBook"]));
                int strIDUser = Convert.ToInt32(Utils.GetStringValue(e.Values["IDUser"]));
                string strLeaseDate = Utils.GetStringValue(e.Values["LeaseDate"]);
                string strScheduledReturnDate = Utils.GetDateValue(e.Values["ScheduledReturnDate"]);
                string strActualReturnDate = Utils.GetDateValue(e.Values["ActualReturnDate"]);
                string strCopies = Utils.GetStringValue(e.Values["Copies"]);
                string strRemarks = Utils.GetStringValue(e.Values["Remarks"]);

                // get the values from joined tables

                BookLeaseInstance foundLease = new BookLeaseInstance();
                foundLease = Utils.GetBookLeaseInstanceByIDBookAndIDUser(strIDBook, strIDUser);

                // SendHTMLMail();

                try
                {
                    // book details
                    string foundBookName = foundLease.BookName;
                    string foundBookHasDisk = foundLease.HasDisk.ToString();
                    string foundISBN = foundLease.ISBN;

                    // author details
                    string foundAuthorName = foundLease.AuthorName;

                    // user details
                    string foundUserName = foundLease.UserName;
                    string foundUserFullName = foundLease.FullName;
                    string foundEmail = foundLease.Email;

                    string strSubject = ConfigurationManager.AppSettings["SmtpMailSubjectInsert"];

                    string strBody = Utils.BuildMailMessageBody(
                        "<b>" + ConfigurationManager.AppSettings["NewBookHasBeenLeasedToYouOn"] + "</b>: "
                        , foundUserName
                        , foundUserFullName
                        , foundEmail
                        , foundBookName
                        , foundISBN
                        , foundBookHasDisk
                        , strLeaseDate
                        , strScheduledReturnDate
                        , strActualReturnDate
                        , strCopies
                        , strRemarks);


                    // string strImageUrl = "<img src=\"~/Handlers/ImageHandler.asxh?id=25\" />";

                    ////// send the email
                    // ShowInformationMessage(lblOperationResult, strText);

                    SendMailToUser(foundEmail, strSubject, strBody);
                    // the e-mail notification has been sent
                    showEmailNotificationResult.CssClass = "divInvisible";
                }

                catch
                {
                    // no e-mail notification has been sent
                    showEmailNotificationResult.CssClass = "divVisibleHeader";
                }
            }
        }

        #region mail methods
        /// <summary>
        /// sends an e-mail to the specified user
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="body"></param>
        public void SendMailToUser(string toUser, string subject, string body)
        {
            try
            {
                Utils.SendMailToUser(toUser, subject, body);
            }

            catch
            {
                // no mail has been sent
                // showEmailNotificationResult.CssClass = "divVisible";

                throw;// ex;
            }
            // reader.Dispose();
        }
        #endregion mail methods

        #region messages
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

        #region check
        /// <summary>
        /// checks if the user returns a greater number of items for this book
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForLessOrEqualNumberOfCopies(FormViewInsertEventArgs e)
        {
            int strIDBook = Convert.ToInt32(Utils.GetStringValue(e.Values["IDBook"]));
            string strCopies = Utils.GetStringValue(e.Values["Copies"]);

            int addingBookLeaseStock = Convert.ToInt32(e.Values["Copies"]);

            // get the values from joined tables

            BookInstance foundBook = Utils.GetBookInstanceByIDBook(strIDBook);

            if (foundBook != null)
            {
                int strFoundBookStock = foundBook.Stock;

                if (addingBookLeaseStock > strFoundBookStock) // if the user tries to borrow more copies than the existing stock in library
                {
                    e.Cancel = true;

                    string strText = String.Format(ConfigurationManager.AppSettings["CantAddLeaseQuantityGreaterThanExistingStock"], foundBook.Stock);
                    ShowErrorMessage(lblOperationResult, strText);
                }
            }
        }

        /// <summary>
        /// checks if the user inputs the same cnp for another user
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameUserCNP(FormViewInsertEventArgs e)
        {
            // int strIDUser= Convert.ToInt32(Utils.GetStringValue(e.Values["IDUser"]));
            // string strCopies = Utils.GetStringValue(e.Values["Copies"]);

            string addingUserCNP = Convert.ToString(e.Values["CNP"]);

            // get the values from joined tables

            UserInstance foundUser = Utils.GetUserInstanceByCNP(addingUserCNP);

            if (foundUser != null)
            {
                string strFoundUserISBN = foundUser.CNP;

                e.Cancel = true;

                string strText = String.Format(ConfigurationManager.AppSettings["CantAddAnUserWithTheSameCNP"], foundUser.CNP);
                    ShowErrorMessage(lblOperationResult, strText);
            }
        }

        /// <summary>
        /// checks if the user enters an existing isbn
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameBookISBN(FormViewInsertEventArgs e)
        {
            string strCopies = Utils.GetStringValue(e.Values["Copies"]);

            string addingBookISBN = e.Values["ISBN"].ToString();

            // get the values from joined tables

            BookInstance foundBook = Utils.GetBookInstanceByISBN(addingBookISBN);

            if (foundBook != null) // if we already have a book with the same isbn
            {
                string strFoundBookISBN = foundBook.ISBN;


                e.Cancel = true;

                string strText = String.Format(ConfigurationManager.AppSettings["CantAddABookWithTheSameISBN"], foundBook.ISBN);
                ShowErrorMessage(lblOperationResult, strText);
            }
        }

        /// <summary>
        /// checks if the user enters an existing category name
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameBookCategory(FormViewInsertEventArgs e)
        {
            string strCopies = Utils.GetStringValue(e.Values["CategoryName"]);

            string addingBookCategory = e.Values["CategoryName"].ToString();

            // get the values from joined tables

            CategoryInstance foundCategory = Utils.GetCategoryInstanceByName(addingBookCategory);

            if (foundCategory != null) // if we already have a book with the same isbn
            {
                string strFoundBookCategory = foundCategory.CategoryName;


                e.Cancel = true;

                string strText = String.Format(ConfigurationManager.AppSettings["CantAddABookCategoryWithTheSameName"], foundCategory.CategoryName);
                ShowErrorMessage(lblOperationResult, strText);
            }
        }

        /// <summary>
        /// checks if the user enters an existing author name
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameAuthorName(FormViewInsertEventArgs e)
        {
            string addingAuthorName= e.Values["AuthorName"].ToString();

            // get the values from joined tables

            AuthorInstance foundAuthor = Utils.GetAuthorInstanceByName(addingAuthorName);

            if (foundAuthor != null) // if we already have a book with the same isbn
            {
                string strFoundAuthorName = foundAuthor.AuthorName;


                e.Cancel = true;

                string strText = String.Format(ConfigurationManager.AppSettings["CantAddAnAuthorWithTheSameName"], foundAuthor.AuthorName);
                ShowErrorMessage(lblOperationResult, strText);
            }
        }
        #endregion check
    }
}
