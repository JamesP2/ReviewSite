using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Areas.Admin.Models;
using Review_Site.Core;
using Review_Site.Data.Models;
using Review_Site.Data;

namespace Review_Site.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private DataContext db = new DataContext();

        #region Browse and Details
        [Restrict(Identifier = "Admin.Role.Index")]
        public ActionResult Index()
        {
            return View(db.Roles.Get());
        }

        [Restrict(Identifier = "Admin.Role.Index")]
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.Role.Create")]
        public ActionResult Create()
        {
            return View();
        } 

        [Restrict(Identifier = "Admin.Role.Create")]
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.ID = Guid.NewGuid();
                db.Roles.AddOrUpdate(role);
                return RedirectToAction("Edit", new { id = role.ID });
            }
            return View(role);
        }
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.Role.Edit")]
        public ActionResult Edit(Guid id)
        {
            ViewBag.PermissionList = GetPermissionList();
            Role role = db.Roles.Get(id);
            return View(roleToForm(role));
        }

        [Restrict(Identifier = "Admin.Role.Edit")]
        [HttpPost]
        public ActionResult Edit(RoleForm form)
        {
            if(!ModelState.IsValid) return View(form);
            Role role = db.Roles.Single(x => x.ID == form.ID);
            role.Permissions.Clear();
            foreach (Guid permissionId in form.SelectedPermissionIds)
            {
                role.Permissions.Add(db.Permissions.Single(x => x.ID == permissionId));
            }
            role.Name = form.Name;
            db.Roles.AddOrUpdate(role);
            return RedirectToAction("Index");
        }
        #endregion

        #region Utility
        private List<SelectListItem> GetPermissionList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Permission p in db.Permissions.Get())
            {
                list.Add(new SelectListItem
                {
                    Value = p.ID.ToString(),
                    Text = p.Name + "("+p.Identifier+")"
                });
            }
            return list;
        }
        #endregion

        private RoleForm roleToForm(Role role)
        {
            IList<Guid> permissionGuids = new List<Guid>();
            foreach (Permission p in role.Permissions)
            {
                permissionGuids.Add(p.ID);
            }
            return new RoleForm()
            {
                ID = role.ID,
                SelectedPermissionIds = permissionGuids,
                Name = role.Name
            };
        }

        private Role formToRole(RoleForm form)
        {
            IList<Permission> rolePermissions = new List<Permission>();
            foreach (Guid guid in form.SelectedPermissionIds)
            {
                rolePermissions.Add(db.Permissions.Single(x => x.ID == guid));
            }
            return new Role()
            {
                ID = form.ID,
                Permissions = rolePermissions,
                Name = form.Name
            };
        }
    }
}
