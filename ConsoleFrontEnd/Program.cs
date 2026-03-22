using Autofac;
using BookRepositoryProject;
using BookServiceProject;
using BusinessLogic.DI;
using CommonRepositoryProject;
using DataAccessLayer.DI;
using DomainProject;

class Program
{
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<DependenciesData>();
        builder.RegisterModule<DependenciesBusiness>();

        var containers = builder.Build();

        using (var scope = containers.BeginLifetimeScope())
        {
            var service = scope.Resolve<IBookService>();
            
            var book = service.GetAllAsync().Result;
            Console.WriteLine(String.Join(" ,", book));
        }
        
    }
}