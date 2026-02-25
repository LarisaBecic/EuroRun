using Stripe;

namespace EuroRunAPI.Helpers
{
    public class PaymentHelper
    {
        public static async Task<string> VerifyPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var intent = await service.GetAsync(paymentIntentId);

            return intent.Status;
        }
    }
}
