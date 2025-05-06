using System;

namespace DrinkShop
{
    public class MachineOrderCommand : ICommand
    {
        private readonly IAutoDrinkMaker _machine;
        private readonly Customer _customer;
        private bool _executed = false;
        public MachineOrderCommand(IAutoDrinkMaker machine, Customer customer)
        {
            _machine = machine;
            _customer = customer;
        }

        public void Execute()
        {
        
            if (!_executed)
            {
                // drinkExecutor.MakeDrinkForCustomer(_customer);
                
                _executed = true;
                if (_customer.Pay(_customer.Order.Price))
                {
                    Console.WriteLine($"[Machine] Auto-processing drink for {_customer.name}...");
                    _machine.ProcessAutoDrink(_customer);
                    StoreFinance.Instance.ReceivePayment(_customer.Order.Price);
                    _executed = true;
                }
                else
                {
                    Console.WriteLine($"❌ {_customer.name} 錢不夠，無法購買 {_customer.Order.DrinkName}");
                    _customer.SetAngry();
                }

            }
        }

        public void Undo()
        {
            Console.WriteLine($"[Machine] Cannot undo automatic machine operation for {_customer.name}.");
        }

        public void Redo()
        {
            Console.WriteLine($"[Machine] Cannot redo automatic machine operation for {_customer.name}.");
        }
    }
}