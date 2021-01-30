namespace Level
{
        public static class GameEvents
        { 
                public static System.Action<float> StormEnter;

                public static void OnStormEnter(float duration)
                {
                        StormEnter?.Invoke(duration);
                }
        }
}