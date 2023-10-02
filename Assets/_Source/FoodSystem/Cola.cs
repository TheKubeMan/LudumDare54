using UnityEngine;
using Utils.Extensions;

namespace FoodSystem
{
    public class Cola : Food
    {
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (collisionConfig.GroundMask.Contains(col.gameObject.layer))
            {
                OnGroundEnterInvoke();
                Destroy(gameObject);
            }
        }

        protected void OnCollisionEnter2D(Collision2D col)
        {
            if (collisionConfig.FoodMask.Contains(col.gameObject.layer))
            {
                Destroy(col.gameObject);
            }
            OnGroundEnterInvoke();
            Destroy(gameObject);
        }
        
        public override void ScorerUpdate(int value)
        {
            FoodScorer.AddScore(0);
        }
    }
}