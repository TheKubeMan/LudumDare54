using System;
using Core;
using FoodSystem.Data;
using UnityEngine;
using Utils;
using Utils.Extensions;
using Zenject;

namespace FoodSystem
{
    public abstract class Food : MonoBehaviour
    {
        [SerializeField] protected CollisionConfig collisionConfig;
        [Inject] private FoodScorer _foodScorer;
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (collisionConfig.GroundMask.Contains(col.gameObject.layer))
            {
                Destroy(gameObject);
            }
            
            if (collisionConfig.LoseMask.Contains(col.gameObject.layer))
            {
                SceneChanger.ReloadScene();
            }
        }
    }
}