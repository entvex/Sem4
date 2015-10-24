using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSSQLModel;

namespace SPDS.Controllers
{
    public class HomeController : Controller
    {

        //IDALRetrieving db = new MSSQLModelDAL();
        
        //// GET: Home
        //public ActionResult Index(string option, string search)
        //{
        //    if (option == "Name")
        //    {
        //         return View(db.GetAllDatasetsByFirstName(search));
        //    }
        //    else if (option == "Ion")
        //    {
        //        var ion = db.GetProjectileByName(search);
        //        return View(db.GetAllDatasetsByProjectile(ion));
        //    }
        //    else
        //    {
        //        var target = db.GetTargetMaterialByName(search);
        //        return View(db.GetAllDatasetsByTargetMaterial(target));
        //    }
        //}
    }
}