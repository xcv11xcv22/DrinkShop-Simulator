using System;

namespace DrinkShop
{
    public class BaristaOrderCommand : ICommand
    {
        private readonly IDrinkExecutor drinkExecutor;
        private readonly Customer _customer;
        private bool _executed = false;

        public BaristaOrderCommand(IDrinkExecutor barista, Customer customer)
        {
            drinkExecutor = barista;
            _customer = customer;
        }

        public void Execute()
        {
            if (!_executed)
            {
                _executed = true;
                if (_customer.Pay(_customer.Order.Price))
                {
                    drinkExecutor.MakeDrinkForCustomer(_customer);
                    StoreFinance.Instance.ReceivePayment(_customer.Order.Price);
                    _executed = true;
                }
                else
                {
                    Console.WriteLine($"❌ {_customer.name} 錢不夠，無法購買 {_customer.Order.DrinkName}");
                    _customer.SetAngry();
                }

            }
        }

        public void Undo()
        {
            if (!_executed)
            {
                Console.WriteLine($"[Undo] Order for {_customer.name} canceled before execution.");
                _customer.MarkCanceled();
            }
            else
            {
                Console.WriteLine($"[Undo] Too late to cancel {_customer.name}, drink already made.");
            }
        }

        public void Redo()
        {
            if (!_executed)
            {
                Console.WriteLine($"[Redo] Redoing order for {_customer.name}");
                Execute();
            }
        }
          public double GetMakingTime()
        {
            IList<IDrinkMakingStrategy> strategies = _customer.Order.StrategyPipeline;
            double totalTime = 0;
            bool hasVipStrategy = false;

            foreach (var strategy in strategies)
            {
                totalTime += strategy.GetMakingTime();

                if (strategy is VIPSuperFastStrategy)
                {
                    hasVipStrategy = true;
                }
            }

            if (hasVipStrategy)
            {
                totalTime = (int)(totalTime * GetVipSpeedModifier());
            }
            var traits = drinkExecutor.GetTraits();
            for (int i = 0; i < traits.Count(); i++)
            {
                totalTime = traits[i].AdjustTime(_customer.Order, totalTime);
            }
            return totalTime;
        }

        private double GetVipSpeedModifier()
        {
            switch (_customer.Order.VipLevel)
            {
                case VIPLevel.Silver:
                    return 0.8; // 普通VIP製作時間×0.8
                case VIPLevel.Gold:
                    return 0.5; // 白金VIP製作時間×0.5
                case VIPLevel.Diamond:
                    return 0.3; // 鑽石VIP製作時間×0.3
                default:
                    return 1.0; // 非VIP
            }
        }
    }
}