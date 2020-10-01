using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.CrossCuttingConcerns.Caching;
using TaskManagerApp.Core.Utilities.Interceptors.Castle;
using TaskManagerApp.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace TaskManagerApp.Core.Aspects.Castle.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
