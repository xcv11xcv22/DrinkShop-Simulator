using System.Text.RegularExpressions;

namespace DrinkShop
{
    public class StrategyEntry
    {
        public Regex Pattern { get; set; }       // ✅ 改成正則表達式
        public int Priority { get; set; }         // 權重
        public IDrinkMakingStrategy Strategy { get; set; } // 策略
    }
}
