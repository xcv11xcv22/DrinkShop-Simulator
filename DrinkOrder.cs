namespace DrinkShop
{
    public class DrinkOrder
    {
         public string DrinkName { get; private set; }
        public string Sweetness { get; private set; }
        public string Ice { get; private set; }
        public double Price { get; }

        public IDrinkMakingStrategy CustomStrategy { get; private set; } // 新增！
        public List<IDrinkMakingStrategy> StrategyPipeline { get; set; }
        public VIPLevel VipLevel { get; private set; } = VIPLevel.None;

        public DrinkOrder(string drinkName, string sweetness, string ice, IDrinkMakingStrategy customStrategy = null, VIPLevel vipLevel = VIPLevel.None)
        {
            DrinkName = drinkName;
            Sweetness = sweetness;
            Ice = ice;
            VipLevel = vipLevel;
            StrategyPipeline = StrategyCache.Instance.GetStrategies(drinkName);
            if (CustomStrategy != null)
            {
                // 有VIP特別策略，插到最前面或最後面
                StrategyPipeline.Insert(0, CustomStrategy); // 插在最前面，VIP優先處理
            }
            Price = CalculatePrice();
        }
        private double CalculatePrice()
        {
            // 可依字串判斷定價邏輯
            double basePrice = 40;
            if (DrinkName.Contains("Matcha")) basePrice += 10;
            if (DrinkName.Contains("Taro")) basePrice += 5;
            if (DrinkName.Contains("Extra Pearl")) basePrice += 10;
            return basePrice;
        }
        public override string ToString()
        {
            return $"{DrinkName} - {Sweetness} - {Ice}";
        }
    }
}
