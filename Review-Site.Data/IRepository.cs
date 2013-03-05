﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Review_Site.Data
{
    public interface IRepository<T> : IRepository
    {
        bool Any(Expression<Func<T, bool>> predicate);

        void Delete(T entity);

        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(Guid id);
        T Single(Expression<Func<T, bool>> predicate);

        void Add(params T[] entities);
        void Add(T entity);

        void AddOrUpdate(params T[] entities);
        void AddOrUpdate(T entity);

        void Update(params T[] entities);
        void Update(T entity);
    }
    public interface IRepository
    {
    }
}