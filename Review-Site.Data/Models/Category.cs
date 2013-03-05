using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Category : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        [Required(ErrorMessage = "You must provide a category title.")]
        [RegularExpression(@"[^-]*", ErrorMessage = "Titles may not contain dashes.")]
        public virtual string Title { get; set; }
        public virtual bool IsSystemCategory { get; set; }

        public virtual IList<Article> Articles { get; set; }
        public virtual Grid Grid { get; set; }
        public virtual Color Color { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}