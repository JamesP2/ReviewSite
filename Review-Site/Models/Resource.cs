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

        public virtual string Title { get; set; }
        public virtual string Type { get; set; }

        public virtual DateTime DateAdded { get; set; }

        public virtual User Creator { get; set; }
    }

    public class ResourceConfiguration : EntityTypeConfiguration<Resource>
    {
        public ResourceConfiguration()
        {
            HasRequired(x => x.Creator).WithMany(x => x.Resources).HasForeignKey(x => x.CreatorID);
        }
    }
}