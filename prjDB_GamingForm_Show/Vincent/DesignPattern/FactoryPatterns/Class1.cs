namespace prjDB_GamingForm_Show.Vincent.DesignPattern.FactoryPatterns
{
    public interface IBird
    {
        string Name { get; set; }

        void Fly();
    }

    public class Eagle : IBird
    {
        public string Name { get; set; } = "老鷹";

        public void Fly()
        {
            // 實作可以飛高空
        }
    }

    public class Swan : IBird
    {
        public string Name { get; set; } = "天鵝";

        public void Fly()
        {
            // 實作只能飛低空
        }
    }

    public static class BirdFactory
    {
        public static IBird GetBird(string birdName)
        {
            switch (birdName)
            {
                case "Swan":
                    return new Swan();

                case "Eagle":
                    return new Eagle();

                default:
                    throw new Exception("missing matching bird name");
            }
        }
    }

    //private static void Main(string[] args)
    //{
    //    var eagle = BirdFactory.GetBird("Eagle");
    //    var swan = BirdFactory.GetBird("Swan");

    //    Console.WriteLine($"Bird Name : {eagle.Name}");
    //    Console.WriteLine($"Bird Name : {swan.Name}");
    //}
}
