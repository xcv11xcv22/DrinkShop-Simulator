using System;

namespace DrinkShop
{
    public class AngryState : ICustomerState
    {
        public void OnEnter(Customer customer)
        {
            Console.WriteLine($"{customer.name}: I'm angry and leaving! 😡");
        }

        public void Update(Customer customer)
        {
            // Angry，什麼也不做
        }

        public void ReceiveDrink(Customer customer, Drink drink)
        {
            Console.WriteLine($"{customer.name}: Too late. Received {drink.Name} but I'm angry. 😡");
        }

        public void ReceiveDelay(Customer customer)
        {
            // Angry，無所謂
        }
    }
}
