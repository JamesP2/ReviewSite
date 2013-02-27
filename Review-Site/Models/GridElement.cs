using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using Review_Site.Core.Data;

namespace Review_Site.Models
{
    public class GridElement : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }
        [Required(ErrorMessage = "You must select a colour to use.")]
        public virtual Guid BorderColorID { get; set; }
        [Required(ErrorMessage = "You must select an article to reference.")]
        public virtual Guid ArticleID { get; set; }
        [Required(ErrorMessage = "You must associate this element with a grid.")]
        public virtual Guid GridID { get; set; }
        [Required(ErrorMessage="You must select an image to use.")]
        public virtual Guid ImageID { get; set; }
        [Required]
        public virtual string SizeClass { get; set; }
        [Required]
        public virtual string HeadingClass { get; set; }

        public virtual string HeadingText { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [Required]
        public virtual int Width { get; set; }

        public virtual bool UseHeadingText { get; set; }
        public virtual bool InverseHeading { get; set; }

        public virtual Color BorderColor { get; set; }
        public virtual Article Article { get; set; }
        public virtual Grid Grid { get; set; }
        public virtual Resource Image { get; set; }
    }
}