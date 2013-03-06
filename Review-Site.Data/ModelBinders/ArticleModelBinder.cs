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
    [ModelBinderType(typeof(Article))]
    class ArticleModelBinder : DefaultModelBinder
    {
        private DataContext db;

        public ArticleModelBinder(DataContext _db)
        {
            if (_db == null) throw new ArgumentNullException();
            db = _db;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Article article = base.BindModel(controllerContext, bindingContext) as Article;
            if (article == null) return null;

            ModelBinderUtility.ClearErrors(bindingContext, "Category.Title");

            article.Author = article.Author == null ? null : db.Users.Get(article.Author.ID);
            article.Category = article.Category == null ? null : db.Categories.Get(article.Category.ID);

            return article;
        }
    }
}
