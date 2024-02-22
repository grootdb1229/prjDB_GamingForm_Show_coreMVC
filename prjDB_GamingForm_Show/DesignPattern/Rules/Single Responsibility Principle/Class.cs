using prjDB_GamingForm_Show.DesignPattern.Rules.Single_Responsibility_Principle;

namespace prjDB_GamingForm_Show.DesignPattern.Rules.Single_Responsibility_Principle
{
    public interface CheckData
    {
        string checkOrder();
        string checkContactInfo();
        string checkPlace();
    }

    public interface Payment
    {
        string paid();
    }

    public class ShoppingCart:CheckData, Payment{
    public string checkOrder()
    {
        return("請確認訂單");
    }
    public string checkContactInfo()
    {
        return("請聯絡資料");
    }
    public string checkPlace()
    {
        return("請收貨地點");
    }
    public string paid()
    {
        return("請付款");
    }
}
public class MyCart
{
    public static void main()
    {
        ShoppingCart cart = new ShoppingCart();
        cart.checkOrder();
        cart.checkContactInfo();
        cart.checkPlace();
        cart.paid();
    }
}
}
