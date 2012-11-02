using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using Review_Site.Areas.Admin.Models;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class UserController : Controller
    {
        private ReviewSiteEntities db = new ReviewSiteEntities();

        //
        // GET: /Admin/User/

        public ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Admin/User/Details/5

        public ViewResult Details(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return View(user);
        }

        //
        // GET: /Admin/User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/User/Create

        [HttpPost]
        public ActionResult Create(UserForm form)
        {
            if (ModelState.IsValid)
            {

                //We must check for passwords here, since they are not always required in the form.
                if (form.Password == null)
                {
                    ModelState.AddModelError("Password", "Please provide a Password.");
                    return View(form);
                }

                if (db.Users.Where(x => x.Username == form.Username.ToLower()).Count() != 0)
                {
                    ModelState.AddModelError("Username", "Username taken!");
                    return View(form);
                }

                form.ID = Guid.NewGuid();

                if (form.Password.Equals(form.ConfirmedPassword))
                {
                    db.Users.AddObject(formToUser(form));
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("ConfirmedPassword", "Passwords do not match!");
                    return View(form);
                }
                
                return RedirectToAction("Index");  
            }

            return View(form);
        }

        //
        // GET: /Admin/User/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return View(userToForm(user));
        }

        private User formToUser(UserForm form)
        {
            return new User
            {
                ID = form.ID,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Username = form.Username,
                Password = Core.PasswordHashing.GetHash(form.Password)
            };
        }

        private UserForm userToForm(User user)
        {
            return new UserForm
            { 
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username.ToLower() //Just in case...
            };
        }

        //
        // POST: /Admin/User/Edit/5

        [HttpPost]
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
                        return View(form);
                    }
                }

                user.FirstName = form.FirstName;
                user.LastName = form.LastName;
                user.Username = form.Username;

                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            db.Users.DeleteObject(user);
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