using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int type;
    public GameObject game;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "food")
        {
            if (type == 1)
                GameOver();
            else
            {
                //здесь надо убрать очки из шкалы радости
                Destroy(other.gameObject);
            }
        }
    }
    void GameOver()
    {
        //по идее это останавливает спавн
        game.SetActive(false);
        //отобразить UI конца игры
    }
}
