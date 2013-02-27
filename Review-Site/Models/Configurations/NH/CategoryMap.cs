using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.ID);

            Map(x => x.Title)
                .Not.Nullable();

            Map(x => x.IsSystemCategory);

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            References(x => x.Grid);
            References(x => x.Color);
            HasMany(x => x.Articles);
        }
    }
}