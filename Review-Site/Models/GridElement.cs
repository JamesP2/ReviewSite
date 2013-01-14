using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class GridElement
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

    public class GridElementConfiguration : EntityTypeConfiguration<GridElement>
    {
        public GridElementConfiguration()
        {
            HasRequired(x => x.BorderColor).WithMany().HasForeignKey(x => x.BorderColorID);
            HasRequired(x => x.Article).WithMany().HasForeignKey(x => x.ArticleID).WillCascadeOnDelete(false);
            HasRequired(x => x.Grid).WithMany(x => x.GridElements).HasForeignKey(x => x.GridID);
            HasRequired(x => x.Image).WithMany().HasForeignKey(x => x.ImageID);
        }
    }
}