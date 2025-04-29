using System;

namespace DrinkShop
{
    public class HappyState : ICustomerState
    {
        public void OnEnter(Customer customer)
        {
            Console.WriteLine($"{customer.name}: Thank you! (Happy) 😊");
        }

        public void Update(Customer customer)
        {
            // Happy，不做任何事
        }

        public void ReceiveDrink(Customer customer, Drink drink)
        {
            Console.WriteLine($"{customer.name}: Already happy, no need more drink.");
        }

        public void ReceiveDelay(Customer customer)
        {
            // Happy，不會生氣
        }
    }
}
