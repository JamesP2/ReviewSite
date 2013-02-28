using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Configurations.EF
{
    public class ResourceConfiguration : EntityTypeConfiguration<Resource>
    {
        public ResourceConfiguration()
        {
            HasRequired(x => x.Creator).WithMany(x => x.Resources).HasForeignKey(x => x.CreatorID);
            HasOptional(x => x.SourceTextColor).WithMany().HasForeignKey(x => x.SourceTextColorID);
        }
    }
}