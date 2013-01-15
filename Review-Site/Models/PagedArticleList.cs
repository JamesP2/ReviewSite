using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Models
{
    public class PagedArticleList
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}