using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using System.IO;
using System.Web.Helpers;

namespace Review_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Article> articleRepository = new Repository<Article>();
        private readonly IRepository<Category> categoryRepository = new Repository<Category>();
        private readonly IRepository<Grid> gridRepository = new Repository<Grid>();
        private readonly IRepository<Resource> resourceRepository = new Repository<Resource>();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("LandingPage");

            var cat = categoryRepository.Get(x => x.ID == new Guid("a323a95c-b475-4886-9f8d-006c2cc84c64")).Single();

            return View(cat);
        }
        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult GetResource(Guid id)
        {
            //get the resource.
            var res = resourceRepository.Get(r => r.ID == id).Single();
            if (res == null) throw new HttpException(404, "That resource cannot be found.");

            var path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());

            var stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            Stream outputStream = stream;
            if (res.Type.StartsWith("image"))
            {
                var image = new WebImage(stream);

                if (!string.IsNullOrWhiteSpace(res.Source)) image.AddTextWatermark(res.Source, "#" + ((res.SourceTextColour == null) ? "FFFFFF" : res.SourceTextColour.Value));

                outputStream = new MemoryStream(image.GetBytes());
            }

            return File(outputStream, res.Type);
        }

        public ActionResult GetArticle(Guid id)
        {
            //get the article and present it!
            var article = articleRepository.Get(x => x.ID == id).SingleOrDefault();
            if (article == null) return HttpNotFound("That article does not exist");

            return View(article);
        }

        public ActionResult GetCategory(string id)
        {
            Guid categoryguid;
            Category category;
            if (Guid.TryParse(id, out categoryguid))
            {
                category = categoryRepository.Get(x => x.ID == categoryguid).SingleOrDefault();
                if (category == null) return HttpNotFound("That category does not exist");

                return View(category);
            }

            //Try and match to category name instead.
            var name = id.Replace('-', ' ').ToLower();
            category = categoryRepository.Get(x => x.Title.ToLower() == name).SingleOrDefault();
            if (category == null) return HttpNotFound("That category does not exist");

            return View(category);
        }

        public ActionResult GetGrid(string id)
        {
            Guid gridguid;
            Grid grid;

            if (Guid.TryParse(id, out gridguid))
            {
                grid = gridRepository.Get(x => x.ID == gridguid).SingleOrDefault();
                if (grid == null) return HttpNotFound("That grid does not exist");

                return View(grid);
            }

            //No? try the ID instead.
            var alias = id.ToLower();

            grid = gridRepository.Get(x => x.Alias.ToLower() == alias).SingleOrDefault();
            if (grid == null) return HttpNotFound("That grid does not exist");

            return View(grid);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error(Exception e)
        {
            return Content(e.ToString());
        }
    }
}
