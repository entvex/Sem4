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
        private WebModel _webmodel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatmodel"></param>
        public WebController(WebModel formatmodel)
        {
            this._webmodel = formatmodel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpGet]
        public string[,] table(string projectile, string targetMarterial)
        {
            return _webmodel.GetDataPointsByDataSet(_webmodel.GetDatasetList(projectile, targetMarterial)[0]);
        }


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
        public string[] ProjectileNames()
        {
            return _webmodel.GetProjectileNameList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string[] TargetMartialsNames()
        {
            return _webmodel.GetTargetMaterialNameList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void DatasetManuel(DatasetQuery d)
        {
             _webmodel.SetDataset(d);
        }

        [HttpGet]
        public User[] GetAllUsers()
        {
            return _webmodel.GetALlUsers();
        }
    }
}
