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
        public event Action OnAllStomachFoodSpawned;
        public static List<GameObject> LandedFood = new List<GameObject>();
        [SerializeField] private List<GameObject> foodPrefabs;
        [SerializeField] private GameObject cola;
        [SerializeField] private int foodCountMin;
        [SerializeField] private int foodCountMax;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform foodParent;
        [SerializeField] private int foodCount;
        [SerializeField] private float sizeForOneCola;
        [SerializeField] private bool inStomach;
        [field:SerializeField] public float WinFoodPercentile { get; private set; }
        [Inject] private FoodSpawnPoint _foodSpawnPoint;
        [Inject] private FoodScorer _foodScorer;
        [Inject] private DiContainer _diContainer;
        private FoodPool _foodPool = new FoodPool();
        private Random rnd = new Random();
        private float _totalFoodSize;
        private bool _spawnerFinish;
        public int SpawnedFoodCount { get; private set; }

        private void Awake()
        {
            if (!inStomach)
            {
                var playerMaxScore = PlayerPrefs.GetInt("MaxScore");
                LandedFood.Clear();
                if (playerMaxScore < foodCountMin)
                {
                    foodCount = rnd.Next(foodCountMin, foodCountMax+1);
                }
                else
                {
                    foodCount = rnd.Next(playerMaxScore, foodCountMax + 1 + playerMaxScore - foodCountMin);
                }
            }
            else
            {
                foodCount = LandedFood.Count;
            }
            _foodScorer.SetMaxScore(foodCount);

            _foodSpawnPoint.OnStomachSpawnPointFill += SpawnerFinish;
        }

        private void Start()
        {
            SpawnFoodPool();
            Spawn();
        }

        private void OnDisable()
        {
            var controllObjects = transform.GetComponentsInChildren<ControllObject>();
            foreach (var obj in controllObjects)
            {
                obj.OnLanding -= Spawn;
            }
            
            _foodSpawnPoint.OnStomachSpawnPointFill -= SpawnerFinish;
        }

        public void Spawn()
        {
            if (_spawnerFinish) return;
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

        public void SpawnFoodObject(GameObject gameObject, Type type)
        {
            var food = _diContainer.InstantiatePrefab(gameObject, spawnPoint.transform.position, quaternion.identity, foodParent);
            food.SetActive(false);
            if (type == typeof(Food))
            {
                _foodPool.Add(food);
            }
            else if(type == typeof(Cola))
            {
                _foodPool.Insert(food, rnd.Next(1, _foodPool.Count()-1));
            }
            food.GetComponent<ControllObject>().OnLanding += Spawn;
            _totalFoodSize += food.GetComponent<Food>().Size;
        }
    
        private void SpawnFoodPool()
        {
            Debug.Log("SpawnFoodPool");
            if (!inStomach)
            {
                for (int i = 0; i < foodCount; i++)
                {
                    SpawnFoodObject(foodPrefabs[rnd.Next(0, foodPrefabs.Count)], typeof(Food));
                }
            }
            else
            {
                for (int i = 0; i < foodCount; i++)
                {
                    SpawnFoodObject(LandedFood[i],typeof(Food));
                }

                for (int i = 0; i < _totalFoodSize / sizeForOneCola; i++)
                {
                    SpawnFoodObject(cola,typeof(Cola));
                    foodCount++;
                }
            }
        }

        private void SpawnerFinish()
        {
            Debug.Log("SpawnerFinish");
            _spawnerFinish = true;
            if (inStomach)
            {
                if (PlayerPrefs.HasKey("MaxScore"))
                {
                    if (PlayerPrefs.GetInt("MaxScore") < _foodScorer.Score)
                    {
                        PlayerPrefs.SetInt("MaxScore", _foodScorer.Score);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("MaxScore", _foodScorer.Score);
                }
                OnAllStomachFoodSpawned?.Invoke();
            }
            else
            {
                SceneChanger.LoadSceneBySceneIndex(2);
            }
        }
    }
}