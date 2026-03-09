using Autofac;
using BookServiceProject;
using BusinessLogic.DI;
using DataAccessLayer.DI;

class Program
{
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<DependenciesData>();
        builder.RegisterModule<DependenciesBusiness>();
    }
}