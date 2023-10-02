using FoodSystem;
using UnityEngine;
using Zenject;

namespace UISystem
{
    public class EndStomachScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        [Inject] private FoodSpawner _foodSpawner;
        [Inject] private FoodScorer _foodScorer;

        private void OnEnable()
        {
            _foodSpawner.OnAllStomachFoodSpawned += EndScreenUpdate;
        }

        private void OnDisable()
        {
            _foodSpawner.OnAllStomachFoodSpawned -= EndScreenUpdate;
        }

        private void EndScreenUpdate()
        {
            if (_foodScorer.MaxScore * _foodSpawner.WinFoodPercentile / 100 < _foodScorer.Score)
            {
                winScreen.SetActive(true);
            }
            else
            {
                loseScreen.SetActive(true);
            }
        }
    }
}
