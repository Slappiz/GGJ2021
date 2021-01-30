using UnityEngine;

namespace Controller
{
    public class WallClimbModule : MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _climbSpeed = 3f;

        protected override bool Applicable()
        {
            if (_overlapDetection.OnWall && GetButtonInput(_inputString).IsPressed)
            {
                return true;
            }

            return false;
        }

        protected override void Init()
        {
            UseGravity(false);
        }

        protected override void Clear()
        {
            UseGravity(true);
        }

        public override void FixedUpdateModule()
        {
            if (_characterController.GetInputMovement().x > .1f || _characterController.GetInputMovement().x < -.1f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            }
            
            if (_overlapDetection.OnWallLeft)
            {
                _direction = Direction.Left;
            }
            else
            {
                _direction = Direction.Right;
            }

            if (_characterController.GetInputMovement() == Vector2.zero)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _direction = Direction.None;
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(0, _characterController.GetInputMovement().y * (_climbSpeed));
            }
        }
    }
}