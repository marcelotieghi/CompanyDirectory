using CompanyDirectory.Core.Entities;
using CompanyDirectory.Core.Interface;
using CompanyDirectory.Infra.Data.Context;
using CompanyDirectory.Infra.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyDirectory.CrossCutting.AppDependencies;

public static class InfraLayerServices
{
    public static void AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConn")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddScoped<IReadRepos<Location>, ReadRepos<Location>>()
            .AddScoped<IReadRepos<Department>, ReadRepos<Department>>()
            .AddScoped<IReadRepos<Personnel>, ReadRepos<Personnel>>();

        services
            .AddScoped<IWriteRepos<Location>, WriteRepos<Location>>()
            .AddScoped<IWriteRepos<Department>, WriteRepos<Department>>()
            .AddScoped<IWriteRepos<Personnel>, WriteRepos<Personnel>>();
    }
}