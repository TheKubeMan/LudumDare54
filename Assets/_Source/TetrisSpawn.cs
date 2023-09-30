using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class TetrisSpawn : MonoBehaviour
{
    public List<GameObject> items;
    string arr;
    string[] arr2;
    public Transform spawnpoint;
    //Это вешаем на независимый объект для отслеживания того что мы можем спавнить, а что нет
    void Start()
    {
        arr = PlayerPrefs.GetString("items", "");
        if (arr == "")
            End();
        else
        {
            arr2 = arr.Split(' ');
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < arr2.Length; j++)
                {
                    if (i == Convert.ToInt32(arr2[j]))
                        break;
                    else
                        items.Remove(items[i]);
                }
                Instantiate(items[i], spawnpoint.position, spawnpoint. rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void End()
    {
        //Здесь прописать что будет происходить по завершению игры
    }
    void Spawn()
    {
        int a = UnityEngine.Random.Range(0, items.Count);
        Instantiate(items[a], spawnpoint.position, spawnpoint.rotation);
    }
}
