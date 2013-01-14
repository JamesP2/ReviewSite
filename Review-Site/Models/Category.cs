using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Category
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid ColourID { get; set; }
        public virtual Guid? GridID { get; set; }

        [Required(ErrorMessage = "You must provide a category title.")]
        [RegularExpression(@"[^-]*", ErrorMessage = "Titles may not contain dashes.")]
        public virtual string Title { get; set; }
        public virtual bool IsSystemCategory { get; set; }

        public virtual IList<Article> Articles { get; set; }
        public virtual Grid Grid { get; set; }
        public virtual Colour Colour { get; set; }

        public virtual DateTime? Created { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}