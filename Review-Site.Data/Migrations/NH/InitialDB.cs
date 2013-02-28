using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace Review_Site.Data.Migrations.NH
{
    public class InitialDB : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("article")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Issue").AsInt32()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("ShortDescription").AsString(150)
                .WithColumn("Text").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime()
                .WithColumn("Author_id").AsString(40)
                .WithColumn("Category_id").AsString(40)
                .WithColumn("Tag_id").AsString(40);

            Create.Table("category")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("IsSystemCategory").AsBoolean()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime()
                .WithColumn("Grid_id").AsString(40)
                .WithColumn("Color_id").AsString(40);

            Create.Table("color")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Value").AsString(6).NotNullable();

            Create.Table("grid")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Alias").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime();

            Create.Table("gridelement")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("SizeClass").AsString().NotNullable()
                .WithColumn("Width").AsInt32().NotNullable()
                .WithColumn("HeadingClass").AsString().NotNullable()
                .WithColumn("HeadingText").AsString().NotNullable()
                .WithColumn("UseHeadingText").AsBoolean().NotNullable()
                .WithColumn("InverseHeading").AsBoolean().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime()
                .WithColumn("Article_id").AsString(40)
                .WithColumn("BorderColor_id").AsString(40)
                .WithColumn("Grid_id").AsString(40)
                .WithColumn("Image_id").AsString(40);

            Create.Table("pagehit")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Target").AsString().NotNullable()
                .WithColumn("Time").AsDateTime().NotNullable()
                .WithColumn("ClientAddress").AsString();

            Create.Table("permission")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Identifier").AsDateTime().NotNullable();

            Create.Table("permissionstoroles")
                .WithColumn("Permission_id").AsString(40).NotNullable()
                .WithColumn("Role_id").AsString(40).NotNullable();

            Create.Table("resource")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime()
                .WithColumn("Creator_id").AsString(40)
                .WithColumn("SourceTextColor_id").AsString(40);

            Create.Table("role")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("rolestoassignedusers")
                .WithColumn("Role_id").AsString(40).NotNullable()
                .WithColumn("User_id").AsString(40).NotNullable();

            Create.Table("tag")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("tagtoarticle")
                .WithColumn("Article_id").AsString(40).NotNullable()
                .WithColumn("Tag_id").AsString(40).NotNullable();

            Create.Table("user")
                .WithColumn("ID").AsString(40).NotNullable().PrimaryKey()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsBinary(32).NotNullable()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("AuthWithAD").AsBoolean()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime();
        }
    }
}