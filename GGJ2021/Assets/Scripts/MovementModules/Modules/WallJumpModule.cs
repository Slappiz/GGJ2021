using System.Collections;
using UnityEngine;

namespace Controller
{
    public class WallJumpModule : MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _jumpForce = 10f;

        private Vector2 _wallDirection;
        private Vector2 _velocity;
        private bool _isWallJumping;
        
        protected override bool Applicable()
        {
            if (_overlapDetection.OnWall && 
                !_overlapDetection.OnCeiling && 
                !_overlapDetection.OnGround && 
                GetButtonInput(_inputString).WasJustPressed )
            {
                GetButtonInput(_inputString).WasJustPressed = false;
                return true;
            }

            if (_isWallJumping)
            {
                return true;
            }

            return false;
        }

        protected override void Init()
        {
            _rigidbody2D.gravityScale = 0;
            _isWallJumping = true;
            _characterController.LockMovement(true);
            _wallDirection = _overlapDetection.OnWallRight ? Vector2.left : Vector2.right;
            _velocity = ((Vector2.up / 1.5f) + (_wallDirection / 1.5f)) * _jumpForce;
            StartCoroutine(Disable());
        }

        protected override void Clear()
        {
            _rigidbody2D.gravityScale = 3;
        }

        public override void FixedUpdateModule()
        {
            _rigidbody2D.velocity = _velocity;
        }

        IEnumerator Disable()
        {
            yield return new WaitForSeconds(.2f);
            _isWallJumping = false;
            _characterController.LockMovement(false);
        }
    }
}