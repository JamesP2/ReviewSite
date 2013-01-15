﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Review_Site.Models;

namespace Review_Site.Core
{
    public static class PageHits
    {
        public static void RegisterHit(Guid target)
        {
            SiteContext db = new SiteContext();
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if(ip == null || ip.ToLower() == "unknown") ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            db.PageHits.Add(new PageHit
            {
                ID = Guid.NewGuid(),
                ClientAddress = ip,
                Target = target,
                Time = DateTime.Now
            });
            db.SaveChanges();
        }
    }
}