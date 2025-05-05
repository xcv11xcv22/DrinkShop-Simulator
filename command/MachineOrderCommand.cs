using System;

namespace DrinkShop
{
    public class MachineOrderCommand : ICommand
    {
        private readonly IAutoDrinkMaker _machine;
        private readonly Customer _customer;

        public MachineOrderCommand(IAutoDrinkMaker machine, Customer customer)
        {
            _machine = machine;
            _customer = customer;
        }

        public void Execute()
        {
            Console.WriteLine($"[Machine] Auto-processing drink for {_customer.name}...");
            _machine.ProcessAutoDrink(_customer);
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