namespace DrinkShop
{
    public interface ICustomerState
    {
       void OnEnter(Customer customer);
        void Update(Customer customer);         // 通用每秒Tick
        void ReceiveDrink(Customer customer, Drink drink); // 拿到飲料
        void ReceiveDelay(Customer customer);   // 等太久觸發（可以抱怨）
    }
}
