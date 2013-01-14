using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    public class GridElementController : Controller
    {
        private readonly IRepository<Colour> colourRepository = new Repository<Colour>();
        private readonly IRepository<Grid> gridRepository = new Repository<Grid>();
        private readonly IRepository<GridElement> gridElementRepository = new Repository<GridElement>();
        private readonly IRepository<Resource> resourceRepository = new Repository<Resource>();

        //
        // GET: /Admin/GridElement/
        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ActionResult Index(Guid? id)
        {
            IEnumerable<GridElement> gridelements = gridElementRepository.GetAll();
            if (id != null)
            {
                if (!gridRepository.GetAll().Any(x => x.ID == id)) return HttpNotFound();

                gridelements = gridelements.Where(x => x.GridID == id);

                var grid = gridRepository.Get(x => x.ID == id).Single();
                ViewBag.GridName = grid.Name;
            }

            return View(gridelements.OrderBy(x => x.Article.Title).ToList());
        }

        //
        // GET: /Admin/GridElement/Details/5
        [Restrict(Identifier = "Admin.GridElement.Index")]
        public ViewResult Details(Guid id)
        {
            var gridelement = gridElementRepository.Get(g => g.ID == id).Single();

            return View(gridelement);
        }

        //
        // GET: /Admin/GridElement/Create
        [Restrict(Identifier = "Admin.GridElement.Create")]
        public ActionResult Create()
        {
            ViewBag.BorderColourID = new SelectList(colourRepository.GetAll(), "ID", "Name");
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name");

            PopulateFormViewBag();

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
                gridElementRepository.SaveOrUpdate(gridelement);

                return RedirectToAction("Index");
            }

            ViewBag.BorderColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", gridelement.BorderColourID);
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name", gridelement.GridID);

            PopulateFormViewBag(gridelement);

            if (gridelement.ImageID != Guid.Empty) gridelement.Image = resourceRepository.Get(x => x.ID == gridelement.ImageID).Single();

            return View(gridelement);
        }

        //
        // GET: /Admin/GridElement/Edit/5
        [Restrict(Identifier = "Admin.GridElement.Edit")]
        public ActionResult Edit(Guid id)
        {
            var gridelement = gridElementRepository.Get(g => g.ID == id).Single();

            ViewBag.BorderColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", gridelement.BorderColourID);
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name", gridelement.GridID);

            PopulateFormViewBag(gridelement);

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
                gridElementRepository.SaveOrUpdate(gridelement);

                return RedirectToAction("Index");
            }

            ViewBag.BorderColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", gridelement.BorderColourID);
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name", gridelement.GridID);

            PopulateFormViewBag(gridelement);

            return View(gridelement);
        }

        private void PopulateFormViewBag(GridElement model = null)
        {
            var width = new SelectList(new List<int> { 4, 6, 8, 12 }, ((model != null) ? model.Width.ToString() : "4"));

            var sizeClass = new SelectList(new Dictionary<String, String> 
            { 
                {"Tall", "tall"},
                {"Regular", "regular"},
                {"Small", "small"},
            }, "Value", "Key", ((model != null) ? model.SizeClass : "tall"));

            var headingClass = new SelectList(new Dictionary<String, String>
            {
                {"Very Top", "anchorVeryTop"},
                {"Top Middle", "anchorTopMiddle"},
                {"Bottom Middle", "anchorBottomMiddle"},
                {"Very Bottom", "anchorVeryBottom"},
            }, "Value", "Key", ((model != null) ? model.HeadingClass : "anchorVeryTop"));

            ViewBag.Width = width;
            ViewBag.SizeClass = sizeClass;
            ViewBag.HeadingClass = headingClass;
        }

        //
        // GET: /Admin/GridElement/Delete/5

        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult Delete(Guid id)
        {
            var gridelement = gridElementRepository.Get(g => g.ID == id).Single();

            return View(gridelement);
        }

        //
        // POST: /Admin/GridElement/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.GridElement.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var gridelement = gridElementRepository.Get(g => g.ID == id).Single();

            gridElementRepository.Delete(gridelement);

            return RedirectToAction("Index");
        }
    }
}