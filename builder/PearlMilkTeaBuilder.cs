using System;

namespace DrinkShop
{
    public class PearlMilkTeaBuilder : BasicDrinkBuilder
    {
        public PearlMilkTeaBuilder(DrinkOrder order) : base(order) { }
        public override double FailChance => 0.1; // 10% 失敗率
        public override void AddBase()
        {
            base.AddBase();
            Console.WriteLine($"Adding pearls to {_drinkName}...");
            _drink.HasPearl = true;
        }
    }
}
