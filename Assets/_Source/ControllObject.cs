using System;
using System.Collections;
using System.Collections.Generic;
using FoodSystem.Data;
using UnityEngine;

public class ControllObject : MonoBehaviour
{
    public event Action OnFoodLanding;
    Rigidbody2D rb;
    bool landed;
    public float rSpeed;
    public float rFriction;
    public float rSmoothness;
    float rotateVal;
    Quaternion RotateTo;
    string i;
    //мы закидываем индекс в стринг потому что в playerprefs нельзя использовать массивы
    //полученные индексы забираем в желудке, кидаем в массив и по ним спавним объекты 
    public string index;

    // Start is called before the first frame update
    void Start()
    {
        landed = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX != 0)
            rb.velocity = new Vector2(moveX * 3, rb.velocity.y);
        rotateVal += Input.GetAxis("Vertical") * rSpeed * rFriction;
        RotateTo = Quaternion.Euler(0, 0, rotateVal);
        transform.rotation = Quaternion.Lerp(transform.rotation, RotateTo, Time.deltaTime * rSmoothness);
    }

    void FixedUpdate()
    {
        if (rb.velocity.y > 0.25 || Mathf.Abs(rb.velocity.x) > 0.25)
            gameObject.tag = "food";
        else
            gameObject.tag = "Finish";
        if (rb.velocity.y >= 0 && landed && rb.velocity.y < 0.1)
        {
            i = PlayerPrefs.GetString("items");
            //Debug.Log("i " + i);
            PlayerPrefs.SetString("items", index + " " + i);
            //Debug.Log("items " + PlayerPrefs.GetString("items"));
            //Camera.main.GetComponent<ClearCount>().Spawn();
            gameObject.GetComponent<ControllObject>().enabled = false;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if ((other.tag == "Finish" || other.tag == "food") && landed == false)
        {
            Debug.Log("OnTriggerEnter2D");
            landed = true;
            OnFoodLanding?.Invoke();
        }
    }
}