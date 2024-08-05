using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FIT5120Project.Models;

//AccountController

namespace FIT5120Project.Controllers
{

    public class AccountController : Controller
    {
        private const string CorrectPassword = "sbmonash"; // Adjust the password

        public ActionResult Login()
        {
            return View();
        }

        //Login Page 
        //Password: sbmonash
        [HttpPost]
        public ActionResult VerifyPassword(string password)
        {
            if (password == CorrectPassword)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", "Account");
        }
    }   
}