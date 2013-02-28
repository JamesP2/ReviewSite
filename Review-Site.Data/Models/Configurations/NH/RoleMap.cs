using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Configurations.NH
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.ID);

            Map(x => x.Name)
                .Not.Nullable();

            HasManyToMany(x => x.Permissions);
            HasManyToMany(x => x.AssignedUsers);
        }
    }
}