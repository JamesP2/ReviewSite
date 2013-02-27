using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Id(x => x.ID);

            Map(x => x.Name)
                .Not.Nullable();
            Map(x => x.Identifier)
                .Not.Nullable();

            HasManyToMany(x => x.Roles);
        }
    }
}