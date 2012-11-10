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
    public class GridController : Controller
    {
        private SiteContext db = new SiteContext();

        //
        // GET: /Admin/Grid/

        public ViewResult Index()
        {
            return View(db.Grids.ToList());
        }

        //
        // GET: /Admin/Grid/Details/5

        public ViewResult Details(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // GET: /Admin/Grid/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Grid/Create

        [HttpPost]
        public ActionResult Create(Grid grid)
        {
            if (ModelState.IsValid)
            {
                grid.ID = Guid.NewGuid();
                db.Grids.Add(grid);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(grid);
        }
        
        //
        // GET: /Admin/Grid/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // POST: /Admin/Grid/Edit/5

        [HttpPost]
        public ActionResult Edit(Grid grid)
        {
            if (ModelState.IsValid)
            {
                db.Grids.Attach(grid);
                db.Entry(grid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grid);
        }

        //
        // GET: /Admin/Grid/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // POST: /Admin/Grid/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Grid grid = db.Grids.Single(g => g.ID == id);
            List<GridElement> elements = grid.GridElements.ToList();
            foreach (GridElement element in elements)
            {
                db.GridElements.Remove(element);
            }
            db.Grids.Remove(grid);
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