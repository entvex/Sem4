using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPDS.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Data()
        {
            return View();
        }

        public ActionResult View_Data()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Review_Data()
        {
            return View();
        }

        public ActionResult Submit_Data()
        {
            return View();
        }
    }
}