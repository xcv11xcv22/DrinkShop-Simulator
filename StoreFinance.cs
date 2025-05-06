using DrinkShop.Events;
namespace DrinkShop
{
    public class StoreFinance
    {
        private static readonly StoreFinance _instance = new StoreFinance();
        public static StoreFinance Instance => _instance;

        public double TotalRevenue { get; private set; } = 0;

        public void ReceivePayment(double amount)
        {
            
            _instance.TotalRevenue += amount;
            EventBus.Instance.Publish(new PaymentEvent( _instance.TotalRevenue, amount));
            Console.WriteLine($"💰 收到付款：{amount}，累積總收入：{ _instance.TotalRevenue}");
        }

        public void Refund(double amount)
        {
             _instance.TotalRevenue -= amount;
            EventBus.Instance.Publish(new PaymentEvent( _instance.TotalRevenue, amount));
            Console.WriteLine($"🔁 退款：{amount}，目前總收入：{ _instance.TotalRevenue}");
        }
    }
}

