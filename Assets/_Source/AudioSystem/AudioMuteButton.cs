using UnityEngine;

namespace AudioSystem
{
    public class AudioMuteButton : MonoBehaviour
    {
        public static void MuteBackgroundAudio()
        {
            Audio.Instance.Background.mute = true;
        }
    
        public static void UnmuteBackgroundAudio()
        {
            Audio.Instance.Background.mute = false;
        }
    }
}