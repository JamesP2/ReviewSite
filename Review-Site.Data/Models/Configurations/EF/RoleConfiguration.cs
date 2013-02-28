using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Configurations.EF
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasMany(x => x.Permissions).WithMany(x => x.Roles);
        }
    }
}