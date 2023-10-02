using UnityEngine;

namespace AudioSystem
{
    public class Audio : MonoBehaviour
    {
        [field:SerializeField] public AudioSource Background { get; private set; }
        [field:SerializeField] public AudioClip BackgroundMusic { get; private set; }
        public static Audio Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void ChangeBackgroundMusic(AudioClip bgmusic)
        {
            Background.clip = bgmusic;
            Background.Play();
        }
    }
}