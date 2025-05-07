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
            if (!_executed)
            {
                Console.WriteLine($"[Undo] Order for {_customer.name} canceled before execution.");
                _customer.MarkCanceled();
            }
            else
            {
                Console.WriteLine($"[Undo] Too late to cancel {_customer.name}, drink already made.");
            }
        }
        public void Redo()
        {
            if (!_executed)
            {
                Console.WriteLine($"[Machine] Reprocessing {_customer.name}'s order...");
                Execute();
            }
        }
    }
}