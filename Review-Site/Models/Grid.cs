using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Grid
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GridElement> GridElements { get; set; }
    }

    public class GridConfiguration : EntityTypeConfiguration<Grid>
    {
        public GridConfiguration()
        {
            
        }
    }
}