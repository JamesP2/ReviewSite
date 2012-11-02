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
    [Authorize]
    public class ArticleController : Controller
    {
        private ReviewSiteEntities db = new ReviewSiteEntities();

        //
        // GET: /Admin/Article/

        public ViewResult Index()
        {
            var articles = db.Articles.Include("Category").Include("User");
            return View(articles.ToList());
        }

        //
        // GET: /Admin/Article/MiniBrowser

        public ActionResult MiniBrowser()
        {
            return View(db.Articles.ToList());
        }

        //
        // GET: /Admin/Article/Details/5

        public ViewResult Details(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            return View(article);
        }

        //
        // GET: /Admin/Article/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Title");
            return View();
        } 

        //
        // POST: /Admin/Article/Create

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.ID = Guid.NewGuid();
                DateTime currentTime = DateTime.Now;
                article.LastModified = currentTime;
                article.Created = currentTime;
                article.AuthorID = SiteAuthentication.GetUserCookie().ID;
                db.Articles.AddObject(article);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Title", article.CategoryID);
            return View(article);
        }
        
        //
        // GET: /Admin/Article/Edit/5

        public ActionResult Edit(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Title", article.CategoryID);
            return View(article);
        }

        //
        // POST: /Admin/Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                article.LastModified = DateTime.Now;
                db.Articles.Attach(article);
                db.ObjectStateManager.ChangeObjectState(article, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Title", article.CategoryID);
            return View(article);
        }

        //
        // GET: /Admin/Article/Delete/5

        public ActionResult Delete(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            return View(article);
        }

        //
        // POST: /Admin/Article/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Article article = db.Articles.Single(a => a.ID == id);
            db.Articles.DeleteObject(article);
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