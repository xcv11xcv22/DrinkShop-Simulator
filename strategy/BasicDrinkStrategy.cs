namespace DrinkShop
{
    public class BasicDrinkStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making a basic drink...");
            drink.Base = order.DrinkName.Contains("Tea") ? "Tea" : "Milk";
            drink.Sweetness = order.Sweetness;
            drink.Ice = order.Ice;
            drink.Mixed = true;
            drink.IsCompleted = true;
        }

        public int GetMakingTime()
        {
            return 5000; // 5秒
        }
    }

}