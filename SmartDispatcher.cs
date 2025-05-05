namespace DrinkShop
{
    public class SmartDispatcher : IOrderDispatcher
    {
        private readonly List<IDrinkMaker> _devices;

        public SmartDispatcher(List<IDrinkMaker> devices)
        {
            _devices = devices;
        }

        public void DispatchOrder(Customer customer)
        {
            // 取最閒的設備（可以是 Barista 或 Machine）
            var selected = _devices.OrderBy(d => d.GetQueueCount()).FirstOrDefault();

            if (selected == null)
            {
                Console.WriteLine("No available drink maker.");
                return;
            }

            var command = OrderCommandFactory.Create(selected, customer);
            selected.EnqueueOrder(command);
            customer.HasOrderAssigned = true;

            Console.WriteLine($"Dispatched {customer.name} to {selected.GetType().Name}");
        }
    }

}
