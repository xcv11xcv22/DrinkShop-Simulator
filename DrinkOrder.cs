namespace DrinkShop
{
    public class DrinkOrder
    {
         public string DrinkName { get; private set; }
        public string Sweetness { get; private set; }
        public string Ice { get; private set; }
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
        }

        public override string ToString()
        {
            return $"{DrinkName} - {Sweetness} - {Ice}";
        }
    }
}
