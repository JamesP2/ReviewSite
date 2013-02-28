using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Tag : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual String Name { get; set; }

        public virtual IList<Article> Articles { get; set; }

        public Tag()
        {
            Articles = new List<Article>();
        }
    }
}