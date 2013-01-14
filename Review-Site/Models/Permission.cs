using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Permission
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
        public virtual string Identifier { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}