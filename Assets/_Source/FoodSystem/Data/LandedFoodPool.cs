using UnityEngine;

namespace FoodSystem.Data
{
    public static class LandedFoodPool
    {
        public static FoodPool FoodPool = new FoodPool();

        public static void Add(GameObject pool)
        {
            FoodPool.Add(pool);
        }

        public static void ClearPool()
        {
            FoodPool = new FoodPool();
        }
    }
}