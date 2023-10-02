using FoodSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace UISystem
{
    public class LeftFoodView : MonoBehaviour
    {
        [SerializeField] private TMP_Text foodScoreText;
        [Inject] private FoodScorer _foodScorer;
        [Inject] private FoodSpawner _foodSpawner;

        private void OnEnable()
        {
            _foodSpawner.OnFoodSpawn += LeftFoodUpdate;
        }

        private void OnDisable()
        {
            _foodSpawner.OnFoodSpawn -= LeftFoodUpdate;
        }

        private void Start()
        {
            LeftFoodUpdate();
        }

        private void LeftFoodUpdate()
        {
            foodScoreText.text = $"{_foodSpawner.SpawnedFoodCount}/{_foodScorer.MaxScore}";
        }
    }
}
