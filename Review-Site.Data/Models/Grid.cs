using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class Grid : IEntity
    {
        [Key]
        public virtual Guid ID { get; set; }

        [Required(ErrorMessage="You must provide a name.")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "You must provide an alias.")]
        [RegularExpression(@"(\S)+", ErrorMessage = "Spaces cannot be used in an alias.")]
        public virtual string Alias { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<GridElement> GridElements { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        public Grid()
        {
            GridElements = new List<GridElement>();
        }
    }
}