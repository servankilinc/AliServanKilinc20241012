using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Model;

public static class ServiceRegistration_Model
{
    public static IServiceCollection AddServices_Model(this IServiceCollection services)
    {
        // ----------------------------- FluentValidation Injection -----------------------------
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
