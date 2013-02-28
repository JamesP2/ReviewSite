using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Review_Site.Data.Models;
using Review_Site.Data.Models.Configurations.EF;

namespace Review_Site.Data
{
    /// <summary>
    /// The soon-to-be legacy method of accessing the database. Being phased out in favour of an NH Repository approach
    /// </summary>
    public class SiteContext : DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Grid> Grids { get; set; }
        public DbSet<GridElement> GridElements { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PageHit> PageHits { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Map all relationships from each models configuration.
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new ResourceConfiguration());
            modelBuilder.Configurations.Add(new GridElementConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}