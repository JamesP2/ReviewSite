using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using Review_Site.Data.Models;

namespace Review_Site.Core
{
    public class SitePrincipal : IPrincipal
    {
        public SitePrincipal(IIdentity ident)
        {
            Identity = ident;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            SiteIdentity identity = (SiteIdentity)Identity;
            User user = identity.User;

            return identity.IsAuthenticated; //no roles yet, so everything is permitted.
        }
    }
}