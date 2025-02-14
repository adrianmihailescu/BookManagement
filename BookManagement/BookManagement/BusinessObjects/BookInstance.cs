using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookManagement.BusinessObjects
{
    public class BookInstance
    {
        public int IDAuthor;
        public int IDBook;
        public int IDCategory;
        public string ISBN;
        public string AuthorName;
        public string BookName;
        public string CategoryName;
        public int Stock;
        public bool? HasDisk;
        public bool? FlagDel;
    }
}