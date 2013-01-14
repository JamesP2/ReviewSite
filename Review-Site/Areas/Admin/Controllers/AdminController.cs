using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rebuild()
        {
            LuceneSearch.Rebuild();

            return Content("Fully rebuilt search indexes");
        }
        public ActionResult Optimise()
        {
            LuceneSearch.Optimize();

            return Content("Optimised search indexes");
        }

    }
}
