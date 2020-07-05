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

namespace LibraryManagement.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
          public AdminController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

          public AdminController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        UserContext db = new UserContext();
        AdminManager admin = new AdminManager();
        Global global = new Global();
        //
        // GET: /Admin/Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSchool(Int32 Schoolid = 0)
        {
            if (Schoolid > 0)
            {
                var schooldetails = db.School.Where(x => x.SchoolId == Schoolid).FirstOrDefault();
                ViewData["schooldetails"] = schooldetails;
                PopulateMedium(schooldetails.MediumId);
                PopulateBlock(schooldetails.BlockID);
            }
            else
            {
                PopulateMedium();
                PopulateBlock();
            }
            return View();
        }
        public ActionResult ViewSchool()
        {
            var schoollist = admin.GetOrderList().ToList();
            ViewData["ViewSchool"] = schoollist;
            return View();
        }
        [HttpPost]
          public async Task<ActionResult> AddSchool(Models.School school, Int32 SchoolId = 0)
        {
            string userid = User.Identity.GetUserId();
            string Status = "NA";
            string pass = "School@123";
            var usernem = school.SchoolName.Split(' ')[0]+school.BlockID;
            var blockname = db.Block.Where(x => x.BlockID == school.BlockID).Select(x => x.BlockName).FirstOrDefault();
            var medname = db.SchoolMedium.Where(x => x.MediumId == school.MediumId).Select(x => x.MediumName).FirstOrDefault();
            Models.School saveschool = new Models.School();
            try
            {

                var StatusExist = db.School.Find(school.SchoolId);


                if (StatusExist != null)
                {
                   
                    StatusExist.SchoolName = school.SchoolName;
                    StatusExist.MediumId = school.MediumId;
                    StatusExist.BlockID = school.BlockID;
                    StatusExist.Contact = school.Contact;
                    StatusExist.Address = school.Address;
                    StatusExist.PrincipleName = school.PrincipleName;
                    db.SaveChanges();
                    Status = "Succeeded";

                }
                else
                {
                    var users = new ApplicationUser() { UserName = usernem };
                    var results = UserManager.Create(users, pass);
                  
                  
                    if (results.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(users.Id, "School");
                        saveschool.SchoolUserId = users.Id;
                        saveschool.SchoolId = school.SchoolId;
                        saveschool.SchoolName = school.SchoolName;
                        saveschool.MediumId = school.MediumId;
                        saveschool.BlockID = school.BlockID;
                        saveschool.Contact = school.Contact;
                        saveschool.Address = school.Address;
                        saveschool.CreatedDate = DateTime.Now;
                        saveschool.CreatedBy = User.Identity.GetUserId();
                        saveschool.PrincipleName = school.PrincipleName;
                        db.School.Add(saveschool);
                        db.SaveChanges();
                        Status = "Succeeded";

                    }
                    else {
                        Status = "School name is already taken";
                    
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
        private void PopulateMedium(object selectedvalue = null)
        {
            var Mediumlist = db.SchoolMedium.ToList();
            var MediumId = new SelectList(Mediumlist, "MediumId", "MediumName", selectedvalue);
            ViewBag.MediumId = MediumId;
        }
        private void PopulateBlock(object selectedvalue = null)
        {
            var BlockList = db.Block.ToList();
            var blockId = new SelectList(BlockList, "BlockID", "BlockName", selectedvalue);
            ViewBag.blockId = blockId;
        }
        public ActionResult AddClass(int ClassID = 0, int filterid = 0)
        {
            ClassMaster cmaster = new ClassMaster();
            if (ClassID > 0)
            {
                var detail = db.ClassMaster.Where(c => c.ClassID == ClassID).FirstOrDefault();
                   cmaster.ClassName = detail.ClassName;
                   cmaster.ClassID=ClassID;

            }

             var classdetail = db.ClassMaster.ToList();
             if (filterid > 0)
            {
                classdetail = classdetail.Where(c => c.ClassID == filterid).ToList();
            }
            ViewData["clases"] = classdetail;
            return View(cmaster);
       
        }

        [HttpPost]
        public ActionResult AddClass(string ClassName = null, int stuid = 0)
        {
            ClassMaster classmaster = new ClassMaster();
            classmaster.ClassName = ClassName;
           
            var exitClass = db.ClassMaster.Where(s => s.ClassID == stuid).FirstOrDefault();

            if (exitClass != null)
            {
                exitClass.ClassName = ClassName;
                db.SaveChanges();
            }
            else
            {
                db.ClassMaster.Add(classmaster);
                db.SaveChanges();

            }
               
                return RedirectToAction("AddClass", "Admin", new { area="Admin"});
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            //Session.RemoveAll();
            //Session.Abandon();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //// return RedirectToAction("Login", "Account", new { area = "" });
            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}