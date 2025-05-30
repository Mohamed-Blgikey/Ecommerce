using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("customPolicy", opt =>
    {
        opt.PermitLimit = 2;
        opt.Window = TimeSpan.FromSeconds(12);
    });
});

var app = builder.Build();
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
