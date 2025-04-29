using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkShop
{
    public class StoreManager
    {
        private List<Customer> customers = new List<Customer>();
        private List<Barista> baristas = new List<Barista>();
        private Random rand = new Random();

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void AddBarista(Barista barista)
        {
            baristas.Add(barista);
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
                            var selectedBarista = baristas
                            .OrderBy(x => x.GetQueueCount())
                            .FirstOrDefault();
                            OrderCommand command = new OrderCommand(selectedBarista, c);
                            selectedBarista.EnqueueOrder(command);

                            c.HasOrderAssigned = true;
                        }
                    }
                }

                await Task.Delay(1000);
            }
        }

    }
}
