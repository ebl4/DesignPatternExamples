namespace DesignPatternExamples.Strategy
{
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, PaymentInfo paymentInfo)
        {
            await Task.Delay(800);
            return new PaymentResult(true, Guid.NewGuid().ToString(), "PayPal payment processed");
        }

        public bool CanProcess(PaymentMethod method) => method == PaymentMethod.PayPal;
    }
}
