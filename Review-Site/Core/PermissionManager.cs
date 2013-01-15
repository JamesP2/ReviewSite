using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Review_Site.Models;

namespace Review_Site.Core
{
    public static class PermissionManager
    {
        public static bool HasPermission(User user, string identifier)
        {
            foreach (Role role in user.Roles)
            {
                if (HasPermission(role, identifier)) return true;
            }
            return false;
        }

        public static bool HasPermission(Role role, string identifier)
        {
            foreach (Permission permission in role.Permissions)
            {
                if (permission.Identifier.ToLower().Equals(identifier.ToLower()))
                    return true;
            }
            return false;
        }

        public static bool HasPermission(Role role, Permission permission)
        {
            return HasPermission(role, permission.Identifier);
        }

        public static bool HasPermission(User user, Permission permission)
        {
            return HasPermission(user, permission.Identifier);
        }
    }
}