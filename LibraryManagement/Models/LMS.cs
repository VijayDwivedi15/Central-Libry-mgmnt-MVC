using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class ClassMaster
    {
        [Key]
        public Int64 ClassID { get; set; }
        public string ClassName { get; set; } 
        public string CreatedBy { get; set; }
    }
    public class Subject
    {
        [Key]
        public Int64 SubjectID { get; set; }
        public string SubjectName { get; set; }
        public Int64 ClassID { get; set; } 
        public string CreatedBy { get; set; }
    }
    public class Block
    {
        [Key]
        public Int64 BlockID { get; set; }
        public string BlockName { get; set; }
        
    }
   
    public class School
    {
        [Key]
        public Int64 SchoolId { get; set; }
        [MaxLength(128)]
        public string SchoolUserId { get; set; }
        public string SchoolName { get; set; }
        [MaxLength(16)]
        public string Contact { get; set; }
        public string Address { get; set; }
        [MaxLength(128)]
        public string PrincipleName { get; set; }

        public Int64 MediumId { get; set; }
        [ForeignKey("MediumId")]
        public virtual SchoolMedium SchoolMedium { get; set; }
       
        public Int64 BlockID { get; set; }
         [ForeignKey("BlockID")]
        public virtual Block Block { get; set; }
         public string CreatedBy { get; set; }
         public DateTime CreatedDate { get; set; }

    }

    public class LibrarienDetail
    {
        [Key]
        public Int64 LibrarienId { get; set; }
        [MaxLength(128)]
        public string UserId { get; set; }
         
        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public string Name { get; set; }
        [MaxLength(16)]
        public string Contact { get; set; }
        [MaxLength(128)]
        public string CreatedBy { get;set; }
        public DateTime CreatedOn { get; set; }

    }

    public class StudentDetail
    {
        [Key]
        public Int64 StudentID { get; set; }
       
         
        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public string StudentNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        [MaxLength(16)]
        public string Contact { get; set; }
        public string DOB { get; set; }
        public Int64 ClassID { get; set; }
        public string Address { get; set; }
        [MaxLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }

    public class BookDetail
    {
        [Key]
        public Int64 BookID { get; set; }

         
        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Edition { get; set; }
        public float Price { get; set; }
        public Int64 TotalCopies { get; set; }
        public Int64 ClassID { get; set; }
        public Int64 SubjectID { get; set; }

        [MaxLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 ISBN {get;set;}

    }


    public class BookPurchase
    {
        [Key]
        public Int64 PurchaseID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Supplier { get; set; }
        public string SupplierAddress { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }

        [MaxLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
    }
     

    public class BookPurchaseDetail
    {
        [Key]
        public Int64 BookPurchaseDetailID { get; set; }

        public Int64 PurchaseID { get; set; }
        public Int64 ClassID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public Int64 TotalQuantiry { get; set; }

        public float UnitPrice { get; set; }

      
    }

    public class IssueBook
    {
        [Key]
        public Int64 IssueID { get; set; }
        public DateTime IssueDate { get; set; }
        public Int64 StudentID { get; set; }
        public DateTime ReturnDate { get; set; }
        //public string BookName { get; set; }
        public Int64 ClassID { get; set; } 
        [MaxLength(128)]
        public string IssueBy { get; set; }
         
        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
    }
    public class BookIssueDetail
    {
        [Key]
        public Int64 IssueDetailID { get; set; }
        public Int64 IssueID { get; set; }
        [ForeignKey("IssueID")]
        public virtual IssueBook IssueBook { get; set; }
        public Int64 BookID { get; set; } 

    }

    public class BookReturn
    {
        [Key]
        public Int64 ReturnID { get; set; }
        public Int64 IssueID { get; set; }

        public DateTime BookReturnDate { get; set; }

        [MaxLength(128)]
        public string ReturnBy { get; set; }
        public Int64 SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
    }

    public class BookRequest
    {
        [Key]

        public Int64 BookRequestID { get; set; }
        public Int64 BookID { get; set; }
        public Int64 Quantity { get; set; }
        public Int64 RequestedByschool { get; set; }
        public Int64 RequestedToschool { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public DateTime RequestDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsApprove { get; set; }
    }

    public class BookResponse
    {
        [Key]
        public Int64 BookResponseID { get;set;}
        public Int64 BookRequestID { get; set; }

        public bool IsBookSend { get; set; }
        public Int64 CreatedBySchool { get; set; }
        public DateTime ResponseDate { get; set; }
        public string Remark { get; set; }
        public Int64 SendQuantity { get; set; }
       
    }
    public class BookReceived
    {
        [Key]
        public Int64 BookReceivedId { get; set; }
        public Int64 BookResponseID { get; set; }
        public Int64 ReceivedBySchool { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsReceived { get; set; }
    }

    public class BookNotification
    {
        [Key]
        public Int64 NotificationID { get; set; }

        public string Body { get; set; }

        public bool IsRead { get; set; }

        public string Type { get; set; }
        public Int64 NotificationToSchool { get; set; }
        public DateTime Date { get; set; }
    }
    public class SchoolMedium
    {
        [Key]
        public Int64 MediumId { get; set; }
        public string MediumName { get; set; }

    }
}