using System;
using FoodSystem.Data;
using UnityEngine;
using Utils;
using Utils.Extensions;
using Zenject;

namespace FoodSystem
{
    public abstract class Food : MonoBehaviour
    {
        public event Action OnGroundEnter;
        [SerializeField] protected CollisionConfig collisionConfig;
        [field:SerializeField] protected float Size { get; private set; }
        [Inject] public FoodScorer FoodScorer { get; private set; }
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (collisionConfig.GroundMask.Contains(col.gameObject.layer))
            {
                OnGroundEnter?.Invoke();
                Destroy(gameObject);
            }
            
            if (collisionConfig.LoseMask.Contains(col.gameObject.layer))
            {
                SceneChanger.ReloadScene();
            }
        }
    }
}