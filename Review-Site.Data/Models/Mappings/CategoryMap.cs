﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Title)
                .Not.Nullable();

            Map(x => x.IsSystemCategory);

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            References(x => x.Grid).Nullable();
            References(x => x.Color).Not.Nullable();
            HasMany(x => x.Articles);
        }
    }
}