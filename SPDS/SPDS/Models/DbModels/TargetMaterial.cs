namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TargetMaterial")]
    public partial class TargetMaterial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TargetMaterial()
        {
            Dataset = new HashSet<Dataset>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ChemicalFormula { get; set; }

        public double? MolarMass { get; set; }

        public double? Mass { get; set; }

        public string ZCharge { get; set; }

        public string ICRUId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dataset> Dataset { get; set; }
    }
}
