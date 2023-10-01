using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTagCheck : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.25 || Mathf.Abs(rb.velocity.x) > 0.25)
            gameObject.tag = "food";
        else
            gameObject.tag = "Finish";
    }
}
