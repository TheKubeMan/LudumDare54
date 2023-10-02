using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem;
using FoodSystem.Data;
using UnityEngine;

[RequireComponent(typeof(Food))]
public class ControllObject : MonoBehaviour
{
    public event Action OnLanding;
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

        _food.OnGroundEnter += Land;
        _food.OnGroundEnter += RemoveFromLandedFood;
    }

    private void OnDestroy()
    {
        _food.OnGroundEnter -= Land;
        _food.OnGroundEnter -= RemoveFromLandedFood;
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
        LandCompareTags(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        LandCompareTags(col.gameObject);
    }

    private void LandCompareTags(GameObject comparedGameObject)
    {
        if ((comparedGameObject.CompareTag("Finish") || comparedGameObject.CompareTag("food")) && landed == false)
        {
            Land();
            _food.ScorerUpdate(1);
            FoodSpawner.LandedFood.Add(StomachPrefab);
        }
    }

    private void RemoveFromLandedFood()
    {
        FoodSpawner.LandedFood.Remove(StomachPrefab);
    }

    private void Land()
    {
        if (!landed)
        {
            rb.velocity = new Vector2(0, 0);
            landed = true;
            OnLanding?.Invoke();
        }
    }
}