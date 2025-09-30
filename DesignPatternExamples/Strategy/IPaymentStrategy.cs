namespace DesignPatternExamples.Strategy
{
    public interface IPaymentStrategy
    {
        Task<PaymentResult> ProcessPaymentAsync(decimal amount, PaymentInfo paymentInfo);
        bool CanProcess(PaymentMethod method);
    }

    public record PaymentResult(bool Success, string TransactionId, string Message);
    public record PaymentInfo(string CardNumber, string HolderName, decimal Amount);
    public enum PaymentMethod { CreditCard, PayPal, Crypto }
}
