namespace DrinkShop
{
    public abstract class PatienceWaitingState : ICustomerState
    {
        protected int secondsPassed = 0;
        private Random rand = new Random();
        public virtual void OnEnter(Customer customer)
        {
            Console.WriteLine("Customer is waiting with patience...");
        }

        public virtual void Update(Customer customer)
        {
            int rate = GetPatienceDecreaseRate();
            Console.WriteLine($"{customer.name} 耐心減少{rate},剩下{customer.Patience-rate}");
            customer.Patience -= GetPatienceDecreaseRate();

            if (customer.Patience <= 0)
            {

                customer.SetAngry();
                customer.SubmitReview(1);
            }
            if(rand.NextDouble() > 0.5)
                secondsPassed++;
            if(secondsPassed % 5 == 0 && secondsPassed > 0){
                ReceiveDelay(customer);
            }
        }

        protected virtual int GetPatienceDecreaseRate()
        {
            return 1; // 預設每秒扣 1 點
        }

        public abstract void ReceiveDrink(Customer customer, Drink drink);
        public abstract void ReceiveDelay(Customer customer);
    }
}
