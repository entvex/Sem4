namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Revision = new HashSet<Revision>();
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Institute { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PermissionPermissionId { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual ICollection<Revision> Revision { get; set; }
    }
}
