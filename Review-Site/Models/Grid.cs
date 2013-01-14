using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review_Site.Models
{
    public class Grid
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

        public virtual DateTime? Created { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}