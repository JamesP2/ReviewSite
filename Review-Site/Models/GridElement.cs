using System;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class GridElement
    {
        [Key]
        public virtual Guid ID { get; set; }
        [Required(ErrorMessage = "You must select a colour to use.")]
        public virtual Guid BorderColourID { get; set; }
        [Required(ErrorMessage = "You must select an article to reference.")]
        public virtual Guid ArticleID { get; set; }
        [Required(ErrorMessage = "You must associate this element with a grid.")]
        public virtual Guid GridID { get; set; }
        [Required(ErrorMessage = "You must select an image to use.")]
        public virtual Guid ImageID { get; set; }

        [Required]
        public virtual string SizeClass { get; set; }
        [Required]
        public virtual string HeadingClass { get; set; }

        public virtual string HeadingText { get; set; }

        public virtual DateTime? Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [Required]
        public virtual int Width { get; set; }

        public virtual bool UseHeadingText { get; set; }
        public virtual bool InverseHeading { get; set; }

        public virtual Colour BorderColour { get; set; }
        public virtual Article Article { get; set; }
        public virtual Grid Grid { get; set; }
        public virtual Resource Image { get; set; }
    }
}