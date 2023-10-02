using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using FoodSystem.Data;
using UnityEngine;

[RequireComponent(typeof(Food))]
public class ControllObject : MonoBehaviour
{
    public event Action OnFoodFullLanding;
    [field:SerializeField] public GameObject StomachPrefab { get; private set; }
    Rigidbody2D rb;
    bool landed;
    public float rSpeed;
    public float rFriction;
    public float rSmoothness;
    float rotateVal;
    Quaternion RotateTo;
    string i;
    public string index;
    private Food _food;
    
    // Start is called before the first frame update
    void Start()
    {
        landed = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        _food = GetComponent<Food>();
        _food.OnGroundEnter += RemoveFromLandedFood;
        _food.OnGroundEnter += OnFoodFullLandingInvoke;
    }

    private void OnDestroy()
    {
        _food.OnGroundEnter -= RemoveFromLandedFood;
        _food.OnGroundEnter -= OnFoodFullLandingInvoke;
    }

    void Update()
    {
        if (!landed)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            if (moveX != 0)
                rb.velocity = new Vector2(moveX * 3, rb.velocity.y);
            rotateVal += Input.GetAxis("Vertical") * rSpeed * rFriction;
            RotateTo = Quaternion.Euler(0, 0, rotateVal);
            transform.rotation = Quaternion.Lerp(transform.rotation, RotateTo, Time.deltaTime * rSmoothness);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if ((other.tag == "Finish" || other.tag == "food") && landed == false)
        {
            Land();
        }
    }

    private void RemoveFromLandedFood()
    {
        FoodSpawner.LandedFood.Remove(gameObject);
    }

    private void OnFoodFullLandingInvoke()
    {
        OnFoodFullLanding?.Invoke();
    }

    private void Land()
    {
        rb.velocity = new Vector2(0, 0);
        landed = true;
        _food.FoodScorer.AddScore(1);
        FoodSpawner.LandedFood.Add(StomachPrefab);
        OnFoodFullLandingInvoke();
    }
}