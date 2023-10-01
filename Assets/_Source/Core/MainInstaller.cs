using FoodSystem;
using FoodSystem.Data;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FoodScorer>().AsSingle().NonLazy();
        }
    }
}