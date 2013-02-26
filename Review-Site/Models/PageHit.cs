using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using Review_Site.Core.Data;

namespace Review_Site.Models
{
    public class PageHit : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid Target { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual string ClientAddress { get; set; }
    }
}