using System;
using System.Collections.Generic;
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

        #region Browsers and Details

        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Index()
        {
            var articles = db.Articles.Get();
            return View(articles.OrderBy(x => x.Title).ToList());
        }

        [Restrict(Identifier = "Admin.Article.Index")]
        public ActionResult MiniBrowser()
        {
            return View(db.Articles.Get().ToList());
        }

        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Details(Guid id)
        {
            Article article = db.Articles.Get(id);
            return View(article);
        }
        #endregion

        #region Create
        [Restrict(Identifier = "Admin.Article.Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title");
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View();
        }

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
                    db.Tags.AddOrUpdate(tag);
                    article.Tags.Add(tag);
                }
            }

            if (ModelState.IsValid)
            {
                if (db.Articles.Any(x => x.Title == article.Title))
                {
                    ModelState.AddModelError("Title", "An Article already exists with that title.");
                    ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.Category == null ? (Guid?)null : article.Category.ID);
                    ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
                    return View(article);
                }
                article.ID = Guid.NewGuid();
                DateTime currentTime = DateTime.Now;
                article.LastModified = currentTime;
                article.Created = currentTime;
                article.Author = db.Users.Get(SiteAuthentication.GetUserCookie().ID);
                db.Articles.Add(article);

                LuceneSearch.AddUpdate(article);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.Category == null ? (Guid?)null : article.Category.ID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }
        #endregion

        #region Edit
        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Guid id)
        {
            Article article = db.Articles.Single(a => a.ID == id);
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.Category == null ? (Guid?)null : article.Category.ID);
            ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.Author.ID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Article article, string tagList)
        {
            if (ModelState.IsValid)
            {
                if (db.Articles.Any(x => x.Title == article.Title && x.ID != article.ID))
                {
                    ModelState.AddModelError("Title", "Another Article exists with that title.");
                    ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.Category == null ? (Guid?)null : article.Category.ID);
                    ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.Author.ID);
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
                db.Articles.Update(article);
                LuceneSearch.AddUpdate(article);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Get(), "ID", "Title", article.Category == null ? (Guid?)null : article.Category.ID);
            ViewBag.AuthorID = new SelectList(db.Users.Get(), "ID", "FullName", article.Author.ID);
            ViewBag.TagList = db.Tags.Get().Select(x => x.Name).ToList();
            return View(article);
        }
        #endregion

        #region Delete
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
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}