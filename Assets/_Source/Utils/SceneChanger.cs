using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneChanger : MonoBehaviour
    {
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public static void LoadSceneBySceneIndex(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}