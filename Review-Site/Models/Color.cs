using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Color
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}