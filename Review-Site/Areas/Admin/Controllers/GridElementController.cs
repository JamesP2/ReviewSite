using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using Review_Site.Core;
using Review_Site.Data;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class GridElementController : Controller
    {
        private DataContext db = new DataContext();

        #region Browse and Details
        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ActionResult Index(Guid? id)
        {
            IEnumerable<GridElement> gridelements;
            if (id != null)
            {
                if (!db.Grids.Any(x => x.ID == id)) return HttpNotFound();
                gridelements = db.GridElements.Get().Where(x => x.Grid.ID == id);
                Grid grid = db.Grids.Get(id.Value);
                ViewBag.GridName = grid.Name;
            }
            else
            {
                gridelements = db.GridElements.Get();
            }
            return View(gridelements.OrderBy(x => x.Article.Title).ToList());
        }

        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ViewResult Details(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.GridElement.Create")]
        public ActionResult Create()
        {
            ViewBag.BorderColorIDs = new SelectList(db.Colors.Get(), "ID", "Name");
            ViewBag.GridIDs = new SelectList(db.Grids.Get(), "ID", "Name");
            populateFormViewBag();
            return View();
        } 

        [HttpPost]
        [Restrict(Identifier = "Admin.GridElement.Create")]
        public ActionResult Create(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                gridelement.ID = Guid.NewGuid();
                gridelement.LastModified = DateTime.Now;
                gridelement.Created = DateTime.Now;
                db.GridElements.AddOrUpdate(gridelement);
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorIDs = new SelectList(db.Colors.Get(), "ID", "Name", gridelement.BorderColor.ID);
            ViewBag.GridIDs = new SelectList(db.Grids.Get(), "ID", "Name", gridelement.Grid.ID);
            populateFormViewBag(gridelement);
            if(gridelement.Image != null) gridelement.Image = db.Resources.Single(x => x.ID == gridelement.Image.ID);
            return View(gridelement);
        }
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.GridElement.Edit")]
        public ActionResult Edit(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            ViewBag.BorderColorIDs = new SelectList(db.Colors.Get(), "ID", "Name", gridelement.BorderColor.ID);
            ViewBag.GridIDs = new SelectList(db.Grids.Get(), "ID", "Name", gridelement.Grid.ID);
            populateFormViewBag(gridelement);
            return View(gridelement);
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.GridElement.Edit")]
        public ActionResult Edit(GridElement gridelement)
        {
            if (ModelState.IsValid)
            {
                gridelement.LastModified = DateTime.Now;
                db.GridElements.AddOrUpdate(gridelement);
                return RedirectToAction("Index");
            }
            ViewBag.BorderColorIDs = new SelectList(db.Colors.Get(), "ID", "Name", gridelement.BorderColor.ID);
            ViewBag.GridIDs = new SelectList(db.Grids.Get(), "ID", "Name", gridelement.Grid.ID);
            populateFormViewBag(gridelement);
            return View(gridelement);
        }
        #endregion

        #region Delete
        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult Delete(Guid id)
        {
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            return View(gridelement);
        }

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            GridElement gridelement = db.GridElements.Single(g => g.ID == id);
            db.GridElements.Delete(gridelement);
            return RedirectToAction("Index");
        }
        #endregion

        #region ViewBag
        private void populateFormViewBag()
        {
            populateFormViewBag(null);
        }

        private void populateFormViewBag(GridElement Model)
        {

            SelectList widths = new SelectList(new List<int> { 4, 6, 8, 12 }, ((Model != null) ? Model.Width.ToString() : "4"));
            SelectList sizeClasses = new SelectList(new Dictionary<String, String> 
            { 
                {"Tall", "tall"},
                {"Regular", "regular"},
                {"Small", "small"},
            }, "Value", "Key", ((Model != null) ? Model.SizeClass : "tall"));
            SelectList headingClasses = new SelectList(new Dictionary<String, String>
            {
                {"Very Top", "anchorVeryTop"},
                {"Top Middle", "anchorTopMiddle"},
                {"Bottom Middle", "anchorBottomMiddle"},
                {"Very Bottom", "anchorVeryBottom"},
            }, "Value", "Key", ((Model != null) ? Model.HeadingClass : "anchorVeryTop"));
            ViewBag.Widths = widths;
            ViewBag.SizeClasses = sizeClasses;
            ViewBag.HeadingClasses = headingClasses;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}