using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class PageHit
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid Target { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual string ClientAddress { get; set; }
    }
}