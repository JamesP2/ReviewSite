using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private SiteContext db = new SiteContext();

        //
        // GET: /Admin/Category/
        [Restrict(Identifier="Admin.Category.Index")]
        public ViewResult Index()
        {
            return View(db.Categories.OrderBy(x => x.Title).ToList());
        }

        //
        // GET: /Admin/Category/Create
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create()
        {
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name");
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name");
            return View();
        } 

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                category.Created = DateTime.Now;
                category.LastModified = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name");
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name");
            return View(category);
        }
        
        //
        // GET: /Admin/Category/Edit/5

        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name", category.ColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", category.GridID);
            return View(category);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.LastModified = DateTime.Now;
                db.Categories.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name", category.ColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", category.GridID);
            return View(category);
        }

        //
        // GET: /Admin/Category/Delete/5

        [Restrict(Identifier = "Admin.Category.Delete")]
        public ActionResult Delete(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            if (!category.IsSystemCategory)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            else ModelState.AddModelError("", "That category belongs to the system. It cannot be deleted.");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}