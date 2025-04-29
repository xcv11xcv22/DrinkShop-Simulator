namespace DrinkShop
{
    public class MilkTeaStrategy : IDrinkMakingStrategy
    {
        public void MakeDrink(Drink drink, DrinkOrder order)
        {
            Console.WriteLine("Making a  milk tea...");
            drink.Base = "Milk";
            drink.Sweetness = order.Sweetness;
            drink.Ice = order.Ice;
            drink.IsCompleted = true;
        }

        public int GetMakingTime()
        {
            return 4000; // 
        }
    }
}

