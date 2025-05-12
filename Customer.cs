using DrinkShop.Events;
namespace DrinkShop
{
    public class Customer
    {
        public DrinkOrder Order { get; private set; }
        public int Patience { get; set; } = 20;
        public int Hunger { get; set; } = 15; // 若有吃東西流程

        private ICustomerState _currentState;

        private Random rand = new Random();
        public string name;
        public bool HasOrderAssigned;
        public LeaveReason LeaveReason { get; private set; } = LeaveReason.None;
        private bool _isCanceled = false;
        public double Wallet { get; private set; } = 100;

        public int ReviewScore { get; private set; } = -1;

      
        public Customer(string iname)
        {
            name = iname;
            GenerateRandomOrder();
            ChangeState(new WaitingState());
            
        }
     
        private void GenerateRandomOrder()
        {
            // "Milk Tea", "Green Tea", "Black Tea", "Oolong Tea", "Taro Milk", "Matcha Tea" 
            string[] drinks = {"Milk Pearl Tea", "Milk Tea", "Green Tea", "Black Tea", "Oolong Tea", "Taro Milk", "Matcha Tea" };
            string[] sweetnessLevels = { "Full Sugar", "Half Sugar", "Less Sugar", "No Sugar" };
            string[] iceLevels = { "More Ice", "Regular Ice", "Less Ice", "No Ice", "Hot" };

            string randomDrink = drinks[rand.Next(drinks.Length)];
            string randomSweetness = sweetnessLevels[rand.Next(sweetnessLevels.Length)];
            string randomIce = iceLevels[rand.Next(iceLevels.Length)];

            // Order = new DrinkOrder(randomDrink, randomSweetness, randomIce);
              // 隨機決定是否要客製化
            if (rand.NextDouble() < 0.3) // 30%客人是VIP
            {
                Console.WriteLine($"{name}: 我要特別客製化飲料喔！");
                VIPLevel level = VIPLevel.Diamond;
                Order = new DrinkOrder(randomDrink, randomSweetness, randomIce, new VIPSuperFastStrategy(), level);
            }
            else
            {
                Order = new DrinkOrder(randomDrink, randomSweetness, randomIce);
            }
            Order.StrategyPipeline = StrategyCache.Instance.GetStrategies(randomDrink);
        }
        public void MarkCanceled()
        {
            _isCanceled = true;
            LeaveReason = LeaveReason.Canceled;
        }
        public void SetAngry()
        {
            LeaveReason = LeaveReason.Angry;
            ChangeState(new AngryState());
        }
        public void ChangeState(ICustomerState newState)
        {
            _currentState = newState;
           
            _currentState.OnEnter(this);
        }
        public ICustomerState GetState(){
            return _currentState;
        }
        public void Update()
        {
            _currentState.Update(this);
        }

        public void ReceiveDrink(Drink drink)
        {
            _currentState.ReceiveDrink(this, drink);
            EventBus.Instance.Publish(new CustomerReviewEvent(this));
    
        }
        public void SubmitReview(int score)
        {
            ReviewScore = Math.Clamp(score, 1, 5);
            Console.WriteLine($"顧客 {name} 留下評分：{ReviewScore} 分");
            ReviewManager.Instance.Record(this, ReviewScore);
        }
        public void ReceiveDelay()
        {
            _currentState.ReceiveDelay(this);
        }

        public bool HasLeft()
        {
              return _currentState is AngryState || _isCanceled;
        }

        public void PrintOrder()
        {
            Console.WriteLine($"{name} Order: {Order}");
        }
        public bool Pay(double amount)
        {
            if (Wallet >= amount)
            {
                Wallet -= amount;
                return true;
            }
            return false;
        }

        public void Refund(double amount)
        {
            Wallet += amount;
        }
    }
}
