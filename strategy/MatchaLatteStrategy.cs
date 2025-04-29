namespace DrinkShop
{
    public class MatchaLatteStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making a matcha latte...");
            drink.Base = "Milk";
            drink.Sweetness = order.Sweetness;
            drink.Ice = order.Ice;
            drink.HasMatcha = true;
            drink.Mixed = true;
            drink.IsCompleted = true;
        }

        public int GetMakingTime()
        {
            return 10000; // 10秒，因為抹茶要慢慢拌
        }
    }
}

