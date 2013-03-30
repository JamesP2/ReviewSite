using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Mappings
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Not.Nullable();

            HasManyToMany(x => x.Articles);
        }
    }
}