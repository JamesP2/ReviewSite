using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Configurations.EF
{
    public class GridElementConfiguration : EntityTypeConfiguration<GridElement>
    {
        public GridElementConfiguration()
        {
            HasRequired(x => x.BorderColor).WithMany().HasForeignKey(x => x.BorderColorID);
            HasRequired(x => x.Article).WithMany().HasForeignKey(x => x.ArticleID).WillCascadeOnDelete(false);
            HasRequired(x => x.Grid).WithMany(x => x.GridElements).HasForeignKey(x => x.GridID);
            HasRequired(x => x.Image).WithMany().HasForeignKey(x => x.ImageID);
        }
    }
}