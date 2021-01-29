using UnityEngine;

namespace Controller
{
    public class IdleModule : MovementModule
    {
        protected override void Init()
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
        }

        protected override void Clear() { }

        public override void FixedUpdateModule()
        {
            //_rigidbody2D.velocity = new Vector2(0, 0);
        }
        
        protected override bool Applicable()
        {
            if (_overlapDetection.OnGround && GetDirectionInput(_inputString).RawInput == Vector2.zero)
            {
                return true;
            }

            return false;
        }
    }
}