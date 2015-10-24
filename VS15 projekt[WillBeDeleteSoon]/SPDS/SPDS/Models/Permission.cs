namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        public Permission()
        {
            User = new HashSet<User>();
        }

        public int PermissionId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
