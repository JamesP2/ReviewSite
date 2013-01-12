using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;

namespace Review_Site.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Restrict : ActionFilterAttribute
    {
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        public string Identifier { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = SiteAuthentication.GetUserCookie();
            if (user == null) throw new HttpException(401, "You do not have permission to access that area.");
            if (Role != null)
            {
                //try role first.
                if (user.Roles.Contains(Role)) return;
            }
            if (Permission != null)
            {
                //try permission next.
                if (PermissionManager.HasPermission(user, Permission.Identifier)) return;
            }
            if (!string.IsNullOrEmpty(Identifier))
            {
                //finally, a permission identifier
                if (PermissionManager.HasPermission(user, Identifier)) return;
            }

            //If we got that far our user does not have permission to do this.
            throw new HttpException(401, "You do not have permission to access that area.");
        }
    }
}