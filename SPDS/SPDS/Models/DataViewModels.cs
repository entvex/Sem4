
using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;

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



        private IDalRetrieve dal;
        /// <summary>
        /// The method shall return the targetmaterial witch is equal to the searched request
        /// The method shall handle any name, mass etc. and return the propper data
        /// </summary>
        /// <returns></returns>
        public List<Dataset> Search(string targetName, string projectileName)
        {
            List<Dataset> dataName;

            try
            {
                ParametersForDataset parameters = new ParametersForDataset() { TargetMaterialName = targetName, ProjectileName = projectileName };
                dal = new MSSQLModelDAL();
                dataName = dal.GetDatasets(parameters);
                return dataName;
            }
            catch (NullReferenceException e)
            {
                dataName = new List<Dataset>();
                return dataName;
            }
        }
    }
}