using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
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

        public Role()
        {
            Permissions = new List<Permission>();
            AssignedUsers = new List<User>();
        }
    }

    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasMany(x => x.Permissions).WithMany(x => x.Roles);
        }
    }
}