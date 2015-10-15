namespace SPDS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Revision")]
    public partial class Revision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Revision()
        {
            PrevRevision = new HashSet<Revision>();
        }

        public int Id { get; set; }

        public string Comment { get; set; }

        public int? UserUserId { get; set; }

        public DateTime? Date { get; set; }

        public bool? Approved { get; set; }

        public int Dataset_Id { get; set; }

        public int? HeadRevision_Id { get; set; }

        public virtual Dataset Dataset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revision> PrevRevision { get; set; }

        public virtual Revision HeadRevision { get; set; }

        public virtual User User { get; set; }
    }
}
