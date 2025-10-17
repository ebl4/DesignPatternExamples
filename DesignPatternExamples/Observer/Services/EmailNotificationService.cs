public class EmailNotificationService : IOrderObserver
{
    public void OnOrderStatusChanged(string orderId, OrderStatus status)
    {
        Console.WriteLine($"Email: Order {orderId} is now {status}");
        // Lógica de envio de email...
    }
}