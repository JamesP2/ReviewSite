using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Review_Site.Models
{
    public class Tag
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