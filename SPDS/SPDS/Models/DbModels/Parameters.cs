using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLModel
{
    /// <summary>
    /// Parameters to search for when retrieving a dataset.
    /// </summary>
    public class ParametersForDataset
    {
        public string ProjectileName { get; set; }
        public string TargetMaterialName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Institute { get; set; }

        public int? RevId { get; set; }
        public int? MethodId { get; set; }
        public int? ArticleReferencesId { get; set; }
        public int? StateOfAggregationId { get; set; }

        public bool? Approved { get; set; }

        public string TargetMaterialChemicalFormula { get; set; }

        public double? TargetMaterialMolarMass { get; set; }

        public double? TargetMaterialMass { get; set; }

        public string TargetMaterialZCharge { get; set; }

        public string TargetMaterialICRUId { get; set; }

        public int? ProjectilezCharge { get; set; }

        public double? ProjectileMass { get; set; }

        public string ProjectilePDGNumber { get; set; }
    }

    public class ParametersForArticelreferences
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Year { get; set; }
        public string DOINumber { get; set; }
        public int ArticleReferencesId { get; set; }
    }

    public class ParametersForUsers
    {
        public string Email { get; set; }

        public string Institute { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public bool? WaitingOnPromotion { get; set; }
    }

}
