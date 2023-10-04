using System;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace FoodSystem
{
    public class FoodSpawnPoint : MonoBehaviour
    {
        public event Action OnStomachSpawnPointFill;
        [SerializeField] private float upDistance;
        [SerializeField] private Vector2 xOffset;
        [SerializeField] private float doubleFoodCheckRadius;
        [SerializeField] private LayerMask foodLayerMask;
        [SerializeField] private bool inStomach;
        [Inject] private FoodSpawner _foodSpawner;
        private bool _stomachSpawnPointFill;
        private Collider2D[] _overlapResults = new Collider2D[10];
        private Random _rnd = new Random();

        private void Awake()
        {
            if (!inStomach)
            {
                _foodSpawner.OnFoodSpawn += MoveHorizontal; 
            }
        }

        private void OnDisable()
        {
            if (!inStomach)
            {
                _foodSpawner.OnFoodSpawn -= MoveHorizontal; 
            }
        }

        private void Update()
        {
            if(Physics2D.OverlapCircleNonAlloc(transform.position, doubleFoodCheckRadius, _overlapResults, foodLayerMask) >= 2)
            {
                if (!inStomach)
                {
                    MoveUp();
                }
                else if(!_stomachSpawnPointFill)
                {
                    _stomachSpawnPointFill = true;
                    OnStomachSpawnPointFill?.Invoke();
                }
            }
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.75F);
            Gizmos.DrawWireSphere(transform.position, doubleFoodCheckRadius);
        }

        private void MoveUp()
        {
            transform.position += new Vector3(0, upDistance, 0);
        }

        private void MoveHorizontal()
        {
            if (_rnd.Next(0, 2) == 0)
            {
                transform.position += new Vector3(UnityEngine.Random.Range(xOffset[0], xOffset[1]), 0, 0);
            }
            else
            {
                transform.position -= new Vector3(UnityEngine.Random.Range(xOffset[0], xOffset[1]), 0, 0);
            }
        }
    }
}
