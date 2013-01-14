using System;
using System.Linq;
using System.Web;
using Review_Site.Core.Data;
using Review_Site.Models;
using System.Web.Security;

namespace Review_Site.Core
{
    public class SiteAuthentication
    {
        public static void SetAuthCookie(User u, bool rememberme)
        {
            var ticket = new FormsAuthenticationTicket(1, u.Username, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), rememberme, u.ID.ToString());
            
            var encTicket = FormsAuthentication.Encrypt(ticket);

            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        public static User GetUserCookie()
        {
            var encCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (encCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(encCookie.Value);

                var userRepository = new Repository<User>();

                return userRepository.Get(x => x.ID == new Guid(ticket.UserData)).SingleOrDefault();
            }

            return null;
        }
    }
}