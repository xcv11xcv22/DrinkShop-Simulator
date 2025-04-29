namespace DrinkShop
{
    public interface IDrinkMakingStrategy
    {
        void MakeDrink(Drink drink, DrinkOrder order);
        int GetMakingTime(); // 新增：取得該飲料需要的製作時間 (ms)
    }
}
