using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Data.Models.Feed
{
	public class Feed
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public DateTime LastBuildDate { get; set; }
        public DateTime PubDate { get; set; }

        public int TTL { get; set; }

        public IList<FeedItem> FeedItems { get; set; }

        public Feed()
        {
            FeedItems = new List<FeedItem>();
            TTL = 1800;
        }
	}
}