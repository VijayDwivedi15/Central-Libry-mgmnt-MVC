using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LibraryManagement.Models;
namespace LibraryManagement.DAL
{
    public class AdminManager 
    {
        UserContext db = new UserContext();
        public List<Models.SchoolViewList> GetOrderList()
        {

            var status = new List<Models.SchoolViewList>();

            using (var db = new UserContext())
            {
               
                status = db.Database
                          .SqlQuery<Models.SchoolViewList>("exec USP_GetSchoolViewDetails").ToList();
            }

            return status;
        }
        public List<Models.LibraryViewDetails> GetLibrarianlist()
        {

            var status = new List<Models.LibraryViewDetails>();

            using (var db = new UserContext())
            {

                status = db.Database
                          .SqlQuery<Models.LibraryViewDetails>("exec USP_GetViewLibrarian").ToList();
            }

            return status;
        }

        public List<Models.BookDetailView> GetBookslist()
        {

            var status = new List<Models.BookDetailView>();

            using (var db = new UserContext())
            {

                status = db.Database
                          .SqlQuery<Models.BookDetailView>("exec USP_GetBookDetailsView").ToList();
            }

            return status;
        }
        public List<Models.IssueBookDetailView> GetIssuebookDetails()
        {

            var status = new List<Models.IssueBookDetailView>();

            using (var db = new UserContext())
            {

                status = db.Database
                          .SqlQuery<Models.IssueBookDetailView>("exec [USP_GetIssueBookDetails]").ToList();
            }

            return status;
        }

        public List<Models.SubjectViewDetails> GetSubjectDetaillist()
        {

            var status = new List<Models.SubjectViewDetails>();

            using (var db = new UserContext())
            {

                status = db.Database
                          .SqlQuery<Models.SubjectViewDetails>("exec USP_GetSubjectDetailView").ToList();
            }

            return status;
        }
      
	}
}