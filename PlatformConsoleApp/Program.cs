using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlatformConsoleApp.Data;
using PlatformConsoleApp.Services;

var builder = Host.CreateApplicationBuilder(args);

// Register database
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString)
);

// Register HttpClient
builder.Services.AddHttpClient("ExternalApi", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiSettings:BaseUrl"]!
    );
});

// Register services
builder.Services.AddTransient<AuthenticationService>(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();

    return new AuthenticationService(
        factory.CreateClient("ExternalApi"),
        provider.GetRequiredService<IConfiguration>()
    );
});

builder.Services.AddTransient<PlatformApiService>(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();

    return new PlatformApiService(
        factory.CreateClient("ExternalApi"),
        provider.GetRequiredService<IConfiguration>()
    );
});

builder.Services.AddScoped<PlatformSyncService>();

using var host = builder.Build();

// Execute application
using var scope = host.Services.CreateScope();

var authService = scope.ServiceProvider
    .GetRequiredService<AuthenticationService>();

var apiService = scope.ServiceProvider
    .GetRequiredService<PlatformApiService>();

var syncService = scope.ServiceProvider
    .GetRequiredService<PlatformSyncService>();

try
{
    // Step 1: Login
    Console.WriteLine("Logging into external API...");

    var token = await authService.LoginAsync();

    Console.WriteLine("Login successful!");

    // Step 2: Retrieve actual data
    Console.WriteLine("Retrieving Platform and Well data...");

    var platforms = await apiService
        .GetPlatformWellActualAsync(token);

    // for testing purposes
    //var platforms = await apiService
    //    .GetPlatformWellDummyAsync(token);

    Console.WriteLine($"Retrieved {platforms.Count} platforms.");

    // Step 3: Synchronize data
    foreach (var platform in platforms)
    {
        await syncService.SyncPlatformAsync(platform);
    }

    Console.WriteLine("Database synchronization completed!");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
}