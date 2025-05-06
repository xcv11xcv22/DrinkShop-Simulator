using System;
namespace DrinkShop.Factory
{
    public static class OrderCommandFactory
    {
        public static ICommand Create(IDrinkMaker maker, Customer customer)
        {
            return maker switch
            {
                IDrinkExecutor b => new BaristaOrderCommand(b, customer),
                IAutoDrinkMaker vm => new MachineOrderCommand(vm, customer),
                _ => throw new NotSupportedException($"Unsupported drink maker type: {maker.GetType().Name}")
            };
        }
    }
}