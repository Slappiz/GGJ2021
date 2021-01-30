using UnityEngine;

namespace Controller
{
    public class WallSlideModule : MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _slideSpeed = 4f;

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
            var velocity = _rigidbody2D.velocity;
            var pushingWall = (velocity.x > 0 && _overlapDetection.OnWallRight) || velocity.x < 0 && _overlapDetection.OnWallLeft;
            var push = pushingWall ? 0 : velocity.x;
            velocity = new Vector2(push, -_slideSpeed);
            _rigidbody2D.velocity = velocity;
            
            if (_overlapDetection.OnWallLeft)
            {
                _direction = Direction.Right;
            }
            else
            {
                _direction = Direction.Left;
            }
        }
        
        protected override bool Applicable()
        {
            if (!_overlapDetection.OnGround && _overlapDetection.OnWall)
            {
                return true;
            }

            return false;
        }
    }
}