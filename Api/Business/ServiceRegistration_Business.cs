using Business.Abstract;
using Business.Concrete;
using Business.MappingProfiles_;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class ServiceRegistration_Business
{
    public static IServiceCollection AddServices_Business(this IServiceCollection services)
    {
        // ----------------------------- Distributed Cache -----------------------------
        services.AddDistributedMemoryCache();

        // ----------------------------- AutoMapper -----------------------------
        services.AddAutoMapper(typeof(MappingProfiles));

        // ----------------------------- Business Servicses Implemantation -----------------------------
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
