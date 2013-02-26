using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Review_Site.Core.Data
{
    public interface IRepository<T> : IRepository
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(Guid id);
        void SaveOrUpdate(params T[] entities);
        void SaveOrUpdate(T entity);
        void Delete(T entity);
    }
    public interface IRepository
    {
    }
}