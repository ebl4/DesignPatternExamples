public interface IOrderObserver
{
    void OnOrderStatusChanged(string orderId, OrderStatus status);
}