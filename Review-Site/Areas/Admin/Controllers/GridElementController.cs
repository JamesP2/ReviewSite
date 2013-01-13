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
    public class GridElementController : Controller
    {
        private SiteContext db = new SiteContext();

        //
        // GET: /Admin/GridElement/

        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ActionResult Index(Guid? id)
        {
            IEnumerable<GridElement> gridelements;
            if (id != null)
            {
                if (!db.Grids.Any(x => x.ID == id)) return HttpNotFound();
                gridelements = db.GridElements.Where(x => x.GridID == id).Include("Article").Include("BorderColor").Include("Grid").Include("Image");
                Grid grid = db.Grids.Single(x => x.ID == id);
                ViewBag.GridName = grid.Name;
            }
            else
            {
                gridelements = db.GridElements.Include("Article").Include("BorderColor").Include("Grid").Include("Image");
            }
            return View(gridelements.OrderBy(x => x.Article.Title).ToList());
        }

        //
        // GET: /Admin/GridElement/Details/5

        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ViewResult Details(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }

        //
        // GET: /Admin/GridElement/Create

        [Restrict(Identifier = "Admin.GridElement.Create")]
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
        [Restrict(Identifier = "Admin.GridElement.Create")]
        public ActionResult Create(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                gridelement.ID = Guid.NewGuid();
                db.GridElements.Add(gridelement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag(gridelement);
            if(gridelement.ImageID != Guid.Empty) gridelement.Image = db.Resources.Single(x => x.ID == gridelement.ImageID);
            return View(gridelement);
        }
        
        //
        // GET: /Admin/GridElement/Edit/5

        [Restrict(Identifier = "Admin.GridElement.Edit")]
        public ActionResult Edit(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag(gridelement);
            return View(gridelement);
        }

        //
        // POST: /Admin/GridElement/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.GridElement.Edit")]
        public ActionResult Edit(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                db.GridElements.Attach(gridelement);
                db.Entry(gridelement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorID = new SelectList(db.Colors, "ID", "Name", gridelement.BorderColorID);
            ViewBag.GridID = new SelectList(db.Grids, "ID", "Name", gridelement.GridID);
            populateFormViewBag(gridelement);
            return View(gridelement);
        }

        private void populateFormViewBag()
        {
            populateFormViewBag(null);
        }

        private void populateFormViewBag(GridElement Model)
        {

            SelectList width = new SelectList(new List<int> { 4, 6, 8, 12 }, ((Model != null) ? Model.Width.ToString() : "4"));
            SelectList sizeClass = new SelectList(new Dictionary<String, String> 
            { 
                {"Tall", "tall"},
                {"Regular", "regular"},
                {"Small", "small"},
            }, "Value", "Key", ((Model != null) ? Model.SizeClass : "tall"));
            SelectList headingClass = new SelectList(new Dictionary<String, String>
            {
                {"Very Top", "anchorVeryTop"},
                {"Top Middle", "anchorTopMiddle"},
                {"Bottom Middle", "anchorBottomMiddle"},
                {"Very Bottom", "anchorVeryBottom"},
            }, "Value", "Key", ((Model != null) ? Model.HeadingClass : "anchorVeryTop"));
            ViewBag.Width = width;
            ViewBag.SizeClass = sizeClass;
            ViewBag.HeadingClass = headingClass;
        }

        //
        // GET: /Admin/GridElement/Delete/5

        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult Delete(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }

        //
        // POST: /Admin/GridElement/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            db.GridElements.Remove(gridelement);
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