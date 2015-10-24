namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dataformat")]
    public partial class Dataformat
    {
        public Dataformat()
        {
            DataPoint = new HashSet<DataPoint>();
            DataPoint1 = new HashSet<DataPoint>();
        }

        [Key]
        public int FormatId { get; set; }

        public string DataNotiation { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DataPoint> DataPoint { get; set; }

        public virtual ICollection<DataPoint> DataPoint1 { get; set; }
    }
}
