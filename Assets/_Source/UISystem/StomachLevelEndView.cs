using System;
using FoodSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace UISystem
{
    public class StomachLevelEndView : MonoBehaviour
    {
        [SerializeField] private GameObject endMenu;
        [Inject] private FoodSpawner _foodSpawner;

        private void OnEnable()
        {
            _foodSpawner.OnAllStomachFoodSpawned += EndMenuActiveTrue;
        }

        private void OnDisable()
        {
            _foodSpawner.OnAllStomachFoodSpawned -= EndMenuActiveTrue;
        }

        private void EndMenuActiveTrue()
        {
            endMenu.SetActive(true);
        }
    }
}
