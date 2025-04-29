using System;

namespace DrinkShop
{
    public class WaitingState : PatienceWaitingState
    {
        public override void OnEnter(Customer customer)
        {
            Console.WriteLine($"{customer.name} is calmly waiting...");
        }

        protected override int GetPatienceDecreaseRate()
        {
            return 1; // 正常等待，每秒扣1點
        }

        public override void ReceiveDrink(Customer customer, Drink drink)
        {
            customer.ChangeState(new HappyState());
        }

        public override void ReceiveDelay(Customer customer)
        {
            Console.WriteLine("Customer: It's taking too long...");
            customer.ChangeState(new ComplainState());
            
        }
    }
}
