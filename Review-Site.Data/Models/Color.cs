﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Color : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}