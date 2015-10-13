namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Revision")]
    public partial class Revision
    {
        public Revision()
        {
            HeadRevision = new HashSet<Revision>();
        }

        [Key]
        public int RevId { get; set; }

        public string Comment { get; set; }

        public int? UserUserId { get; set; }

        public DateTime? Date { get; set; }

        public int PrevRevision_RevId { get; set; }

        public int Dataset_DatasetId { get; set; }

        public virtual Dataset Dataset { get; set; }

        public virtual ICollection<Revision> HeadRevision { get; set; }

        public virtual Revision PrevRevision { get; set; }

        public virtual User User { get; set; }
    }
}
