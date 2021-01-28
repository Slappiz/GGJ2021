using DG.Tweening;
using UnityEngine;

namespace Controller.Effects
{
    public class TimeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;

        public void DoSlowmotion(float duration)
        {
            SetSlowmotion(duration);
        }

        private void SetSlowmotion(float duration)
        {
            //Time.fixedDeltaTime = Time.timeScale * 1.02f;
            DOVirtual.Float(slowdownFactor, 1, duration, TimeScale);
        }
        
        private void TimeScale(float x)
        {
            Time.timeScale = x;
        }
    }
}