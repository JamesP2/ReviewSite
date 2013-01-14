using System;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Colour
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public virtual DateTime? Created { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}