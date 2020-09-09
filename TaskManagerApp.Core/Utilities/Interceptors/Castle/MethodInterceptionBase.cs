using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApp.Core.Utilities.Interceptors.Castle
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple =true, Inherited = true)]
    public abstract class MethodInterceptionBase : Attribute, IInterceptor
    {
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
