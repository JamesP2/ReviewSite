using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Role
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Permission> Permissions { get; set; }
        public virtual IList<User> AssignedUsers { get; set; }
    }
}