namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Projectile")]
    public partial class Projectile
    {
        public Projectile()
        {
            Dataset = new HashSet<Dataset>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? zCharge { get; set; }

        public double? Mass { get; set; }

        public string PDGNumber { get; set; }

        public virtual ICollection<Dataset> Dataset { get; set; }
    }
}
