using DrinkShop.Events;
using DrinkShop.Equipment;
using DrinkShop.Factory;

namespace DrinkShop
{
    public class VendingMachine : IDrinkMaker, IAutoDrinkMaker
    {
        private Queue<ICommand> orderQueue = new Queue<ICommand>();
        bool isProcessing = false;
        private readonly IIceMachine _iceMachine;
        public void EnqueueOrder(ICommand command)
        {
            orderQueue.Enqueue(command);
            Console.WriteLine($"[VendingMachine]{Name} Received a new order.");

             if (!isProcessing)
            {
                _ = ProcessOrdersAsync();
                isProcessing =true;
            }
        }
        private string Name ;
        public VendingMachine(string name, IIceMachine iceMachine){
            Name = name;
            _iceMachine = iceMachine;
        }
        public int GetQueueCount()
        {
            return orderQueue.Count;
        }

        // 模擬自動快速出飲料
        public async Task ProcessOrdersAsync()
        {
            while (true)
            {
                if (orderQueue.Count > 0)
                {
                    ICommand cmd = orderQueue.Dequeue();
                    await Task.Delay(2000); // 自動販賣機固定2秒出貨
                    cmd.Execute();
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
        public void ProcessAutoDrink(Customer customer)
        {
             if (customer.HasLeft())
            {
                Console.WriteLine($"VendingMachine {Name}: Customer already left. Skip making {customer.Order.DrinkName}.");
                return;
            }

            Console.WriteLine($"VendingMachine {Name} Start making {customer.Order.DrinkName}...");

            IDrinkBuilder builder = DrinkBuilderFactory.Create(customer.Order, _iceMachine);
            builder.AddBase();
            builder.AddSweetness(customer.Order.Sweetness);
            builder.AddIce(customer.Order.Ice);
            builder.Mix();
            builder.FinalizeDrink(customer.Order);

            Drink drink = builder.GetResult();

            Console.WriteLine($"VendingMachine {Name} : Finished making {drink.Name} (Base: {drink.Base}, Sweetness: {drink.Sweetness}, Ice: {drink.Ice}, " +
                $"Pearl: {(drink.HasPearl ? "Yes" : "No")}, Matcha: {(drink.HasMatcha ? "Yes" : "No")}).");
            EventBus.Instance.Publish(new DrinkCompletedEvent(customer, drink));
            customer.ReceiveDrink(drink); // 交給顧客
        }
    }
}
