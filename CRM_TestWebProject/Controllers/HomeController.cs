using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_TestWebProject.Controllers
{
    public class HomeController : Controller
    {
        public List<CRMUser> Users
        {
            get
            {
                if (Session["tmp"] == null)
                {
                    /*Session["tmp"] = new List<CRMUser>
                    {
                        new CRMUser("FirstName1", "Lastname1", "Login1"),
                        new CRMUser("FirstName2", "Lastname2", "Login2"),
                        new CRMUser("FirstName3", "Lastname3", "Login3")
                    };*/
                    Session["tmp"] = UnionProvider.getUsers();
                }
                return Session["tmp"] as List<CRMUser>;
            }
            set
            {
                Session["tmp"] = value;
            }

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult List()
        {
            // temp
            return View(Users);
        }

        public ActionResult Delete(string i)
        {
            var st = Users.Find(c => c.Login == i);
            Users.Remove(st);
            return View("List", Users);

            /*var st = lst.Find(c => c.Login == i);
            lst.Remove(st);
            return View("Index", lst);*/
        }

        public ActionResult DeleteAll()
        {
            Users.Clear();
            return View("List", Users);
        }
    }
}