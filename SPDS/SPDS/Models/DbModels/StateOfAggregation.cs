namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StateOfAggregation")]
    public partial class StateOfAggregation
    {
        public int StateOfAggregationId { get; set; }

        public string Form { get; set; }

        public int? DatasetStateOfAggregation_StateOfAggregation_DatasetId { get; set; }

        public virtual Dataset Dataset { get; set; }
    }
}
