using static prjDB_GamingForm_Show.Vincent.DesignPattern.FactoryPatterns.Restaurant;

namespace prjDB_GamingForm_Show.Vincent.DesignPattern.FactoryPatterns
{
    public interface CookMeal
    {
        string cook();
        string delivery();
    }

    class Steak : CookMeal
    {


        public string cook()
        {
            return "把牛排煮熟";
        }

        public string delivery()
        {
            return "送牛排";
        }
    }

    class Chicken : CookMeal
    {

        public string cook()
        {
            return "把雞肉煮熟";
        }

        public string delivery()
        {
            return "送雞肉";
        }
    }

    class Pork : CookMeal
    {

        public string cook()
        {
            return "把雞肉煮熟";
        }

        public string delivery()
        {
            return "送雞肉";
        }
    }

    class Restaurant
    {
        CookMeal mealOrder(string mealType)
        {
            CookMeal meal = null;

            if ("Steak".Equals(mealType))
            {
                meal = new Steak();
            }
            else if ("Chicken".Equals(mealType))
            {
                meal = new Chicken();
            }
            else if ("Pork".Equals(mealType))
            {
                meal = new Pork();
            }

            return meal;
        }
    }

    class MealFactory
    {
        public CookMeal createMeal(string mealType)
        {
            CookMeal meal = null;

            if (mealType.Equals("Steak"))
            {
                meal = new Steak();
            }
            else if (mealType.Equals("Chicken"))
            {
                meal = new Chicken();
            }
            else if (mealType.Equals("Pork"))
            {
                meal = new Pork();
            }
            return meal;
        }
    }

}
