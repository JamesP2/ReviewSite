using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using Review_Site.Areas.Admin.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class UserController : Controller
    {
        private SiteContext db = new SiteContext();


        private List<SelectListItem> GetRoleList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Role r in db.Roles)
            {
                list.Add(new SelectListItem
                {
                    Value = r.ID.ToString(),
                    Text = r.Name
                });
            }
            return list;
        }

        //
        // GET: /Admin/User/
        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Index()
        {
            return View(db.Users.OrderBy(x => x.Username).ToList());
        }

        //
        // GET: /Admin/User/Details/5

        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Details(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return View(user);
        }

        //
        // GET: /Admin/User/Create

        [Restrict(Identifier = "Admin.User.Create")]
        public ActionResult Create()
        {
            ViewBag.RoleList = GetRoleList(); 
            return View();
        } 

        //
        // POST: /Admin/User/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.User.Create")]
        public ActionResult Create(UserForm form)
        {
            if (ModelState.IsValid)
            {

                //We must check for passwords here, since they are not always required in the form.
                if (form.Password == null)
                {
                    ModelState.AddModelError("Password", "Please provide a Password.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                if (form.SelectedRoleIds.Count == 0)
                {
                    ModelState.AddModelError("SelectedRoleIds", "Users must have at least one role assigned.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                if (db.Users.Where(x => x.Username == form.Username.ToLower()).Count() != 0)
                {
                    ModelState.AddModelError("Username", "Username taken!");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                form.ID = Guid.NewGuid();

                if (form.Password.Equals(form.ConfirmedPassword))
                {
                    db.Users.Add(formToUser(form));
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("ConfirmedPassword", "Passwords do not match!");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }
                
                return RedirectToAction("Index");  
            }
            ViewBag.RoleList = GetRoleList();
            return View(form);
        }

        //
        // GET: /Admin/User/Edit/5

        [Restrict(Identifier = "Admin.User.Edit")]
        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            ViewBag.RoleList = GetRoleList();
            return View(userToForm(user));
        }

        private User formToUser(UserForm form)
        {
            ICollection<Role> roleList = new List<Role>();
            foreach (Guid g in form.SelectedRoleIds)
            {
                roleList.Add(db.Roles.Single(x => x.ID == g));
            }
            return new User
            {
                ID = form.ID,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Username = form.Username,
                Password = Core.PasswordHashing.GetHash(form.Password),
                Roles = roleList
            };
        }

        private UserForm userToForm(User user)
        {
            ICollection<Guid> roleGuids = new List<Guid>();
            foreach (Role r in user.Roles)
            {
                roleGuids.Add(r.ID);
            }
            return new UserForm
            { 
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username.ToLower(), //Just in case...
                SelectedRoleIds = roleGuids
            };
        }

        //
        // POST: /Admin/User/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.User.Edit")]
        public ActionResult Edit(UserForm form)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Single(x => x.ID == form.ID);

                //Check for new passwords
                if (form.Password != null && form.ConfirmedPassword != null)
                {
                    if (form.ConfirmedPassword.Equals(form.Password))
                    {
                        user.Password = Core.PasswordHashing.GetHash(form.Password);
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmedPassword", "The new Passwords do not match.");
                        ViewBag.RoleList = GetRoleList(); 
                        return View(form);
                    }
                }

                if (form.SelectedRoleIds.Count == 0)
                {
                    ModelState.AddModelError("SelectedRoleIds", "Users must have at least one role assigned.");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                user.FirstName = form.FirstName;
                user.LastName = form.LastName;
                user.Username = form.Username;

                user.Roles.Clear();

                List<Role> userRoles = new List<Role>();
                foreach (Guid g in form.SelectedRoleIds)
                {
                    user.Roles.Add(db.Roles.Single(x => x.ID == g));
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleList = GetRoleList(); 
            return View(form);
        }

        //
        // GET: /Admin/User/Delete/5
        /*
        public ActionResult Delete(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return View(user);
        }

        //
        // POST: /Admin/User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            User user = db.Users.Single(u => u.ID == id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         */

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}