using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using Review_Site.Models;
using FluentNHibernate.Cfg.Db;

namespace Review_Site.Core.Data
{
    public static class NHProvider
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var config = Fluently.Configure()
                        .Database(MySQLConfiguration.Standard.ConnectionString(
                                x => x.FromConnectionStringWithKey("NHibernateConnection")       
                            )
                        )
                        .Mappings(x =>
                            x.AutoMappings
                                .Add(AutoMap.AssemblyOf<Article>));

                    _sessionFactory = config.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}