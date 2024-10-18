using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Model;

public static class ServiceRegistration_Model
{
    public static IServiceCollection AddServices_Model(this IServiceCollection services)
    {
        // ----------------------------- FluentValidation Injection -----------------------------
        //services.AddFluentValidationAutoValidation();
        //services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
