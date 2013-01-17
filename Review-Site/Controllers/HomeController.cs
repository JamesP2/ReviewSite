using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using System.IO;
using System.Web.Helpers;
using Review_Site.Core;

namespace Review_Site.Controllers
{
    public class HomeController : Controller
    {
        private SiteContext db = new SiteContext();
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("LandingPage");

            Category category = db.Categories.Single(x => x.ID == new Guid("a323a95c-b475-4886-9f8d-006c2cc84c64"));
            ViewBag.NoHeader = false;
            return View("GetCategory", new CategoryViewModel
            {
                Category = category,
                Articles = category.Articles.OrderByDescending(x => x.Created).Take(5),
            });
        }
        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult Search(string query, int? page = 1)
        {
            var results = LuceneSearch.SearchDefault(query).Select(x => x.ID).ToList();

            var totalPages = (int)Math.Ceiling(results.Count() / 5d);
            if (!page.HasValue || page < 0 || page > totalPages) page = 1;

            results = results.Skip((page.Value - 1) * 5).Take(5).ToList();

            var articles = db.Articles.Where(x => results.Any(y => y == x.ID)).ToList().OrderBy(x => results.IndexOf(x.ID));

            return View(new SearchViewModel
            {
                Query = query,
                Articles = articles,
                Page = page.Value,
                PageCount = totalPages
            });
        }

        public ActionResult GetResource(Guid id)
        {
            if (!db.Resources.Any(r => r.ID == id)) throw new HttpException(404, "That resource cannot be found.");
            //get the resource.
            Resource res = db.Resources.Single(r => r.ID == id);
            

            string path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            byte[] streamBytes = new byte[stream.Length];
            stream.Read(streamBytes, 0, (int)stream.Length);

            stream.Close();

            if (res.Type.StartsWith("image"))
            {
                WebImage image = new WebImage(streamBytes);
                if (!string.IsNullOrWhiteSpace(res.Source)) image.AddTextWatermark(res.Source, "#" + ((res.SourceTextColor == null) ? "FFFFFF" : res.SourceTextColor.Value));
                streamBytes = new byte[image.GetBytes().Length];
                streamBytes = image.GetBytes();
            }

            return File(streamBytes, res.Type);
        }

        public ActionResult GetArticle(Guid id)
        {
            //get the article and present it!
            if (!db.Articles.Any(x => x.ID == id)) throw new HttpException(404, "That article does not exist");
            Article article = db.Articles.Single(x => x.ID == id);
            PageHits.RegisterHit(article.ID);
            return View(article);
        }

        public ActionResult GetCategory(string id, int? page)
        {
            Guid categoryguid;
            Category category;
            if (Guid.TryParse(id, out categoryguid))
            {
                if (!db.Categories.Any(x => x.ID == categoryguid)) return HttpNotFound("That category does not exist");
                category = db.Categories.SingleOrDefault(x => x.ID == categoryguid);
            }
            else
            {
                //Try and match to category name instead.
                string name = id.Replace('-', ' ').ToLower();
                if (!db.Categories.Any(x => x.Title.ToLower() == name)) return HttpNotFound("That category does not exist");
                category = db.Categories.SingleOrDefault(x => x.Title.ToLower() == name);
            }

            int totalPages = (int)Math.Ceiling(category.Articles.Count() / 5d);
            if (!page.HasValue || page < 0 || page > totalPages) page = 1;

            return View(new CategoryViewModel
                            {
                                Category = category,
                                Articles = category.Articles.OrderByDescending(x => x.Created).Skip((page.Value - 1) * 5).Take(5),
                                Page = page.Value,
                                PageCount = totalPages
                            });
        }

        public ActionResult GetTag(string id, int? page)
        {
            Guid tagguid;
            Tag tag;
            if (Guid.TryParse(id, out tagguid))
            {
                if (!db.Tags.Any(x => x.ID == tagguid)) return HttpNotFound("That category does not exist");
                tag = db.Tags.SingleOrDefault(x => x.ID == tagguid);
            }
            else
            {
                //Try and match to category name instead.
                string name = id.Replace('-', ' ').ToLower();
                if (!db.Tags.Any(x => x.Name.ToLower() == name)) return HttpNotFound("That category does not exist");
                tag = db.Tags.SingleOrDefault(x => x.Name.ToLower() == name);
            }

            var totalPages = (int)Math.Ceiling(tag.Articles.Count() / 5d);
            if (!page.HasValue || page < 0 || page > totalPages) page = 1;

            return View(new TagViewModel
            {
                Tag = tag,
                Articles = tag.Articles.OrderByDescending(x => x.Created).Skip((page.Value - 1) * 5).Take(5),
                Page = page.Value,
                PageCount = totalPages
            });
        }

        public ActionResult GetGrid(string id)
        {
            Guid gridguid;
            Grid grid;
            //Is it a GUID?
            if (Guid.TryParse(id, out gridguid))
            {
                if (!db.Grids.Any(x => x.ID == gridguid)) throw new HttpException(404, "That grid does not exist");
                grid = db.Grids.Single(x => x.ID == gridguid);
                return View(grid);
            }
            //No? try the ID instead.
            string alias = id.ToLower();
            if (!db.Grids.Any(x => x.Alias.ToLower() == alias)) throw new HttpException(404, "That grid does not exist");
            grid = db.Grids.Single(x => x.Alias.ToLower() == alias);
            return View(grid);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error(Exception e)
        {
            if (e.GetType() == typeof(HttpException))
            {
                return View("HttpError", e);
            }
            return Content(e.ToString());
        }
    }
}
