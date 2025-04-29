namespace DrinkShop
{
    public interface IDrinkBuilder
    {
        void AddBase(); // 加基底
        void AddSweetness(string level); // 加甜度
        void AddIce(string level); // 加冰量
        void Mix(); // 攪拌
        void FinalizeDrink(DrinkOrder order); // 完成飲料
        Drink GetResult();
        double FailChance { get; } // 失敗機率
    }
}
