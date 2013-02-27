﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class ResourceMap : ClassMap<Resource>
    {
        public ResourceMap()
        {
            Id(x => x.ID);

            Map(x => x.Title)
                .Not.Nullable();
            Map(x => x.Type)
                .Not.Nullable();

            Map(x => x.DateAdded)
                .Not.Nullable();
            Map(x => x.LastModified);

            References(x => x.Creator);
            References(x => x.SourceTextColor);
        }
    }
}