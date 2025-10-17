// Event args customizados
public class OrderEventArgs : EventArgs
{
    public string OrderId { get; }
    public OrderStatus Status { get; }
    public DateTime Timestamp { get; }

    public OrderEventArgs(string orderId, OrderStatus status)
    {
        OrderId = orderId;
        Status = status;
        Timestamp = DateTime.UtcNow;
    }
}