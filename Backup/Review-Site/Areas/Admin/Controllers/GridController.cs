using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class GridController : Controller
    {
        private SiteContext db = new SiteContext();

        //
        // GET: /Admin/Grid/
        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Index()
        {
            return View(db.Grids.OrderBy(x => x.Name).ToList());
        }

        //
        // GET: /Admin/Grid/Details/5

        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Details(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // GET: /Admin/Grid/Create
        [Restrict(Identifier = "Admin.Grid.Create")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Grid/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.Grid.Index")]
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

        [Restrict(Identifier = "Admin.Grid.Edit")]
        public ActionResult Edit(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // POST: /Admin/Grid/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.Grid.Edit")]
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

        [Restrict(Identifier = "Admin.Grid.Delete")]
        public ActionResult Delete(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        //
        // POST: /Admin/Grid/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.Grid.Delete")]
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