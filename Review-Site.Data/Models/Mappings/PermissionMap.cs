using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Mappings
{
    public class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Not.Nullable();
            Map(x => x.Identifier)
                .Not.Nullable();

            HasManyToMany(x => x.Roles);
        }
    }
}