using Microsoft.Extensions.DependencyInjection;

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
}