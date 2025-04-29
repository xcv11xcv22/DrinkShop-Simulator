using System;

namespace DrinkShop
{
    public class ExtraPearlStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making a drink with Extra Pearls!");
            drink.Sweetness = order.Sweetness;
            drink.Ice = order.Ice;
            drink.HasPearl = true; // 加更多珍珠
            drink.Mixed = true;
            drink.IsCompleted = true;
        }

        public int GetMakingTime()
        {
            return 5000; // 加很多珍珠比較慢
        }
    }
}
