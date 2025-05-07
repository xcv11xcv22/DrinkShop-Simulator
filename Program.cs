﻿
using DrinkShop.Equipment;

namespace DrinkShop
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Drink Shop Simulator!");
            IIceMachine iceMachine = new DefaultIceMachine();
            ISealingMachine sealingMachine = new SealingMachine();
            Barista barista1 = new Barista("Alice", BaristaLevel.Professional, sealingMachine, iceMachine);
            Barista barista2 = new Barista("Bob", BaristaLevel.Beginner, sealingMachine, iceMachine);
            Barista barista3 = new Barista("Charlie", BaristaLevel.Beginner, sealingMachine, iceMachine);
            // Barista barista4 = new Barista("Boss", BaristaLevel.Master);
            Barista barista5 = new Barista("AOA", BaristaLevel.Beginner, sealingMachine, iceMachine);

            VendingMachine vendingMachine1 = new VendingMachine("robotA", iceMachine);
            VendingMachine vendingMachine2 = new VendingMachine("robotB", iceMachine);
            MatchaExpertTrait matchaExpertTrait = new MatchaExpertTrait();
            barista1.AddTrait(matchaExpertTrait);
            barista2.AddTrait(matchaExpertTrait);
            barista3.AddTrait(matchaExpertTrait);

            var makers = new List<IDrinkMaker> { barista1, barista2, barista3, barista5, vendingMachine1, vendingMachine2 };
            IOrderDispatcher dispatcher = new SmartDispatcher(makers);
            StoreManager storeManager = new StoreManager(dispatcher, iceMachine);
            // storeManager.AddBarista(barista6);
            // storeManager.AddBarista(barista7);
            StrategyCache.Instance.RegisterStrategy("Fruit Tea", 7, new FruitTeaStrategy());

            _ = storeManager.MonitorCustomersAsync(); // 背景扣耐心、安排飲料製作

            for (int i = 0; i < 200; i++)
            {
                Customer customer = new Customer($"Customer{i + 1}");
                customer.PrintOrder();
                storeManager.AddCustomer(customer);

                await Task.Delay(3000); // 每5秒進一個顧客
            }

            await Task.Delay(80000); // 模擬一段時間
            Console.WriteLine("Simulation End. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
