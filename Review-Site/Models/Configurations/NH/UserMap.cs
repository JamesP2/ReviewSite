using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace Review_Site.Models.Configurations.NH
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.ID);

            Map(x => x.Username)
                .Not.Nullable();
            Map(x => x.Password)
                .Length(32)
                .Not.Nullable();

            Map(x => x.FirstName)
                .Not.Nullable();
            Map(x => x.LastName)
                .Not.Nullable();

            Map(x => x.AuthWithAD);

            Map(x => x.Created)
                .Not.Nullable();
            Map(x => x.LastModified);

            HasMany(x => x.Articles).KeyColumn("Author_id");
            HasMany(x => x.Resources).KeyColumn("Creator_id");
            HasManyToMany(x => x.Roles);
        }
    }
}