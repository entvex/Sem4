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
        [AllowAnonymous]
        public ActionResult ForgotPass()
        {
            return View();
        }


        [Authorize(Roles = "Reviewer,Waiting for approval,Submitter")]
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
            if (ModelState.IsValid && (caVM._Institution != "" || caVM._Institution != null))
            {
                if (caVM.Create(caVM._Email,
                    caVM._confirmEmail,
                    caVM._Pass,
                    caVM._confirmPass,
                    caVM._Institution,
                    caVM._FirstName,
                    caVM._LastName))
                {
                    FormsAuthentication.SetAuthCookie(caVM._Email, false);
                    return RedirectToAction("View_Data", "Data");
                }
                
                
                ModelState.AddModelError("", "Login Data is incorrect!");
                return View(caVM);
                

            }

            if (ModelState.IsValid && string.IsNullOrEmpty(caVM._Institution))
            {
                if (caVM.Create(caVM._Email,
                    caVM._confirmEmail,
                    caVM._Pass,
                    caVM._confirmPass,
                    caVM._FirstName,
                    caVM._LastName))
                {
                    FormsAuthentication.SetAuthCookie(caVM._Email, false);
                    return RedirectToAction("View_Data", "Data");
                }
                
                ModelState.AddModelError("", "Login Data is incorrect!");
                return View(caVM);
                

            }
            return View(caVM);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginReturn result = model.Login(model._Email, model._Pass);

                if (result.status)
                {
                    FormsAuthentication.SetAuthCookie(model._Email, false);
                    return RedirectToAction("View_Data", "Data");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect!");
                    ViewBag.Message = result.message;
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Reviewer,Waiting for approval,Submitter")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("View_Data","Data");
        }
    }
}