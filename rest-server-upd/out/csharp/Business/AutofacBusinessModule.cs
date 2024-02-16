using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete;


namespace Business
{
    public class AutofacBusinessModule : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentManager>().As<IDocumentService>().SingleInstance();
            builder.RegisterType<EfDocumentDal>().As<IDocumentDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
