using UnityEngine;

namespace Utils
{
    public class PauseChanger : MonoBehaviour
    {
        public static void Pause()
        {
            Time.timeScale = 0f;
        }
        public static void Unpause()
        {
            Time.timeScale = 1f;
        }
    }
}