using UnityEngine;

public static class LandedObjectPool
{
    public static ObjectPool _objectPool = new ObjectPool();

    public static void Add(GameObject pool)
    {
        _objectPool.Add(pool);
    }

    public static void ClearPool()
    {
        _objectPool = new ObjectPool();
    }
}