using UnityEngine;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        public string StartSong = "";
        private void Start()
        {
            Play(StartSong);
        }

        void Play(string sound)
        {
            SoundManager.instance.Play(sound);
        }
    }
}