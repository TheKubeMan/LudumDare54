using UnityEngine;
using Utils.Extensions;

namespace FoodSystem
{
    public class Cola : Food
    {
        private bool inStomach = false;
        protected void OnCollisionEnter2D(Collision2D col)
        {
            if (inStomach && collisionConfig.FoodMask.Contains(col.gameObject.layer))
            {
                Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
