namespace DrinkShop
{
    public class PearlMasterTrait : IBaristaTrait
    {
        public string Name => "Pearl";
        public double AdjustTime(DrinkOrder order, double currentTime)
        {
            if (order.DrinkName.Contains("Pearl"))
            {
                Console.WriteLine("[Trait] 珍珠達人加速製作！");
                return currentTime * 0.85; // 珍珠類飲料製作時間 -15%
            }
            return currentTime;
        }

        public double AdjustFailChance(DrinkOrder order, double currentFailChance)
        {
            if (order.DrinkName.Contains("Pearl"))
            {
                Console.WriteLine("[Trait] 珍珠達人減少失敗率！");
                return currentFailChance * 0.6; // 珍珠類飲料失敗率 -40%
            }
            return currentFailChance;
        }
    }
}
