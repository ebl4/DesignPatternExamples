public class AnalyticsService : IOrderObserver
{
    public void OnOrderStatusChanged(string orderId, OrderStatus status)
    {
        Console.WriteLine($"Analytics: Recording order {orderId} status change to {status}");
        // Lógica de analytics...
    }
}