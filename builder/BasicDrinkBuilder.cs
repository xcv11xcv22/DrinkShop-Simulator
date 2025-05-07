using System;
using DrinkShop.Equipment;
using DrinkShop.Events;
namespace DrinkShop
{
    public class BasicDrinkBuilder : IDrinkBuilder
    {
        protected Drink _drink;
        protected string _drinkName;

        public virtual double FailChance => 0.05;
        private readonly IIceMachine _iceMachine;
        public BasicDrinkBuilder(DrinkOrder order, IIceMachine iceMachine)
        {
            _drinkName = order.DrinkName;
            _drink = new Drink(order.DrinkName);
            _iceMachine = iceMachine;
        }

        public virtual void AddBase() { /* 留空，改給策略 */ }

        public virtual void AddSweetness(string level) { /* 留空 */ }

        // public virtual void AddIce(string level) { /* 留空 */ }
        public virtual void AddIce(string level)
        {
            if (!_iceMachine.CanDispense(level))
            {
                Console.WriteLine("冰塊不足，無法加冰，已跳過。");
                EventBus.Instance.Publish(new FeatureUnavailableEvent("IceMachine", "冰塊不足"));
                return;
            }
            _iceMachine.Dispense(level);
            _drink.Ice = level;
            Console.WriteLine($"已加冰（{level}），剩餘冰塊：{_iceMachine.IceAmount}");
        }
        public virtual void Mix() { /* 留空 */ }

        public virtual void FinalizeDrink(DrinkOrder order)
        {
            Console.WriteLine($"Using strategy to make drink: {_drinkName}");
            foreach (var strategy in order.StrategyPipeline)
            {
                strategy.MakeDrink(_drink, order);
            }
        }

        public Drink GetResult()
        {
            return _drink;
        }
    }
}
