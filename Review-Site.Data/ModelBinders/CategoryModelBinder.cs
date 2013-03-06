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
    [ModelBinderType(typeof(Category))]
    class CategoryModelBinder : DefaultModelBinder
    {
        private DataContext db;

        public CategoryModelBinder(DataContext _db)
        {
            if (_db == null) throw new ArgumentNullException();
            db = _db;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Category category = base.BindModel(controllerContext, bindingContext) as Category;
            if (category == null) return null;

            ModelBinderUtility.ClearErrors(bindingContext, "Grid.ID");
            ModelBinderUtility.ClearErrors(bindingContext, "Grid.Alias");
            ModelBinderUtility.ClearErrors(bindingContext, "Grid.Name");

            category.Color = category.Color == null ? null : db.Colors.Get(category.Color.ID);
            category.Grid = category.Grid == null ? null : db.Grids.Get(category.Grid.ID);

            return category;
        }
    }
}
