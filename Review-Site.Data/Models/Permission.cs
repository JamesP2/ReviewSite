using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Permission : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
        public virtual string Identifier { get; set; }


        public virtual IList<Role> Roles { get; set; }

        public Permission()
        {
            Roles = new List<Role>();
        }
    }
}