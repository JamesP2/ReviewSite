using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Configurations.EF
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(x => x.Articles).WithRequired(x => x.Author).HasForeignKey(x => x.AuthorID);
            HasMany(x => x.Roles).WithMany(x => x.AssignedUsers);
        }
    }
}