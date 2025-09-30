namespace DesignPatternExamples.Strategy
{
    // Contexto que usa as estratégias
    public class PaymentProcessor
    {
        private readonly IEnumerable<IPaymentStrategy> _strategies;

        public PaymentProcessor(IEnumerable<IPaymentStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, PaymentInfo paymentInfo, PaymentMethod method)
        {
            var strategy = _strategies.FirstOrDefault(s => s.CanProcess(method));

            if (strategy == null)
                throw new InvalidOperationException($"No payment strategy found for {method}");

            return await strategy.ProcessPaymentAsync(amount, paymentInfo);
        }
    }
}
