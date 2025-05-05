namespace DrinkShop
{
    public interface ISealingMachine
    {
        void EnqueueDrink(Drink drink, Customer customer);
    }
}
