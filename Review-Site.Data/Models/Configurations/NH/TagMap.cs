using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Configurations.NH
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(x => x.ID);

            Map(x => x.Name)
                .Not.Nullable();

            HasMany(x => x.Articles);
        }
    }
}