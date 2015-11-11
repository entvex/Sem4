using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPDS.Models;
using SPDS.Models.DbModels;

namespace SPDS.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Data()
        {
            return View();
        }
        [HttpGet]
        public ActionResult View_Data()
        {
            return View();
        }
        [HttpPost]
        public ActionResult View_Data(ViewDataViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.Search(model._targetMaterial);
            }
            return View(model);
        }

        [Authorize(Roles = "3")]
        public ActionResult Review_Data()
        {
            return View();
        }

        [Authorize(Roles = "3,1")]
        public ActionResult Submit_Data()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Submit_Data(submitmodel model)
        {
            if(ModelState.IsValid)
            {

                TempData["notice"] = "Data was successfully Submitted";
                return RedirectToAction("Index", "Home");
                
            }
            
       

            return View();  
        }
    }
}