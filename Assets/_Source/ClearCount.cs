using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCount : MonoBehaviour
{
    public GameObject[] food;
    public Transform spawnpoint;
    void Start()
    {
        PlayerPrefs.SetString("items", "");
    }
    public void Spawn()
    {
        int a = UnityEngine.Random.Range(0, food.Length);
        Instantiate(food[a], spawnpoint.position, spawnpoint.rotation);
    }
}
