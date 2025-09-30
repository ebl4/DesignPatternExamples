namespace DesignPatternExamples.Strategy
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, PaymentInfo paymentInfo)
        {
            // Simulação de processamento
            await Task.Delay(1000);
            return new PaymentResult(true, Guid.NewGuid().ToString(), "Credit card payment processed");
        }

        public bool CanProcess(PaymentMethod method) => method == PaymentMethod.CreditCard;
    }
}
