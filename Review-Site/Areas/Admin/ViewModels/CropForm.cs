using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Areas.Admin.Models
{
    public class CropForm
    {
        public Guid ResourceID { get; set; }
        public Guid? SourceTextColorID { get; set; }

        [Required(ErrorMessage="You must provide a new title for the image."), Display(Name="New Image Title")]
        public string Title { get; set; }

        public string Type { get; set; }

        public int OrigWidth { get; set; }
        public int OrigHeight { get; set; }

        public double x { get; set; }
        public double y { get; set; }
        public double x2 { get; set; }
        public double y2 { get; set; }

        public string Source { get; set; }        
    }
}