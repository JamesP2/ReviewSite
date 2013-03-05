﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using Review_Site.Areas.Admin.Models;
using Review_Site.Core;
using Review_Site.Data;
using Review_Site.Data.Utility;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class UserController : Controller
    {
        private DataContext db = new DataContext();

        #region Browse and Details
        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Index()
        {
            return View(db.Users.Get().OrderBy(x => x.Username).ToList());
        }

        [Restrict(Identifier = "Admin.User.Index")]
        public ViewResult Details(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            return View(user);
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.User.Create")]
        public ActionResult Create()
        {
            ViewBag.RoleList = GetRoleList(); 
            return View();
        } 

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

                if (db.Users.Get().Where(x => x.Username == form.Username.ToLower()).Count() != 0)
                {
                    ModelState.AddModelError("Username", "Username taken!");
                    ViewBag.RoleList = GetRoleList();
                    return View(form);
                }

                form.ID = Guid.NewGuid();

                if (form.Password.Equals(form.ConfirmedPassword))
                {
                    User user = formToUser(form);
                    user.Created = DateTime.Now;
                    user.LastModified = DateTime.Now;
                    db.Users.AddOrUpdate(user);
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
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.User.Edit")]
        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Single(u => u.ID == id);
            ViewBag.RoleList = GetRoleList();
            return View(userToForm(user));
        }

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
                        user.Password = PasswordHashing.GetHash(form.Password);
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

                user.LastModified = DateTime.Now;
                db.Users.AddOrUpdate(user);
                return RedirectToAction("Index");
            }
            ViewBag.RoleList = GetRoleList(); 
            return View(form);
        }

        #endregion

        private List<SelectListItem> GetRoleList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Role r in db.Roles.Get())
            {
                list.Add(new SelectListItem
                {
                    Value = r.ID.ToString(),
                    Text = r.Name
                });
            }
            return list;
        }

        private User formToUser(UserForm form)
        {
            IList<Role> roleList = new List<Role>();
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
                Password = Data.Utility.PasswordHashing.GetHash(form.Password),
                Roles = roleList,
                Created = form.Created,
                LastModified = form.LastModified
            };
        }

        private UserForm userToForm(User user)
        {
            IList<Guid> roleGuids = new List<Guid>();
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
                SelectedRoleIds = roleGuids,
                Created = user.Created,
                LastModified = user.LastModified
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(true);
        }
    }
}