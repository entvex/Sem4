using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSSQLModel;

namespace SPDS.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Administrator()
        {

            IDalUserManagement daluserManagement = new MSSQLModelDAL();


            //var merp = daluserManagement.GetUsers(new ParametersForUsers());

            //merp[0]

            ViewBag.users = daluserManagement.GetUsers( new ParametersForUsers() );

            return View();
        }
    }
}