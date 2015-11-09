using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSSQLModel;
using SPDS.Models.DbModels;

namespace SPDS.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WebModel
    {
        private readonly IDalRetrieve dal = new MSSQLModelDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dataset> GetDatasetList(string projectileName, string targetMaterialName)
        {
            ParametersForDataset parameters = new ParametersForDataset();

            parameters.ProjectileName = projectileName;
            parameters.TargetMaterialName = targetMaterialName;

            List<Dataset> datasets = dal.GetDatasets(parameters);
            
            return datasets;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string[,] GetDataPointsByDataSet(Dataset d)
        {
            DataPoint[] data = dal.GetDataPointsByDataSet(d).ToArray();
            string[,] convertedlist = new string[data.Length + 1, 5];

            convertedlist[0, 0] = "ProjectileCharge";
            convertedlist[0, 1] = "EqEnergy";
            convertedlist[0, 2] = "StoppingPower";
            convertedlist[0, 3] = "ConvertetData";
            convertedlist[0, 4] = "Error";

            for (int i = 1; i < data.Length; i++)
            {
                convertedlist[i, 0] = data[i].ProjectileCharge.ToString();
                convertedlist[i, 1] = data[i].EqEnergy.ToString();
                convertedlist[i, 2] = data[i].StoppingPower.ToString();
                convertedlist[i, 3] = data[i].ConvertetData.ToString();
                convertedlist[i, 4] = data[i].Error.ToString();
            }

            return convertedlist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetProjectileNameList()
        {
            List<Projectile> projectiles = dal.GetallProjectiles();
            string[] projectilelist = new string[projectiles.Count];
            int i = 0;

            foreach (var p in projectiles)
            {
                projectilelist[i] = p.Name;
                ++i;
            }
            return projectilelist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetTargetMaterialNameList()
        {
            List<TargetMaterial> targetMartials = dal.GetAllTargetMaterials();
            string[] targetMartiallist = new string[targetMartials.Count];
            int i = 0;

            foreach (var p in targetMartials)
            {
                targetMartiallist[i] = p.Name;
                ++i;
            }
            return targetMartiallist;
        }

    }
}