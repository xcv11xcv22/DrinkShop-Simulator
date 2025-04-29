namespace DrinkShop
{
    public interface IBaristaTrait
    {
        double AdjustMakingTime(DrinkOrder order, double currentTime);
        double AdjustFailChance(DrinkOrder order, double currentFailChance);
    }
}
