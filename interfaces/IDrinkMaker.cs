namespace DrinkShop
{
    public interface IDrinkMaker
    {
        void EnqueueOrder(ICommand command);
        int GetQueueCount();
    }
}
