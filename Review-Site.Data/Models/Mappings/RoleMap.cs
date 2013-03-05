﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Data.Models.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.Name)
                .Not.Nullable();

            HasManyToMany(x => x.Permissions);
            HasManyToMany(x => x.AssignedUsers);
        }
    }
}