﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteMulti(Expression<Func<T, bool>> where);
        T GetSingleById(int id);
        T GetSingleByCondition(Expression<Func<T, bool>> where, string[] includes = null);
        IQueryable<T> GetAll(string[] includes = null);
        IQueryable<T> GetMulti(Expression<Func<T, bool>> where, string[] includes = null);

    }
}
