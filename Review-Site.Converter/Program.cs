using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Review_Site.Data;
using Review_Site.Data.Models;

namespace Review_Site.Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            string efSourceString = "";
            string nhString = "";
            Console.WriteLine("EF to NH Database Conversion");
            Console.WriteLine("");
            Console.WriteLine("Running any pending migrations...");
            DataCore.MigrateDBToLatest(ConfigurationManager.ConnectionStrings["NHibernateConnection"].ConnectionString);
            
            foreach(ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings){
                if (connectionString.Name.Equals("SiteContext")) efSourceString = connectionString.ConnectionString;
                if (connectionString.Name.Equals("NHibernateConnection")) nhString = connectionString.ConnectionString;
            }
            if (string.IsNullOrEmpty(efSourceString) || string.IsNullOrEmpty(nhString))
            {
                Console.WriteLine("You must provide a source string (\"SiteContext\") and a destination (\"NHibernateConnection\") in app.config or ConnectionStrings.config");
                Environment.Exit(1);
            }
            /*try
            {*/
                SiteContext efCon = new SiteContext(); //TODO: Specify connection string? Just use sitecontext for now...
                DataContext ctx = new DataContext();

                Console.WriteLine("Beginning Conversion");
                foreach (Article article in efCon.Articles.ToList())
                {
                    Console.WriteLine("Adding Article " + article.ID.ToString() + " to the NHibernate Repository");
                    Article newArticle = new Article
                    {
                        ID = article.ID,
                        Title = article.Title,
                        ShortDescription = article.ShortDescription,
                        Text = article.Text,
                        Created = article.Created,
                        LastModified = article.LastModified,
                        Issue = article.Issue,
                        Author = article.Author,
                        Category = article.Category,
                        Tags = article.Tags
                    };
                    ctx.Articles.SaveOrUpdate(newArticle);
                }
                foreach (Category cat in efCon.Categories.ToList())
                {
                    Console.WriteLine("Adding Category " + cat.ID.ToString() + " to the NHibernate Repository");
                    Category newCategory = new Category
                    {
                        ID = cat.ID,
                        Title = cat.Title,
                        IsSystemCategory = cat.IsSystemCategory,
                        Articles = cat.Articles,
                        Grid = cat.Grid,
                        Color = cat.Color,
                        Created = cat.Created,
                        LastModified = cat.LastModified
                    };
                    ctx.Categories.SaveOrUpdate(newCategory);
                }
                foreach (Color col in efCon.Colors.ToList())
                {
                    Console.WriteLine("Adding Color " + col.ID.ToString() + " to the NHibernate Repository");
                    Color newColor = new Color
                    {
                        ID = col.ID,
                        Name = col.Name,
                        Value = col.Value
                    };
                    ctx.Colors.SaveOrUpdate(newColor);
                }
                foreach (Grid grid in efCon.Grids.ToList())
                {
                    Console.WriteLine("Adding Grid " + grid.ID.ToString() + " to the NHibernate Repository");
                    Grid newGrid = new Grid
                    {
                        ID = grid.ID,
                        Name = grid.Name,
                        Alias = grid.Alias,
                        Description = grid.Description,
                        GridElements = grid.GridElements,
                        Created = grid.Created,
                        LastModified = grid.LastModified
                    };
                    ctx.Grids.SaveOrUpdate(newGrid);
                }
                foreach (GridElement gridElement in efCon.GridElements.ToList())
                {
                    Console.WriteLine("Adding GridElement " + gridElement.ID.ToString() + " to the NHibernate Repository");
                    GridElement newGridElement = new GridElement
                    {
                        ID = gridElement.ID,
                        SizeClass = gridElement.SizeClass,
                        HeadingClass = gridElement.HeadingClass,
                        HeadingText = gridElement.HeadingText,
                        Width = gridElement.Width,
                        UseHeadingText = gridElement.UseHeadingText,
                        InverseHeading = gridElement.InverseHeading,
                        BorderColor = gridElement.BorderColor,
                        Article = gridElement.Article,
                        Grid = gridElement.Grid,
                        Image = gridElement.Image
                    };
                    ctx.GridElements.SaveOrUpdate(newGridElement);
                }
                foreach (PageHit hit in efCon.PageHits.ToList())
                {
                    Console.WriteLine("Adding PageHit " + hit.ID.ToString() + " to the NHibernate Repository");
                    PageHit newPageHit = new PageHit
                    {
                        ID = hit.ID,
                        Target = hit.Target,
                        Time = hit.Time,
                        ClientAddress = hit.ClientAddress
                    };
                    ctx.PageHits.SaveOrUpdate(newPageHit);
                }
                foreach (Permission permission in efCon.Permissions.ToList())
                {
                    Console.WriteLine("Adding Permission " + permission.ID.ToString() + " to the NHibernate Repository");
                    Permission newPermission = new Permission
                    {
                        ID = permission.ID,
                        Name = permission.Name,
                        Identifier = permission.Identifier,
                        Roles = permission.Roles
                    };
                    ctx.Permissions.SaveOrUpdate(newPermission);
                }
                foreach (Resource resource in efCon.Resources.ToList())
                {
                    Console.WriteLine("Adding Resource " + resource.ID.ToString() + " to the NHibernate Repository");
                    Resource newResource = new Resource
                    {
                        ID = resource.ID,
                        Title = resource.Title,
                        Type = resource.Type,
                        Source = resource.Source,
                        Created = resource.Created,
                        LastModified = resource.LastModified,
                        Creator = resource.Creator,
                        SourceTextColor = resource.SourceTextColor
                    };
                    ctx.Resources.SaveOrUpdate(newResource);
                }
                foreach (Role role in efCon.Roles.ToList())
                {
                    Console.WriteLine("Adding Role " + role.ID.ToString() + " to the NHibernate Repository");
                    Role newRole = new Role
                    {
                        ID = role.ID,
                        Name = role.Name,
                        AssignedUsers = role.AssignedUsers,
                        Permissions = role.Permissions
                    };
                    ctx.Roles.SaveOrUpdate(newRole);
                }
                foreach (Tag tag in efCon.Tags.ToList())
                {
                    Console.WriteLine("Adding Tag " + tag.ID.ToString() + " to the NHibernate Repository");
                    Tag newTag = new Tag
                    {
                        ID = tag.ID,
                        Name = tag.Name,
                        Articles = tag.Articles
                    };
                    ctx.Tags.SaveOrUpdate(newTag);
                }
                foreach (User user in efCon.Users.ToList())
                {
                    Console.WriteLine("Adding User " + user.ID.ToString() + " to the NHibernate Repository");
                    User newUser = new User
                    {
                        ID = user.ID,
                        Username = user.Username,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        AuthWithAD = user.AuthWithAD,
                        Articles = user.Articles,
                        Resources = user.Resources,
                        Roles = user.Roles,
                        Created = user.Created,
                        LastModified = user.LastModified
                    };
                    ctx.Users.SaveOrUpdate(newUser);
                }
                Console.WriteLine("Conversion is now complete. Press anything to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            /*}
            catch (Exception e)
            {
                throw e;
                Console.WriteLine("Something really bad happened: " + e.Message);
                Console.WriteLine("A stack trace is below:");
                Console.Write(e.StackTrace);
                Console.WriteLine("");
                Console.WriteLine("Press a key to exit.");
                Console.ReadKey();
                Environment.Exit(1);
            }*/
        }
    }
}
