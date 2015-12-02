namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Projectile")]
    public partial class Projectile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dataset> Dataset { get; set; }
    }
}
