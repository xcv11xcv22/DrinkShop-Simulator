using DrinkShop.Equipment;

namespace DrinkShop.Factory
{
    public static class DrinkBuilderFactory
    {
        public static IDrinkBuilder Create(DrinkOrder order, IIceMachine iceMachine)
        {
            string name = order.DrinkName;

            if (name.Contains("Taro Milk"))
                return new PearlMilkTeaBuilder(order, iceMachine);
            else if (name.Contains("Matcha"))
                return new MatchaLatteBuilder(order, iceMachine);
            else
                return new BasicDrinkBuilder(order, iceMachine);
        }
    }
}