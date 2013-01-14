using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Core.Data;
using Review_Site.Models;
using System.IO;
using Review_Site.Core;
using Review_Site.Areas.Admin.Models;
using System.Web.Helpers;

namespace Review_Site.Areas.Admin.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IRepository<Colour> colourRepository = new Repository<Colour>();
        private readonly IRepository<Resource> resourceRepository = new Repository<Resource>();

        //
        // GET: /Admin/Resource/
        [Restrict(Identifier = "Admin.Resource.Index")]
        public ViewResult Index()
        {
            return View(resourceRepository.GetAll().OrderBy(x => x.Title).ToList());
        }

        //
        // GET: /Admin/Resource/Details/5
        [Restrict(Identifier = "Admin.Resource.Index")]
        public ViewResult Details(Guid id)
        {
            var resource = resourceRepository.Get(r => r.ID == id).Single();

            return View(resource);
        }

        //
        // GET: /Admin/Resource/Create
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult Upload()
        {
            ViewBag.SourceTextColourID = new SelectList(colourRepository.GetAll(), "ID", "Name");
            return View();
        }

        //
        // POST: /Admin/Resource/Create
        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult Upload(Resource resource, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                resource.Type = file.ContentType;
                resource.CreatorID = SiteAuthentication.GetUserCookie().ID;
                resource.DateAdded = DateTime.Now;

                if (ModelState.IsValid)
                {
                    resourceRepository.SaveOrUpdate(resource);

                    var path = Path.Combine(Server.MapPath("~/ResourceUploads"), resource.ID.ToString());
                    file.SaveAs(path);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "You must provide a file to upload!");
            }

            ViewBag.SourceTextColourID = new SelectList(colourRepository.GetAll(), "ID", "Name");

            return View(resource);
        }

        //
        // GET: /Admin/Resource/Edit/5
        [Restrict(Identifier = "Admin.Resource.Edit")]
        public ActionResult Edit(Guid id)
        {
            var resource = resourceRepository.Get(r => r.ID == id).Single();

            ViewBag.SourceTextColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", resource.SourceTextColourID);

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
                resourceRepository.SaveOrUpdate(resource);

                return RedirectToAction("Index");
            }
            ViewBag.SourceTextColourID = new SelectList(colourRepository.GetAll(), "ID", "Name", resource.SourceTextColourID);
            return View(resource);
        }

        //
        // GET: /Admin/Resource/Delete/5
        // This is confirmed in a JS modal in the webadmin
        [Restrict(Identifier = "Admin.Resource.Delete")]
        public ActionResult Delete(Guid id)
        {
            var resource = resourceRepository.Get(r => r.ID == id).Single();

            var path = Path.Combine(Server.MapPath("~/ResourceUploads"), resource.ID.ToString());
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            resourceRepository.Delete(resource);

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Resource/MiniBrowser
        [Restrict(Identifier = "Admin.Resource.Index")]
        public ActionResult MiniBrowser(string filter)
        {
            IQueryable<Resource> result = !String.IsNullOrEmpty(filter) ? resourceRepository.Get(x => x.Type.StartsWith(filter)) : resourceRepository.GetAll();

            return View(result);
        }

        [Restrict(Identifier = "Admin.Resource.Index")]
        public ActionResult CKEditorBrowser()
        {
            ViewBag.CKEditor = true;
            return View("MiniBrowser", resourceRepository.GetAll());
        }

        // MiniUpload
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult MiniUpload()
        {
            return View();
        }

        [HttpPost]
        [Restrict(Identifier = "Admin.Resource.Upload")]
        public ActionResult MiniUpload(Resource resource, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                resource.Type = file.ContentType;
                resource.CreatorID = SiteAuthentication.GetUserCookie().ID;
                resource.DateAdded = DateTime.Now;

                if (ModelState.IsValid)
                {
                    resourceRepository.SaveOrUpdate(resource);

                    var path = Path.Combine(Server.MapPath("~/ResourceUploads"), resource.ID.ToString());
                    file.SaveAs(path);

                    return RedirectToAction("MiniBrowser");
                }
            }
            else
            {
                ModelState.AddModelError("", "You must provide a file to upload!");
            }


            return View(resource);
        }
        [Restrict(Identifier = "Admin.Resource.Crop")]
        public ActionResult Crop(Guid id)
        {
            var resource = resourceRepository.Get(x => x.ID == id).SingleOrDefault();
            if (resource == null) return HttpNotFound("Resource not found");

            if (!resource.Type.StartsWith("image")) return Content("You cannot crop a non-image resource!");

            var path = Path.Combine(Server.MapPath("~/ResourceUploads"), id.ToString());
            var stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0) throw new HttpException(503, "An internal server error occured whilst fetching the resource.");

            var img = new WebImage(stream);
            var form = new CropForm
            {
                ResourceID = id,
                Type = resource.Type,
                Source = resource.Source,
                SourceTextColourID = resource.SourceTextColourID,
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

            var oldImagePath = Path.Combine(Server.MapPath("~/ResourceUploads"), form.ResourceID.ToString());
            var stream = new FileStream(oldImagePath, FileMode.Open);

            var image = new WebImage(stream);

            var width = image.Width;
            var height = image.Height;

            image.Crop((int)form.y, (int)form.x, height - (int)form.y2, width - (int)form.x2);

            var newResource = new Resource
            {
                Title = form.Title,
                CreatorID = SiteAuthentication.GetUserCookie().ID,
                DateAdded = DateTime.Now,
                Type = form.Type,
                Source = form.Source,
                SourceTextColourID = form.SourceTextColourID
            };

            resourceRepository.SaveOrUpdate(newResource);

            string newImagePath = Path.Combine(Server.MapPath("~/ResourceUploads"), newResource.ID.ToString());
            image.Save(newImagePath, null, false);

            return View("_CloseAndRefreshParent");
        }
    }
}