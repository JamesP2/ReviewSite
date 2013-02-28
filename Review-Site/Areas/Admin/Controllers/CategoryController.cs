using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using Review_Site.Core;
using Review_Site.Data;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private SiteContext db = new SiteContext();

        //
        // GET: /Admin/Category/
        [Restrict(Identifier = "Admin.Category.Index")]
        public ViewResult Index()
        {
            return View(db.Categories.OrderBy(x => x.Title).ToList());
        }

        //
        // GET: /Admin/Category/Create
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create()
        {
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Name");
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
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
                if (db.Categories.Any(x => x.Title == category.Title))
                {
                    ModelState.AddModelError("Title", "A Category with that Title already exists.");
                    ViewBag.Colors = new SelectList(db.Colors, "ID", "Name");
                    ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
                    return View(category);
                }
                category.ID = Guid.NewGuid();
                category.Created = DateTime.Now;
                category.LastModified = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Name");
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
            return View(category);
        }

        //
        // GET: /Admin/Category/Edit/5

        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Name", category.ColorID);
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text", category.GridID);
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
                if (db.Categories.Any(x => x.Title == category.Title && x.ID != category.ID))
                {
                    ModelState.AddModelError("Title", "A Category with that Title already exists.");
                    ViewBag.Colors = new SelectList(db.Colors, "ID", "Name");
                    ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
                    return View(category);
                }
                category.LastModified = DateTime.Now;
                db.Categories.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Name", category.ColorID);
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text", category.GridID);
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

        private IEnumerable<SelectListItem> GetGrids()
        {
            return
                new List<SelectListItem> { new SelectListItem { Text = " -- None --", Value = null } }.Concat(
                    db.Grids.ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}