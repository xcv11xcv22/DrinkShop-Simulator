namespace DrinkShop
{
    public class ReviewManager
    {
        private static readonly ReviewManager _instance = new();
        public static ReviewManager Instance => _instance;

        private List<int> _scores = new();

        public void Record(Customer c, int score)
        {
            _scores.Add(score);
            Console.WriteLine($"收到 {c.name} 評分 {score} 分，目前平均 {(double)_scores.Sum() / _scores.Count:0.0}");
        }

        public double Average => _scores.Count > 0 ? _scores.Average() : 0;
    }


}
