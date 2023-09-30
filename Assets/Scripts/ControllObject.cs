using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllObject : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject[] food;
    public Transform spawnpoint;
    bool landed;
    //мы закидываем индекс в стринг потому что в playerprefs нельзя использовать массивы
    //полученные индексы забираем в желудке, кидаем в массив и по ним спавним объекты 
    public string index;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX != 0)
            rb.velocity = new Vector2(moveX * 3, rb.velocity.y);
    }

    void FixedUpdate()
    {
        if (rb.velocity.y >= 0 && landed)
        {
            string i = PlayerPrefs.GetString("items");
            PlayerPrefs.SetString("items", index + " " + i);
            int a = UnityEngine.Random.Range(0, food.Length);
            Instantiate(food[0], new Vector3(0, 3, 0), spawnpoint.rotation);
            gameObject.GetComponent<ControllObject>().enabled = false;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "food")
            landed = true;
    }
}
