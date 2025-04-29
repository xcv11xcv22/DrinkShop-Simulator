using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace DrinkShop
{
    public class StrategyCache
    {
        private static readonly StrategyCache _instance = new StrategyCache();

        private readonly List<StrategyEntry> _strategyList;

        private StrategyCache()
        {
            _strategyList = new List<StrategyEntry>
            {
                new StrategyEntry { Pattern = new Regex(@"Milk", RegexOptions.IgnoreCase), Priority = 10, Strategy = new MilkTeaStrategy() },
                new StrategyEntry { Pattern = new Regex(@"Matcha", RegexOptions.IgnoreCase), Priority = 10, Strategy = new MatchaLatteStrategy() },
                new StrategyEntry { Pattern = new Regex(@"Pearl", RegexOptions.IgnoreCase), Priority = 20, Strategy = new ExtraPearlStrategy() },
                new StrategyEntry { Pattern = new Regex(@".*", RegexOptions.IgnoreCase), Priority = 1, Strategy = new BasicDrinkStrategy() } // fallback用
            };
        }

        public static StrategyCache Instance => _instance;

        public void RegisterStrategy(string pattern, int priority, IDrinkMakingStrategy strategy)
        {
            _strategyList.Add(new StrategyEntry
            {
                Pattern = new Regex(pattern, RegexOptions.IgnoreCase),
                Priority = priority,
                Strategy = strategy
            });
            Console.WriteLine($"[StrategyCache] 註冊新策略: {pattern}, Priority={priority}");
        }

        public List<IDrinkMakingStrategy> GetStrategies(string drinkName)
        {
            var matched = _strategyList
                .Where(entry => entry.Pattern.IsMatch(drinkName))
                .OrderByDescending(entry => entry.Priority)
                .ToList();

            if (matched.Count > 0)
                return matched.Select(x => x.Strategy).ToList();

            return new List<IDrinkMakingStrategy> { _strategyList.First(x => x.Pattern.ToString() == ".*").Strategy };
        }
        
    }
}
