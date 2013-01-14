using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Review_Site.Models
{
    public class Article
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid CategoryID { get; set; }
        public virtual Guid UserID { get; set; }

        [Required(ErrorMessage = "You must provide a title!")]
        public virtual string Title { get; set; }
        [MaxLength(150)]
        public virtual string ShortDescription { get; set; }
        [AllowHtml, Required(ErrorMessage = "You must enter some article text.")]
        public virtual string Text { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        public virtual int Issue { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }

}