using System;
using System.Linq;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> categoryRepository = new Repository<Category>();
        private readonly IRepository<Colour> colourRepository = new Repository<Colour>();
        private readonly IRepository<Grid> gridRepository = new Repository<Grid>();

        //
        // GET: /Admin/Category/
        [Restrict(Identifier="Admin.Category.Index")]
        public ViewResult Index()
        {
            return View(categoryRepository.GetAll().OrderBy(x => x.Title));
        }

        //
        // GET: /Admin/Category/Create
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create()
        {
            ViewBag.ColourID = new SelectList(colourRepository.GetAll(), "ID", "Name");
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name");

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
                categoryRepository.SaveOrUpdate(category);

                return RedirectToAction("Index");  
            }

            ViewBag.ColourID = new SelectList(colourRepository.GetAll(), "ID", "Name");
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name");

            return View(category);
        }
        
        //
        // GET: /Admin/Category/Edit/5

        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Guid id)
        {
            var category = categoryRepository.Get(c => c.ID == id).Single();

            ViewBag.ColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", category.ColourID);
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name", category.GridID);

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
                categoryRepository.SaveOrUpdate(category);

                return RedirectToAction("Index");
            }

            ViewBag.ColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", category.ColourID);
            ViewBag.GridID = new SelectList(gridRepository.GetAll(), "ID", "Name", category.GridID);

            return View(category);
        }

        //
        // GET: /Admin/Category/Delete/5

        [Restrict(Identifier = "Admin.Category.Delete")]
        public ActionResult Delete(Guid id)
        {
            var category = categoryRepository.Get(c => c.ID == id).Single();

            if (!category.IsSystemCategory)
            {
                categoryRepository.Delete(category);
            }
            else ModelState.AddModelError("", "That category belongs to the system. It cannot be deleted.");
            return RedirectToAction("Index");
        }
    }
}