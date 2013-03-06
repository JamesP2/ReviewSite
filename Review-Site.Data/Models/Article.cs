using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Article : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        [Required(ErrorMessage="You must provide a title!")]
        public virtual string Title { get; set; }
        [MaxLength(150)]
        public virtual string ShortDescription { get; set; }
        [AllowHtml, Required(ErrorMessage = "You must enter some article text.")]
        public virtual string Text { get; set; }
        
        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        public virtual int Issue { get; set; }

        public virtual User Author { get; set; }
        public virtual Category Category { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public Article()
        {
            Tags = new List<Tag>();
        }
    }
}