using System;

namespace DrinkShop
{
    public class FruitTeaStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making a fresh fruit tea...");
            drink.Base = "Tea";
            drink.Sweetness = order.Sweetness;
            drink.Ice = order.Ice;
            drink.Mixed = true;
            drink.IsCompleted = true;
        }

        public int GetMakingTime()
        {
            return 7000; // 7秒，要加水果
        }
    }
}
