using MSSQLModel;

namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dataset")]
    public partial class Dataset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dataset()
        {
            DataPoint = new HashSet<DataPoint>();
            Revision = new HashSet<Revision>();
        }

        public int Id { get; set; }

        public int TargetMaterial_Id { get; set; }

        public int Projectile_Id { get; set; }

        public int? ArticleReferences_Id { get; set; }

        public int? Method_Id { get; set; }

        public int? StateOfAggregation_Id { get; set; }

        public virtual ArticleReferences ArticleReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataPoint> DataPoint { get; set; }

        public virtual Method Method { get; set; }

        public virtual Projectile Projectile { get; set; }

        public virtual StateOfAggregation StateOfAggregation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revision> Revision { get; set; }

        public virtual TargetMaterial TargetMaterial { get; set; }
    }
}
