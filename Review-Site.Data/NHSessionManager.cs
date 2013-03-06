using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Review_Site.Data.Models.Mappings;

namespace Review_Site.Data
{
    class NHSessionManager : IDisposable
    {
        public ISession Session { get; set; }

        public NHSessionManager()
        {
            ISessionFactory factory = getConfiguration().BuildSessionFactory();
            Session = factory.OpenSession();
        }

        private Configuration getConfiguration()
        {
            var config = Fluently.Configure()
                        .Database(MySQLConfiguration.Standard.ConnectionString(
                                x => x.FromConnectionStringWithKey("NHibernateConnection")
                            )
                        );

            config.Mappings(x => x.FluentMappings.AddFromAssemblyOf<ArticleMap>());

            //For debugging only. Migrations will build the DB to the specification we want but this can be used to verify the schema is right.
            //config.ExposeConfiguration(x => new SchemaExport(x).Execute(true, true, false));

            return config.BuildConfiguration();
        }

        public void Dispose()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}
