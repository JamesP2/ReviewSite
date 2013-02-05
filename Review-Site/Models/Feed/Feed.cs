using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Review_Site.Models.Feed
{
	public class Feed
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public DateTime lastBuildDate { get; set; }
        public DateTime pubDate { get; set; }

        public int ttl { get; set; }
	}
}