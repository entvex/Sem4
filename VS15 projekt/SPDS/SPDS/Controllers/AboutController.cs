using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPDS.Models;

namespace SPDS.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {

            //TSPDSModelContainer dam;
            //var bruger = new User();
            //bruger.Email = "jdksad";
            //bruger.FirstName = "jdksad";
            //bruger.LastName = "jdksad";
            //bruger.Institute = "jdksad";
            //bruger.Password = "jdksad";
            //bruger.PermissionPermissionId = 1;
            //dam.UserSet.Add(bruger);


            return View();
        }
    }
}