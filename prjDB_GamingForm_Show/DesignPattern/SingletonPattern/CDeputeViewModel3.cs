namespace prjDB_GamingForm_Show.DesignPattern.Rules.SingletonPattern
{
    public class CDeputeViewModel3 { 
    private int quantity = 100;
    private static class LazyHolder
    {
        internal static CDeputeViewModel3 uniqueInstance = new CDeputeViewModel3();
    }
    private CDeputeViewModel3()
    {
    }
    public static CDeputeViewModel3 getInstance()
    {
        return LazyHolder.uniqueInstance;
    }

    }

}
