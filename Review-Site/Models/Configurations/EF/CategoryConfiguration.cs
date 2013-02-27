using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Models.Configurations.EF
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasRequired(x => x.Color).WithMany().HasForeignKey(x => x.ColorID);
            HasOptional(x => x.Grid).WithMany().HasForeignKey(x => x.GridID);
        }
    }
}