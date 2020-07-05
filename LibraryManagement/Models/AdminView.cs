using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Models
{
    public class SchoolViewList
    {

        public Int64 SchoolId { get; set; }

        public string SchoolUserId { get; set; }
        public string SchoolName { get; set; }

        public string Contact { get; set; }
        public string Address { get; set; }

        public string PrincipleName { get; set; }

        public Int64 MediumId { get; set; }

        public Int64 BlockID { get; set; }
        public string BlockName { get; set; }
        public string MediumName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }


    public class LibraryViewDetails
    {

        public string SchoolName { get; set; }
        public Int64 LibrarienId { get; set; }

        public string UserId { get; set; }

        public Int64 SchoolId { get; set; }

        public virtual School School { get; set; }
        public string Name { get; set; }

        public string Contact { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }

    public class SubjectViewDetails
    {

        public string SubjectName { get; set; }
        public Int64 SubjectID { get; set; }

        public Int64 ClassID { get; set; }

        public string ClassName { get; set; }



    }
    public class BookDetailView
    {

        public Int64 BookID { get; set; }

        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public Int64 SchoolId { get; set; }

        public virtual School School { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Edition { get; set; }
        public float Price { get; set; }
        public Int64 TotalCopies { get; set; }
        public Int64 ClassID { get; set; }
        public Int64 SubjectID { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ISBN { get; set; }

    }
    public class IssueBookDetailView
    {

        public Int64 IssueID { get; set; }
        public Int64 SchoolId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ClassName { get; set; }
        public string SchoolName { get; set; }
        public string Name { get; set; }
        public Int64 BookID { get; set; }
        public string BookName { get; set; }
        public string Issuedby { get; set; }
    
        public Int64 StudentID { get; set; }
        public Int64 TotalCopies { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string StudentNo { get; set; }
    }
}