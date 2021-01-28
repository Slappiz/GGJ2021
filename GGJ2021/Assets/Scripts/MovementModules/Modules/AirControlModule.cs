using UnityEngine;

namespace Controller
{
    public class AirControlModule : MovementModule
    {
        [Header("Modifier Settings")]
        [SerializeField] private float _airLerp = 10f;
        [SerializeField] private float _airMoveSpeed = 8f;

        protected override void Init() { }

        protected override void Clear() { }
        protected override bool Applicable()
        {
            if (!_overlapDetection.OnGround && GetDirectionInput("Move").RawInput != Vector2.zero)
            {
                return true;
            }
            return false;
        }

        public override void FixedUpdateModule()
        {
            var velocity = _rigidbody2D.velocity;
            var newVelocity = Vector2.Lerp(
                velocity,
                (new Vector2(_characterController.GetInputMovement().x * _airMoveSpeed, velocity.y)), 
                _airLerp * Time.fixedDeltaTime);

            _rigidbody2D.velocity = newVelocity;
            
            if (velocity.x == 0) { _direction = Direction.None;; }
            else {_direction = velocity.x > 0 ? Direction.Right : Direction.Left;}
        }
    }
}