namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Review_Site.Models;
    using Review_Site.Core;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Review_Site.Models.SiteContext>
    {

        private SiteContext db;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Review_Site.Models.SiteContext context)
        {

            db = context;

            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("85a540a7-2de7-4812-b8fe-0698bb62ae06"), Name = "Light Green", Value = "8AA359" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("e5475bc5-5aa8-4116-bfdd-0749dff93bfb"), Name = "Purple", Value = "9900FF" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("01ad4b4b-fd0d-496c-85d3-0b3d4b577780"), Name = "Blue", Value = "0B76C2" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("5697e61f-aa6c-49c1-8dbc-10003ccc43ef"), Name = "Light Orange", Value = "F3B32B" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("f6945f5b-4cbd-4b0d-b21c-158d7140eb84"), Name = "Light Red", Value = "FF5250" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("d026fd65-9b7f-4fc4-b844-1afa97f85c09"), Name = "Pink", Value = "FF36FF" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("1aebb28b-0b46-4839-862b-29bec947f592"), Name = "Light Blue", Value = "1582B3" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("fe0bc322-4c83-4463-866d-35b2ba63ec08"), Name = "Light Brown", Value = "DA9349" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("8ba75eed-27d4-4c9e-af33-40bce4a785e6"), Name = "Grey", Value = "858786" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("c4997c1e-b9fc-4642-81bb-4319618104aa"), Name = "Black", Value = "000000" });
            context.Colors.AddOrUpdate(new Color { ID = Guid.Parse("54b074d0-42c8-41f2-9d54-45fc194adf70"), Name = "White", Value = "FFFFFF" });

            context.SaveChanges();

            context.Categories.AddOrUpdate(new Category
                {
                    ID = Guid.Parse("a323a95c-b475-4886-9f8d-006c2cc84c64"),
                    Title = "Home",
                    IsSystemCategory = true,
                    ColorID = context.Colors.Single(x => x.Name == "Grey").ID
                }
                );

            context.SaveChanges();

            context.Permissions.AddOrUpdate(new Permission[]{
                new Permission{
                    ID = Guid.Parse("b7c48bc8-7674-4cae-b96e-327e9c50d920"),
                    Identifier = "Admin.Article.Index",
                    Name = "View Article Admin"
                },
                new Permission{
                    ID = Guid.Parse("bbb39b4b-6247-4a82-9b71-3311e2cae01e"),
                    Identifier = "Admin.Article.Create",
                    Name = "Create Articles"
                },
                new Permission{
                    ID = Guid.Parse("885e4bcf-c858-4b78-b639-35bfc6f803d2"),
                    Identifier = "Admin.Article.Edit",
                    Name = "Edit Articles"
                },
                new Permission{
                    ID = Guid.Parse("76c435bc-c1ba-4bf9-bbf9-3f973668c344"),
                    Identifier = "Admin.Article.Delete",
                    Name = "Delete Articles"
                },new Permission{
                    ID = Guid.Parse("8db3ce0e-30a6-4ba3-89d4-46eaebbffb29"),
                    Identifier = "Admin.User.Index",
                    Name = "View User Admin"
                },
                new Permission{
                    ID = Guid.Parse("c3a8e1bd-8e42-4c30-9729-52669e46b21e"),
                    Identifier = "Admin.User.Create",
                    Name = "Create Users"
                },
                new Permission{
                    ID = Guid.Parse("8ee578a4-d351-4b59-a79e-591f6524d5a2"),
                    Identifier = "Admin.User.Edit",
                    Name = "Edit Users"
                },
                new Permission{
                    ID = Guid.Parse("c82c52fc-a824-4cb6-92e8-5c78678d5a73"),
                    Identifier = "Admin.User.Delete",
                    Name = "Delete Users"
                },new Permission{
                    ID = Guid.Parse("da889323-4fa1-40a3-ae18-76b4819aa1f4"),
                    Identifier = "Admin.Category.Index",
                    Name = "View Category Admin"
                },
                new Permission{
                    ID = Guid.Parse("0409d7be-1c8d-4154-8cdb-89ce97f4ad25"),
                    Identifier = "Admin.Category.Create",
                    Name = "Create Categories"
                },
                new Permission{
                    ID = Guid.Parse("ddc1f5b4-b38a-4099-b9dd-9a4740a2b41e"),
                    Identifier = "Admin.Category.Edit",
                    Name = "Edit Categories"
                },
                new Permission{
                    ID = Guid.Parse("64d1ea79-8294-4a84-93c6-9b35d669c560"),
                    Identifier = "Admin.Category.Delete",
                    Name = "Delete Categories"
                },new Permission{
                    ID = Guid.Parse("3bad25a3-b964-45cc-b136-acb1bdb8c069"),
                    Identifier = "Admin.Grid.Index",
                    Name = "View Grid Admin"
                },
                new Permission{
                    ID = Guid.Parse("12f5a2f8-ab28-4b6d-8706-cb85c0c0cd2a"),
                    Identifier = "Admin.Grid.Create",
                    Name = "Create Grids"
                },
                new Permission{
                    ID = Guid.Parse("676ed096-3875-444d-ab45-ccbe475301b6"),
                    Identifier = "Admin.Grid.Edit",
                    Name = "Edit Grids"
                },
                new Permission{
                    ID = Guid.Parse("991bf242-6239-4e67-ae25-d64d2c8c2d32"),
                    Identifier = "Admin.Grid.Delete",
                    Name = "Delete Grids"
                },new Permission{
                    ID = Guid.Parse("4820f6f5-c8de-49fa-b215-d6c62bfde2ac"),
                    Identifier = "Admin.GridElement.Index",
                    Name = "View Grid Element Admin"
                },
                new Permission{
                    ID = Guid.Parse("3681f2d7-9ffa-4252-8e36-e2a3915179e7"),
                    Identifier = "Admin.GridElement.Create",
                    Name = "Create Grid Elements"
                },
                new Permission{
                    ID = Guid.Parse("643e90da-356c-4b85-b5e4-e463a61afc40"),
                    Identifier = "Admin.GridElement.Edit",
                    Name = "Edit Grid Elements"
                },
                new Permission{
                    ID = Guid.Parse("59287382-22b9-49d9-aaa2-f6b778567582"),
                    Identifier = "Admin.GridElement.Delete",
                    Name = "Delete Grid Elements"
                },new Permission{
                    ID = Guid.Parse("2a413e38-b4a0-4b7f-91bd-3b6a99631323"),
                    Identifier = "Admin.Resource.Index",
                    Name = "View Resource Admin"
                },
                new Permission{
                    ID = Guid.Parse("f76f72dd-b3a9-46af-b476-3fbe4d89f214"),
                    Identifier = "Admin.Resource.Upload",
                    Name = "Upload Resources"
                },
                new Permission{
                    ID = Guid.Parse("3acdefe5-7515-4737-84d1-25caf189210f"),
                    Identifier = "Admin.Resource.Edit",
                    Name = "Edit Resources"
                },
                new Permission{
                    ID = Guid.Parse("d58ddd15-c314-4c7c-af4b-5505ed70b63c"),
                    Identifier = "Admin.Resource.Delete",
                    Name = "Delete Resources"
                },
                new Permission{
                    ID = Guid.Parse("d4872022-4d74-43de-936e-19051a6e9360"),
                    Identifier = "Admin.Resource.Crop",
                    Name = "Crop Image Resources"
                }
            });

            context.SaveChanges();

            context.Roles.AddOrUpdate(new Role[]{
                new Role{
                    ID = Guid.Parse("eae9a548-8930-485d-b37c-10587492a216"),
                    Name = "Global Administrator",
                    Permissions = GetPermissionCollection("Admin")
                },
                new Role{
                    ID = Guid.Parse("e736f81e-4d2d-4135-9b49-93134a614580"),
                    Name = "Content Editor",
                    Permissions = GetPermissionCollection("Admin.Article").Union(GetPermissionCollection("Admin.Resource")).ToList()
                }
            });

            context.SaveChanges();

            context.Users.AddOrUpdate(new User
            {
                ID = Guid.Parse("4dd27ddf-995c-43cf-9867-43e4f9bee080"),
                Username = "admin",
                Password = PasswordHashing.GetHash("password"),
                FirstName = "System",
                LastName = "Administrator",
                Roles = new Role[]{
                    context.Roles.Single(x => x.Name == "Global Administrator")
                }
            });

        }

        private System.Collections.Generic.IList<Permission> GetPermissionCollection(string partialIdent)
        {
            List<Permission> permissions = db.Permissions.Where(x => x.Identifier.StartsWith(partialIdent)).ToList();
            foreach(Permission p in permissions) { db.Permissions.Attach(p); }
            return permissions;
        }

        private Permission GetPermission(string identifier)
        {
            Permission permission = db.Permissions.Single(x => x.Identifier.Equals(identifier));
            db.Permissions.Attach(permission);
            return permission;
        }
    }
}
