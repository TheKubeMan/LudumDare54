using FoodSystem;
using FoodSystem.Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private FoodSpawner foodSpawner;
        [SerializeField] private FoodSpawnPoint foodSpawnPoint;
        public override void InstallBindings()
        {
            Container.Bind<FoodScorer>().AsSingle().NonLazy();
            Container.Bind<FoodSpawner>().FromInstance(foodSpawner).AsSingle().NonLazy();
            Container.Bind<FoodSpawnPoint>().FromInstance(foodSpawnPoint).AsSingle().NonLazy();
        }
    }
}