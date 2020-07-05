using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryManagement.Models;
namespace LibraryManagement.DAL
{
    public class UserContext : DbContext
    {
        public UserContext()

            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public virtual DbSet<LibraryManagement.Models.Block> Block { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookDetail> BookDetail { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookNotification> BookNotification { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookPurchase> BookPurchase { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookPurchaseDetail> BookPurchaseDetail { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookReceived> BookReceived { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookRequest> BookRequest { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookResponse> BookResponse { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookReturn> BookReturn { get; set; }
        public virtual DbSet<LibraryManagement.Models.ClassMaster> ClassMaster { get; set; }
        public virtual DbSet<LibraryManagement.Models.IssueBook> IssueBook { get; set; }
        public virtual DbSet<LibraryManagement.Models.LibrarienDetail> LibrarienDetail { get; set; } 
        //
       

        public virtual DbSet<LibraryManagement.Models.School> School { get; set; }

        public virtual DbSet<LibraryManagement.Models.StudentDetail> StudentDetail { get; set; }

        public virtual DbSet<LibraryManagement.Models.Subject> Subject { get; set; }
        public virtual DbSet<LibraryManagement.Models.SchoolMedium> SchoolMedium { get; set; }
        public virtual DbSet<LibraryManagement.Models.BookIssueDetail> BookIssueDetail { get; set; }

        

    }
}