// Handlers base
public interface IOrderHandler
{
    IOrderHandler SetNext(IOrderHandler handler);
    Task<OrderResult> HandleAsync(OrderRequest request);
}

public abstract class BaseOrderHandler : IOrderHandler
{
    private IOrderHandler? _nextHandler;

    public IOrderHandler SetNext(IOrderHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual async Task<OrderResult> HandleAsync(OrderRequest request)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(request);
        }
        
        return OrderResult.Success(); // Fim da cadeia
    }

    protected virtual bool CanHandle(OrderRequest request) => true;
}

// Modelos
public record OrderRequest(
    string OrderId,
    decimal Amount,
    string CustomerEmail,
    string PaymentMethod,
    string ShippingAddress,
    List<OrderItem> Items
);

public record OrderItem(string ProductId, string Name, int Quantity, decimal Price);

public record OrderResult(bool IsSuccess, string Message, string HandlerName)
{
    public static OrderResult Success(string message = "", string handlerName = "") 
        => new OrderResult(true, message, handlerName);
    
    public static OrderResult Failure(string message, string handlerName = "") 
        => new OrderResult(false, message, handlerName);
}

// Handlers concretos
public class InventoryCheckHandler : BaseOrderHandler
{
    private readonly IInventoryService _inventoryService;

    public InventoryCheckHandler(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public override async Task<OrderResult> HandleAsync(OrderRequest request)
    {
        foreach (var item in request.Items)
        {
            var available = await _inventoryService.CheckAvailability(item.ProductId, item.Quantity);
            if (!available)
            {
                return OrderResult.Failure(
                    $"Product {item.Name} is out of stock", 
                    nameof(InventoryCheckHandler));
            }
        }

        Console.WriteLine("Inventory check passed");
        return await base.HandleAsync(request);
    }
}

public class FraudDetectionHandler : BaseOrderHandler
{
    private readonly IFraudDetectionService _fraudService;

    public FraudDetectionHandler(IFraudDetectionService fraudService)
    {
        _fraudService = fraudService;
    }

    public override async Task<OrderResult> HandleAsync(OrderRequest request)
    {
        var isFraudulent = await _fraudService.IsSuspiciousOrderAsync(request);
        
        if (isFraudulent)
        {
            return OrderResult.Failure(
                "Order flagged as potentially fraudulent", 
                nameof(FraudDetectionHandler));
        }

        Console.WriteLine("Fraud check passed");
        return await base.HandleAsync(request);
    }
}

public class PaymentProcessingHandler : BaseOrderHandler
{
    private readonly IPaymentGateway _paymentGateway;

    public PaymentProcessingHandler(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public override async Task<OrderResult> HandleAsync(OrderRequest request)
    {
        var paymentResult = await _paymentGateway.ProcessPaymentAsync(
            request.Amount, 
            request.PaymentMethod);
        
        if (!paymentResult.Success)
        {
            return OrderResult.Failure(
                $"Payment failed: {paymentResult.ErrorMessage}", 
                nameof(PaymentProcessingHandler));
        }

        Console.WriteLine("Payment processed successfully");
        return await base.HandleAsync(request);
    }
}

public class ShippingPreparationHandler : BaseOrderHandler
{
    private readonly IShippingService _shippingService;

    public ShippingPreparationHandler(IShippingService shippingService)
    {
        _shippingService = shippingService;
    }

    public override async Task<OrderResult> HandleAsync(OrderRequest request)
    {
        var shippingLabel = await _shippingService.CreateShippingLabelAsync(
            request.ShippingAddress, 
            request.Items);
        
        Console.WriteLine($"Shipping label created: {shippingLabel}");
        return OrderResult.Success("Order processed successfully", nameof(ShippingPreparationHandler));
    }
}

// Serviços de suporte (mocks)
public interface IInventoryService
{
    Task<bool> CheckAvailability(string productId, int quantity);
}

public interface IFraudDetectionService
{
    Task<bool> IsSuspiciousOrderAsync(OrderRequest request);
}

public interface IPaymentGateway
{
    Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod);
}

public interface IShippingService
{
    Task<string> CreateShippingLabelAsync(string address, List<OrderItem> items);
}

public record PaymentResult(bool Success, string TransactionId, string ErrorMessage = "");

// Configuração da cadeia
public class OrderProcessingPipeline
{
    private readonly IOrderHandler _pipeline;

    public OrderProcessingPipeline(
        IInventoryService inventoryService,
        IFraudDetectionService fraudService,
        IPaymentGateway paymentGateway,
        IShippingService shippingService)
    {
        var inventoryHandler = new InventoryCheckHandler(inventoryService);
        var fraudHandler = new FraudDetectionHandler(fraudService);
        var paymentHandler = new PaymentProcessingHandler(paymentGateway);
        var shippingHandler = new ShippingPreparationHandler(shippingService);

        // Montar a cadeia
        inventoryHandler
            .SetNext(fraudHandler)
            .SetNext(paymentHandler)
            .SetNext(shippingHandler);

        _pipeline = inventoryHandler;
    }

    public async Task<OrderResult> ProcessOrderAsync(OrderRequest request)
    {
        return await _pipeline.HandleAsync(request);
    }
}