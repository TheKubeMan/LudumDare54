using UnityEngine;

namespace FoodSystem.Data
{
    [CreateAssetMenu(fileName = "CollisionConfigSO", menuName = "SO/FoodSystem/CollisionConfigSO")]
    public class CollisionConfig : ScriptableObject
    {
        [field:SerializeField] public LayerMask LoseMask { get; private set; }
        [field:SerializeField] public LayerMask GroundMask { get; private set;}
        [field:SerializeField] public LayerMask FoodMask { get; private set;}
    }
}