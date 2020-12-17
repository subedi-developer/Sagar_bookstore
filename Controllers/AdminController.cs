using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sagar_bookstore;

namespace Sagar_bookstore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(user usr)

        {
            sagarbooksEntities1 dbObject = new sagarbooksEntities1();
            var checkUser = dbObject.users.Where(l => l.u_name.Equals(usr.u_name) && l.u_password.Equals(usr.u_password)).FirstOrDefault();
            if (checkUser != null)
            {
                var loggeduser = dbObject.users.Where(l => l.u_name.Equals(usr.u_name)).FirstOrDefault();
                Session["u_name"] = loggeduser.u_name.ToString();
                Session["u_id"] = loggeduser.u_id.ToString();
                Session["u_type"] = loggeduser.u_type.ToString();

                return RedirectToAction("Dashboard");


            }
            else
            {
                ViewBag.msg = "invalid username or password";
            }


            return View();


        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
           












