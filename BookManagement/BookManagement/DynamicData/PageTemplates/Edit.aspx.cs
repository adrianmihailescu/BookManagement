using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

using BookManagement.db;
using BookManagement.BusinessObjects;

using System.Configuration;

using System.Linq;

namespace BookManagement
{
    public partial class Edit : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = table.DisplayName;
            DetailsDataSource.Include = table.ForeignKeyColumnsNames;

            string i = String.Empty;


            ///////////////////////////
            if ((Request.QueryString["IDBook"] != null) || (Request.QueryString["IDUser"] != null))
            {
                string strType = table.Name.ToLower();

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
        }

        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == DataControlCommands.CancelCommandName)
            {
                Response.Redirect(table.ListActionPath);
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            /*
            string strText = String.Format(ConfigurationManager.AppSettings["TheRecordHasBeenChanged"], table.Name);
            ShowInformationMessage(lblOperationResult, strText);
            */

            ShowMessageSuccess(showOperationResult, lblOperationResult, ConfigurationManager.AppSettings["TheRecordHasBeenChanged"], table.Name);

            // send an e-mail to the user with the changes
            if (table.Name.ToLower().Equals("lease"))
            {
                int strIDBook = Convert.ToInt32(Utils.GetStringValue(e.NewValues["IDBook"]));
                int strIDUser = Convert.ToInt32(Utils.GetStringValue(e.NewValues["IDUser"]));
                string strLeaseDate = Utils.GetStringValue(e.NewValues["LeaseDate"]);
                string strScheduledReturnDate = Utils.GetDateValue(e.NewValues["ScheduledReturnDate"]);
                string strActualReturnDate = Utils.GetDateValue(e.NewValues["ActualReturnDate"]);
                string strCopies = Utils.GetStringValue(e.NewValues["Copies"]);
                string strRemarks = Utils.GetStringValue(e.NewValues["Remarks"]);

                // get the values from joined tables

                BookLeaseInstance foundLease = new BookLeaseInstance();
                foundLease = Utils.GetBookLeaseInstanceByIDBookAndIDUser(strIDBook, strIDUser);


                // SendHTMLMail();

                try
                {
                    // book details
                    string foundBookName = foundLease.BookName;
                    string foundISBN = foundLease.ISBN;
                    string foundBookHasDisk = foundLease.HasDisk.ToString();

                    // author details
                    string foundAuthorName = foundLease.AuthorName;

                    // user details
                    string foundUserName = foundLease.UserName;
                    string foundUserFullName = foundLease.FullName;
                    string foundEmail = foundLease.Email;

                    string strSubject = ConfigurationManager.AppSettings["SmtpMailSubjectUpdate"];

                    string strBody = Utils.BuildMailMessageBody(
                        "<b>" + ConfigurationManager.AppSettings["SmtpMailHeaderUpdate"] + "</b>: "
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

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
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
        #endregion page methods

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

                throw; //ex;
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
        protected void CheckForLessOrEqualNumberOfCopies(FormViewUpdateEventArgs e)
        {
            int strIDBook = Convert.ToInt32(Utils.GetStringValue(e.NewValues["IDBook"]));
            string strCopies = Utils.GetStringValue(e.NewValues["Copies"]);

            int addingBookLeaseStock = Convert.ToInt32(e.NewValues["Copies"]);

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
        /// checks if the user enters an existing book category name
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameBookCategory(FormViewUpdateEventArgs e)
        {
            string strCopies = Utils.GetStringValue(e.NewValues["CategoryName"]);

            string addingBookCategory = e.NewValues["CategoryName"].ToString();

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
        /// checks if the user enters an existing isbn
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameBookISBN(FormViewUpdateEventArgs e)
        {
            // string strCopies = Utils.GetStringValue(e.NewValues["Copies"]);

            // string existingBookISBN = "";

            string addingBookISBN = e.NewValues["ISBN"].ToString().ToLower();
            string editingBookISBN = e.OldValues["ISBN"].ToString().ToLower();

            // get the values from joined tables

            BookInstance foundBook = Utils.GetBookInstanceByISBN(addingBookISBN);

            if (foundBook != null) // if we already have a book with the same isbn
            {
                if (!editingBookISBN.Equals(addingBookISBN))
                {

                    string strFoundBookISBN = foundBook.ISBN;


                    e.Cancel = true;

                    string strText = String.Format(ConfigurationManager.AppSettings["CantAddABookWithTheSameISBN"], foundBook.ISBN);
                    ShowErrorMessage(lblOperationResult, strText);
                }
            }
        }

        /// <summary>
        /// checks if the user inputs the same cnp for another user
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameUserCNP(FormViewUpdateEventArgs e)
        {
            // int strIDUser= Convert.ToInt32(Utils.GetStringValue(e.Values["IDUser"]));
            // string strCopies = Utils.GetStringValue(e.Values["Copies"]);

            string addingUserCNP = Convert.ToString(e.NewValues["CNP"]);
            string editingUserCNP = e.OldValues["CNP"].ToString().ToLower();

            // get the values from joined tables

            UserInstance foundUser = Utils.GetUserInstanceByCNP(addingUserCNP);

            if (foundUser != null)
            {
                if (!editingUserCNP.Equals(addingUserCNP))
                {
                    string strFoundUserISBN = foundUser.CNP;

                    e.Cancel = true;

                    string strText = String.Format(ConfigurationManager.AppSettings["CantAddAnUserWithTheSameCNP"], foundUser.CNP);
                    ShowErrorMessage(lblOperationResult, strText);
                }
            }
        }

        /// <summary>
        /// checks if the user enters an existing author name
        /// </summary>
        /// <param name="e"></param>
        protected void CheckForTheSameAuthorName(FormViewUpdateEventArgs e)
        {
            string addingAuthorName = e.NewValues["AuthorName"].ToString();

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
