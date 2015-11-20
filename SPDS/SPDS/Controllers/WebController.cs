using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SPDS.Models;
using SPDS.Models.DbModels;

namespace SPDS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WebController : ApiController
    {
        private WebModel _webmodel = new WebModel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpGet]
        public Dataset[] Datasets(string projectile, string targetMarterial)
        {
            return _webmodel.GetDatasetList(projectile, targetMarterial).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpGet]
        public string[,] Datapoints(Dataset d)
        {
            return _webmodel.GetDataPointsByDataSet(d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Projectile[] Projectiles()
        {
            return _webmodel.GetProjectiles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public TargetMaterial[] TargetMaterials()
        {
            return _webmodel.GetTargetMaterials();
        }

    }
}
