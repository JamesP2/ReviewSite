﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Category
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual Guid ColorID { get; set; }
        public virtual Guid? GridID { get; set; }

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

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasRequired(x => x.Color).WithMany().HasForeignKey(x => x.ColorID);
            HasOptional(x => x.Grid).WithMany().HasForeignKey(x => x.GridID);
        }
    }
}