// using System;

// namespace DrinkShop
// {
//     public class OrderCommand : ICommand
//     {
//         private IDrinkMaker maker;
//         private Customer _customer;
//         private bool _executed = false;

//         public OrderCommand(IDrinkMaker maker, Customer customer)
//         {
//             this.maker = maker;
//             _customer = customer;
//         }

//         public void Execute()
//         {
//             if (!_executed)
//             {
//                 maker.MakeDrinkForCustomer(_customer);
//                 _executed = true;
//             }
//         }

//         public void Undo()
//         {
//             if (!_executed)
//             {
//                 Console.WriteLine($"Order for {_customer.name} ({_customer.Order.DrinkName}) is canceled. Customer leaves unhappily.");
//                 _customer.MarkCanceled(); // 同步標記顧客取消
//             }
//             else
//             {
//                 Console.WriteLine($"Too late to cancel {_customer.name} ({_customer.Order.DrinkName}), already made.");
//             }
//         }

//         public void Redo()
//         {
//             if (!_executed)
//             {
//                 Console.WriteLine($"Redo order: {_customer.Order.DrinkName}");
//                 Execute();
//             }
//             else
//             {
//                 Console.WriteLine($"Order {_customer.Order.DrinkName} already done, no need redo.");
//             }
//         }
//         // public double GetMakingTime()
//         // {
//         //     IList<IDrinkMakingStrategy> strategies = _customer.Order.StrategyPipeline;
//         //     double totalTime = 0;
//         //     bool hasVipStrategy = false;

//         //     foreach (var strategy in strategies)
//         //     {
//         //         totalTime += strategy.GetMakingTime();

//         //         if (strategy is VIPSuperFastStrategy)
//         //         {
//         //             hasVipStrategy = true;
//         //         }
//         //     }

//         //     if (hasVipStrategy)
//         //     {
//         //         totalTime = (int)(totalTime * GetVipSpeedModifier());
//         //     }
//         //     var traits = _barista.GetTraits();
//         //     for (int i = 0; i < traits.Count(); i++)
//         //     {
//         //         totalTime = traits[i].AdjustMakingTime(_customer.Order, totalTime);
//         //     }
//         //     return totalTime;
//         // }

//         // private double GetVipSpeedModifier()
//         // {
//         //     switch (_customer.Order.VipLevel)
//         //     {
//         //         case VIPLevel.Silver:
//         //             return 0.8; // 普通VIP製作時間×0.8
//         //         case VIPLevel.Gold:
//         //             return 0.5; // 白金VIP製作時間×0.5
//         //         case VIPLevel.Diamond:
//         //             return 0.3; // 鑽石VIP製作時間×0.3
//         //         default:
//         //             return 1.0; // 非VIP
//         //     }
//         // }
//     }
// }
