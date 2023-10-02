using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FoodSystem.Data;
using Unity.Mathematics;
using UnityEngine;
using Utils;
using Zenject;
using Random = System.Random;

namespace FoodSystem
{
    public class FoodSpawner : MonoBehaviour
    {
        public event Action OnFoodSpawn;
        public static List<GameObject> LandedFood = new List<GameObject>();
        [SerializeField] private List<GameObject> foodPrefabs;
        [SerializeField] private int foodCount;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform foodParent;
        [SerializeField] private bool inStomach;
        [Inject] private FoodScorer _foodScorer;
        [Inject] private DiContainer _diContainer;
        private FoodPool _foodPool = new FoodPool();
        private Random rnd = new Random();
        public int SpawnedFoodCount { get; private set; }

        private void Awake()
        {
            _foodScorer.SetMaxScore(foodCount);
            if (!inStomach)
            {
                LandedFood.Clear();
            }
            else
            {
                foodCount = LandedFood.Count;
            }
        }

        private void Start()
        {
            SpawnFoodPool();
            SpawnFood();
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
            if (SpawnedFoodCount >= _foodPool.Count())
            {
                SpawnerFinish();
                return;
            }
            
            var food = _foodPool.GetByIndex(SpawnedFoodCount);

            food.transform.position = spawnPoint.transform.position;
            food.SetActive(true);
            SpawnedFoodCount++;
            
            OnFoodSpawn?.Invoke();
        }

        public void SpawnFoodObject(GameObject gameObject)
        {
            var food = _diContainer.InstantiatePrefab(gameObject, Vector3.zero, quaternion.identity, foodParent);
            food.SetActive(false);
            _foodPool.Add(food);
            food.GetComponent<ControllObject>().OnFoodLanding += SpawnFood;
        }
    
        private void SpawnFoodPool()
        {
            if (!inStomach)
            {
                for (int i = 0; i < foodCount; i++)
                {
                    SpawnFoodObject(foodPrefabs[rnd.Next(0, foodPrefabs.Count)]);
                }
            }
            else
            {
                for (int i = 0; i < foodCount; i++)
                {
                    SpawnFoodObject(LandedFood[i]);
                }
            }
        }

        private void SpawnerFinish()
        {
            Debug.Log("SpawnerFinish");
            SceneChanger.LoadSceneBySceneIndex(2);
        }
    }
}