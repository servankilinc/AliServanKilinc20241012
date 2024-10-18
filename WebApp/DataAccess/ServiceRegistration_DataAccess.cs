using Core.DataAccess;
using DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Identity;
using Model.Entities;

namespace DataAccess;

public static class ServiceRegistration_DataAccess
{
    public static IServiceCollection AddServices_DataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // ----------------------------- Database Implementation -----------------------------
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddDbContext<AppBaseDbContext>((serviceProvider, opt) =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Database"))
                .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
        });


        // ----------------------------- Repository Services Implementation -----------------------------
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<ITransferTypeRepository, TransferTypeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
