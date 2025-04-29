namespace DrinkShop
{
    public class Drink
    {
        public string Name { get; private set; }
        public string Base { get; set; }
        public string Sweetness { get; set; }
        public string Ice { get; set; }
        public bool Mixed { get; set; }
        public bool IsCompleted { get; set; }
        public bool HasPearl { get; set; }
        public bool HasMatcha { get; set; }

        public Drink(string name)
        {
            Name = name;
        }
    }
}
