using System;
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
        private readonly SiteContext db = new SiteContext();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("LandingPage");

            ViewBag.NoHeader = false;

            var category = db.Categories.Single(x => x.ID == new Guid("a323a95c-b475-4886-9f8d-006c2cc84c64"));
            var articles = category.Articles.OrderByDescending(x => x.Created).Take(5);

            return View("GetCategory", new CategoryViewModel
            {
                Category = category,
                Articles = articles,
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
            //get the resource.
            var res = db.Resources.SingleOrDefault(r => r.ID == id);
            if (res == null) return HttpNotFound("That resource cannot be found.");

            var path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());
            var stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            Stream outputStream = stream;

            if (res.Type.StartsWith("image"))
            {
                var image = new WebImage(stream);

                if (!string.IsNullOrWhiteSpace(res.Source))
                {
                    image.AddTextWatermark(res.Source, "#" + ((res.SourceTextColor == null) ? "FFFFFF" : res.SourceTextColor.Value));
                }

                outputStream = new MemoryStream(image.GetBytes());
            }

            stream.Close();

            return File(outputStream, res.Type);
        }
        public ActionResult GetArticle(Guid id)
        {
            //get the article and present it!
            Article article = db.Articles.SingleOrDefault(x => x.ID == id);
            if (article == null) return HttpNotFound("That article does not exist");

            PageHits.RegisterHit(article.ID);

            return View(article);
        }
        public ActionResult GetCategory(string id, int? page)
        {
            Category category;

            Guid categoryguid;
            if (Guid.TryParse(id, out categoryguid))
            {
                category = db.Categories.SingleOrDefault(x => x.ID == categoryguid);
            }
            else
            {
                //Try and match to category name instead.
                var name = id.Replace('-', ' ').ToLower();
                category = db.Categories.SingleOrDefault(x => x.Title.ToLower() == name);
            }

            if (category == null) return HttpNotFound("That category does not exist");

            var totalPages = (int)Math.Ceiling(category.Articles.Count() / 5d);
            if (!page.HasValue || page < 0 || page > totalPages) page = 1;

            var articles = category.Articles.OrderByDescending(x => x.Created).Skip((page.Value - 1) * 5).Take(5);

            return View(new CategoryViewModel
                            {
                                Category = category,
                                Articles = articles,
                                Page = page.Value,
                                PageCount = totalPages
                            });
        }
        public ActionResult GetTag(string id, int? page)
        {
            Tag tag;

            Guid tagguid;
            if (Guid.TryParse(id, out tagguid))
            {
                tag = db.Tags.SingleOrDefault(x => x.ID == tagguid);
            }
            else
            {
                //Try and match to tag name instead.
                var name = id.Replace('-', ' ').ToLower();
                tag = db.Tags.SingleOrDefault(x => x.Name.ToLower() == name);
            }

            if (tag == null) return HttpNotFound("That tag does not exist");

            var totalPages = (int)Math.Ceiling(tag.Articles.Count() / 5d);
            if (!page.HasValue || page < 0 || page > totalPages) page = 1;

            var articles = tag.Articles.OrderByDescending(x => x.Created).Skip((page.Value - 1) * 5).Take(5);

            return View(new TagViewModel
            {
                Tag = tag,
                Articles = articles,
                Page = page.Value,
                PageCount = totalPages
            });
        }

        public ActionResult GetGrid(string id)
        {
            Grid grid;

            Guid gridguid;
            if (Guid.TryParse(id, out gridguid))
            {
                grid = db.Grids.SingleOrDefault(x => x.ID == gridguid);
            }
            else
            {
                var alias = id.ToLower();
                grid = db.Grids.SingleOrDefault(x => x.Alias.ToLower() == alias);

            }

            if (grid == null) return HttpNotFound("That grid does not exist");

            return View(grid);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error(Exception e)
        {
            var httpException = e as HttpException;
            if (httpException != null)
            {
                return View("HttpError", httpException);
            }

            return Content(e.ToString());
        }
    }
}
