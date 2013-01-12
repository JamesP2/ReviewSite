using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Review_Site.Models;

namespace Review_Site.Core
{
    public class SiteIdentity : IIdentity
    {
        public User User { get; private set; }
        public SiteIdentity(User user)
        {
            User = user;
        }
        public string AuthenticationType
        {
            get { return "Site Authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return User != null; }
        }

        public string Name
        {
            get { return User.Username; }
        }
    }
}