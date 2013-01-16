using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Review_Site.Models
{
    public class User
    {
        [Key]
        public virtual Guid ID { get; set; }

        public virtual string Username { get; set; }
        public virtual byte[] Password { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual bool AuthWithAD { get; set; }

        public virtual IList<Article> Articles { get; set; }
        public virtual IList<Resource> Resources { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? LastModified { get; set; }

        [NotMapped]
        public virtual string FullName {
            get{
                return FirstName + " " + LastName;
            }
        }

        public User()
        {
            Articles = new List<Article>();
            Resources = new List<Resource>();
            Roles = new List<Role>();
        }
    }

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(x => x.Articles).WithRequired(x => x.Author).HasForeignKey(x => x.AuthorID);
            HasMany(x => x.Roles).WithMany(x => x.AssignedUsers);
        }
    }
}