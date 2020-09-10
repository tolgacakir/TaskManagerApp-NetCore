using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TaskManagerApp.Core.Utilities.Interceptors.Castle;

namespace TaskManagerApp.Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    throw ex;
                }
            }
        }
    }
}
