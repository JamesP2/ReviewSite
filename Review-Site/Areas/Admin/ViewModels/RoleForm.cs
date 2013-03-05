using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Review_Site.Areas.Admin.Models
{
    public class RoleForm
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Please provide a Name for the Role.")]
        public string Name { get; set; }

        [Display(Name = "Permissions of this Role(Hold Control to select multiple Permissions)")]
        public IList<Guid> SelectedPermissionIds { get; set; }
    }
}