using System.ComponentModel.DataAnnotations;
using SPDS.Models.DbModels;
using MSSQLModel;
using System.Collections.Generic;

namespace SPDS.Models
{
    public class ViewDataViewModel
    {
        public enum SoA
        {
            Gaseous,
            Condensed
        };

        public bool _gaseous { get; set; }
        public bool _condensed { get; set; }
        public string _targetMaterial { get; set; }
        public string _projectile { get; set; }

        [Display(Name = "StateOfAggregation")]
        public SoA _stateOfAggregation { get; set; }

        [Display(Name = "ShowReviewed")]
        public bool _showReviewed { get; set; }



        private IDalRetrieve dal;

        public List<TargetMaterial> GetNameOnTargetMatrials()
        {
            List<TargetMaterial> target = dal.GetAllTargetMaterials();
            List<TargetMaterial> list = new List<TargetMaterial>();

            foreach (var t in target)
            {
                if(_stateOfAggregation == _targetMaterial )
            }
            return list;
        }

    }
}