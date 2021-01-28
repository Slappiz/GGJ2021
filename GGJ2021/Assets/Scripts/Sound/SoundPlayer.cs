using UnityEngine;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        private void Start()
        {
            Play("Memories");
        }

        void Play(string sound)
        {
            SoundManager.instance.Play(sound);
        }
    }
}