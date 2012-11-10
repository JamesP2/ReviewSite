using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Controllers
{
    public class AccountController : Controller
    {

        private SiteMembershipProvider membership = new SiteMembershipProvider();

        //
        // GET: /Account/LogIn

        public ActionResult LogIn()
        {
            return View();
        }

        //
        // POST: /Account/LogIn

        [HttpPost]
        public ActionResult LogIn(LogInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (membership.ValidateUser(model.UserName, model.Password))
                {
                    SiteContext db = new SiteContext();
                    User u = db.Users.Single(x => x.Username.ToLower() == model.UserName.ToLower());
                    SiteAuthentication.SetAuthCookie(u, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOut

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
