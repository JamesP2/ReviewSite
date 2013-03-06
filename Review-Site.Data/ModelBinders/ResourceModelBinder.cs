using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Review_Site.Data.Models;
using Review_Site.Data.Utility;

namespace Review_Site.Data.ModelBinders
{
    [ModelBinderType(typeof(Resource))]
    class ResourceModelBinder : DefaultModelBinder
    {
        private DataContext db;

        public ResourceModelBinder(DataContext _db)
        {
            if (_db == null) throw new ArgumentNullException();
            db = _db;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Resource resource = base.BindModel(controllerContext, bindingContext) as Resource;
            if (resource == null) return null;

            resource.Creator = resource.Creator == null ? null : db.Users.Get(resource.Creator.ID);
            resource.SourceTextColor = resource.SourceTextColor == null ? null : db.Colors.Get(resource.SourceTextColor.ID);

            return resource;
        }
    }
}
