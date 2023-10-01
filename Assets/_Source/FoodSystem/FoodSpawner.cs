using System.Collections.Generic;
using FoodSystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;
    [SerializeField] private int foodCount;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform foodParent;
    [Inject] private FoodScorer _foodScorer;
    private ObjectPool _objectPool = new ObjectPool();
    private Random rnd = new Random();
    private int _spawnedFoodCount;
    private bool _inStomach;

    private void Awake()
    {
        SpawnFoodPool();
        _foodScorer.SetMaxScore(foodCount);
    }

    public void SpawnFood()
    {
        var food = _objectPool.GetByIndex(_spawnedFoodCount);
        food.transform.position = spawnPoint.transform.position;
        food.SetActive(true);
        _spawnedFoodCount++;
    }

    public void SpawnFoodObject(GameObject gameObject)
    {
        var food = Instantiate(gameObject, Vector3.zero, quaternion.identity, foodParent);
        food.SetActive(false);
        _objectPool.Add(food);
    }
    
    private void SpawnFoodPool()
    {
        for (int i = 0; i < foodCount; i++)
        {
            SpawnFoodObject(foodPrefabs[rnd.Next(0, foodPrefabs.Count)]);
        }
    }

    private void SpawnLandedFoodPool()
    {
        for (int i = 0; i < LandedObjectPool._objectPool.Count(); i++)
        {
            SpawnFoodObject(LandedObjectPool._objectPool.GetByIndex(i));
        }
    }
}