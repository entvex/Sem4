using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using MSSQLModel;
using SPDS.Models;
using SPDS.Models.DbModels;

namespace SPDS.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        [HttpGet]
        [Authorize(Roles = "Reviewer")]
        public ActionResult Administrator()
        {
            IDalUserManagement daluserManagement = new MSSQLModelDAL();
            ViewBag.users = daluserManagement.GetUsers( new ParametersForUsers() );

            return View();
        }

        /// <summary>
        /// Handles the action requests from the Administrator page.
        /// </summary>
        /// <param name="email"> user email </param>
        /// <returns>200 for ok else 404</returns>
        [HttpPost]
        [Authorize(Roles = "Reviewer")]

        public ActionResult Administrator(string email)
        {
            if (!String.IsNullOrWhiteSpace(email) && email.Contains(";") && email.Contains("@"))
            {
                var mail = email.Split(';');

                IDalUserManagement daluserManagement = new MSSQLModelDAL();
                var user = daluserManagement.GetUsers(new ParametersForUsers() { Email = mail[1] });

                //If no user was found in the database then return an error
                if (user[0] == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);


                if (mail[0] == "delete")
                {
                    if (mail[1].Contains("@"))
                    {
                        daluserManagement.DeleteUser(user.First());

                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }

                if (mail[0] == "promote")
                {
                    if (mail[1].Contains("@"))
                    {
                        var perm = daluserManagement.GetPermByAccessLevel(AccessLevel.Reviewer);

                        daluserManagement.UpdateUserPermission(user.First(), perm);

                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }

                if (mail[0] == "demote")
                {
                    if (mail[1].Contains("@"))
                    {
                        var perm = daluserManagement.GetPermByAccessLevel(AccessLevel.Submitter);

                        daluserManagement.UpdateUserPermission(user.First(), perm);

                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

    }
}