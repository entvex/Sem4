using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSSQLModel;
using SPDS.Models;
using SPDS.Models.DbModels;
using System.IO;

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

        [Authorize(Roles = "Reviewer")]
        public ActionResult Review_Data()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Reviewer")]
        public ActionResult Submit_Data()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Submit_Data(Submitmodel model)
        {
            if (ModelState.IsValid)
            {
                WebModel _web = new WebModel();

                TempData["notice"] = "Data was successfully Submitted";

                var Datacollection = new DatasetQuery();
                Datacollection.comment = model._comment;
                Datacollection.format = model._format;
                Datacollection.doiNumber = model._doiNumber;
                Datacollection.method = model._method;
                Datacollection.projectile = model._projectile;
                Datacollection.stateOfAggregation = model._stateOfAggregation;
                Datacollection.targetMaterial = model._targetMaterial;
                Datacollection.email = User.Identity.Name;

                string result = new StreamReader(model._uploadedfile.InputStream).ReadToEnd();

                if (result != null)
                {
                    Datacollection.datapoints = result;
                }
                else
                {
                    Datacollection.datapoints = model._manualString;
                }

                _web.SetDataset(Datacollection);

                return RedirectToAction("View_Data", "Data");

            }

            return View();
        }
    }
}