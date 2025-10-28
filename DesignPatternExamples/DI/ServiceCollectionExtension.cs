using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserManagement(this IServiceCollection services)
    {
        // MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));

        // Repository
        // services.AddScoped<IUserRepository, MockUserRepository>();

        // Application Service
        services.AddScoped<UserService>();

        return services;
    }
    
    public static IServiceCollection AddDecoratedDataService(this IServiceCollection services)
    {
        services.AddMemoryCache();
        
        services.AddScoped<IDataService>(provider =>
        {
            var baseService = new DatabaseDataService(
                provider.GetRequiredService<ILogger<DatabaseDataService>>());
            
            var cachedService = new CachingDataServiceDecorator(
                baseService,
                provider.GetRequiredService<IMemoryCache>(),
                provider.GetRequiredService<ILogger<CachingDataServiceDecorator>>());
            
            var loggedService = new LoggingDataServiceDecorator(
                cachedService,
                provider.GetRequiredService<ILogger<LoggingDataServiceDecorator>>());
            
            return loggedService;
        });

        return services;
    }
}