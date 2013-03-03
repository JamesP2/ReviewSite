using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate;
using NHibernate.Linq;

namespace Review_Site.Data
{
    public class NHRepository<T> : IRepository<T>
    {
        protected ISession session = null;
        protected ITransaction transaction = null;
        
        public NHRepository()
        {
            session = NHSession.OpenSession();
        }

        public NHRepository(ISession existingSession)
        {
            session = existingSession;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            if (Get(predicate).Count() > 0) return true;
            return false;
        }

        public IQueryable<T> Get()
        {
            return session.Query<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Get().Where(predicate);
        }

        public T Get(Guid id)
        {
            return session.Get<T>(id);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return session.Query<T>().Single(predicate);
        }

        public void SaveOrUpdate(params T[] entities)
        {
            foreach (T entity in entities)
            {
                SaveOrUpdate(entity);
            }
        }

        public void SaveOrUpdate(T entity)
        {
            session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
        }
    }
}