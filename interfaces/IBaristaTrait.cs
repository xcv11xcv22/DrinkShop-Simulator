namespace DrinkShop
{
    public interface IBaristaTrait : ITimeModifier
    {
        string Name { get; }
        double AdjustFailChance(DrinkOrder order, double currentFailChance);
    }
}
