using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using Review_Site.Core;
using Review_Site.Areas.Admin.Models;
using Review_Site.Data;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Admin/Article/

        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Index()
        {
            var articles = db.Articles.Get();
            return View(articles.OrderBy(x => x.Title).ToList());
        }

        //
        // GET: /Admin/Article/MiniBrowser

        [Restrict(Identifier = "Admin.Article.Index")]
        public ActionResult MiniBrowser()
        {
            return View(db.Articles.Get().ToList());
        }

        //
        // GET: /Admin/Article/Details/5
        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Details(Guid id)
        {
            Article article = db.Articles.Get(id);
            return View(article);
        }

        //
        // GET: /Admin/Article/Create
        [Restrict(Identifier = "Admin.Article.Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title");
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View();
        }

        //
        // POST: /Admin/Article/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.Article.Create")]
        public ActionResult Create(Article article, string tagList)
        {
            var tagNameList = (tagList ?? "").Split(',').ToList();
            foreach (var s in tagNameList.Where(s => !String.IsNullOrEmpty(s)))
            {
                if (db.Tags.Any(x => x.Name.ToLower() == s.ToLower()))
                {
                    article.Tags.Add(db.Tags.Single(x => x.Name.ToLower() == s.ToLower()));
                }
                else
                {
                    var tag = new Tag
                                  {
                                      ID = Guid.NewGuid(),
                                      Name = s
                                  };
                    db.Tags.SaveOrUpdate(tag);
                    article.Tags.Add(tag);
                }
            }

            if (ModelState.IsValid)
            {
                if (db.Articles.Any(x => x.Title == article.Title))
                {
                    ModelState.AddModelError("Title", "An Article already exists with that title.");
                    ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.CategoryID);
                    ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
                    return View(article);
                }
                article.ID = Guid.NewGuid();
                DateTime currentTime = DateTime.Now;
                article.LastModified = currentTime;
                article.Created = currentTime;
                article.AuthorID = SiteAuthentication.GetUserCookie().ID;
                db.Articles.SaveOrUpdate(article);

                LuceneSearch.AddUpdate(article);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.CategoryID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }

        //
        // GET: /Admin/Article/Edit/5

        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.CategoryID);
            ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.AuthorID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }

        //
        // POST: /Admin/Article/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Article article, string tagList)
        {
            if (ModelState.IsValid)
            {
                if (db.Articles.Any(x => x.Title == article.Title && x.ID != article.ID))
                {
                    ModelState.AddModelError("Title", "Another Article exists with that title.");
                    ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.CategoryID);
                    ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.AuthorID);
                    ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
                    return View(article);
                }
                article.Tags.Clear();
                List<String> tagNameList = tagList.Split(',').ToList();
                foreach (String s in tagNameList)
                {
                    if (String.IsNullOrEmpty(s)) continue;
                    if (db.Tags.Any(x => x.Name.ToLower() == s.ToLower()))
                    {
                        article.Tags.Add(db.Tags.Single(x => x.Name.ToLower() == s.ToLower()));
                    }
                    else
                    {
                        Tag tag = new Tag
                        {
                            ID = Guid.NewGuid(),
                            Name = s
                        };
                        article.Tags.Add(tag);
                    }
                }
                article.LastModified = DateTime.Now;
                db.Articles.SaveOrUpdate(article);
                LuceneSearch.AddUpdate(article);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.CategoryID);
            ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.AuthorID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }

        //
        // GET: /Admin/Article/Delete/5
        [Restrict(Identifier = "Admin.Article.Delete")]
        public ActionResult Delete(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            return View(article);
        }

        //
        // POST: /Admin/Article/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.Article.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            db.Articles.Delete(article);

            LuceneSearch.Remove(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}