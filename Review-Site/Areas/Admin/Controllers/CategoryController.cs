﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using Review_Site.Core;
using Review_Site.Data;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private DataContext db = new DataContext();

        #region Browse
        [Restrict(Identifier = "Admin.Category.Index")]
        public ViewResult Index()
        {
            return View(db.Categories.Get().OrderBy(x => x.Title).ToList());
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create()
        {
            ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name");
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Category.Create")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (db.Categories.Any(x => x.Title == category.Title))
                {
                    ModelState.AddModelError("Title", "A Category with that Title already exists.");
                    ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name");
                    ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
                    return View(category);
                }
                category.ID = Guid.NewGuid();
                category.Created = DateTime.Now;
                category.LastModified = DateTime.Now;
                db.Categories.AddOrUpdate(category);
                return RedirectToAction("Index");
            }
            ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name");
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
            return View(category);
        }
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Guid id)
        {
            Category category = db.Categories.Single(c => c.ID == id);
            ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name", category.Color.ID);
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text", category.Grid == null ? (Guid?)null : category.Grid.ID);
            return View(category);
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Category.Edit")]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (db.Categories.Any(x => x.Title == category.Title && x.ID != category.ID))
                {
                    ModelState.AddModelError("Title", "A Category with that Title already exists.");
                    ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name");
                    ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text");
                    return View(category);
                }
                category.LastModified = DateTime.Now;
                db.Categories.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.Colors = new SelectList(db.Colors.Get(), "ID", "Name", category.Color.ID);
            ViewBag.Grids = new SelectList(GetGrids(), "Value", "Text", category.Grid == null ? (Guid?)null : category.Grid.ID);
            return View(category);
        }
        #endregion

        #region Delete
        [Restrict(Identifier = "Admin.Category.Delete")]
        public ActionResult Delete(Guid id)
        {
            Category category = db.Categories.Get(id);
            if (!category.IsSystemCategory)
            {
                db.Categories.Delete(category);
            }
            else ModelState.AddModelError("", "That category belongs to the system. It cannot be deleted.");
            return RedirectToAction("Index");
        }
        #endregion

        private IEnumerable<SelectListItem> GetGrids()
        {
            return
                new List<SelectListItem> { new SelectListItem { Text = " -- None --", Value = null } }.Concat(
                    db.Grids.Get().ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}