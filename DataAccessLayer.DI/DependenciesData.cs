using AuthorRepositoryProject;
using Autofac;
using BookRepositoryProject;
using Microsoft.Extensions.Configuration;
using DomainProject;
using MagazineIssueRepositoryProject;
using MagazineRepositoryProject;
using PatentRepositoryProject;
using PublisherRepositoryProject;
using RepositoryToolsProject;

namespace DataAccessLayer.DI;

public class DependenciesData : Module
{
    private readonly IConfiguration _configuration;

    public DependenciesData(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void Load(ContainerBuilder builder)
    {
        
        var connectionString = _configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string not found.");
        
        builder.RegisterInstance(new DbConnectionFactory(connectionString))
            .As<IDbConnectionFactory>()
            .SingleInstance();

        builder.RegisterType<DatabaseInitializer>()
            .AsSelf()
            .SingleInstance();
        
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