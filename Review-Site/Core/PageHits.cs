using System;
using System.Web;
using Review_Site.Data.Models;
using Review_Site.Data;

namespace Review_Site.Core
{
    public static class PageHits
    {
        public static void RegisterHit(Guid target)
        {
            var db = new DataContext();

            var ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if(ip == null || ip.ToLower() == "unknown") ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            db.PageHits.AddOrUpdate(new PageHit
            {
                ID = Guid.NewGuid(),
                ClientAddress = ip,
                Target = target,
                Time = DateTime.Now
            });
        }
    }
}