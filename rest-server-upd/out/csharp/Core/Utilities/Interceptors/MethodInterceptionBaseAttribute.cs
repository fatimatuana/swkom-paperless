using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    //attributes can be used for classes methods, inherited.. vererbt
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
