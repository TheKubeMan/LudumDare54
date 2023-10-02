using System;
using UnityEngine;

namespace AudioSystem
{
    public class AudioMute : MonoBehaviour
    {
        private void Start()
        {
            if (PlayerPrefs.GetInt("bgMusicMute") == 1)
            {
                MuteBackgroundAudio();
            }
        }

        public static void MuteBackgroundAudio()
        {
            Audio.Instance.Background.mute = true;
            PlayerPrefs.SetInt("bgMusicMute", 1);
        }
    
        public static void UnmuteBackgroundAudio()
        {
            Audio.Instance.Background.mute = false;
            PlayerPrefs.SetInt("bgMusicMute", 0);
        }
    }
}