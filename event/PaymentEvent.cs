namespace DrinkShop.Events
{
    public class PaymentEvent : IEvent
    {
        public Customer Customer { get; }

        public double Price { get; }
        public double Total { get; }

        public PaymentEvent(double total, double price)
        {
            Price = price;
            Total = total;
            // Customer = customer;
        }
    }
}