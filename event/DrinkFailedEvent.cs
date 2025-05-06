namespace DrinkShop.Events
{
    public class DrinkFailedEvent : IEvent
    {
        public Customer Customer { get; }
        public string Reason { get; }

        public DrinkFailedEvent(Customer customer, string reason)
        {
            Customer = customer;
            Reason = reason;
        }
    }
}