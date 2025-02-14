using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookManagement.BusinessObjects
{
    public class BookLeaseInstance
    {
        public int IDAuthor;
        public int IDBook;
        public int IDUser;
        public string ISBN;
        public string AuthorName;
        public string BookName;
        public string CategoryName;
        public int Stock;
        public int Copies;
        public DateTime LeaseDate;
        public DateTime ScheduledReturnDate;
        public DateTime? ActualReturnDate;
        public string Remarks;
        public bool? HasDisk;
        public string UserName;
        public string FullName;
        public string Email;
        public bool? FlagDel;
    }
}