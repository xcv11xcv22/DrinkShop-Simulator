using System;

namespace DrinkShop
{
    public class VIPSuperFastStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making VIP super fast drink (skip all steps)...");
            drink.Base = "Special Base";
            drink.Sweetness = "Custom Sweetness";
            drink.Ice = "Custom Ice";
            drink.Mixed = true;
            drink.IsCompleted = true;
        }
        public int GetMakingTime()
        {
            return 0; 
        }
    }
}
