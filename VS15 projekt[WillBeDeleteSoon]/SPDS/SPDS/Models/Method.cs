namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Method")]
    public partial class Method
    {
        public int MethodId { get; set; }

        public string Description { get; set; }

        public int? DatasetMethod_Method_DatasetId { get; set; }

        public virtual Dataset Dataset { get; set; }
    }
}
