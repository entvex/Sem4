
using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SPDS.Models
{
    public class ViewDataViewModel
    {
        [Display(Name = "Gaseous")]
        public bool _gaseous { get; set; }

        [Display(Name = "Condensed")]
        public bool _condensed { get; set; }

        [Display(Name = "TargetMaterial")]
        public string _targetMaterial { get; set; }

        [Display(Name = "Projectile")]
        public string _projectile { get; set; }

        [Display(Name = "ShowReviewed")]
        public bool _showReviewed { get; set; }

        [Display(Name = "FoundDataSets")]
        //public List<string[]> _foundDataSets { get; set; }
        public List<Dataset> _foundDataSets { get; set; }

        [Display(Name = "Energy Max")]
        public bool _energyMax { get; set; }

        [Display(Name = "Energy Min")]
        public bool _energyMin { get; set; }

        [Display(Name = "Author")]
        public string _author { get; set; }

        [Display(Name = "Year")]
        public string _year { get; set; }

        [Display(Name = "DOI")]
        public string _doi { get; set; }

        [Display(Name = "Methods")]
        public string _methods { get; set; }


        private IDalRetrieve dal;

        /// <summary>
        /// The method shall return the targetmaterial witch is equal to the searched request
        /// The method shall handle any name, mass etc. and return the propper data
        /// </summary>
        /// <returns></returns>
        public void Search(string targetName)
        {
            dal = new MSSQLModelDAL();

            //retrieve datasets with desired targetmaterial
            ParametersForDataset parameters = new ParametersForDataset() {TargetMaterialName = targetName};
            var foundData = dal.GetDatasets(parameters);

            if (!foundData.Any())
            {
                new Dataset();
            }

            foreach (var dataSet in foundData)
            {
                if (dataSet.TargetMaterial == null)
                {
                    dataSet.TargetMaterial = new TargetMaterial();
                    dataSet.TargetMaterial.Name = "";
                }

                if (dataSet.StateOfAggregation == null)
                {
                    dataSet.StateOfAggregation = new StateOfAggregation();
                    dataSet.StateOfAggregation.Form = "";
                }

                if (dataSet.Projectile == null)
                {
                    dataSet.Projectile = new Projectile();
                    dataSet.Projectile.Name = "";
                }

                if (dataSet.Method == null)
                {
                    dataSet.Method = new Method();
                    dataSet.Method.Name = "";
                }
            }

            _foundDataSets = foundData;
        }

        public void Search(string projectileName, int dummy)
        {
            dal = new MSSQLModelDAL();

            //retrieve datasets with desired targetmaterial
            ParametersForDataset parameters = new ParametersForDataset() {ProjectileName = projectileName};
            var foundData = dal.GetDatasets(parameters);

            if (!foundData.Any())
            {
                new Dataset();
            }

            foreach (var dataSet in foundData)
            {
                if (dataSet.TargetMaterial == null)
                {
                    dataSet.TargetMaterial = new TargetMaterial();
                    dataSet.TargetMaterial.Name = "";
                }

                if (dataSet.StateOfAggregation == null)
                {
                    dataSet.StateOfAggregation = new StateOfAggregation();
                    dataSet.StateOfAggregation.Form = "";
                }

                if (dataSet.Projectile == null)
                {
                    dataSet.Projectile = new Projectile();
                    dataSet.Projectile.Name = "";
                }

                if (dataSet.Method == null)
                {
                    dataSet.Method = new Method();
                    dataSet.Method.Name = "";
                }
            }
            _foundDataSets = foundData;

        }

        public void Search(string projectileName, string targetMaterial)
        {

            dal = new MSSQLModelDAL();

            //retrieve datasets with desired targetmaterial
            ParametersForDataset parameters = new ParametersForDataset()
            {
                ProjectileName = projectileName,
                TargetMaterialName = targetMaterial
            };
            var foundData = dal.GetDatasets(parameters);

            if (!foundData.Any())
            {
                new Dataset();
            }

            foreach (var dataSet in foundData)
            {
                if (dataSet.TargetMaterial == null)
                {
                    dataSet.TargetMaterial = new TargetMaterial();
                    dataSet.TargetMaterial.Name = "";
                }

                if (dataSet.StateOfAggregation == null)
                {
                    dataSet.StateOfAggregation = new StateOfAggregation();
                    dataSet.StateOfAggregation.Form = "";
                }

                if (dataSet.Projectile == null)
                {
                    dataSet.Projectile = new Projectile();
                    dataSet.Projectile.Name = "";
                }

                if (dataSet.Method == null)
                {
                    dataSet.Method = new Method();
                    dataSet.Method.Name = "";
                }

            }
        }
    }
}