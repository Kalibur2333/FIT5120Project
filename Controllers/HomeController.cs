using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5120Project.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Info()
        {
            ViewBag.Message = "Your Info page.";

            return View();
        }

        public ActionResult Working()
        {
            ViewBag.Message = "Your Working page.";

            return View();
        }

        public ActionResult MapView()
        {
            ViewBag.Message = "Your Working page.";

            return View();
        }
        public ActionResult Uploadlabel()
        {
            ViewBag.Message = "Your Working page.";

            return View();
        }
    }
}