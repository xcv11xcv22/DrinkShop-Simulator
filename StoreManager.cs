using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrinkShop.Events;
using DrinkShop.Equipment;
namespace DrinkShop
{
    public class StoreManager
    {
        private List<Customer> customers = new List<Customer>();
        private List<IDrinkMaker> devices = new List<IDrinkMaker>();

        private Random rand = new Random();

        private IOrderDispatcher  orderDispatcher;
        private IIceMachine iceMachine;
        private uint count = 0;
        public StoreManager(IOrderDispatcher orderDispatcher, IIceMachine iceMachine){
            this.orderDispatcher = orderDispatcher;
            this.iceMachine = iceMachine;
        }
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            EventBus.Instance.Publish(new CustomerEnterEvent(customer));
        }

        public void AddDevice(IDrinkMaker drinkMaker)
        {
            devices.Add(drinkMaker);
        }
        private IDrinkMaker SelectBestDevice()
        {
            // 優先找 queue 最短的 Barista
            var barista = devices
                .Where(d => d is Barista)
                .OrderBy(d => d.GetQueueCount())
                .FirstOrDefault();

            if (barista != null && barista.GetQueueCount() < 1) // 若人力負載還OK，就派給人
                return barista;

            // 否則找機器
            var machine = devices
                .Where(d => d is not Barista)
                .OrderBy(d => d.GetQueueCount())
                .FirstOrDefault();

            return machine ?? barista; // fallback 萬一沒機器，就還是找人（不管幾杯）
        }

        public async Task MonitorCustomersAsync()
        {
            while (true)
            {
                if (customers.Count == 0)
                {
                    await Task.Delay(1000);
                    continue;
                }

                for (int i = customers.Count - 1; i >= 0; i--)
                {
                    Customer c = customers[i];
                    c.Update();

                    if (c.HasLeft())
                    {
                        switch (c.LeaveReason)
                        {
                            case LeaveReason.Canceled:
                                Console.WriteLine($"{c.name} has left because the order was canceled.");
                                break;
                            case LeaveReason.Angry:
                                Console.WriteLine($"{c.name} has left angrily due to losing patience! 😡");
                                break;
                            default:
                                Console.WriteLine($"{c.name} has left the shop.");
                                break;
                        }

                        customers.RemoveAt(i);
                    }
                    else
                    {
                        // 安排製作（如果還沒安排過）
                        if (!c.HasOrderAssigned)
                        {
                            orderDispatcher.DispatchOrder(c);
                        }
                    }
                }
                count++;
                if(count % 20 == 0){
                    iceMachine.Refill(100);
                }
                await Task.Delay(1000);
            }
        }

    }
}
