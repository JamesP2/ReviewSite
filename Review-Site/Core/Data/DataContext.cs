using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Review_Site.Models;

namespace Review_Site.Core.Data
{
    public class DataContext
    {
        public IRepository<Article> Articles { get { return getRepository<Article>(); } }
        public IRepository<Category> Categories { get { return getRepository<Category>(); } }
        public IRepository<Color> Colors { get { return getRepository<Color>(); } }
        public IRepository<Grid> Grids { get { return getRepository<Grid>(); } }
        public IRepository<GridElement> GridElements { get { return getRepository<GridElement>(); } }
        public IRepository<PageHit> PageHits { get { return getRepository<PageHit>(); } }
        public IRepository<Permission> Permissions { get { return getRepository<Permission>(); } }
        public IRepository<Resource> Resources { get { return getRepository<Resource>(); } }
        public IRepository<Role> Roles { get { return getRepository<Role>(); } }
        public IRepository<Tag> Tags { get { return getRepository<Tag>(); } }
        public IRepository<User> Users { get { return getRepository<User>(); } }

        private IRepository<T> getRepository<T>() where T : IEntity
        {
            return DependencyResolver.Current.GetService<IRepository<T>>();
        }
    }
}