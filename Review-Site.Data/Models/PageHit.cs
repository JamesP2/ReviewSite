﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
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