using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Review_Site.Models
{
    public class User
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Username { get; set; }
        public virtual byte[] Password { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual bool AuthWithAD { get; set; }

        public virtual IList<Article> Articles { get; set; }
        public virtual IList<Resource> Resources { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public virtual DateTime? Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [NotMapped]
        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public class UserMappingOverrides : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.IgnoreProperty(x => x.FullName);
        }
    }
}