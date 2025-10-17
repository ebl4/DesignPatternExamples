// Subject (Observable)
public class OrderService
{
    private readonly List<IOrderObserver> _observers = new();

    public event EventHandler<OrderEventArgs>? OrderStatusChanged;

    public void Attach(IOrderObserver observer) => _observers.Add(observer);
    public void Detach(IOrderObserver observer) => _observers.Remove(observer);

    public void UpdateOrderStatus(string orderId, OrderStatus newStatus)
    {
        // Lógica de atualização...
        Console.WriteLine($"Order {orderId} status changed to {newStatus}");
        
        OnOrderStatusChanged(new OrderEventArgs(orderId, newStatus));
        NotifyObservers(orderId, newStatus);
    }

    protected virtual void OnOrderStatusChanged(OrderEventArgs e)
    {
        OrderStatusChanged?.Invoke(this, e);
    }

    private void NotifyObservers(string orderId, OrderStatus status)
    {
        foreach (var observer in _observers)
        {
            observer.OnOrderStatusChanged(orderId, status);
        }
    }
}