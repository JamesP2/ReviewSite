using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Resource
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid CreatorID { get; set; }
        [Display(Name = "Source Text Colour")]
        public virtual Guid? SourceTextColorID { get; set; }

        [Required(ErrorMessage="You must name your Resource.")]
        public virtual string Title { get; set; }
        public virtual string Type { get; set; }

        [Display(Name="Source Text")]
        public virtual string Source { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime? LastModified { get; set; }

        public virtual User Creator { get; set; }
        [Display(Name="Source Text Colour")]
        public virtual Color SourceTextColor { get; set; }
    }

    public class ResourceConfiguration : EntityTypeConfiguration<Resource>
    {
        public ResourceConfiguration()
        {
            HasRequired(x => x.Creator).WithMany(x => x.Resources).HasForeignKey(x => x.CreatorID);
            HasOptional(x => x.SourceTextColor).WithMany().HasForeignKey(x => x.SourceTextColorID);
        }
    }
}