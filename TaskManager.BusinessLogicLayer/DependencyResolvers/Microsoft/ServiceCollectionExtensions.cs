using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.BusinessLogicLayer.Abstract;
using TaskManagerApp.BusinessLogicLayer.Concrete;
using TaskManagerApp.Core.CrossCuttingConcerns.Caching;
using TaskManagerApp.Core.CrossCuttingConcerns.Caching.Microsoft;
using TaskManagerApp.DataAccessLayer.Abstract;
using TaskManagerApp.DataAccessLayer.Concrete.EntityFramework;

namespace TaskManagerApp.BusinessLogicLayer.DependencyResolvers.Microsoft
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaskManagerDependencyResolver(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ITaskTypeService, TaskTypeManager>();
            services.AddScoped<ITaskTypeDal, EfTaskTypeDal>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<ITaskDal, EfTaskDal>();
            return services;
        }
    }
}
