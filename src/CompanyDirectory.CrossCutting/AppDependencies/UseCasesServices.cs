using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyDirectory.CrossCutting.AppDependencies;

public static class UseCasesServices
{
    public static void AddUseCasesLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(Assembly.Load("CompanyDirectory.UseCases")));
    }
}