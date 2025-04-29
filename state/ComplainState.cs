using System;

namespace DrinkShop
{
    public class ComplainState : PatienceWaitingState
    {
        private int _patienceDecreaseRate = 2;
        public override void OnEnter(Customer customer)
        {
            Console.WriteLine($"{customer.name}: Complaining loudly! Service is too slow! 😡");
        }

        protected override int GetPatienceDecreaseRate()
        {
            return _patienceDecreaseRate; // 抱怨中，每秒扣_patienceDecreaseRate
        }

        public override void ReceiveDrink(Customer customer, Drink drink)
        {
            customer.ChangeState(new HappyState());
        }

        public override void ReceiveDelay(Customer customer)
        {
            Console.WriteLine("Customer: Still no drink?! Getting even angrier...");
            _patienceDecreaseRate++;
        }
    }
}
