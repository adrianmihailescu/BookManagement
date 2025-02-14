using System;

using System.Data;
using System.Data.SqlClient;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

using System.Net.Mail;
using System.Configuration;

using BookManagement.db;
using BookManagement.BusinessObjects;

namespace BookManagement
{   
    public class Utils
    {
        protected static BookManagementEntities context = new BookManagementEntities();
        protected static string connectionString = ConfigurationManager.ConnectionStrings["BookManagementConnectionString"].ConnectionString;

        #region sql
        /// <summary>
        /// intoarce o comanda generica de tip SqlCommand
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="strNumeProcedura"></param>
        /// <returns></returns>
        protected static SqlCommand GetSqlCommand(string connectionString, string strNumeProcedura)
        {
            /////////////////////////////////

            // Create a new data adapter based on the specified query.
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlConnection.Open();

                sqlCommand = new SqlCommand(strNumeProcedura, sqlConnection);

                // transmit valorile parametrilor
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = strNumeProcedura;
                /////////////////////////////

                return sqlCommand;
            }

            catch
            {
                sqlCommand = new SqlCommand();
                return sqlCommand;
            }
        }

        /// <summary>
        /// o metoda generica pentru adaugare / modificare
        /// </summary>
        /// <param name="strNumeProcedura"></param>
        /// <param name="alListaParametri"></param>
        /// <param name="alValoriParametri"></param>
        public static int GenericOperation(string strNumeProcedura, ArrayList alListaParametri, ArrayList alValoriParametri)
        {
            SqlCommand sqlCommand = GetSqlCommand(connectionString, strNumeProcedura);

            /////////////
            int idMesajProcedura = 0;

            // adaug parametrii si valorile
            for (int i = 0; i < alListaParametri.Count; i++)
            {
                sqlCommand.Parameters.AddWithValue(alListaParametri[i].ToString(), alValoriParametri[i]);
            }

            try
            {
                idMesajProcedura = sqlCommand.ExecuteNonQuery();
                //////////////////////////

                return idMesajProcedura;
            }

            catch
            {
                // return idMesajProcedura;
                throw;
            }

            finally
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                    sqlCommand.Connection.Close();
            }
        }
        #endregion sql

        #region linq

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentBookIndex"></param>
        /// <returns></returns>
        public static BookLeaseInstance GetBookLeaseInstanceByBookId(int currentBookIndex)
        {

            BookLeaseInstance newBookLease = null;


            var foundLease = (from b in context.Lease
                                  where ((b.Book.IDBook == currentBookIndex) && (b.FlagDel == false))
                                  select new
                                  {
                                      BookName = b.Book.BookName
                                      , ISBN = b.Book.ISBN
                                      , AuthorName = b.Book.Author.AuthorName
                                      , HasDisk = b.Book.HasDisk
                                      , UserName = b.User.UserName
                                      , FullName = b.User.FullName
                                      , Email = b.User.Email

                                      , ScheduledReturnDate = b.ScheduledReturnDate
                                      , ActualReturnDate = b.ActualReturnDate
                                      , Copies = b.Copies
                                      , Remarks = b.Remarks
                                      , FlagDel = b.FlagDel
                                  }).FirstOrDefault();

            if (foundLease != null)
            {
                newBookLease = new BookLeaseInstance();

                newBookLease.BookName = foundLease.BookName;
                newBookLease.ISBN = foundLease.ISBN;
                newBookLease.AuthorName = foundLease.AuthorName;
                newBookLease.HasDisk = foundLease.HasDisk;
                newBookLease.UserName = foundLease.UserName;
                newBookLease.FullName = foundLease.FullName;
                newBookLease.Email = foundLease.Email;
                /////
                newBookLease.ScheduledReturnDate = foundLease.ScheduledReturnDate;
                newBookLease.ActualReturnDate = foundLease.ActualReturnDate;
                newBookLease.Copies = foundLease.Copies;
                newBookLease.Remarks = foundLease.Remarks;
                newBookLease.FlagDel = foundLease.FlagDel;
            }

            return newBookLease;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentBookIndex"></param>
        /// <returns></returns>
        public static BookLeaseInstance GetBookLeaseInstanceByUserId(int currentIndex)
        {

            BookLeaseInstance newBookLease = null;


            var foundLease = (from b in context.Lease
                              where ((b.User.IDUser == currentIndex) && (b.FlagDel == false))
                              select new
                              {
                                  BookName = b.Book.BookName
                                  , ISBN = b.Book.ISBN
                                  , AuthorName = b.Book.Author.AuthorName
                                  , HasDisk = b.Book.HasDisk
                                  , UserName = b.User.UserName
                                  , FullName = b.User.FullName
                                  , Email = b.User.Email

                                  , ScheduledReturnDate = b.ScheduledReturnDate
                                  , ActualReturnDate = b.ActualReturnDate
                                  , Copies = b.Copies
                                  , Remarks = b.Remarks
                                  , FlagDel = b.FlagDel
                              }).FirstOrDefault();

            if (foundLease != null)
            {
                newBookLease = new BookLeaseInstance();

                newBookLease.BookName = foundLease.BookName;
                newBookLease.ISBN = foundLease.ISBN;
                newBookLease.AuthorName = foundLease.AuthorName;
                newBookLease.HasDisk = foundLease.HasDisk;
                newBookLease.UserName = foundLease.UserName;
                newBookLease.FullName = foundLease.FullName;
                newBookLease.Email = foundLease.Email;
                /////
                newBookLease.ScheduledReturnDate = foundLease.ScheduledReturnDate;
                newBookLease.ActualReturnDate = foundLease.ActualReturnDate;
                newBookLease.Copies = foundLease.Copies;
                newBookLease.Remarks = foundLease.Remarks;
                newBookLease.FlagDel = foundLease.FlagDel;
            }

            return newBookLease;
        }

        /// <summary>
        /// Gets a book lease instance identified by book and user
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static BookLeaseInstance GetBookLeaseInstanceByIDLease(int strIDLease)
        {
            BookLeaseInstance newBookLease = null;

            var foundLease = (from b in context.Lease
                              where (b.IDLease == strIDLease)
                              select new
                              {
                                  BookName = b.Book.BookName
                                  , ISBN = b.Book.ISBN
                                  , AuthorName = b.Book.Author.AuthorName
                                  , HasDisk = b.Book.HasDisk
                                  , UserName = b.User.UserName
                                  , FullName = b.User.FullName
                                  , Email = b.User.Email
                                  
                                  , ScheduledReturnDate = b.ScheduledReturnDate
                                  , ActualReturnDate = b.ActualReturnDate
                                  , Copies = b.Copies
                                  , Remarks = b.Remarks
                                  , FlagDel = b.FlagDel
                              }).FirstOrDefault();

            if (foundLease != null)
            {
                newBookLease = new BookLeaseInstance();

                newBookLease.BookName = foundLease.BookName;
                newBookLease.ISBN = foundLease.ISBN;
                newBookLease.AuthorName = foundLease.AuthorName;
                newBookLease.HasDisk = foundLease.HasDisk;
                newBookLease.UserName = foundLease.UserName;
                newBookLease.FullName = foundLease.FullName;
                newBookLease.Email = foundLease.Email;
                /////
                newBookLease.ScheduledReturnDate = foundLease.ScheduledReturnDate;
                newBookLease.ActualReturnDate = foundLease.ActualReturnDate;
                newBookLease.Copies = foundLease.Copies;
                newBookLease.Remarks = foundLease.Remarks;
                newBookLease.FlagDel = foundLease.FlagDel;
            }

            return newBookLease;
        }

        /// <summary>
        /// Gets a book lease instance identified by book and user
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static BookLeaseInstance GetBookLeaseInstanceByIDBookAndIDUser(int strIDBook, int strIDUser)
        {
            BookLeaseInstance newBookLease = null;

            var foundLease = (from b in context.Lease
                              where (b.IDBook == strIDBook) && (b.IDUser == strIDUser)
                              select new
                              {
                                  BookName = b.Book.BookName
                                  , ISBN = b.Book.ISBN
                                  , AuthorName = b.Book.Author.AuthorName
                                  , HasDisk = b.Book.HasDisk
                                  , UserName = b.User.UserName
                                  , FullName = b.User.FullName
                                  , Email = b.User.Email
                                  , ScheduledReturnDate = b.ScheduledReturnDate
                                  , ActualReturnDate = b.ActualReturnDate
                                  , Copies = b.Copies
                                  , Remarks = b.Remarks
                              }).FirstOrDefault();

            if (foundLease != null)
            {
                newBookLease = new BookLeaseInstance();

                newBookLease.BookName = foundLease.BookName;
                newBookLease.ISBN = foundLease.ISBN;
                newBookLease.AuthorName = foundLease.AuthorName;
                newBookLease.HasDisk = foundLease.HasDisk;
                newBookLease.UserName = foundLease.UserName;
                newBookLease.FullName = foundLease.FullName;
                newBookLease.Email = foundLease.Email;
                /////
                newBookLease.ScheduledReturnDate = foundLease.ScheduledReturnDate;
                newBookLease.ActualReturnDate = foundLease.ActualReturnDate;
                newBookLease.Copies = foundLease.Copies;
                newBookLease.Remarks = foundLease.Remarks;
            }
            return newBookLease;
        }

        /// <summary>
        /// Gets a book instance identified by book
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static BookInstance GetBookInstanceByIDBook(int strIDBook)
        {
            BookInstance newBook = null;

            var foundBook = (from b in context.Book
                              where (b.IDBook == strIDBook)
                              select new
                              {
                                  BookName = b.BookName
                                  , ISBN = b.ISBN
                                  , Stock = b.Stock
                                  , HasDisk = b.HasDisk
                                  , AuthorName = b.Author.AuthorName
                                  , IDAuthor = b.IDAuthor
                                  , IDBook = b.IDBook
                                  , IDCategory = b.IDCategory
                                  , CategoryName = b.Category.CategoryName
                              }).FirstOrDefault();

            if (foundBook != null)
            {
                newBook = new BookInstance();

                newBook.BookName = foundBook.BookName;
                newBook.ISBN = foundBook.ISBN;
                newBook.AuthorName = foundBook.AuthorName;
                newBook.Stock = foundBook.Stock;
                newBook.HasDisk = foundBook.HasDisk;
                newBook.IDAuthor = foundBook.IDAuthor;
                newBook.IDBook = foundBook.IDBook;
                newBook.CategoryName = foundBook.CategoryName;
            }
            return newBook;
        }

        /// <summary>
        /// Gets a book lease instance identified by ISBN
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static BookInstance GetBookInstanceByISBN(string strISBN)
        {
            BookInstance newBook = null;

            var foundBook = (from b in context.Book
                             where (b.ISBN.ToLower().Equals(strISBN.ToLower()))
                             select new
                             {
                                 BookName = b.BookName
                                 , ISBN = b.ISBN
                                 , Stock = b.Stock
                                 , HasDisk = b.HasDisk
                                 , AuthorName = b.Author.AuthorName
                                 , IDAuthor = b.IDAuthor
                                 , IDBook = b.IDBook
                                 , IDCategory = b.IDCategory
                                 , CategoryName = b.Category.CategoryName
                             }).FirstOrDefault();

            if (foundBook != null)
            {
                newBook = new BookInstance();
                newBook.BookName = foundBook.BookName;
                newBook.ISBN = foundBook.ISBN;
                newBook.AuthorName = foundBook.AuthorName;
                newBook.Stock = foundBook.Stock;
                newBook.HasDisk = foundBook.HasDisk;
                newBook.IDAuthor = foundBook.IDAuthor;
                newBook.IDBook = foundBook.IDBook;
                newBook.CategoryName = foundBook.CategoryName;
            }

            return newBook;
        }

        /// <summary>
        /// Gets a book category instance identified by name
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static CategoryInstance GetCategoryInstanceByName(string strCategoryName)
        {
            CategoryInstance newBook = null;

            var foundCategory = (from b in context.Category
                             where (b.CategoryName.ToLower().Equals(strCategoryName.ToLower()))
                             select new
                             {
                                 IDCategory = b.IDCategory
                                 , CategoryName = b.CategoryName
                             }).FirstOrDefault();
            if (foundCategory != null)
            {
                newBook = new CategoryInstance();

                newBook.IDCategory = foundCategory.IDCategory;
                newBook.CategoryName = foundCategory.CategoryName;
            }

            return newBook;
        }

        /// <summary>
        /// Gets an author instance identified by name
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static AuthorInstance GetAuthorInstanceByName(string strAuthorName)
        {
            AuthorInstance newAuthor = null;

            var foundAuthor = (from b in context.Author
                               where (b.AuthorName.ToLower().Equals(strAuthorName.ToLower()))
                                 select new
                                 {
                                     IDAuthor = b.IDAuthor
                                     , AuthorName = b.AuthorName
                                 }).FirstOrDefault();

            if (foundAuthor != null)
            {
                newAuthor = new AuthorInstance();

                newAuthor.IDAuthor = foundAuthor.IDAuthor;
                newAuthor.AuthorName = foundAuthor.AuthorName;
            }

            return newAuthor;
        }

        /// <summary>
        /// Gets an user instance identified by user
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static UserInstance GetUserInstanceByIDUser(int strIDUser)
        {
            UserInstance newUser = null;

            var foundUser = (from b in context.User
                             where (b.IDUser == strIDUser)
                             select new
                             {
                                 UserName = b.UserName
                                 , FullName = b.FullName
                                 , Email = b.Email
                                 , Address = b.Address
                             }).FirstOrDefault();

            if (foundUser != null)
            {
                newUser = new UserInstance();

                newUser.UserName = foundUser.UserName;
                newUser.FullName = foundUser.FullName;
                newUser.Email = foundUser.Email;
                newUser.Address = foundUser.Address;
            }
            return newUser;
        }

        /// <summary>
        /// Gets an user instance identified by CNP
        /// </summary>
        /// <param name="strIDBook"></param>
        /// <param name="strIDUser"></param>
        /// <returns></returns>
        public static UserInstance GetUserInstanceByCNP(string strCNP)
        {
            UserInstance newUser = null;

            var foundUser = (from b in context.User
                             where (b.CNP.Equals(strCNP))
                             select new
                             {
                                 UserName = b.UserName
                                 , FullName = b.FullName
                                 , Email = b.Email
                                 , Address = b.Address
                                 , CNP = b.CNP
                             }).FirstOrDefault();

            if (foundUser != null)
            {
                newUser = new UserInstance();

                newUser.UserName = foundUser.UserName;
                newUser.FullName = foundUser.FullName;
                newUser.Email = foundUser.Email;
                newUser.Address = foundUser.Address;
                newUser.CNP = foundUser.CNP;
            }
            return newUser;
        }
        #endregion linq

        #region text formatting
        /// <summary>
        /// replaces char codes with characters
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ReplaceCharCodesToChars(string strValue)
        {
            string replacedString = "";

            replacedString =
                strValue
                .Replace("&amp;#355;", "ț")
                .Replace("&amp;#354;", "Ţ")
                .Replace("&amp;#258;", "Ă")
                .Replace("&amp;#259;", "ă")
                .Replace("&amp;#194;", "Â")
                .Replace("&amp;#226;", "â")
                .Replace("&amp;#206;", "Î")
                .Replace("&amp;#238;", "î")
                .Replace("&amp;#194;", "Â")
                .Replace("&amp;#350;", "Ş")
                .Replace("&amp;#351;", "ş")

                .Replace("&#355;", "ț")
                .Replace("&#354;", "Ţ")
                .Replace("&#258;", "Ă")
                .Replace("&#259;", "ă")
                .Replace("&#194;", "Â")
                .Replace("&#226;", "â")
                .Replace("&#206;", "Î")
                .Replace("&#238;", "î")
                .Replace("&#194;", "Â")
                .Replace("&#350;", "Ş")
                .Replace("&#351;", "ş")
                ;
            ;

            return replacedString;
        }

        /// <summary>
        /// replaces characters with char odes
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ReplaceCharsToCharCodes(string strValue)
        {
            string replacedString = "";

            replacedString =
                strValue
                .Replace("ț", "&#355;")
                .Replace("Ţ", "&#355;")
                .Replace("Ă", "&#258;")
                .Replace("ă", "&#259;")
                .Replace("Â", "&#194;")
                .Replace("â", "&#226;")
                .Replace("Î", "&#206;")
                .Replace("î", "&#238;")
                .Replace("Â", "&#194;")
                .Replace("Ş", "&#350;")
                .Replace("ş", "&#351;")
            ;

            return replacedString;
        }
        #endregion text formatting

        #region grid methods
        /// <summary>
        /// changes a gridview page if not the last one or the first one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void GridView1_PageIndexChanging(GridView gridViewToSet, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex;
            int gridTotalPages = gridViewToSet.PageCount;

            if (newPageIndex <= gridTotalPages - 1)
            {
                e.Cancel = true;

                if (newPageIndex >= 0)
                {
                    gridViewToSet.PageIndex = e.NewPageIndex;
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// changes highlight / unhighlight on a row
        /// </summary>
        /// <param name="e"></param>
        public static void SetGridRowHighlight(System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#ede4b1'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='white'");
            }
        }

        /// <summary>
        /// builds the back link for an entity after inserting a new one
        /// </summary>
        /// <param name="DynamicHyperLinkBack"></param>
        /// <param name="tableName"></param>
        /// <param name="e"></param>
        public static void ShowBackLinkOnEntity(HyperLink DynamicHyperLinkBack, string tableName, FormViewInsertedEventArgs e)
        {
            /////

            DynamicHyperLinkBack.CssClass = "displayBlock";
            DynamicHyperLinkBack.Text = "back";

            DynamicHyperLinkBack.NavigateUrl = String.Format(ConfigurationManager.AppSettings["EntityNavigateBackUrl"], tableName);
            /////
        }

        /// <summary>
        /// builds the back link for an entity after inserting a new one
        /// </summary>
        /// <param name="DynamicHyperLinkBack"></param>
        /// <param name="tableName"></param>
        /// <param name="e"></param>
        public static void ShowBackLinkOnEntity(HyperLink DynamicHyperLinkBack, string tableName)
        {
            /////
            DynamicHyperLinkBack.CssClass = "displayBlock";
            DynamicHyperLinkBack.Text = "back";

            DynamicHyperLinkBack.NavigateUrl = String.Format(ConfigurationManager.AppSettings["EntityNavigateBackUrl"], tableName);
            /////
        }

        public static bool IsDateTimeValue(string strValue)
        {
            bool result = true;

            try
            {
                DateTime dtValue = Convert.ToDateTime(strValue);
                result = true;
            }

            catch (FormatException ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// exports a gridview to excel
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strFileName"></param>
        public static void ExportGridToExcel(Control gvToExport, string strFileName, string strReportTitle)
        {
            // bool b = IsDateTimeValue("2013-05-23");

            Table tblToExport = new Table();

            TableRow trImage; trImage = new TableRow();
            TableCell tcImage; tcImage = new TableCell();

            TableRow trGrid; trGrid = new TableRow();
            TableCell tcGrid; tcGrid = new TableCell();

            Image imgHeader = new Image();

            try
            {
                if (gvToExport is GridView)
                {
                    ((GridView)gvToExport).AllowPaging = false;
                    ((GridView)gvToExport).DataBind();
                }

                // BindGrid();

                imgHeader.ImageUrl = HttpContext.Current.Server.MapPath("~/DynamicData/Content/Images/endava/endava_logo.png"); imgHeader.ID = "imgHeader";

                System.Drawing.Image imgToAdd = System.Drawing.Image.FromFile(imgHeader.ImageUrl);

                Unit width = new Unit(imgToAdd.Width, UnitType.Pixel);
                Unit height = new Unit(imgToAdd.Height, UnitType.Pixel);

                tcImage.Width = width;
                tcImage.Height = height;

                // add header image
                tcImage.Controls.Add(imgHeader);

                trImage.Cells.Add(tcImage);
                tblToExport.Rows.Add(trImage);

                tcGrid.Controls.Add(gvToExport);
                trGrid.Cells.Add(tcGrid);
                tblToExport.Rows.Add(trGrid);

                // add date of generation
                Label lblGeneratedAt = new Label();
                lblGeneratedAt.Text = strReportTitle + " " + DateTime.Now.ToString("yyyy'-'MM'-'dd' || 'HH':'mm':'ss");

                tcImage = new TableCell();
                tcImage.HorizontalAlign = HorizontalAlign.Center;
                tcImage.Font.Bold = true;
                tcImage.Controls.Add(lblGeneratedAt);

                //////// 
                trImage = new TableRow();
                trImage.Cells.Add(tcImage);
                tblToExport.Rows.Add(trImage);

                tcGrid.Controls.Add(gvToExport);
                trGrid.Cells.Add(tcGrid);
                tblToExport.Rows.Add(trGrid);

                PrepareGridViewForExport(tblToExport);

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "application/vnd.xls";
                HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", strFileName));
                // HttpContext.Current.Response.Charset = "";

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
                HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

                using (StringWriter stringWriter = new StringWriter())
                {
                    HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
                    tblToExport.RenderControl(htmlWriter);
                    HttpContext.Current.Response.Write(stringWriter.ToString());
                    HttpContext.Current.Response.End();
                    // HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }

            catch (System.Threading.ThreadAbortException ex)
            {
            }

            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                // cleanup code
                tcGrid.Dispose();
                trGrid.Dispose();
                tcImage.Dispose();
                trImage.Dispose();
                tblToExport.Dispose();
                imgHeader.Dispose();

            }
        }

        /// <summary>
        /// prepare a control for exporting to excel
        /// </summary>
        /// <param name="gridView"></param>
        public static void PrepareGridViewForExport(Control controlToExport)
        {
            for (int i = 0; i < controlToExport.Controls.Count; i++)
            {
                //Get the control

                Control currentControl = controlToExport.Controls[i];

                if (currentControl is LinkButton)
                {
                    controlToExport.Controls.Remove(currentControl);
                    controlToExport.Controls.AddAt(i, new LiteralControl((currentControl as LinkButton).Text));
                }

                else if ((currentControl is Image) && !(currentControl is ImageButton))
                {
                    // RenderGridViewControlToExport(controlToExport, i, currentControl);

                    controlToExport.Controls.Remove(currentControl);

                    Image imgToAdd = (Image)currentControl;
                    // imgToAdd.ImageUrl = "file://" + HttpContext.Current.Server.MapPath("~/DynamicData/Content/Images/" + ((Image)currentControl).ImageUrl);
                    string tempPath = ((Image)currentControl).ImageUrl;
                    if (tempPath.Contains("~/"))
                    {
                        imgToAdd.ImageUrl = HttpContext.Current.Server.MapPath(tempPath);
                    }

                    imgToAdd.ImageUrl = "file://" + ((Image)currentControl).ImageUrl.Replace("\\", "/");

                    controlToExport.Controls.AddAt(i, imgToAdd);
                }

                else if (currentControl is ImageButton)
                {
                    controlToExport.Controls.Remove(currentControl);
                    controlToExport.Controls.AddAt(i, new LiteralControl((currentControl as ImageButton).AlternateText));

                    ImageButton imgToAdd = (ImageButton)currentControl;
                    imgToAdd.ImageUrl = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ImagesFolder"].Replace("../../", "~/") + ((ImageButton)currentControl).ImageUrl);
                }

                else if (currentControl is HyperLink)
                {
                    controlToExport.Controls.Remove(currentControl);
                    controlToExport.Controls.AddAt(i, new LiteralControl((currentControl as HyperLink).Text));
                }

                else if (currentControl is Label)
                {
                    controlToExport.Controls.Remove(currentControl);

                    string currentLabelText = (currentControl as Label).Text;
                    controlToExport.Controls.AddAt(i, new LiteralControl((currentControl as Label).Text));
                }

                else if (currentControl is DropDownList)
                {
                    controlToExport.Controls.Remove(currentControl);
                    controlToExport.Controls.AddAt(i, (new LiteralControl((currentControl as DropDownList).SelectedItem.Text)));
                }

                else if (currentControl is CheckBox)
                {
                    controlToExport.Controls.Remove(currentControl);
                    controlToExport.Controls.AddAt(i, new LiteralControl((currentControl as CheckBox).Checked ? "True" : "False"));
                }

                else if (currentControl.GetType().Name == "dynamicdata_fieldtemplates_boolean_ascx") // checks if the image is a boolean (checkbox) field
                {
                    RenderGridViewControlToExport(controlToExport, i, currentControl);
                }

                else if (currentControl.GetType().Name == "dynamicdata_fieldtemplates_custom_picture_ascx") // checks if the image is an image (picture) field
                {
                    RenderGridViewControlToExport(controlToExport, i, currentControl);
                }

                else if (currentControl.GetType().Name == "dynamicdata_fieldtemplates_custom_textrating_ascx") // checks if the image is an image (picture) field
                {
                    RenderGridViewControlToExport(controlToExport, i, currentControl);
                }


                if (currentControl.HasControls())
                {
                    // if there is any child controls, call this method to prepare for export
                    PrepareGridViewForExport(currentControl);
                }
            }
        }

        /// <summary>
        /// removes unnecessary characters from an image path
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        private static string SanitizeTextPath(string strText)
        {
            string tempText = "";

            tempText = strText
                .Replace("../../", "~/")
                .Replace("../", "~/")
                .Replace("\"", "")
                .Replace(@"Employee\", @"")
                .Replace(@"Delegation\", @"")
                .Replace(@"Flight\", @"")
                .Replace(@"Airport\", @"")
                .Replace(@"CostCenter\", @"")
                .Replace(@"TaxiCompany\", @"")
                .Replace(@"TaxiBooking\", @"")
                ;

            if (strText.IndexOf("class=") > 0)
            {
                tempText = tempText.Substring(0, tempText.IndexOf("class=") - 1);
            }

            if (strText.IndexOf("id=") > 0)
            {
                tempText = tempText.Substring(0, tempText.IndexOf("id=") - 1);
            }

            return tempText;
        }

        /// <summary>
        /// renders a custom gridview control for export
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="i"></param>
        /// <param name="currentControl"></param>
        private static void RenderGridViewControlToExport(Control gridView, int i, Control currentControl)
        {
            Control controlToAdd = currentControl.FindControl("Literal1");
            Label lblToDisplay = new Label(); lblToDisplay.Width = Unit.Pixel(100);

            try
            {

                if (controlToAdd != null)
                {
                    string tempText = ((Literal)controlToAdd).Text;
                    //string textWithoutAlt = "";
                    string text = "";

                    if (tempText != String.Empty)
                    {
                        string[] imagesInList = tempText.Split(new string[] { "<img src=" }, StringSplitOptions.None);

                        if (imagesInList.Length > 0)
                        {

                            foreach (string currentImage in imagesInList)
                            {
                                if (!String.IsNullOrEmpty(currentImage))
                                {
                                    text = currentImage.Substring(currentImage.IndexOf("<img src=") + 5).Replace("\" />", ""); // get image source

                                    if (text.IndexOf("alt=") > 0)
                                    {
                                        lblToDisplay.Text = (imagesInList.Length - 1).ToString();
                                    }

                                    else
                                    {
                                        lblToDisplay.Text += "<img border=\"0\" src=\"" + SanitizeTextPath(HttpContext.Current.Server.MapPath(SanitizeTextPath(text))) + "\" />";
                                    }

                                    gridView.Controls.Remove(currentControl);
                                    gridView.Controls.AddAt(i, lblToDisplay);
                                }
                            }
                        }
                    }
                }
            }

            catch
            {
                throw;
            }

            finally
            {
                lblToDisplay.Dispose();
                controlToAdd.Dispose();
            }
        }
        #endregion grid methods

        #region collections
        /// <summary>
        /// sorts a list box alphabetically
        /// </summary>
        /// <param name="lstToSort"></param>
        /// <returns></returns>
        public static DropDownList SortedListBox(DropDownList lstToSort)
        {
            List<ListItem> t = new List<ListItem>();

            Comparison<ListItem> compare = new Comparison<ListItem>(CompareListItems);
            foreach (ListItem lbItem in lstToSort.Items)
            {
                t.Add(lbItem);
            }

            t.Sort(compare);
            lstToSort.Items.Clear();
            lstToSort.Items.AddRange(t.ToArray());

            return lstToSort;

        }

        /// <summary>
        /// compares two list items
        /// </summary>
        /// <param name="liFirst"></param>
        /// <param name="liSecond"></param>
        /// <returns></returns>
        public static int CompareListItems(ListItem liFirst, ListItem liSecond)
        {
            return String.Compare(liFirst.Text, liSecond.Text);
        }
        #endregion collections

        #region convert methods
        /// <summary>
        /// converts an object value to string
        /// </summary>
        /// <param name="valoare"></param>
        /// <returns></returns>
        public static string GetStringValue(object valoare)
        {
            return (valoare == null ? String.Empty : valoare.ToString());
        }

        /// <summary>
        /// converts an object value to DateTime
        /// </summary>
        /// <param name="valoare"></param>
        /// <returns></returns>
        public static string GetDateValue(object valoare)
        {
            return (valoare == null ? String.Empty : Convert.ToDateTime(valoare).ToString(ConfigurationManager.AppSettings["DateFormatDisplay"]));
        }

        /// <summary>
        /// formats a DateTimeValue to yyyy.MM.dd
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dtValue)
        {
            return dtValue.ToString(ConfigurationManager.AppSettings["DateFormatDisplay"]);
        }

        /// <summary>
        /// Gets the date value without hour min sec from a DateTime
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public static DateTime GetDateFromDateTime(DateTime dtValue)
        {
            DateTime result = new DateTime(dtValue.Year, dtValue.Month, dtValue.Day);
            return result;
        }

        #endregion convert methods

        #region image
        /// <summary>
        /// gets the preview image of a given id in the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="imgId"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetPreviewImageFromDB(HttpContext context, int imgId, string type)
        {
            MemoryStream memoryStream =new MemoryStream();
                System.Drawing.Image imgFromDB;
                try
                {
                    memoryStream = new MemoryStream(Utils.GetImageMemoryStreamFromDB(imgId, type), false);

                    imgFromDB = System.Drawing.Image.FromStream(memoryStream);

                    if (memoryStream.Length > 0)
                    {
                        imgFromDB = System.Drawing.Image.FromStream(memoryStream);
                        // imgFromGB.Size = new System.Drawing.Size(100, 100);

                        imgFromDB.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    return imgFromDB;
                }

                catch
                {
                    imgFromDB = null; // the image could not be bound
                    return imgFromDB;
                }

                finally
                {
                    memoryStream.Dispose();
                }
        }

        /// <summary>
        /// gets the memory stream representation of an image
        /// </summary>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        public static byte[] GetImageMemoryStreamFromDB(int ImgId, string type)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();

            byte[] ImageByteArray = new byte[] { };

            try
            {
                conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["BookManagementConnectionString"].ConnectionString);

                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;

                if (type == "book")
                {
                    cmd.CommandText = "GetBookPreviewImage";
                }

                else if (type == "user")
                {
                    cmd.CommandText = "GetUserPreviewImage";
                }

                cmd.Parameters.Add("@id", SqlDbType.Int, 0).Value = ImgId;

                SqlDataReader drTemp = cmd.ExecuteReader();
                if (drTemp.Read())
                {
                    if (drTemp["result"] != DBNull.Value)
                        ImageByteArray = (byte[])drTemp["result"];
                }

                if (!drTemp.IsClosed)
                {
                    drTemp.Close();
                }
                return ImageByteArray;
            }

            catch
            {
                return null;
            }

            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                /*
                conn.Dispose();
                 */
                cmd.Dispose();
            }
        }
        #endregion image

        #region mail methods
        /// <summary>
        /// sends an email to the user
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="body"></param>
        public static void SendMailToUser(string toUser, string subject, string body)
        {
            // string myString = "test mail";

            SmtpClient a = new SmtpClient();

            try
            {
                using (MailMessage msgToSend = new MailMessage())
                {
                    msgToSend.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFromAddress"]);
                    msgToSend.To.Add(new MailAddress(toUser));
                    // msgToSend.Subject = ConfigurationManager.AppSettings["SmtpMailSubjectInsert"];
                    msgToSend.Subject = subject;
                    msgToSend.Body = body;
                    msgToSend.IsBodyHtml = true;

                    string strSmtpServer = ConfigurationManager.AppSettings["SmtpMailServer"];

                    a.Host = strSmtpServer;
                    a.Send(msgToSend);
                }

            }

            catch
            {
                throw; // ex;
            }

            finally
            {
                a.Dispose();
            }
        }



        /// <summary>
        /// formats a mail message body
        /// </summary>
        /// <param name="foundUserName"></param>
        /// <param name="foundUserFullName"></param>
        /// <param name="foundEmail"></param>
        /// <param name="foundBookName"></param>
        /// <param name="foundISBN"></param>
        /// <param name="strLeaseDate"></param>
        /// <param name="strScheduledReturnDate"></param>
        /// <param name="strActualReturnDate"></param>
        /// <param name="strCopies"></param>
        /// <param name="strRemarks"></param>
        /// <returns></returns>
        public static string BuildMailMessageBody(
            string strHeaderTitle
            , string foundUserName
            , string foundUserFullName
            , string foundEmail
            , string foundBookName
            , string foundISBN
            , string foundBookHasDisk
            , string strLeaseDate
            , string strScheduledReturnDate
            , string strActualReturnDate
            , string strCopies
            , string strRemarks)
        {
            string strBody =
                // user details
                strHeaderTitle + DateTime.Now.ToString()
                + "<br /><br />"
                + "<br /><b>User Name</b>: " + foundUserName
                + "<br /><b>Full Name</b>: " + foundUserFullName
                + "<br /><b>Email</b>: " + foundEmail

                + "<br />"

                // book details
                + "<br /><b>Book Name</b> : " + foundBookName
                + "<br /><b>ISBN</b>: " + foundISBN
                + "<br /><b>Has Disk</b>: " + foundBookHasDisk 
                + "<br /><b>Lease Date</b>: " + strLeaseDate
                + "<br /><b>Scheduled Return Date</b>: " + strScheduledReturnDate
                + "<br /><b>Actual Return Date</b>: " + strActualReturnDate
                + "<br /><b>Copies</b>: " + strCopies
                + "<br /><b>Remarks</b>: " + strRemarks;

            return strBody;
        }
        #endregion mail methods

    }
}