using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Core.CrossCuttingConcerns.Caching;
using TaskManagerApp.Core.CrossCuttingConcerns.Caching.Microsoft;
using TaskManagerApp.Core.Utilities.IoC;

namespace TaskManagerApp.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
