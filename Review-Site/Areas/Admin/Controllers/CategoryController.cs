using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ReviewSiteEntities db = new ReviewSiteEntities();

        //
        // GET: /Admin/Category/

        public ViewResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            ViewBag.ColorID= new SelectList(db.Colors, "ID", "Name");
            return View();
        } 

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                db.Categories.AddObject(category);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name");
            return View(category);
        }
        
        //
        // GET: /Admin/Category/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name");
            return View(category);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                db.ObjectStateManager.ChangeObjectState(category, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Name");
            return View(category);
        }

        //
        // GET: /Admin/Category/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            db.Categories.DeleteObject(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}