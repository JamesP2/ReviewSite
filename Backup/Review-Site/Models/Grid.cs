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

        [Required(ErrorMessage="You must provide a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide an alias.")]
        [RegularExpression(@"(\S)+", ErrorMessage = "Spaces cannot be used in an alias.")]
        public string Alias { get; set; }

        public string Description { get; set; }

        public virtual IList<GridElement> GridElements { get; set; }
    }

    public class GridConfiguration : EntityTypeConfiguration<Grid>
    {
        public GridConfiguration()
        {
            
        }
    }
}