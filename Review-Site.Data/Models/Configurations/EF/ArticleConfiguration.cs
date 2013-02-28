using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Configurations.EF
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasRequired(x => x.Category).WithMany(x => x.Articles)
                .HasForeignKey(x => x.CategoryID);
            HasRequired(x => x.Author).WithMany(x => x.Articles)
                .HasForeignKey(x => x.AuthorID);
            HasMany(x => x.Tags).WithMany(x => x.Articles);
        }
    }
}