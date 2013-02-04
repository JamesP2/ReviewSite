using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Areas.Admin.Models;
using Review_Site.Models;

namespace Review_Site.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private SiteContext db = new SiteContext();


        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Role/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Role/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.ID = new Guid();
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Edit", role.ID);
            }
            return View(role);
        }
        
        //
        // GET: /Admin/Role/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            ViewBag.PermissionList = GetPermissionList();
            Role role = db.Roles.Single(x => x.ID == id);
            return View(roleToForm(role));
        }

        //
        // POST: /Admin/Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, RoleForm collection)
        {
            throw new NotImplementedException();
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

        ////
        //// GET: /Admin/Role/Delete/5
 
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Admin/Role/Delete/5

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
