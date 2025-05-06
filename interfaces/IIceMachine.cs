namespace DrinkShop.Equipment
{
    public interface IIceMachine
    {
        int IceAmount { get; }
        bool CanDispense(string level);
        void Dispense(string level);
        void Refill(int amount);
    }
}