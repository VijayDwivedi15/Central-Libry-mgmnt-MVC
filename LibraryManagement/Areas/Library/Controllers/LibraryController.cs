using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.DAL;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using Microsoft.Owin.Security;

namespace LibraryManagement.Areas.Library.Controllers
{
    public class LibraryController : Controller
    {

        // GET: /Library/Library/
        public LibraryController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public LibraryController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        UserContext db = new UserContext();
        AdminManager admin = new AdminManager();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSubjects(Int64 ClassID = 0)
        {
            if (ClassID > 0)
            {
                var subjectlist = db.Subject.Where(c => c.ClassID == ClassID).ToList();
                ViewData["SubjectDetailslist"] = subjectlist;
                PopulateClass(ClassID);
            }
            else
            {
                PopulateClass();
            } return View();
        }

        [HttpPost]
        public ActionResult AddSubject(Int32[] SubjectsID, string[] SubjectsName = null, Int64 ClassID = 0)
        {

            Subject subject = new Subject();
            var Status = "N.A";
            try
            {
                if (SubjectsID != null)
                {

                    for (int i = 0; i < SubjectsID.Length; i++)
                    {
                        var tmp = SubjectsID[i];

                        var exist = db.Subject.Where(a => a.SubjectID == tmp).FirstOrDefault();
                        if (exist != null)
                        {
                            exist.SubjectName = SubjectsName[i];
                            exist.ClassID = ClassID;
                            db.SaveChanges();
                            Status = "Succeeded";
                        }
                        else
                        {
                            subject.CreatedBy = User.Identity.GetUserId();
                            subject.SubjectName = SubjectsName[i];
                            subject.ClassID = ClassID;
                            db.Subject.Add(subject);
                            db.SaveChanges();
                            Status = "Succeeded";
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Status = "Unsucceeded " + ex;
            }


            return Json(Status, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddBooks(Int32 BookID = 0)
        {
            if (BookID > 0)
            {
                var bookdetails = db.BookDetail.Where(x => x.BookID == BookID).FirstOrDefault();
                ViewData["bookdetails"] = bookdetails;
                PopulateClass(bookdetails.ClassID);
                populatesubject(bookdetails.SubjectID);
            }
            else
            {
                PopulateClass();
                populatesubject();
            }

            return View();

        }
        public ActionResult IssueBooks(Int64 IssueID = 0)
        {

            var userid = User.Identity.GetUserId();
            var schoolid = db.LibrarienDetail.Where(x => x.UserId == userid).Select(c => c.SchoolId).FirstOrDefault();
            var issuebookdetailslist = admin.GetIssuebookDetails().Where(x => x.SchoolId == schoolid).ToList();

            ViewData["issuebookdetails"] = issuebookdetailslist;
            PopulateClass();
            PopulateBook();

            return View();

        }
        
        public ActionResult ViewIssueBooks(Int64 IssueID = 0)
        {

            var userid = User.Identity.GetUserId();
            var schoolid = db.LibrarienDetail.Where(x => x.UserId == userid).Select(c => c.SchoolId).FirstOrDefault();
            var issuebookdetailslist = admin.GetIssuebookDetails().Where(x => x.SchoolId == schoolid).ToList();
            ViewData["issuebookdetails"] = issuebookdetailslist;
            return View();

        }
        public ActionResult StudentIssueBookDetails(Int64 StudentID = 0)
        {
            var issuebookdetailslist = admin.GetIssuebookDetails().Where(x => x.StudentID == StudentID).ToList();
            ViewData["studentissuebookdetails"] = issuebookdetailslist;
            return View();

        }
        [HttpPost]
        public ActionResult IssueBooks(Models.IssueBook issuebook, Int64[] BooksID, Int64 StudentID, Int64 ClassID, DateTime IssueDate, DateTime ReturnDate, Int64 IssueID = 0)
        {

            IssueBook issuebooks = new IssueBook();
            var userid = User.Identity.GetUserId();
            var schoolid = db.LibrarienDetail.Where(x => x.UserId == userid).Select(c => c.SchoolId).FirstOrDefault();
            var Status = "N.A";
            try
            {
                if (IssueID != null)
                {

                    var exist = db.IssueBook.Where(a => a.IssueID == IssueID).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.IssueDate = issuebook.IssueDate;
                        exist.StudentID = issuebook.StudentID;
                        // exist.BookID = issuebook.BookID;
                        exist.ReturnDate = issuebook.ReturnDate;
                        exist.SchoolId = schoolid;
                        exist.IssueBy = userid;
                        exist.ClassID = issuebook.ClassID;
                        db.SaveChanges();
                        Status = "Succeeded";
                    }
                    else
                    {
                        issuebooks.IssueDate = issuebook.IssueDate;
                        issuebooks.StudentID = issuebook.StudentID;
                        //issuebooks.BookID = issuebook.BookID;
                        issuebooks.ReturnDate = issuebook.ReturnDate;
                        issuebooks.SchoolId = schoolid;
                        issuebooks.IssueBy = userid;
                        issuebooks.ClassID = issuebook.ClassID;
                         db.IssueBook.Add(issuebooks);
                         int i = db.SaveChanges();
                         if (i > 0)
                         {

                           

                             var issueid = db.IssueBook.Where(s=>s.SchoolId==schoolid).Max(c=>c.IssueID);
                             for (int j = 0; j < BooksID.Length; j++)
                             {
                                 BookIssueDetail bookissuedet = new BookIssueDetail();
                                 bookissuedet.BookID = BooksID[j];
                                 bookissuedet.IssueID = issueid;
                                 db.BookIssueDetail.Add(bookissuedet);
                                 db.SaveChanges();
                                
                                 var bookid=BooksID[j];
                                 var existbook = db.BookDetail.Where(x => x.BookID == bookid).FirstOrDefault();
                                 existbook.TotalCopies = existbook.TotalCopies - 1;
                                 db.SaveChanges();
                             
                             }
                         
                         }
                        Status = "Succeeded";
                    }


                }
            }

            catch (Exception ex)
            {
                Status = "Unsucceeded " + ex;
            }


            return Json(Status, JsonRequestBehavior.AllowGet);
        }


        private void PopulateBook(object selectedvalue = null)
        {
            var userid = User.Identity.GetUserId();
            var schoolid = db.LibrarienDetail.Where(x => x.UserId == userid).Select(c => c.SchoolId).FirstOrDefault();
            var booklist = db.BookDetail.Where(x => x.SchoolId == schoolid).ToList();
            var BookID = new SelectList(booklist, "BookID", "BookName", selectedvalue);
            ViewBag.BookID = BookID;
        }



        [HttpPost]
        public ActionResult AddBooks(Models.BookDetail bookdetails, Int32 BookID = 0)
        {
            string schooluserid = User.Identity.GetUserId();
            var schoolid = db.LibrarienDetail.Where(x => x.UserId == schooluserid).Select(c => c.SchoolId).FirstOrDefault();
            string Status = "NA";


            Models.BookDetail savebookdetails = new Models.BookDetail();
            try
            {

                var StatusExist = db.BookDetail.Find(bookdetails.BookID);


                if (StatusExist != null)
                {

                    StatusExist.BookName = bookdetails.BookName;
                    StatusExist.AuthorName = bookdetails.AuthorName;
                    StatusExist.Edition = bookdetails.Edition;
                    StatusExist.Price = bookdetails.Price;
                    StatusExist.TotalCopies = bookdetails.TotalCopies;
                    StatusExist.ClassID = bookdetails.ClassID;
                    StatusExist.SubjectID = bookdetails.SubjectID;
                    StatusExist.ISBN = bookdetails.ISBN;
                    db.SaveChanges();
                    Status = "Succeeded";

                }
                else
                {
                    savebookdetails.BookName = bookdetails.BookName;
                    savebookdetails.AuthorName = bookdetails.AuthorName;
                    savebookdetails.Edition = bookdetails.Edition;
                    savebookdetails.Price = bookdetails.Price;
                    savebookdetails.TotalCopies = bookdetails.TotalCopies;
                    savebookdetails.ClassID = bookdetails.ClassID;
                    savebookdetails.SubjectID = bookdetails.SubjectID;
                    savebookdetails.ISBN = bookdetails.ISBN;
                    savebookdetails.CreatedOn = DateTime.Now;
                    savebookdetails.CreatedBy = schooluserid;
                    savebookdetails.SchoolId = schoolid;
                    db.BookDetail.Add(savebookdetails);
                    db.SaveChanges();
                    Status = "Succeeded";


                }


            }
            catch (Exception ex)
            {
                Status = "Unsucceeded " + ex;
            }

            ViewBag.Status = Status;
            return Json(Status, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult AddStudent(int StudentID = 0, int filterid = 0)
        {
            StudentDetail sdetails = new StudentDetail();
            var studentlist = db.StudentDetail.ToList();
            if (StudentID > 0)
            {
                var detail = db.StudentDetail.Where(c => c.StudentID == StudentID).FirstOrDefault();
                sdetails.Name = detail.Name;
                sdetails.Address = detail.Address;
                sdetails.Contact = detail.Contact;
                sdetails.StudentID = detail.StudentID;
                PopulateClass(detail.ClassID);

            }
            else
            {
                PopulateClass();

            }
            ViewData["student"] = studentlist;
            return View();
        }

        [HttpPost]
        public ActionResult GetBookNameDetail(Int64 ClassID)
        {
            List<SelectListItem> MemberID = new List<SelectListItem>();

            var members = db.BookDetail.Where(s => s.ClassID == ClassID).ToList();
            members.ForEach(x =>
            {
                MemberID.Add(new SelectListItem { Text = x.BookName, Value = x.BookID.ToString() });
            });

            return Json(MemberID, JsonRequestBehavior.AllowGet);
        }
        //post student
        [HttpPost]
        public ActionResult AddStudent(Int64 stuid = 0, string Name = null, Int64 ClassID = 0, string Fname = null, string Contact = null, string Address = null, string Dob = null)
        {
            StudentDetail sdetails = new StudentDetail();
            string userid = User.Identity.GetUserId();
            var schoolno = db.LibrarienDetail.Where(l => l.UserId == userid).FirstOrDefault();
            var existstudent = db.StudentDetail.Where(s => s.StudentID == stuid).FirstOrDefault();

            if (existstudent != null)
            {

            }
            else
            {
                var stu = db.StudentDetail.ToList();
                Int64 cno = 0;
                if (stu.Count() > 0)
                {
                    cno = stu.Count() + 1;
                }
                else
                {
                    cno = 1;
                }
                string stuno = null;
                stuno = "STU" + "-" + cno;
                sdetails.Address = Address;
                sdetails.ClassID = ClassID;
                sdetails.Contact = Contact;
                sdetails.CreatedBy = userid;
                sdetails.CreatedOn = DateTime.Now;
                sdetails.DOB = Dob;
                sdetails.FatherName = Fname;
                sdetails.Name = Name;
                sdetails.SchoolId = schoolno.SchoolId;
                sdetails.StudentNo = stuno;
                db.StudentDetail.Add(sdetails);
                db.SaveChanges();
            }


            return View();
        }

        //end


        [HttpPost]
        public ActionResult GetStudentDetailView(Int64 StudentID)
        {
            StudentDetail student = new StudentDetail();

            {
                var members = db.StudentDetail.Where(s => s.StudentID == StudentID).FirstOrDefault();

                if (members != null)
                {
                    student.Name = members.Name;
                    student.StudentNo = members.StudentNo;
                    student.DOB = members.DOB;
                    student.FatherName = members.FatherName;
                }

            }

            return Json(student, JsonRequestBehavior.AllowGet);
        }

        private void PopulateClass(object selectedvalue = null)
        {
            var classlist = db.ClassMaster.ToList();
            var ClassID = new SelectList(classlist, "ClassID", "ClassName", selectedvalue);
            ViewBag.ClassID = ClassID;
        }
        private void populatesubject(object selectedvalue = null)
        {
            var subjectlist = db.Subject.ToList();
            var SubjectID = new SelectList(subjectlist, "SubjectID", "SubjectName", selectedvalue);
            ViewBag.SubjectID = SubjectID;
        }
        private void populateSubjectName(object selectedvalue = null)
        {
            var subjectlist = db.Subject.ToList();
            var SubjectName = new SelectList(subjectlist, "SubjectName", "SubjectName", selectedvalue);
            ViewBag.SubjectName = SubjectName;
        }
        public ActionResult ViewBooks(Int64? ClassID)
        {
            if (ClassID != null)
            {
                var bookdetailslist = admin.GetBookslist().ToList();
                PopulateClass(ClassID);
                ViewData["viewbookdetailslist"] = bookdetailslist.Where(c => c.ClassID == ClassID).ToList();
                var bookslist = bookdetailslist.Where(x => x.ClassID == ClassID);
            }
            else
            {
                var bookdetailslist = admin.GetBookslist().ToList();
                PopulateClass();
                ViewData["viewbookdetailslist"] = bookdetailslist;
            }
            return View();
        }
        public ActionResult ViewSubjects(Int64? ClassID, string SubjectName = null)
        {
            if (ClassID != null)
            {
                var subjectdetaillist = admin.GetSubjectDetaillist().ToList();
                ViewData["viewbookdetailslist"] = subjectdetaillist.Where(c => c.ClassID == ClassID).ToList();
                PopulateClass(ClassID);
                populateSubjectName();
            }
            else if (SubjectName != null)
            {
                var subjectdetaillist = admin.GetSubjectDetaillist().ToList();
                ViewData["viewbookdetailslist"] = subjectdetaillist.Where(c => c.SubjectName == SubjectName).ToList();
                PopulateClass();
                populateSubjectName(SubjectName);
            }
            else
            {

                var subjectdetaillist = admin.GetSubjectDetaillist().ToList();
                ViewData["viewbookdetailslist"] = subjectdetaillist;
                PopulateClass();
                populateSubjectName();
            }
            return View();
        }

    }
}