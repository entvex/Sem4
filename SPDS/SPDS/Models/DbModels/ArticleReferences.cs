namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticleReferences
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArticleReferences()
        {
            Dataset = new HashSet<Dataset>();
        }

        public int Id { get; set; }

        public string DOINumber { get; set; }

        public int? DatasetId { get; set; }

        public int? Year { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dataset> Dataset { get; set; }
    }
}
