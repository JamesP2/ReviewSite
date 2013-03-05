﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Mappings
{
    public class GridMap : ClassMap<Grid>
    {
        public GridMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Not.Nullable();
            Map(x => x.Alias)
                .Not.Nullable();

            Map(x => x.Description);

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            HasMany(x => x.GridElements);
        }
    }
}