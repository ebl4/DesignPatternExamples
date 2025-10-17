public class InventoryService : IOrderObserver
{
    public void OnOrderStatusChanged(string orderId, OrderStatus status)
    {
        if (status == OrderStatus.Confirmed)
        {
            Console.WriteLine($"Inventory: Updating stock for order {orderId}");
            // Lógica de atualização de inventário...
        }
    }
}