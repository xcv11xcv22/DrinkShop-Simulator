using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkShop
{
    public class SealingMachine : ISealingMachine
    {
        private Queue<(Drink drink, Customer customer)> queue = new Queue<(Drink, Customer)>();
        private bool isProcessing = false;

        public void EnqueueDrink(Drink drink, Customer customer)
        {
            queue.Enqueue((drink, customer));
            Console.WriteLine($"[SealingMachine] 收到 {drink.Name} 準備封杯");

            if (!isProcessing)
            {
                _ = ProcessSealingAsync();
            }
        }

        private async Task ProcessSealingAsync()
        {
            isProcessing = true;

            while (true)
            {
                if (queue.Count >= 2)
                {
                    // 一次封兩杯
                    var (drink1, customer1) = queue.Dequeue();
                    var (drink2, customer2) = queue.Dequeue();

                    Console.WriteLine($"[SealingMachine] 同時封杯中：{drink1.Name} & {drink2.Name}");
                    await Task.Delay(2000); // 假設封兩杯也需要2秒
                    Console.WriteLine($"[SealingMachine] {drink1.Name} & {drink2.Name} 封杯完成！");

                    customer1.ReceiveDrink(drink1);
                    customer2.ReceiveDrink(drink2);
                }
                else if (queue.Count == 1)
                {
                    // 如果只剩一杯，等一下看有沒有第二杯進來
                    await Task.Delay(1000); // 再等一秒鐘
                    if (queue.Count == 1)
                    {
                        var (drink, customer) = queue.Dequeue();
                        Console.WriteLine($"[SealingMachine] 封單杯：{drink.Name}");
                        await Task.Delay(1500); // 單杯稍微快一點
                        Console.WriteLine($"[SealingMachine] {drink.Name} 封杯完成！");
                        customer.ReceiveDrink(drink);
                    }
                }
                else
                {
                    await Task.Delay(500); // 沒東西，休息一下
                }
            }
        }
    }
}
