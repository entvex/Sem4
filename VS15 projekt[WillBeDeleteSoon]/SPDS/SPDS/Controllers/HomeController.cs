using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPDS.Models;

namespace SPDS.Controllers
{
    public class HomeController : Controller
    {

        Dataset db = new Dataset();

        // GET: Home
        public ActionResult Index(string option, string search)
        {
            if(option == "Name")
            {
                //return View(db.theDatabase.Where(x => x.Name == search || search == null).ToList());
            }
            else if (option == "Ion")
            {
               // return View(db.theDatabase.Where(x => x.Ion == search || search == null).ToList());
            }
            else if(option == "Projectile")
            {
                // return View(db.theDatabase.Where(x => x.Projectile == search || search == null).ToList());
            }
            else
            {
                // return View(db.theDatabase.Where(x => x.Mass == search || search == null).ToList());
            }
            string o = option;
            string s = search;
            return View(o, s);
        }
    }
}