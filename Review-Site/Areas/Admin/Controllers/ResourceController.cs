using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;
using System.IO;
using Review_Site.Core;

namespace Review_Site.Areas.Admin.Controllers
{ 
    [Authorize]
    public class ResourceController : Controller
    {
        private ReviewSiteEntities db = new ReviewSiteEntities();

        //
        // GET: /Admin/Resource/

        public ViewResult Index()
        {
            return View(db.Resources.ToList());
        }

        //
        // GET: /Admin/Resource/Details/5

        public ViewResult Details(Guid id)
        {
            Resource resource = db.Resources.Single(r => r.ID == id);
            return View(resource);
        }

        //
        // GET: /Admin/Resource/Create

        public ActionResult Upload()
        {
            return View();
        } 

        //
        // POST: /Admin/Resource/Create

        [HttpPost]
        public ActionResult Upload(Resource resource, HttpPostedFileBase file)
        {
            resource.ID = Guid.NewGuid();
            string fileName = "";
            if (file != null && file.ContentLength > 0)
            {
                resource.Type = file.ContentType;
                resource.CreatorID = SiteAuthentication.GetUserCookie().ID;
                resource.DateAdded = DateTime.Now;
                fileName = resource.ID.ToString();
            }
            else
            {
                ModelState.AddModelError("", "You must provide a file to upload!");
                return View(resource);
            }
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Server.MapPath("~/ResourceUploads"), fileName);
                file.SaveAs(path);
                db.Resources.AddObject(resource);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(resource);
        }
        
        //
        // GET: /Admin/Resource/Edit/5

        public ActionResult Rename(Guid id)
        {
            Resource resource = db.Resources.Single(r => r.ID == id);
            return View(resource);
        }

        //
        // POST: /Admin/Resource/Edit/5

        [HttpPost]
        public ActionResult Rename(Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Attach(resource);
                db.ObjectStateManager.ChangeObjectState(resource, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource);
        }

        //
        // GET: /Admin/Resource/Delete/5
        // This is confirmed in a JS modal in the webadmin
 
        public ActionResult Delete(Guid id)
        {
            Resource resource = db.Resources.Single(r => r.ID == id);
            db.Resources.DeleteObject(resource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Admin/Resource/MiniBrowser

        public ActionResult MiniBrowser(string filter)
        {
            List<Resource> result;
            if (!String.IsNullOrEmpty(filter)) result = db.Resources.Where(x => x.Type.StartsWith(filter)).ToList();
            else result = db.Resources.ToList();

            return View(result);
        }

        public ActionResult CKEditorBrowser()
        {
            ViewBag.CKEditor = true;
            return View("MiniBrowser", db.Resources.ToList());
        }

        // MiniUpload

        public ActionResult MiniUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MiniUpload(Resource resource, HttpPostedFileBase file)
        {
            resource.ID = Guid.NewGuid();
            string fileName = "";
            if (file != null && file.ContentLength > 0)
            {
                resource.Type = file.ContentType;
                resource.CreatorID = SiteAuthentication.GetUserCookie().ID;
                resource.DateAdded = DateTime.Now;
                fileName = resource.ID.ToString();
            }
            else
            {
                ModelState.AddModelError("", "You must provide a file to upload!");
                return View(resource);
            }
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Server.MapPath("~/ResourceUploads"), fileName);
                file.SaveAs(path);
                db.Resources.AddObject(resource);
                db.SaveChanges();
                return RedirectToAction("MiniBrowser");
            }

            return View(resource);
        }
    }
}