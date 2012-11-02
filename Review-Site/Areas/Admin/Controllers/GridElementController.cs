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
    public class GridElementController : Controller
    {
        private ReviewSiteEntities db = new ReviewSiteEntities();

        //
        // GET: /Admin/GridElement/

        public ActionResult Index(Guid? id)
        {
            IEnumerable<GridElement> gridelements;
            if (id != null)
            {
                if (!db.Grids.Any(x => x.ID == id)) return HttpNotFound();
                gridelements = db.GridElements.Where(x => x.GridID == id).Include("Article").Include("Color").Include("Grid").Include("Resource");
                Grid grid = db.Grids.Single(x => x.ID == id);
                ViewBag.GridName = grid.Name;
            }
            else
            {
                gridelements = db.GridElements.Include("Article").Include("Color").Include("Grid").Include("Resource");
            }
            return View(gridelements.ToList());
        }

        //
        // GET: /Admin/GridElement/Details/5

        public ViewResult Details(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }

        //
        // GET: /Admin/GridElement/Create

        public ActionResult Create()
        {
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name");
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name");
            populateFormViewBag();
            return View();
        } 

        //
        // POST: /Admin/GridElement/Create

        [HttpPost]
        public ActionResult Create(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                gridelement.ID = Guid.NewGuid();
                db.GridElements.AddObject(gridelement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag();
            if(gridelement.ImageID != null) gridelement.Resource = db.Resources.Single(x => x.ID == gridelement.ImageID);
            return View(gridelement);
        }
        
        //
        // GET: /Admin/GridElement/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag();
            return View(gridelement);
        }

        //
        // POST: /Admin/GridElement/Edit/5

        [HttpPost]
        public ActionResult Edit(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                db.GridElements.Attach(gridelement);
                db.ObjectStateManager.ChangeObjectState(gridelement, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag();
            return View(gridelement);
        }

        private void populateFormViewBag()
        {
            
            ViewBag.Width = new SelectList(new List<int> { 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            ViewBag.SizeClass = new SelectList(new Dictionary<String, String> 
            { 
                {"Tall", "tall"},
                {"Regular", "regular"},
                {"Small", "small"},
            }, "Value", "Key");
            ViewBag.HeadingClass = new SelectList(new Dictionary<String, String>
            {
                {"Very Top", "anchorVeryTop"},
                {"Top Middle", "anchorTopMiddle"},
                {"Bottom Middle", "anchorBottomMiddle"},
                {"Very Bottom", "anchorVeryBottom"},
            }, "Value", "Key");
        }

        //
        // GET: /Admin/GridElement/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }

        //
        // POST: /Admin/GridElement/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            db.GridElements.DeleteObject(gridelement);
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