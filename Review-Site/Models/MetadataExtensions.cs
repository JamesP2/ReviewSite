using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Review_Site.Models
{
    [MetadataType(typeof(ArticleExtension))]
    public partial class Article {}

    public class ArticleExtension
    {
        [AllowHtml, Required(ErrorMessage="You must enter some article text.")]
        public string Text { get; set; }
    }

    [MetadataType(typeof(CategoryExtension))]
    public partial class Category {}

    public class CategoryExtension
    {
        [Required(ErrorMessage = "You must provide a category title.")]
        public string Title { get; set; }
    }
}