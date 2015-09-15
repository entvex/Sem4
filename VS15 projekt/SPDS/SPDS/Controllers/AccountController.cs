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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPass()
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

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
          /*  if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
          &          return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect!");
                }

            }*/
            return View(user);
        }
    }
}