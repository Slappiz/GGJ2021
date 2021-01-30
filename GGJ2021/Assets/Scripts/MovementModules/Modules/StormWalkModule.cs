using UnityEngine;

namespace Controller
{
    public class StormWalkModule : MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private float _acceleration = 0.1f;
        
        protected override bool Applicable()
        {
            // if (_overlapDetection.OnGround && GetDirectionInput(_inputString).RawInput.x != 0 && isStorm)
            // {
            //     return true;
            // }
            //
            return false;
        }

        protected override void Clear()
        {
            
        }
        public override void FixedUpdateModule()
        {
            var velocity = _rigidbody2D.velocity;
            var newVelocity = new Vector2(GetDirectionInput(_inputString).RawInput.x * _movementSpeed, velocity.y);
            _rigidbody2D.velocity = newVelocity;

            if (newVelocity.x == 0) { _direction = Direction.None;; }
            else {_direction = newVelocity.x > 0 ? Direction.Right : Direction.Left;}
        }

        protected override void Init() { }
    }
}