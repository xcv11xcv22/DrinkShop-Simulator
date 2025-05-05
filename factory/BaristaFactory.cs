// namespace DrinkShop
// {
//     public static class BaristaFactory
//     {
//         private static Random rand = new Random();

//         public static Barista CreateRandomBarista(string name)
//         {
//             int level = rand.Next(0, 3); // 0: Beginner, 1: Professional, 2: Master
//             BaristaLevel baristaLevel = (BaristaLevel)level;
//             return new Barista(name, baristaLevel);
//         }

//         public static Barista CreateSpecificBarista(string name, BaristaLevel level)
//         {
//             return new Barista(name, level);
//         }
//     }
// }
