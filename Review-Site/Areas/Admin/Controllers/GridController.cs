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
    public class GridController : Controller
    {
        private DataContext db = new DataContext();

        #region Browse and Details
        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Index()
        {
            return View(db.Grids.Get().OrderBy(x => x.Name).ToList());
        }

        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Details(Guid id)
        {
            Grid grid = db.Grids.Get(id);
            return View(grid);
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.Grid.Create")]
        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [Restrict(Identifier = "Admin.Grid.Index")]
        public ActionResult Create(Grid grid)
        {
            if (ModelState.IsValid)
            {
                if (db.Grids.Any(x => x.Name == grid.Name))
                {
                    ModelState.AddModelError("Name", "A Grid with that Name already exists.");
                    return View(grid);
                }
                grid.Created = DateTime.Now;
                grid.LastModified = DateTime.Now;
                grid.ID = Guid.NewGuid();
                db.Grids.AddOrUpdate(grid);
                return RedirectToAction("Index");  
            }

            return View(grid);
        }
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.Grid.Edit")]
        public ActionResult Edit(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Grid.Edit")]
        public ActionResult Edit(Grid grid)
        {
            if (ModelState.IsValid)
            {
                if (db.Grids.Any(x => x.Name == grid.Name && x.ID != grid.ID))
                {
                    ModelState.AddModelError("Name", "A Grid with that Name already exists.");
                    return View(grid);
                }
                grid.LastModified = DateTime.Now;
                db.Grids.AddOrUpdate(grid);
                return RedirectToAction("Index");
            }
            return View(grid);
        }
        #endregion

        #region Delete
        [Restrict(Identifier = "Admin.Grid.Delete")]
        public ActionResult Delete(Guid id)
        {
            Grid grid = db.Grids.Single(g => g.ID == id);
            return View(grid);
        }

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.Grid.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Grid grid = db.Grids.Single(g => g.ID == id);
            List<GridElement> elements = grid.GridElements.ToList();
            foreach (GridElement element in elements)
            {
                db.GridElements.Delete(element);
            }
            db.Grids.Delete(grid);
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}