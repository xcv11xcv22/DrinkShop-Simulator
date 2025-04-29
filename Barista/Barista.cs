using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkShop
{

    public class Barista
    {
        private Queue<ICommand> orderQueue = new Queue<ICommand>();
        private Stack<ICommand> canceledStack = new Stack<ICommand>();
        private bool isProcessing = false;
        private Random rand = new Random();

        public string Name { get; private set; }
        public BaristaLevel Level { get; private set; }
        public int Exp { get; private set; } = 0;

        private List<IBaristaTrait> traits = new List<IBaristaTrait>();

        public Barista(string name, BaristaLevel level)
        {
            Name = name;
            Level = level;
        }
        public void EnqueueOrder(ICommand command)
        {
            orderQueue.Enqueue(command);
            Console.WriteLine($"Barista:{Name}: Received a new order.");

            if (!isProcessing)
            {
                _ = ProcessOrdersAsync();
                isProcessing =true;
            }
        }
        public void AddTrait(IBaristaTrait trait)
        {
            traits.Add(trait);
            Console.WriteLine($"{Name} 獲得了新特性：{trait.GetType().Name}");
        }
        public List<IBaristaTrait> GetTraits()
        {
            return traits;
        }

        private void GainExp(int amount)
        {
            Exp += amount;
            Console.WriteLine($"{Name} 獲得 {amount} 經驗，目前經驗 {Exp}");

            CheckForLevelUp();
        }
        private void CheckForLevelUp()
        {
            if (Level == BaristaLevel.Beginner && Exp >= 100)
            {
                Level = BaristaLevel.Professional;
                Console.WriteLine($"{Name} 升級為 Professional！");
            }
            else if (Level == BaristaLevel.Professional && Exp >= 300)
            {
                Level = BaristaLevel.Master;
                Console.WriteLine($"{Name} 升級為 Master！");
            }
        }
        public int GetQueueCount(){
            return orderQueue.Count;
        }
        public void CancelNextOrder()
        {
            if (orderQueue.Count > 0)
            {
                ICommand command = orderQueue.Dequeue();
                command.Undo();
                canceledStack.Push(command);
            }
            else
            {
                Console.WriteLine("No orders to cancel.");
            }
        }

        public void RedoLastCanceledOrder()
        {
            if (canceledStack.Count > 0)
            {
                ICommand command = canceledStack.Pop();
                command.Redo();
            }
            else
            {
                Console.WriteLine("No canceled orders to redo.");
            }
        }

        private async Task ProcessOrdersAsync()
        {

            while (true)
            {
               if (orderQueue.Count > 0)
                {
                    ICommand currentCommand = orderQueue.Peek();

                    if (currentCommand is OrderCommand orderCommand)
                    {
                        double baseDelay = orderCommand.GetMakingTime();
                        double speedModifier = GetSpeedModifier();
                        int adjustedDelay = (int)(baseDelay * speedModifier);

                        Console.WriteLine($"{Name} ({Level}) 製作飲料預計 {adjustedDelay} 毫秒完成");

                        await Task.Delay(adjustedDelay);
                        GainExp(10);
                        orderQueue.Dequeue();
                        currentCommand.Execute();
                    }
                    else
                    {
                        await Task.Delay(1000); // 萬一指令不是OrderCommand，延遲一下保護
                    }
                }
                else{
                    await Task.Delay(500); 
                }
            }
        }
        
        public void MakeDrinkForCustomer(Customer customer)
        {
            if (customer.HasLeft())
            {
                Console.WriteLine($"Barista:{Name}: Customer already left. Skip making {customer.Order.DrinkName}.");
                return;
            }

            Console.WriteLine($"Barista: Start making {customer.Order.DrinkName}...");

            Random rand = new Random();
           
            IDrinkBuilder builder;

            if (customer.Order.DrinkName.Contains("Taro Milk"))
            {
                builder = new PearlMilkTeaBuilder(customer.Order);
            }
            else if (customer.Order.DrinkName.Contains("Matcha"))
            {
                builder = new MatchaLatteBuilder(customer.Order);
            }
            else
            {
                builder = new BasicDrinkBuilder(customer.Order);
            }
        
            double adjustedFailChance = builder.FailChance * GetLevelFailModifier() *GetVipFailModifier(customer.Order);
            foreach (var trait in traits)
            {
                adjustedFailChance = trait.AdjustFailChance(customer.Order, adjustedFailChance);
            }
            if (rand.NextDouble() < adjustedFailChance)
            {
                Console.WriteLine($"Barista:{Name}: Oops! Failed to make {customer.Order.DrinkName}. 😢");
                customer.SetAngry();
                return;
            }

           
            builder.AddBase();
            builder.AddSweetness(customer.Order.Sweetness);
            builder.AddIce(customer.Order.Ice);
            builder.Mix();
            builder.FinalizeDrink(customer.Order);

            Drink drink = builder.GetResult();

            Console.WriteLine($"Barista:{Name}: Finished making {drink.Name} (Base: {drink.Base}, Sweetness: {drink.Sweetness}, Ice: {drink.Ice}, " +
                            $"Pearl: {(drink.HasPearl ? "Yes" : "No")}, Matcha: {(drink.HasMatcha ? "Yes" : "No")}).");

            customer.ReceiveDrink(drink);
        }
        private double GetVipFailModifier(DrinkOrder order)
        {
            return order.VipLevel switch
            {
                VIPLevel.Silver => 0.8,  // 銀卡VIP：失敗率 ×0.8
                VIPLevel.Gold => 0.5,    // 金卡VIP：失敗率 ×0.5
                VIPLevel.Diamond => 0.2, // 鑽石VIP：失敗率 ×0.2（幾乎不會失敗）
                _ => 1.0,                // 普通顧客，失敗率不變
            };
        }

        private double GetSpeedModifier()
        {
            return Level switch
            {
                BaristaLevel.Beginner => 1.0,     // Beginner 正常速度
                BaristaLevel.Professional => 0.8, // Professional -20%
                BaristaLevel.Master => 0.6,        // Master -40%
                _ => 1.0,
            };
        }
        private double GetLevelFailModifier()
        {
            return Level switch
            {
                BaristaLevel.Beginner => 1.0,
                BaristaLevel.Professional => 0.5,
                BaristaLevel.Master => 0.2,
                _ => 1.0,
            };
        }

    }
}
