namespace DrinkShop.Events
{
    public class DrinkCompletedEvent : IEvent
    {
        public Customer Customer { get; }
        public Drink Drink { get; }

        public DrinkCompletedEvent(Customer customer, Drink drink)
        {
            Customer = customer;
            Drink = drink;
        }
    }
}