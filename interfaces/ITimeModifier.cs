namespace DrinkShop
{
    public interface ITimeModifier
    {
        double AdjustTime(DrinkOrder order, double currentTime);
    }
}
