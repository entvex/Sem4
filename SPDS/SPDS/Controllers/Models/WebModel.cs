using System;
using System.Collections.Generic;
using System.Linq;
using MSSQLModel;
using MSSQLModel.Exceptions;
using SPDS.Models.DbModels;
using FileHelpers;

namespace SPDS.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WebModel
    {
        private readonly IDalRetrieve _dalRetrieve = new MSSQLModelDAL();
        private readonly IDalInsert _dalInsert = new MSSQLModelDAL();
        private FileHelperEngine<CSVData> engine = new FileHelperEngine<CSVData>();

        public string SetDataset(DatasetQuery dataq)
        {
            CSVData[] result;

            try
            {
                result = engine.ReadString(dataq.datapoints);
            }
            catch (Exception e)
            {
                return "not successful. Reason: invalid datapoints";
            }

            List<DataPoint> data = new List<DataPoint>();
            DataPoint temp = new DataPoint();

            foreach (var d in result)
            {
                temp.ConvertetData = d.ConvertetData;
                temp.EqEnergy = d.EqEnergy;
                temp.Error = d.Error;
                temp.ProjectileCharge = d.ProjectileCharge;
                temp.StoppingPower = d.StoppingPower;

                data.Add(temp);
            }

            var tm = _dalRetrieve.GetTargetMaterialByName(dataq.targetMaterial);
            if (tm.Id == 0)
                return "not successful. Reason: invalid target material";
            var pjt = _dalRetrieve.GetProjectileByName(dataq.projectile);
            if (pjt.Id == 0)
                return "not successful. Reason: invalid projectile";
            var fmt = _dalRetrieve.GetDataformatByNotation(dataq.format);
            if (fmt.Id == 0)
                return "not successful. Reason: invalid format";
            var soa = _dalRetrieve.GetStateOfAggregationByForm(dataq.stateOfAggregation);
            if (soa.Id == 0)
                return "not successful. Reason: invalid physical state";
            var article = _dalRetrieve.GetArticleReferences(new ParametersForArticelreferences() { DOINumber = dataq.doiNumber });
            if (!article.Any())
                return "not successful. Reason: invalid article references";
            var md = _dalRetrieve.GetMethodByName(dataq.method);
            if (md.Id == 0)
            {
                _dalInsert.InsertMethod(new Method { Name = dataq.method });
                md = _dalRetrieve.GetMethodByName(dataq.method);
            }

            Revision revision = new Revision();
            revision.Date = DateTime.Now;
            revision.Comment = dataq.comment;

            var users = _dalRetrieve.GetUsers(new ParametersForUsers() { Email = dataq.email });
            if (!users.Any())
                return "not successful. Reason: invalid user";

            try
            {
                _dalInsert.InsertDataset(data, tm, pjt, fmt, fmt, revision, users[0], null, article[0], md, soa);
                return "successful";
            }
            catch (DALInfoNotSpecifiedException e)
            {
                return e.ToString();
            }
            catch (DALAlreadyExistsException e)
            {
                return e.ToString();
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
        public Projectile[] GetProjectiles()
        {
            List<Projectile> projectiles = _dalRetrieve.GetallProjectiles();

            return projectiles.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TargetMaterial[] GetTargetMaterials()
        {
            List<TargetMaterial> targetMartials = _dalRetrieve.GetAllTargetMaterials();
            return targetMartials.ToArray();
        }

    }
}

[DelimitedRecord(",")]
public class CSVData
{
    public double ProjectileCharge;

    public double EqEnergy;

    public double StoppingPower;

    public double ConvertetData;

    public double Error;
}

public struct DatasetQuery
{
    public string datapoints;
    public string targetMaterial;
    public string projectile;
    public string format;
    public string stateOfAggregation;
    public string doiNumber;
    public string email;
    public string method;
    public string comment;
}
