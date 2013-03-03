using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Configurations.NH
{
    public class PageHitMap : ClassMap<PageHit>
    {
        public PageHitMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Target)
                .Not.Nullable();

            Map(x => x.Time)
                .Not.Nullable();

            Map(x => x.ClientAddress);
        }
    }
}