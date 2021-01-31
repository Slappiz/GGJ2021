using UnityEngine;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        public string StartSong = "";
        [Range(0,1)]public float MasterVolume = 0.5f;
        private void Start()
        {
            Play(StartSong);
        }

        public void Play(string sound)
        {
            SoundManager.instance.Play(sound);
            SoundManager.instance.masterVolumeMultiplier = MasterVolume;
        }
    }
}