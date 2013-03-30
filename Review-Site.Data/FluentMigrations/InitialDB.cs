using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace Review_Site.Data.FluentMigrations
{
    /// <summary>
    /// Initial Database Creation Migration
    /// </summary>
    [Migration(0)]
    public class InitialDB : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("article")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Issue").AsInt32()
                .WithColumn("Title").AsString(500).NotNullable()
                .WithColumn("ShortDescription").AsString(150).Nullable()
                .WithColumn("Text").AsString(int.MaxValue).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable()
                .WithColumn("Author_id").AsGuid()
                .WithColumn("Category_id").AsGuid();

            Create.Table("articlestotags")
                .WithColumn("Article_id").AsGuid().NotNullable()
                .WithColumn("Tag_id").AsGuid().NotNullable();

            Create.Table("category")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Title").AsString(500).NotNullable()
                .WithColumn("IsSystemCategory").AsBoolean()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable()
                .WithColumn("Grid_id").AsGuid().Nullable()
                .WithColumn("Color_id").AsGuid();

            Create.Table("color")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Value").AsString(6).NotNullable();

            Create.Table("grid")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Alias").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable();

            Create.Table("gridelement")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("SizeClass").AsString().NotNullable()
                .WithColumn("Width").AsInt32().NotNullable()
                .WithColumn("HeadingClass").AsString().NotNullable()
                .WithColumn("HeadingText").AsString().Nullable()
                .WithColumn("UseHeadingText").AsBoolean().NotNullable()
                .WithColumn("InverseHeading").AsBoolean().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable()
                .WithColumn("Article_id").AsGuid()
                .WithColumn("BorderColor_id").AsGuid()
                .WithColumn("Grid_id").AsGuid()
                .WithColumn("Image_id").AsGuid();

            Create.Table("pagehit")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Target").AsGuid().NotNullable()
                .WithColumn("Time").AsDateTime().NotNullable()
                .WithColumn("ClientAddress").AsString();

            Create.Table("permission")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Identifier").AsString().NotNullable();

            Create.Table("permissionstoroles")
                .WithColumn("Permission_id").AsGuid().NotNullable()
                .WithColumn("Role_id").AsGuid().NotNullable();

            Create.Table("resource")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Title").AsString(500).NotNullable()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("Source").AsString(500).Nullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable()
                .WithColumn("Creator_id").AsGuid()
                .WithColumn("SourceTextColor_id").AsGuid();

            Create.Table("role")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("rolestoassignedusers")
                .WithColumn("Role_id").AsGuid().NotNullable()
                .WithColumn("User_id").AsGuid().NotNullable();

            Create.Table("tag")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable();

            Create.Table("user")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsBinary(32).NotNullable()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("AuthWithAD").AsBoolean()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("LastModified").AsDateTime().Nullable();
        }
    }
}