using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Feed
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        //Guid can be anything as-per RSS 2.0 spec. However it will remain as a GUID so mobile apps can use it.
        public Guid Guid { get; set; }

        public DateTime PubDate { get; set; }
    }
}