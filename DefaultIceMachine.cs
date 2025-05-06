namespace DrinkShop.Equipment
{
    public class DefaultIceMachine : IIceMachine
    {
        private int _iceAmount = 100;

        public int IceAmount => _iceAmount;

        public bool CanDispense(string level)
        {
            return _iceAmount >= IceCost(level);
        }

        public void Dispense(string level)
        {
            int cost = IceCost(level);
            if (_iceAmount >= cost)
            {
                _iceAmount -= cost;
            }
        }

        public void Refill(int amount)
        {
            _iceAmount += amount;
        }

        private int IceCost(string level)
        {
            return level switch
            {
                "More Ice" => 10,
                "Regular Ice" => 5,
                "Less Ice" => 3,
                "No Ice" or "Hot" => 0,
                _ => 5
            };
        }
    }
}
