using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerApp.Core.Entities;

namespace TaskManagerApp.Core.DataAccess
{
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
