using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotissimusTest.Core.Repositories;
using NotissimusTest.Data.Repositories;

namespace NotissimusTest.Data;

public static class Bootstrapper
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotissimusTestContext>(options => options
            .UseSqlServer(configuration["ConnectionString"]));
        services.AddScoped<IOffersRepository, OffersRepository>();
        return services;
    }
}