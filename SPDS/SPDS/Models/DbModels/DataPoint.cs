namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataPoint")]
    public partial class DataPoint
    {
        public int Id { get; set; }

        public double? ProjectileCharge { get; set; }

        public double? EqEnergy { get; set; }

        public double? StoppingPower { get; set; }

        public double? ConvertetData { get; set; }

        public double? Error { get; set; }

        public int DatasetDatasetId { get; set; }

        public int DataformatForOriginal_Id { get; set; }

        public int DataformatForConverted_Id { get; set; }

        public virtual Dataformat OriginalDataformat { get; set; }

        public virtual Dataformat ConvertedDataformat { get; set; }

        public virtual Dataset Dataset { get; set; }
    }
}
