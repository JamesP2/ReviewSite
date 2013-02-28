using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Configurations.NH
{
    public class GridElementMap : ClassMap<GridElement>
    {
        public GridElementMap()
        {
            Id(x => x.ID);

            Map(x => x.SizeClass)
                .Not.Nullable();
            Map(x => x.Width)
                .Not.Nullable();

            Map(x => x.HeadingClass)
                .Not.Nullable();

            Map(x => x.HeadingText);

            Map(x => x.UseHeadingText);
            Map(x => x.InverseHeading);

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            References(x => x.BorderColor);
            References(x => x.Article);
            References(x => x.Grid);
            References(x => x.Image);
        }
    }
}