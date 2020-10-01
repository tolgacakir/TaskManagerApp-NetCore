using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.Utilities.IoC;

namespace TaskManagerApp.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //TODO: burası implemente edilecek...
            throw new NotImplementedException();
        }
    }
}
