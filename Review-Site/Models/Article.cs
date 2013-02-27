using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Review_Site.Core.Data;

namespace Review_Site.Models
{
    public class Article : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }
        [Required(ErrorMessage="You must select a Category!")]
        public virtual Guid CategoryID { get; set; }
        public virtual Guid AuthorID { get; set; }

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