using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SPDS.Models;

namespace SPDS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult ForgotPass()
        {
            return View();
        }


        [Authorize(Roles = "3,1")]
        [HttpGet]
        public ActionResult ProfilePage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(Models.CreateAccountViewModel caVM)
        {          
            if(ModelState.IsValid)
            {
                caVM.Create(caVM._Email,
                    caVM._confirmEmail,
                    caVM._Pass,
                    caVM._confirmPass,
                    caVM._Institution,
                    caVM._FirstName,
                    caVM._LastName);
                FormsAuthentication.SetAuthCookie(caVM._Email, false);
                return RedirectToAction("Index", "Home");
            }
            return View(caVM);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginViewModel LVM)
        {
            if (ModelState.IsValid)
            {
                if (LVM.Login(LVM._Email, LVM._Pass))
                {
                    FormsAuthentication.SetAuthCookie(LVM._Email, false);
                    return RedirectToAction("View_Data", "Data");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect!");
                }

            }
            return View(LVM);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("View_Data","Data");
        }
    }
}