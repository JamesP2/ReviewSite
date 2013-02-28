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
        private SiteContext db = new SiteContext();


        //
        // GET: /Admin/Role/

        [Restrict(Identifier = "Admin.Role.Index")]
        public ActionResult Index()
        {
            return View(db.Roles);
        }

        //
        // GET: /Admin/Role/Details/5

        [Restrict(Identifier = "Admin.Role.Index")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Role/Create

        [Restrict(Identifier = "Admin.Role.Create")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Role/Create

        [Restrict(Identifier = "Admin.Role.Create")]
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.ID = Guid.NewGuid();
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = role.ID });
            }
            return View(role);
        }
        
        //
        // GET: /Admin/Role/Edit/5

        [Restrict(Identifier = "Admin.Role.Edit")]
        public ActionResult Edit(Guid id)
        {
            ViewBag.PermissionList = GetPermissionList();
            Role role = db.Roles.Single(x => x.ID == id);
            return View(roleToForm(role));
        }

        //
        // POST: /Admin/Role/Edit/5

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
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetPermissionList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Permission p in db.Permissions)
            {
                list.Add(new SelectListItem
                {
                    Value = p.ID.ToString(),
                    Text = p.Name + "("+p.Identifier+")"
                });
            }
            return list;
        }

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

        ////
        //// GET: /Admin/Role/Delete/5
        
        //[Restrict(Identifier = "Admin.Role.Delete")]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Admin/Role/Delete/5

        //[Restrict(Identifier = "Admin.Role.Delete")]
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
 
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
