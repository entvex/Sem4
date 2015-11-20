using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSSQLModel;
using SPDS.Models;
using SPDS.Models.DbModels;

namespace SPDS.Controllers
{
    public class DataController : Controller
    {

        // GET controller for Data popup-window
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Data()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult View_Data()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult View_Data(ViewDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model._targetMaterial) &&
                    string.IsNullOrEmpty(model._projectile))
                {
                    //invalid search - create empty list of datasets
                    model._foundDataSets = new List<Dataset>();
                }

                //invalid projectile entered - valid target material
                if ((model._targetMaterial != null || model._targetMaterial != "") &&
                    string.IsNullOrEmpty(model._projectile))
                {
                    //search for target material
                    model.Search(model._targetMaterial);
                }
                //invalid targetmaterial - valid projectile
                else if (string.IsNullOrEmpty(model._targetMaterial) &&
                         (model._projectile != null || model._projectile != ""))
                {
                    //search for projectile
                    model.Search(model._projectile, 0);
                }
                //both targetmaterial and projectile are valid
                else if ((model._targetMaterial != null || model._targetMaterial != "") &&
                         (model._projectile != null || model._projectile != ""))
                {
                    //search for both target material and projectile
                    model.Search(model._projectile, model._targetMaterial);
                }

                return View(model);
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Reviewer,Waiting for approval,Submitter")]
        public ActionResult Review_Data()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Reviewer,Waiting for approval,Submitter")]
        public ActionResult Submit_Data()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Reviewer,Waiting for approval,Submitter")]
        public ActionResult Submit_Data(Submitmodel model)
        {
            if(ModelState.IsValid)
            {

                TempData["notice"] = "Data was successfully Submitted";

                var Datacollection = new DatasetQuery();
                Datacollection.comment = model._comment;
                Datacollection.doiNumber = model._doiNumber;
                Datacollection.method = model._method;
                Datacollection.projectile = model._projectile;
                Datacollection.stateOfAggregation = model._stateOfAggregation;
                Datacollection.targetMaterial = model._targetMaterial;
                Datacollection.email = User.Identity.Name;
                Datacollection.datapoints = model._manualString;



                return RedirectToAction("View_Data", "Data");
                
            }
            
       

            return View();  
        }
    }
}