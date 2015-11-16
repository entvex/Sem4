using System;
using System.Collections.Generic;
using System.Linq;
using MSSQLModel;
using MSSQLModel.Exceptions;
using SPDS.Models.DbModels;

namespace SPDS.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WebModel
    {
        private readonly IDalRetrieve _dalRetrieve = new MSSQLModelDAL();
        private readonly IDalInsert _dalInsert = new MSSQLModelDAL();
        private readonly IDalUserManagement _dalUserManagement = new MSSQLModelDAL();

        public User[] GetALlUsers()
        {
            return _dalUserManagement.GetUsers(new ParametersForUsers()).ToArray();
        }

        public void SetDataset(DataPoint[] datapoints, string targetMaterial, string projectile, string format,
                               string stateOfAggregation, string doiNumber, string email, string method, string comment)
        {
            var tm = _dalRetrieve.GetTargetMaterialByName(targetMaterial);
            if (tm.Id == 0)
                return;
            var pjt = _dalRetrieve.GetProjectileByName(projectile);
            if (pjt.Id == 0)
                return;
            var fmt = _dalRetrieve.GetDataformatByNotation(format);
            if (fmt.Id == 0)
                return;
            var soa = _dalRetrieve.GetStateOfAggregationByForm(stateOfAggregation);
            if (soa.Id == 0)
                return;
            var article = _dalRetrieve.GetArticleReferences(new ParametersForArticelreferences() { DOINumber = doiNumber });
            if (!article.Any())
                return;
            var md = _dalRetrieve.GetMethodByName(method);
            if (md.Id == 0)
                return;

            Revision revision = new Revision();
            revision.Date = DateTime.Now;
            revision.Comment = comment;

            var users = _dalRetrieve.GetUsers(new ParametersForUsers() { Email = email });
            if (!users.Any())
                return;

            try
            {
                _dalInsert.InsertDataset(datapoints.OfType<DataPoint>().ToList(), tm, pjt, fmt, fmt, revision, users[0], null, article[0], md, soa);
            }
            catch (DALInfoNotSpecifiedException e)
            {
                // error handling
            }
            catch (DALAlreadyExistsException e)
            {
                // error handling
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dataset> GetDatasetList(string projectileName, string targetMaterialName)
        {
            ParametersForDataset parameters = new ParametersForDataset();

            parameters.ProjectileName = projectileName;
            parameters.TargetMaterialName = targetMaterialName;

            List<Dataset> datasets = _dalRetrieve.GetDatasets(parameters);
            
            return datasets;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string[,] GetDataPointsByDataSet(Dataset d)
        {
            DataPoint[] data = _dalRetrieve.GetDataPointsByDataSet(d).ToArray();
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
            List<Projectile> projectiles = _dalRetrieve.GetallProjectiles();
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
            List<TargetMaterial> targetMartials = _dalRetrieve.GetAllTargetMaterials();
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