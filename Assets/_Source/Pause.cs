using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject pauseButton;
    public void Paause()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void Unpause()
    {
        Menu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
