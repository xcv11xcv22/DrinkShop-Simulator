namespace DrinkShop.Events
{
    public class CustomerEnterEvent : IEvent
    {
        public Customer Customer { get; }


        public CustomerEnterEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}