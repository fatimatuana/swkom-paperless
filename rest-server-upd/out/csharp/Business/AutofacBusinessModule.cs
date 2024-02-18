using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Mappers;
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
            builder.RegisterType<FileOperation>().As<IFileOperation>().SingleInstance();
            builder.RegisterType<RabbitMQManager>().As<IRabbitMQService>().SingleInstance();
            builder.RegisterType<ElasticSearchManager>().As<IElasticSearchService>().SingleInstance();


            builder.Register(context => new MapperConfiguration(cfg =>
            {
                // Configure your AutoMapper mappings here
                cfg.AddProfile<DocumentProfile>(); // Replace with your actual AutoMapper profile class
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
