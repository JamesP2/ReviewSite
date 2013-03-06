﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class GridElement : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }
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