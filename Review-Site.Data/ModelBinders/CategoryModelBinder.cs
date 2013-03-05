using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Review_Site.Data.Models;

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

            bindingContext.ModelState["Grid.ID"].Errors.Clear();

            category.Color = category.Color == null ? null : db.Colors.Get(category.Color.ID);
            category.Grid = category.Grid == null ? null : db.Grids.Get(category.Grid.ID);
            
            //These errors will be fixed if a grid is imported.
            bindingContext.ModelState["Grid.Alias"].Errors.Clear();
            bindingContext.ModelState["Grid.Name"].Errors.Clear();
            

            return category;
        }
    }
}
