using AuthorServiceProject;
using Autofac;
using BookServiceProject;
using MagazineIssueServiceProject;
using MagazineServiceProject;
using PatentServiceProject;
using PublisherServiceProject;

namespace BusinessLogic.DI;

public class DependenciesBusiness: Module
{

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BookService>().As<IBookService>();
        builder.RegisterType<AuthorService>().As<IAuthorService>();
        builder.RegisterType<PatentService>().As<IPatentService>();
        builder.RegisterType<PublisherService>().As<IPublisherService>();
        builder.RegisterType<MagazineIssueService>().As<IMagazineIssueService>();
        builder.RegisterType<MagazineService>().As<IMagazineService>();
        
        // builder.RegisterAssemblyTypes(ThisAssembly)
        //     .Where(t => t.Name.EndsWith("Service"))
        //     .AsImplementedInterfaces()
        //     .InstancePerLifetimeScope();
    }
}