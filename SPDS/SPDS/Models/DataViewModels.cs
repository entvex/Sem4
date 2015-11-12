
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

        [Display(Name = "FoundDataSets")]
        public List<string[]> _foundDataSets { get; set; }

        private IDalRetrieve dal;
        /// <summary>
        /// The method shall return the targetmaterial witch is equal to the searched request
        /// The method shall handle any name, mass etc. and return the propper data
        /// </summary>
        /// <returns></returns>
        public void Search(string targetName)
        {

            string[] dummy0 = { "TargetMaterial", "Projectile" };

            string[] dummy1 = { "Target1", "Projectile1" };
            string[] dummy2 = { "Target2", "Projectile1" };

            string[] dummy3 = { "Target2", "Projectile2" };

            string[] dummy4 = { "Target3", "Projectile3" };

            string[] dummy5 = { "Target4", "Projectile3" };

            try
            {
                ParametersForDataset parameters = new ParametersForDataset() { TargetMaterialName = targetName };
                dal = new MSSQLModelDAL();
                var foundData = dal.GetDatasets(parameters);




                _foundDataSets = new List<string[]>();
                _foundDataSets.Add(dummy0);
                _foundDataSets.Add(dummy1);
                _foundDataSets.Add(dummy2);

                _foundDataSets.Add(dummy3);

                _foundDataSets.Add(dummy4);

                _foundDataSets.Add(dummy5);


            }
            catch (NullReferenceException e)
            {
            }

        }
    }
}