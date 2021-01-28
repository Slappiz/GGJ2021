using UnityEngine;

namespace Inputs
{
    public class DirectionInput
    {
        public enum Direction
        {
            Neutral,
            Up,
            Down,
            Left,
            Right
        }
        
        public Vector2 RawInput;
        public Vector2 ClampedInput;
        public Direction DirectionType;
        
        string _horizontalName;
        string _verticalName;
        float _directionThreshold;
        
        public DirectionInput(string horizontalName, string verticalName, float directionThreshold)
        {
            _horizontalName = horizontalName;
            _verticalName = verticalName;
            _directionThreshold = directionThreshold;
        }
        
        public bool IsInThisDirection(Vector2 direction)
        {
            float dot = Vector2.Dot(direction, ClampedInput);
            if (dot >= _directionThreshold)
            {
                return true;
            }
            return false;
        }
        
        public bool HasSurpassedThreshold()
        {
            return (ClampedInput.magnitude >= _directionThreshold);
        }
        
        public void Update ()
        {

            RawInput.x = Input.GetAxisRaw(_horizontalName);
            RawInput.y = Input.GetAxisRaw(_verticalName);
            
            ClampedInput = (RawInput.magnitude > 1) ? RawInput.normalized : RawInput;

            if (Mathf.Abs(ClampedInput.x) > _directionThreshold || Mathf.Abs(ClampedInput.y) > _directionThreshold)
            {
                if (Mathf.Abs(ClampedInput.x) > Mathf.Abs(ClampedInput.y))
                {
                    if (ClampedInput.x > 0)
                    {
                        DirectionType = Direction.Right;
                    }
                    else
                    {
                        DirectionType = Direction.Left;
                    }
                }
                else
                {
                    if (ClampedInput.y > 0)
                    {
                        DirectionType = Direction.Up;
                    }
                    else
                    {
                        DirectionType = Direction.Down;
                    }
                }
            }
            else
            {
                DirectionType = Direction.Neutral;
            }
        }
    }
}