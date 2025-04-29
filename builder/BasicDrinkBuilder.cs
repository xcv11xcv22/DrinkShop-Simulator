using System;

namespace DrinkShop
{
    public class BasicDrinkBuilder : IDrinkBuilder
    {
        protected Drink _drink;
        protected string _drinkName;

        public virtual double FailChance => 0.05;

        public BasicDrinkBuilder(DrinkOrder order)
        {
            _drinkName = order.DrinkName;
            _drink = new Drink(order.DrinkName);

        }

        public virtual void AddBase() { /* 留空，改給策略 */ }

        public virtual void AddSweetness(string level) { /* 留空 */ }

        public virtual void AddIce(string level) { /* 留空 */ }

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
