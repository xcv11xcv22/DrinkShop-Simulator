namespace DrinkShop.Events
{
    public class CustomerReviewEvent : IEvent
    {
        public Customer Customer { get; }

        public CustomerReviewEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
