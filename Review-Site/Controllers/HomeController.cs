using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using System.IO;

namespace Review_Site.Controllers
{
    public class HomeController : Controller
    {
        private SiteContext db = new SiteContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetResource(Guid id)
        {
            //get the resource.
            Resource res = db.Resources.Single(r => r.ID == id);
            if (res == null) throw new HttpException(404, "That resource cannot be found.");

            string path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            byte[] streamBytes = new byte[stream.Length];
            stream.Read(streamBytes, 0, (int)stream.Length);

            stream.Close();

            return File(streamBytes, res.Type);
        }

        public ActionResult GetArticle(Guid id)
        {
            //get the article and present it!
            if (!db.Articles.Any(x => x.ID == id)) throw new HttpException(404, "That article does not exist");
            Article article = db.Articles.Single(x => x.ID == id);
            return View(article);
        }

        public ActionResult GetGrid(Guid id)
        {
            if (!db.Grids.Any(x => x.ID == id)) throw new HttpException(404, "That grid does not exist");
            Grid grid = db.Grids.Single(x => x.ID == id);
            return View(grid);
        }

        public ActionResult GetGrid(string id)
        {
            //Try by aliad instead.
            string alias = id.ToLower();
            if (!db.Grids.Any(x => x.Alias == alias)) throw new HttpException(404, "That grid does not exist");
            Grid grid = db.Grids.Single(x => x.Alias == alias);
            return View(grid);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
