using System;
using System.Linq;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{ 
    public class GridController : Controller
    {
        private readonly IRepository<Grid> gridRepository = new Repository<Grid>();
        private readonly IRepository<GridElement> gridElementRepository = new Repository<GridElement>();

        //
        // GET: /Admin/Grid/
        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Index()
        {
            return View(gridRepository.GetAll().OrderBy(x => x.Name));
        }

        //
        // GET: /Admin/Grid/Details/5

        [Restrict(Identifier = "Admin.Grid.Index")]
        public ViewResult Details(Guid id)
        {
            var grid = gridRepository.Get(g => g.ID == id).Single();

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
                gridRepository.SaveOrUpdate(grid);

                return RedirectToAction("Index");  
            }

            return View(grid);
        }
        
        //
        // GET: /Admin/Grid/Edit/5

        [Restrict(Identifier = "Admin.Grid.Edit")]
        public ActionResult Edit(Guid id)
        {
            var grid = gridRepository.Get(g => g.ID == id).Single();

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
                gridRepository.SaveOrUpdate(grid);

                return RedirectToAction("Index");
            }

            return View(grid);
        }

        //
        // GET: /Admin/Grid/Delete/5

        [Restrict(Identifier = "Admin.Grid.Delete")]
        public ActionResult Delete(Guid id)
        {
            var grid = gridRepository.Get(g => g.ID == id).Single();

            return View(grid);
        }

        //
        // POST: /Admin/Grid/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.Grid.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var grid = gridRepository.Get(g => g.ID == id).Single();
            var elements = grid.GridElements;

            foreach (var element in elements)
            {
                gridElementRepository.Delete(element);
            }
            gridRepository.Delete(grid);

            return RedirectToAction("Index");
        }
    }
}