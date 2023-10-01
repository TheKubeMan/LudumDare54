using System;
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
        [SerializeField] private bool inStomach;
        [Inject] private FoodScorer _foodScorer;
        private FoodPool _foodPool = new FoodPool();
        private Random rnd = new Random();
        public int SpawnedFoodCount { get; private set; }

        private void Start()
        {
            DontDestroyOnLoad(foodParent.gameObject);
            SpawnFoodPool();
            SpawnFood();
            
            _foodScorer.SetMaxScore(foodCount);
        }

        private void OnDisable()
        {
            var controllObjects = transform.GetComponentsInChildren<ControllObject>();
            foreach (var obj in controllObjects)
            {
                obj.OnFoodLanding -= SpawnFood;
            }
        }

        public void SpawnFood()
        {
            GameObject food = _foodPool.GetByIndex(SpawnedFoodCount);
            
            food.transform.position = spawnPoint.transform.position;
            food.SetActive(true);
            SpawnedFoodCount++;
            
            _foodScorer.AddScore(1);
        }

        public void SpawnFoodObject(GameObject gameObject)
        {
            var food = Instantiate(gameObject, Vector3.zero, quaternion.identity, foodParent);
            food.SetActive(false);
            _foodPool.Add(food);
            food.GetComponent<ControllObject>().OnFoodLanding += SpawnFood;
        }
    
        private void SpawnFoodPool()
        {
            for (int i = 0; i < foodCount; i++)
            {
                SpawnFoodObject(foodPrefabs[rnd.Next(0, foodPrefabs.Count)]);
            }
        }
    }
}