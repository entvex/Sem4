using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SPDS.Models;
using SPDS.Models.DbModels;

namespace SPDS.Controllers
{
    public class FormatController : ApiController
    {
        private FormatModel formatmodel;

        [HttpGet]
        public string[,] TwoDDataArray(Dataset d)
        {
            return formatmodel.GetDataPointsByDataSet(d);
        }

        [HttpGet]
        public string[] Projectiles()
        {
            return formatmodel.GetProjectileList();
        }


    }
}
