using Microsoft.Extensions.DependencyInjection;

namespace DesignPatternExamples.Strategy
{
    // Configuração DI
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPaymentStrategies(this IServiceCollection services)
        {
            services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();
            services.AddScoped<IPaymentStrategy, PayPalPaymentStrategy>();
            services.AddScoped<PaymentProcessor>();
            return services;
        }
    }

}
