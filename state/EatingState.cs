using System;

namespace DrinkShop
{
    public class EatingState : ICustomerState
    {
        public void OnEnter(Customer customer)
        {
            Console.WriteLine("Customer: Eating happily... 🍴");
        }

        public void Update(Customer customer)
        {
            customer.Hunger--;

            if (customer.Hunger <= 0)
            {
                Console.WriteLine("Customer: Finished eating and left happily! 🏃‍♂️");
                customer.ChangeState(new HappyState()); // 或者直接離開
            }
        }

        public void ReceiveDrink(Customer customer, Drink drink)
        {
            Console.WriteLine("Customer: Already eating, no need for drink.");
        }

        public void ReceiveDelay(Customer customer)
        {
            // 正在吃飯，忽略delay
        }
    }
}
