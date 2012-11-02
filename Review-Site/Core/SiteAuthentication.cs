using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Review_Site.Models;
using System.Web.Security;

namespace Review_Site.Core
{
    public class SiteAuthentication
    {
        public static void SetAuthCookie(User u, bool rememberme)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, u.Username, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), rememberme, u.ID.ToString());

            string encTicket = FormsAuthentication.Encrypt(ticket);

            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        public static User GetUserCookie()
        {
            HttpCookie encCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (encCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encCookie.Value);
                ReviewSiteEntities db = new ReviewSiteEntities();

                User user = db.Users.Single(x => x.ID == new Guid(ticket.UserData));

                db.ObjectStateManager.ChangeObjectState(user, System.Data.EntityState.Detached);
                return user;
            }

            return null;
        }
    }
}