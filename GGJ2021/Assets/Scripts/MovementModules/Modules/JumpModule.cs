using System;
using Character;
using UnityEngine;
using CharacterController = UnityEngine.CharacterController;

namespace Controller
{
    public class JumpModule: MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _jumpForce = 8f;
        [SerializeField] float _groundedToleranceTime = 0.07f;
        [SerializeField] float _jumpCacheTime = 0.05f;
        
        float _lastJumpPressedTime;
        bool _jumpInputIsCached;
        bool _jumpCutPossible;
        float _lastJumpTime;
        float _lastGroundedTime;
        float _lastTouchingSurfaceTime;
        
        public bool DidJustJump()
        {
            return (Time.fixedTime - _lastJumpTime <= 0.02f + _groundedToleranceTime);
        }
        
        public bool WasJustGrounded()
        {
            return (Time.fixedTime - _lastGroundedTime <= 0.02f);
        }
        
        protected override bool Applicable()
        {
            if (_overlapDetection.OnGround && GetButtonInput("Jump").WasJustPressed)
            {
                GetButtonInput("Jump").WasJustPressed = false;
                _lastJumpPressedTime = Time.fixedTime;
                _lastJumpTime = Time.fixedTime;
                _jumpInputIsCached = true;
                return true;
            }
            
            if (_jumpInputIsCached)
            {
                //Character was grounded or is grounded; jump occurs
                if ((_overlapDetection.OnGround || Time.fixedTime - _lastGroundedTime <= _groundedToleranceTime) && !DidJustJump())
                {
                    return true;
                }
            }

            if (_jumpInputIsCached && GetButtonInput("Jump").IsPressed)
            {
                return true;
            }
            
            return false;
        }

        public override void UpdateInactiveModules()
        {
            //Default jump update (not jumping)
            if (_jumpInputIsCached)
            {
                //Jump has not been started in time; jump cancelled
                if (Time.fixedTime - _lastJumpPressedTime >= _jumpCacheTime)
                {
                    _jumpInputIsCached = false;
                }
            }
            
            if (_overlapDetection.OnGround)
            {
                _lastGroundedTime = Time.fixedTime;
            }
        }

        protected override void Init() { }
        
        public override void FixedUpdateModule()
        {
            var velocity = _rigidbody2D.velocity;
            velocity = new Vector2(velocity.x, 0);
            velocity += Vector2.up * _jumpForce;
            _rigidbody2D.velocity = velocity;
            _lastJumpTime = Time.fixedTime;
            if (velocity.x == 0) { _direction = Direction.None;; }
            else {_direction = velocity.x > 0 ? Direction.Right : Direction.Left;}
        }

        protected override void Clear() { }
        
    }
}