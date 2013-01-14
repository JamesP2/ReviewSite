using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Review_Site.Models;
using System.Web.Mvc;

namespace Review_Site.Areas.Admin.Models
{
    public class UserForm
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage="Please provide a Username.")]
        public string Username { get; set; }

        [Required(ErrorMessage="Please provide a First Name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide a Last Name.")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [Display(Name = "User Roles (Hold Control to select multiple Roles)")]
        [Required(ErrorMessage="Please assign at least one role.")]
        public ICollection<Guid> SelectedRoleIds { get; set; }
    }
}
