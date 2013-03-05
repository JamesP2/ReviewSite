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
        
        public NHRepository()
        {
            session = NHProvider.Session;
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

        public void Add(params T[] entities)
        {
            foreach (T entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(T entity)
        {
            using (ITransaction t = session.BeginTransaction())
            {
                session.Save(entity);
                t.Commit();
            }
        }

        public void AddOrUpdate(params T[] entities)
        {
            foreach (T entity in entities)
            {
                AddOrUpdate(entity);
            }
        }

        public void AddOrUpdate(T entity)
        {
            using (ITransaction t = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                t.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (ITransaction t = session.BeginTransaction())
            {
                session.Delete(entity);
                t.Commit();
            }
        }


        public void Update(params T[] entities)
        {
            foreach (T entity in entities)
            {
                Update(entity);
            }
        }

        public void Update(T entity)
        {
            using (ITransaction t = session.BeginTransaction())
            {
                session.Update(entity);
                t.Commit();
            }
        }
    }
}