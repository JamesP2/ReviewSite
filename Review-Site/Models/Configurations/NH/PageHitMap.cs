﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class PageHitMap : ClassMap<PageHit>
    {
        public PageHitMap()
        {
            Id(x => x.ID);

            Map(x => x.Target)
                .Not.Nullable();

            Map(x => x.Time)
                .Not.Nullable();

            Map(x => x.ClientAddress);
        }
    }
}