using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> userRepository = new Repository<User>();
        private readonly SiteMembershipProvider membership = new SiteMembershipProvider();

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
                    var u = userRepository.Get(x => x.Username.ToLower() == model.UserName.ToLower()).Single();

                    SiteAuthentication.SetAuthCookie(u, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
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
