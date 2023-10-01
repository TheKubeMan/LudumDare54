using System.Collections.Generic;
using FoodSystem.Data;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace FoodSystem
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> foodPrefabs;
        [SerializeField] private int foodCount;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform foodParent;
        [Inject] private FoodScorer _foodScorer;
        private FoodPool _foodPool = new FoodPool();
        private Random rnd = new Random();
        private bool _inStomach;
        public int SpawnedFoodCount { get; private set; }

        private void Awake()
        {
            SpawnFoodPool();
            _foodScorer.SetMaxScore(foodCount);
        }

        public void SpawnFood()
        {
            var food = _foodPool.GetByIndex(SpawnedFoodCount);
            food.transform.position = spawnPoint.transform.position;
            food.SetActive(true);
            SpawnedFoodCount++;
        }

        public void SpawnFoodObject(GameObject gameObject)
        {
            var food = Instantiate(gameObject, Vector3.zero, quaternion.identity, foodParent);
            food.SetActive(false);
            _foodPool.Add(food);
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
            for (int i = 0; i < LandedFoodPool.FoodPool.Count(); i++)
            {
                SpawnFoodObject(LandedFoodPool.FoodPool.GetByIndex(i));
            }
        }
    }
}