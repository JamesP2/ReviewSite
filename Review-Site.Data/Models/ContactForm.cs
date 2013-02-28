using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Review_Site.Data;

namespace Review_Site.Data.Models
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Email Address.")]
        [ValidEmail]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Please enter a message to send to us!")]
        public virtual string Message { get; set; }

        public virtual string ClientAddress { get; set; }
    }
}