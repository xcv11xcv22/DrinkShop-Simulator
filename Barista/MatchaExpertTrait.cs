namespace DrinkShop
{
    public class MatchaExpertTrait : IBaristaTrait
    {
        public string Name => " Matcha";
        public double AdjustTime(DrinkOrder order, double currentTime)
        {
            if (order.DrinkName.Contains("Matcha"))
            {
                Console.WriteLine("[Trait] 抹茶專家加速製作！");
                return currentTime * 0.5; // 抹茶類飲料製作時間 -50%
            }
            return currentTime;
        }

        public double AdjustFailChance(DrinkOrder order, double currentFailChance)
        {
            if (order.DrinkName.Contains("Matcha"))
            {
                Console.WriteLine("[Trait] 抹茶專家減少失敗率！");
                return currentFailChance * 0.7; // 抹茶類飲料失敗率 -30%
            }
            return currentFailChance;
        }
    }
}
