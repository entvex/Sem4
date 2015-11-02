using System.ComponentModel.DataAnnotations;

namespace SPDS.Models
{
    public class ViewDataViewModel
    {
        public enum SoA
        {
            Gaseous,
            Condensed
        };

        [Display(Name = "StateOfAggregation")]
        public SoA _stateOfAggregation { get; set; }

        [Display(Name = "ShowReviewed")]
        public bool _showReviewed { get; set; }
    }
}