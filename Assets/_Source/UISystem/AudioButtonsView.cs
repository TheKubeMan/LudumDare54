using System;
using UnityEngine;

namespace UISystem
{
    public class AudioButtonsView : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundAudioOffButton;
        [SerializeField] private GameObject backgroundAudioOnButton;

        private void Awake()
        {
            backgroundAudioOffButton.SetActive(false);
            backgroundAudioOnButton.SetActive(false);
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("bgMusicMute") && PlayerPrefs.GetInt("bgMusicMute") == 1)
            {
                backgroundAudioOnButton.SetActive(true);
            }
            else
            {
                backgroundAudioOffButton.SetActive(true);
            }
        }
    }
}