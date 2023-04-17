﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(string? includeProperties = null);
        //Edit data
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //Create
        void Add(T entity);
        //Delete
        void Remove(T entity);
        //Delete All
        void RemoveRange(IEnumerable<T> entity);
    }
}