﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPDS.Models;

namespace SPDS.Controllers
{
    public class AboutController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}