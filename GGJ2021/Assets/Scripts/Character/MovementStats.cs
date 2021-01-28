using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "MovementStats", menuName = "Character/Create new movement stats")]
    public class MovementStats : ScriptableObject
    {
        [Space]
        [Header("Stats")]
        public float speed = 10;
        public float jumpForce = 50;
        public float wallJumpForce = 10;
        public float slideSpeed = 5;
        public float wallJumpLerp = 10;
        public float dashSpeed = 20;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
    }
}