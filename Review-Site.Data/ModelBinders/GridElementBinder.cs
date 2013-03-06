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
    [ModelBinderType(typeof(GridElement))]
    class GridElementBinder : DefaultModelBinder
    {
        private DataContext db;

        public GridElementBinder(DataContext _db)
        {
            if (_db == null) throw new ArgumentNullException();
            db = _db;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            GridElement element = base.BindModel(controllerContext, bindingContext) as GridElement;
            if (element == null) return null;

            element.Article = element.Article == null ? null : db.Articles.Get(element.Article.ID);
            element.Grid = element.Grid == null ? null : db.Grids.Get(element.Grid.ID);
            element.Image = element.Image == null ? null : db.Resources.Get(element.Image.ID);
            element.BorderColor = element.BorderColor == null ? null : db.Colors.Get(element.BorderColor.ID);

            return element;
        }
    }
}
