
using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;
using System;

namespace SPDS.Models
{
    public class ViewDataViewModel
    {
        public enum SoA
        {
            Gaseous,
            Condensed
        };

        [Display(Name = "Gaseous")]
        public bool _gaseous { get; set; }

        [Display(Name = "Condensed")]
        public bool _condensed { get; set; }

        [Display(Name = "TargetMaterial")]
        public string _targetMaterial { get; set; }

        [Display(Name = "Projectile")]
        public string _projectile { get; set; }

        [Display(Name = "StateOfAggregation")]
        public SoA _stateOfAggregation { get; set; }

        [Display(Name = "ShowReviewed")]
        public bool _showReviewed { get; set; }



        private IDalRetrieve dal;
        /// <summary>
        /// The method shall return the targetmaterial witch is equal to the searched request
        /// The method shall handle any name, mass etc. and return the propper data
        /// </summary>
        /// <returns></returns>
        public List<Dataset> Search(string name)
        {
            List<Dataset> target;

            try
            {
                ParametersForDataset parameters = new ParametersForDataset() { TargetMaterialName = name };
                target = dal.GetDatasets(parameters);
                return target;
            }
            catch (NullReferenceException e)
            {
                target = new List<Dataset>();
                return target;
            }
        }
    }
}