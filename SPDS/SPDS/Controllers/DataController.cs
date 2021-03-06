﻿using System;
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
                    //add error to modelstate
                    model._foundDataSets = new List<Dataset>();
                    ModelState.AddModelError("", "Please enter Target material and / or Projectile");
                }

                //invalid projectile entered - valid target material
                if (!string.IsNullOrEmpty(model._targetMaterial) &&
                    string.IsNullOrEmpty(model._projectile))
                {
                    //search for target material
                    model.Search(model._targetMaterial);
                }
                //invalid targetmaterial - valid projectile
                else if (string.IsNullOrEmpty(model._targetMaterial) &&
                         !string.IsNullOrEmpty(model._projectile))
                {
                    //search for projectile
                    model.Search(model._projectile, 0);
                }
                //both targetmaterial and projectile are valid
                else if (!string.IsNullOrEmpty(model._targetMaterial) &&
                         !string.IsNullOrEmpty(model._projectile))
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
        [Authorize(Roles = "Reviewer,Submitter")]
        public ActionResult Submit_Data()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Reviewer,Submitter")]
        public ActionResult Submit_Data(Submitmodel model)
        {
            if (ModelState.IsValid)
            {
                WebModel _web = new WebModel();
                string result = null;

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

                if (model._uploadedfile != null)
                {
                    result = new StreamReader(model._uploadedfile.InputStream).ReadToEnd();
                }

                if (result != null)
                {
                    Datacollection.datapoints = result;
                }
                else
                {
                    Datacollection.datapoints = model._manualString;
                }

                result = _web.SetDataset(Datacollection);

                TempData["notice"] = "Submission was " + result;
                return RedirectToAction("Submit_Data", "Data");

            }

            return View();
        }
    }
}