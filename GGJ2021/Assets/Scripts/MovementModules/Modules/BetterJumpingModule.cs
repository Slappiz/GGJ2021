using UnityEngine;

namespace Controller
{
    public class BetterJumpingModule : MovementModule
    {
        [Header("Modifier Settings")]
        [SerializeField] private float _fallMultiplier = 2.5f;
        [SerializeField] private float _lowJumpMultiplier = 2f;
        
        protected override void Clear() { }
        protected override bool Applicable()
        {
            return true;
        }

        public override void FixedUpdateModule()
        {
            if(_rigidbody2D.velocity.y < 0) { _rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime); }
            else if(_rigidbody2D.velocity.y > 0) { _rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime); }
        }

        protected override void Init() { }
    }
}