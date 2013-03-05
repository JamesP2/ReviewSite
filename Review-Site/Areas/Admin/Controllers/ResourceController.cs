using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Data.Models;
using System.IO;
using Review_Site.Core;
using Review_Site.Areas.Admin.Models;
using Review_Site.Data;
using System.Web.Helpers;

namespace Review_Site.Areas.Admin.Controllers
{ 
    [Authorize]
    public class ResourceController : Controller
    {
        private DataContext db = new DataContext();
        #region Browse and Details
        [Restrict(Identifier = "Admin.Resource.Index")]
        public ViewResult Index()
        {
            return View(db.Resources.Get().OrderBy(x => x.Title).ToList());
        }

        [Restrict(Identifier = "Admin.Resource.Index")]
        public ViewResult Details(Guid id)
        {
            Resource resource = db.Resources.Get(id);
            return View(resource);
        }
        #endregion

        #region Upload
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult Upload()
        {
            ViewBag.SourceTextColorID = new SelectList(db.Colors.Get(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult Upload(Resource resource, HttpPostedFileBase file)
        {
            resource.ID = Guid.NewGuid();
            string fileName = "";
            if (db.Resources.Any(x => x.Title == resource.Title))
            {
                ModelState.AddModelError("Title", "A Resource with that Title already exists.");
                return View(resource);
            }
            if (file != null && file.ContentLength > 0)
            {
                resource.Type = file.ContentType;
                resource.CreatorID = SiteAuthentication.GetUserCookie().ID;
                resource.DateAdded = DateTime.Now;
                resource.LastModified = DateTime.Now;
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
                db.Resources.AddOrUpdate(resource);
                return RedirectToAction("Index");  
            }

            ViewBag.SourceTextColorID = new SelectList(db.Colors.Get(), "ID", "Name");
            return View(resource);
        }

        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult MiniUpload()
        {
            return View();
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Upload")]
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
                db.Resources.AddOrUpdate(resource);
                return RedirectToAction("MiniBrowser");
            }

            return View(resource);
        }

        #endregion

        #region Edit

        //
        // GET: /Admin/Resource/Edit/5

        [Restrict(Identifier = "Admin.Resource.Edit")]
        public ActionResult Edit(Guid id)
        {
            Resource resource = db.Resources.Single(r => r.ID == id);
            ViewBag.SourceTextColorID = new SelectList(db.Colors.Get(), "ID", "Name", resource.SourceTextColorID);
            return View(resource);
        }

        //
        // POST: /Admin/Resource/Edit/5

        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Edit")]
        public ActionResult Edit(Resource resource)
        {
            if (ModelState.IsValid)
            {
                if (db.Resources.Any(x => x.Title == resource.Title && x.ID != resource.ID))
                {
                    ModelState.AddModelError("Title", "A Resource with that Title already exists.");
                    ViewBag.SourceTextColorID = new SelectList(db.Colors.Get(), "ID", "Name", resource.SourceTextColorID);
                    return View(resource);
                }
                resource.LastModified = DateTime.Now;
                db.Resources.AddOrUpdate(resource);
                return RedirectToAction("Index");
            }
            ViewBag.SourceTextColorID = new SelectList(db.Colors.Get(), "ID", "Name", resource.SourceTextColorID);
            return View(resource);
        }
        #endregion

        #region Delete
        // This is confirmed in a JS modal in the webadmin
        [Restrict(Identifier = "Admin.Resource.Delete")]
        public ActionResult Delete(Guid id)
        {
            Resource resource = db.Resources.Single(r => r.ID == id);
            var path = Path.Combine(Server.MapPath("~/ResourceUploads"), resource.ID.ToString());
            if(System.IO.File.Exists(path)) System.IO.File.Delete(path);
            db.Resources.Delete(resource);
            return RedirectToAction("Index");
        }
        #endregion

        #region Other Browsers
        [Restrict(Identifier = "Admin.Resource.Index")]
        public ActionResult MiniBrowser(string filter)
        {
            List<Resource> result;
            if (!String.IsNullOrEmpty(filter)) result = db.Resources.Get().Where(x => x.Type.StartsWith(filter)).ToList();
            else result = db.Resources.Get().OrderBy(x => x.Title).ToList();

            return View(result);
        }

        [Restrict(Identifier = "Admin.Resource.Index")]
        public ActionResult CKEditorBrowser()
        {
            ViewBag.CKEditor = true;
            return View("MiniBrowser", db.Resources.Get().ToList());
        }
        #endregion

        #region Crop
        [Restrict(Identifier = "Admin.Resource.Crop")]
        public ActionResult Crop(Guid id)
        {
            if (!db.Resources.Any(x => x.ID == id)) throw new HttpException(404, "Resource not found.");
            Resource res = db.Resources.Get(id);
            if (!res.Type.StartsWith("image")) return Content("You cannot crop a non-image resource!");

            string path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            byte[] streamBytes = new byte[stream.Length];
            stream.Read(streamBytes, 0, (int)stream.Length);
            stream.Close();

            WebImage img = new WebImage(streamBytes);
            CropForm form = new CropForm
            {
                ResourceID = id,
                Type = res.Type,
                Source = res.Source,
                SourceTextColorID = res.SourceTextColorID,
                OrigWidth = img.Width,
                OrigHeight = img.Height
            };
            return View(form);
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Crop")]
        public ActionResult Crop(CropForm form)
        {
            if (form.x <= 0 && form.y <= 0 && form.x2 <= 0 && form.y2 <= 0)
            {
                ModelState.AddModelError("", "You must provide a selection!");
                return View(form);
            }

            if (db.Resources.Any(x => x.Title == form.Title))
            {
                ModelState.AddModelError("Title", "Another Resource has this Title.");
                return View(form);
            }

            string oldImagePath = Path.Combine(Server.MapPath("~/ResourceUploads"), form.ResourceID.ToString());
            FileStream stream = new FileStream(oldImagePath, FileMode.Open);

            WebImage image = new WebImage(stream);

            int width = image.Width;
            int height = image.Height;

            image.Crop((int)Math.Ceiling(form.y), (int)Math.Ceiling(form.x), height - (int)Math.Ceiling(form.y2), width - (int)Math.Ceiling(form.x2));
            //image.Crop((int)form.y, (int)form.x, (int)form.y2, (int)form.x2);

            Resource newResource = new Resource
            {
                ID = Guid.NewGuid(),
                Title = form.Title,
                CreatorID = SiteAuthentication.GetUserCookie().ID,
                DateAdded = DateTime.Now,
                Type = form.Type,
                Source = form.Source,
                SourceTextColorID = form.SourceTextColorID
            };

            string newImagePath = Path.Combine(Server.MapPath("~/ResourceUploads"), newResource.ID.ToString());

            image.Save(newImagePath, null, false);
            db.Resources.AddOrUpdate(newResource);
            return View("_CloseAndRefreshParent");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}