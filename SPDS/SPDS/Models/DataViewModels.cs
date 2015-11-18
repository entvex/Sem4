﻿
using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;
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
        public int _doi { get; set; }

        [Display (Name = "Methods")]
        public string _methods { get; set; }


        private IDalRetrieve dal;
        /// <summary>
        /// The method shall return the targetmaterial witch is equal to the searched request
        /// The method shall handle any name, mass etc. and return the propper data
        /// </summary>
        /// <returns></returns>
        public void Search(string targetName)
        {
            try
            {
                dal = new MSSQLModelDAL();

                //retrieve datasets with desired targetmaterial
                ParametersForDataset parameters = new ParametersForDataset() { TargetMaterialName = targetName };
                var foundData = dal.GetDatasets(parameters);


                foreach (var dataSet in foundData)
                {
                    if (dataSet.Method == null)
                    {
                        dataSet.Method = new Method();
                        dataSet.Method.Name = "";
                    }


                    var projectileList = dal.GetallProjectiles();

                    if (projectileList == null)
                    {
                        //No data in dataset

                    }
                    var physState = dal.GetAllStateOfAggregation();

                    if (physState == null)
                    {
                        //No data in dataset
                    }
                }

                _foundDataSets = foundData;

            }
            catch (NullReferenceException e)
            {
            }

        }

        public void Search(string projectileName, int dummy)
        {
            try
            {
                dal = new MSSQLModelDAL();

                //retrieve datasets with desired targetmaterial
                ParametersForDataset parameters = new ParametersForDataset() { ProjectileName = projectileName  };
                var foundData = dal.GetDatasets(parameters);

                //Retrieve Projectile name
                var projectile = dal.GetProjectileByName(projectileName);

                var targetList = dal.GetAllTargetMaterials();
                var physState = dal.GetAllStateOfAggregation();

                foreach (var VAR in foundData)
                {
                    VAR.Projectile = projectile;

                    foreach (var VARIABLE in targetList)
                    {
                        if (VARIABLE.Id == VAR.TargetMaterial_Id)
                        {
                            VAR.TargetMaterial = VARIABLE;
                        }
                    }
                    if (VAR.StateOfAggregation_Id == null)
                    {
                        VAR.StateOfAggregation = new StateOfAggregation() { Form = "NA" };
                    }
                    else
                    {
                        foreach (var physS in physState)
                        {
                            if (VAR.StateOfAggregation_Id == physS.Id)
                            {
                                VAR.StateOfAggregation = physS;
                            }
                        }
                    }
                }


                _foundDataSets = foundData;

            }
            catch (NullReferenceException e)
            {
            }

        }

        public void Search(string projectileName, string targetMaterial)
        {
            try
            {
                dal = new MSSQLModelDAL();

                //retrieve datasets with desired targetmaterial
                ParametersForDataset parameters = new ParametersForDataset() { ProjectileName = projectileName, TargetMaterialName = targetMaterial };
                var foundData = dal.GetDatasets(parameters);

                //Retrieve Projectile and targetmaterial 
                var projectile = dal.GetProjectileByName(projectileName);
                var target = dal.GetTargetMaterialByName(targetMaterial);
                var physState = dal.GetAllStateOfAggregation(); 

                foreach (var VAR in foundData)
                {
                    VAR.Projectile = projectile;
                    VAR.TargetMaterial = target;
                    if (VAR.StateOfAggregation_Id == null)
                    {
                        VAR.StateOfAggregation = new StateOfAggregation() { Form = "NA" };
                    }
                    else
                    {
                        foreach (var physS in physState)
                        {
                            if (VAR.StateOfAggregation_Id == physS.Id)
                            {
                                VAR.StateOfAggregation = physS;
                            }
                        }
                    }
                }


                _foundDataSets = foundData;

            }
            catch (NullReferenceException e)
            {
            }

        }
    }
}