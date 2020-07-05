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

namespace LibraryManagement.Areas.School.Controllers
{
    public class SchoolController : Controller
    {
        public SchoolController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public SchoolController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        UserContext db = new UserContext();
        AdminManager admin = new AdminManager();

        //
        // GET: /School/School/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddLibrarian(Int32 LibrarienId = 0)
        {
            if (LibrarienId > 0)
            {
                var librariendetails = db.LibrarienDetail.Where(x => x.LibrarienId == LibrarienId).FirstOrDefault();
                ViewData["librariendetails"] = librariendetails;

            }

            return View();

        }

        [HttpPost]
        public async Task<ActionResult> AddLibrarian(Models.LibrarienDetail librariandetail, Int32 LibrarienId = 0)
        {
            string schooluserid = User.Identity.GetUserId();
            var schoolid = db.School.Where(x => x.SchoolUserId == schooluserid).Select(c => c.SchoolId).FirstOrDefault();
            string Status = "NA";
            string pass = "Library@123";
            var usernem = librariandetail.Name.Split(' ')[0] + "1";
            Models.LibrarienDetail savelibrariandetail = new Models.LibrarienDetail();
            try
            {

                var StatusExist = db.LibrarienDetail.Find(librariandetail.LibrarienId);


                if (StatusExist != null)
                {

                    StatusExist.Name = librariandetail.Name;
                    StatusExist.Contact = librariandetail.Contact;
                    StatusExist.SchoolId = schoolid;
                    db.SaveChanges();
                    Status = "Succeeded";

                }
                else
                {
                    var users = new ApplicationUser() { UserName = usernem };
                    var results = UserManager.Create(users, pass);
                    if (results.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(users.Id, "Library");
                        savelibrariandetail.UserId = users.Id;
                        savelibrariandetail.SchoolId = schoolid;
                        savelibrariandetail.Name = librariandetail.Name;
                        savelibrariandetail.Contact = librariandetail.Contact;
                        savelibrariandetail.CreatedOn = DateTime.Now;
                        savelibrariandetail.CreatedBy = User.Identity.GetUserId();
                        db.LibrarienDetail.Add(savelibrariandetail);
                        db.SaveChanges();
                        Status = "Succeeded";

                    }
                    else
                    {
                        Status = "Librarian name is already taken";

                    }
                }


            }
            catch (Exception ex)
            {
                Status = "Unsucceeded " + ex;
            }

            ViewBag.Status = Status;
            return Json(Status, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ViewLibrarian()
        {

            var librarienlist = admin.GetLibrarianlist().ToList();
            ViewData["viewlibrarians"] = librarienlist;
            return View();
        }

   
    }
}