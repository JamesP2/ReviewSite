using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models
{
    public class PagedArticleList
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Article> Articles { get; set; }

        public PagedArticleList()
        {
            //Unless changed, set to one total page which will disable pagination.
            PageCount = 1;
        }
    }
}