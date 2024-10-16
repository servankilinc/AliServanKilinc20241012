using Core.DataAccess;
using DataAccess.Contexts;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------- Database Implementation -----------------------------
builder.Services.AddSingleton<SoftDeleteInterceptor>();
builder.Services.AddDbContext<AppBaseDbContext>((serviceProvider, opt) =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
        .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
});

// ----------------------------- Rate Limiter Implementation -----------------------------
var _policyName = "sliding";
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddSlidingWindowLimiter(policyName: _policyName, slidingOptions =>
    {
        slidingOptions.PermitLimit = 10;
        slidingOptions.Window = TimeSpan.FromSeconds(15);
        slidingOptions.SegmentsPerWindow = 4;
        slidingOptions.QueueLimit = 2;
        slidingOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting(_policyName);

app.Run();
