using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models
{
    public class CategoryViewModel : PagedArticleList
    {
        public Category Category { get; set; }
    }
}