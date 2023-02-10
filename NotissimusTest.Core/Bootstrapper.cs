using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using NotissimusTest.Core.Services;

namespace NotissimusTest.Core;

public static class Bootstrapper
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();
        services.AddScoped<XmlDocument>();
        services.AddScoped<IOffersService, OffersService>();
        
        return services;
    }
}