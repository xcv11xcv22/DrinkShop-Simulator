using System;
using System.Threading.Tasks;
using DrinkShop;

namespace DrinkShop
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Drink Shop Simulator!");

            StoreManager storeManager = new StoreManager();

            Barista barista1 = new Barista("Alice", BaristaLevel.Professional);
            Barista barista2 = new Barista("Bob", BaristaLevel.Beginner);
            Barista barista3 = new Barista("Charlie", BaristaLevel.Beginner);
            // Barista barista4 = new Barista("Boss", BaristaLevel.Master);
            Barista barista5 = new Barista("AOA", BaristaLevel.Beginner);
            // Barista barista6 = new Barista("B", BaristaLevel.Master);
            // Barista barista7 = new Barista("C", BaristaLevel.Master);
            storeManager.AddBarista(barista1);
            storeManager.AddBarista(barista2);
            storeManager.AddBarista(barista3);
            // storeManager.AddBarista(barista4);
            storeManager.AddBarista(barista5);
            barista1.AddTrait(new MatchaExpertTrait());
            barista2.AddTrait(new MatchaExpertTrait());
            barista3.AddTrait(new MatchaExpertTrait());
            // storeManager.AddBarista(barista6);
            // storeManager.AddBarista(barista7);
            StrategyCache.Instance.RegisterStrategy("Fruit Tea", 7, new FruitTeaStrategy());

            _ = storeManager.MonitorCustomersAsync(); // 背景扣耐心、安排飲料製作

            for (int i = 0; i < 200; i++)
            {
                Customer customer = new Customer($"Customer{i + 1}");
                customer.PrintOrder();
                storeManager.AddCustomer(customer);

                await Task.Delay(2000); // 每5秒進一個顧客
            }

            await Task.Delay(80000); // 模擬一段時間
            Console.WriteLine("Simulation End. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
