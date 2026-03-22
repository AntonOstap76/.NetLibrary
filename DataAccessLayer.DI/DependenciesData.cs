using AuthorRepositoryProject;
using Autofac;
using BookRepositoryProject;
using DomainProject;
using MagazineIssueRepositoryProject;
using MagazineRepositoryProject;
using PatentRepositoryProject;
using PublisherRepositoryProject;

namespace DataAccessLayer.DI;

public class DependenciesData : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BookRepository>().As<IBookRepository>();
        builder.RegisterType<AuthorRepository>().As<IAuthorRepository>();
        builder.RegisterType<PatentRepository>().As<IPatentRepository>();
        builder.RegisterType<PublisherRepository>().As<IPublisherRepository>();
        builder.RegisterType<MagazineIssueRepository>().As<IMagazineIssueRepository>();
        builder.RegisterType<MagazineRepository>().As<IMagazineRepository>();

        // builder.RegisterAssemblyTypes(ThisAssembly)
        //     .Where(t => t.Name.EndsWith("Repository"))
        //     .AsImplementedInterfaces();
    }
}