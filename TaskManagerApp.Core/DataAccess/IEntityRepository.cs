﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TaskManagerApp.Core.Entities;

namespace TaskManagerApp.Core.DataAccess
{
    public interface IEntityRepository<T> 
        where T : class, IEntity, new()
    {
        IQueryable<T> GetQueryable();
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
