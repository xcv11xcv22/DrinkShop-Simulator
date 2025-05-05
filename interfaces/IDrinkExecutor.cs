
namespace DrinkShop
{
    public interface IDrinkExecutor
    {
        void MakeDrinkForCustomer(Customer customer);
        List<IBaristaTrait> GetTraits();
        BaristaLevel Level { get; }
    }
}