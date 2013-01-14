using System;
using System.Linq;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IRepository<Article> articleRepository = new Repository<Article>();
        private readonly IRepository<Category> categoryRepository = new Repository<Category>();
        private readonly IRepository<User> userRepository = new Repository<User>();

        //
        // GET: /Admin/Article/

        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Index()
        {
            var articles = articleRepository.GetAll();

            return View(articles.OrderBy(x => x.Title).ToList());
        }

        //
        // GET: /Admin/Article/MiniBrowser

        [Restrict(Identifier = "Admin.Article.Index")]
        public ActionResult MiniBrowser()
        {
            return View(articleRepository.GetAll());
        }

        //
        // GET: /Admin/Article/Details/5
        [Restrict(Identifier = "Admin.Article.Index")]
        public ViewResult Details(Guid id)
        {
            var article = articleRepository.Get(a => a.ID == id).Single();

            return View(article);
        }

        //
        // GET: /Admin/Article/Create
        [Restrict(Identifier = "Admin.Article.Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(categoryRepository.GetAll(), "ID", "Title");
            return View();
        }

        //
        // POST: /Admin/Article/Create

        [HttpPost]
        [Restrict(Identifier = "Admin.Article.Create")]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.LastModified = article.Created = DateTime.Now;
                article.UserID = SiteAuthentication.GetUserCookie().ID;

                articleRepository.SaveOrUpdate(article);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(categoryRepository.GetAll(), "ID", "Title", article.CategoryID);
            return View(article);
        }

        //
        // GET: /Admin/Article/Edit/5

        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Guid id)
        {
            var article = articleRepository.Get(a => a.ID == id).Single();

            ViewBag.CategoryID = new SelectList(categoryRepository.GetAll(), "ID", "Title", article.CategoryID);
            ViewBag.UserID = new SelectList(userRepository.GetAll(), "ID", "FullName", article.UserID);

            return View(article);
        }

        //
        // POST: /Admin/Article/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.Article.Edit")]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                article.LastModified = DateTime.Now;

                articleRepository.SaveOrUpdate(article);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(categoryRepository.GetAll(), "ID", "Title", article.CategoryID);
            ViewBag.UserID = new SelectList(userRepository.GetAll(), "ID", "FullName", article.UserID);

            return View(article);
        }

        //
        // GET: /Admin/Article/Delete/5
        [Restrict(Identifier = "Admin.Article.Delete")]
        public ActionResult Delete(Guid id)
        {
            var article = articleRepository.Get(a => a.ID == id).Single();

            return View(article);
        }

        //
        // POST: /Admin/Article/Delete/5

        [HttpPost, ActionName("Delete")]
        [Restrict(Identifier = "Admin.Article.Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var article = articleRepository.Get(a => a.ID == id).Single();

            articleRepository.Delete(article);

            return RedirectToAction("Index");
        }
    }
}