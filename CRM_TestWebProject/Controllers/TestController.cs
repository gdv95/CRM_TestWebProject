using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_TestWebProject.Controllers
{
    public class TestController : Controller
    {
        //List<CRMUser> lst;

        public TestController()
        {
            /*lst = new List<CRMUser>
            {
            new CRMUser("FirstName1", "Lastname1", "Login1"),
            new CRMUser("FirstName2", "Lastname2", "Login2"),
            new CRMUser("FirstName3", "Lastname3", "Login3")
            };*/
        }

        public List<CRMUser> Students
        {
            get
            {
                if (Session["tmp"] == null)
                {
                    Session["tmp"] = new List<CRMUser>
           {
            new CRMUser("FirstName1", "Lastname1", "Login1"),
            new CRMUser("FirstName2", "Lastname2", "Login2"),
            new CRMUser("FirstName3", "Lastname3", "Login3")
           };
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
            return View(Students);
        }

        public ActionResult Delete(string i)
        {
            var st = Students.Find(c => c.Login == i);
            Students.Remove(st);
            return View("Index", Students);

            /*var st = lst.Find(c => c.Login == i);
            lst.Remove(st);
            return View("Index", lst);*/
        }
    }
}