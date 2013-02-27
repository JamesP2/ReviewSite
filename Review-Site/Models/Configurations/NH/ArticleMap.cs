using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            Id(x => x.ID);

            Map(x => x.Issue);

            Map(x => x.Title)
                .Not.Nullable();
            Map(x => x.ShortDescription)
                .Length(150);
            Map(x => x.Text)
                .Not.Nullable();

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            HasManyToMany(x => x.Tags);
            References(x => x.Author);
            References(x => x.Category);
        }
    }
}