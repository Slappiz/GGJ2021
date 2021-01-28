namespace Inputs
{
    public class ButtonInput
    {
        public enum UnityInputType
        {
            Button,
            Axis,
            ButtonAndAxis
        }
        
        public enum UnityAxisRecognition
        {
            PositiveOrNegative,
            PositiveOnly,
            NegativeOnly
        }
        
        public bool IsPressed;
        public bool WasJustPressed;
        public bool WasJustReleased;

        private UnityInputType _unityInputType;
        private UnityAxisRecognition _unityAxisRecognition;
        private string _buttonName;

        private float _previousAxisValue;
        private bool _isActuallyPressed;
        private bool _isActuallyJustPressed;
        private bool _isActuallyJustReleased;

        public ButtonInput(UnityInputType inputType, UnityAxisRecognition axisRecognition, string buttonName)
        {
            _unityInputType = inputType;
            _unityAxisRecognition = axisRecognition;
            _buttonName = buttonName;
        }

        public void Update()
        {
            float axisValue = 0;

            //In case of the triggers on a controller, still register them as button presses
            if (_unityInputType == UnityInputType.Axis || _unityInputType == UnityInputType.ButtonAndAxis)
            {
                axisValue = UnityEngine.Input.GetAxisRaw(_buttonName);
            }
            
            switch (_unityInputType)
            {
                case UnityInputType.Button:
                    if (UnityEngine.Input.GetButtonDown(_buttonName))
                    {
                        HandleButtonUpdate(true);
                    }
                    if (UnityEngine.Input.GetButtonUp(_buttonName))
                    {
                        HandleButtonUpdate(false);
                    }
                    break;
                case UnityInputType.Axis:
                    if (IsAxisPressed(axisValue) && !IsAxisPressed(_previousAxisValue))
                    {
                        HandleButtonUpdate(true);
                    }
                    if (!IsAxisPressed(axisValue) && IsAxisPressed(_previousAxisValue))
                    {
                        HandleButtonUpdate(false);
                    }
                    _previousAxisValue = axisValue;
                    break;
                case UnityInputType.ButtonAndAxis:
                    if (UnityEngine.Input.GetButtonDown(_buttonName))
                    {
                        HandleButtonUpdate(true);
                    }
                    if (UnityEngine.Input.GetButtonUp(_buttonName))
                    {
                        HandleButtonUpdate(false);
                    }
                    if (IsAxisPressed(axisValue) && !IsAxisPressed(_previousAxisValue))
                    {
                        HandleButtonUpdate(true);
                    }
                    if (!IsAxisPressed(axisValue) && IsAxisPressed(_previousAxisValue))
                    {
                        HandleButtonUpdate(false);
                    }
                    _previousAxisValue = axisValue;
                    break;
            }
        }
        
        public void FixedUpdate()
        {
            WasJustPressed = false;
            WasJustReleased = false;
            if (_isActuallyJustPressed)
            {
                WasJustPressed = true;
                _isActuallyJustPressed = false;
                IsPressed = true;
            }
            else if (_isActuallyJustReleased)//Stagger just pressed and released if they happened within a single fixed update
            {
                WasJustReleased = true;
                _isActuallyJustReleased = false;
                IsPressed = false;
            }
            else
            {
                IsPressed = _isActuallyPressed;
            }
        }

        private bool IsAxisPressed(float value)
        {
            switch (_unityAxisRecognition)
            {
                case UnityAxisRecognition.PositiveOrNegative:
                    return (value != 0.0f);
                case UnityAxisRecognition.PositiveOnly:
                    return (value > 0.0f);
                case UnityAxisRecognition.NegativeOnly:
                    return (value < 0.0f);
            }
            return false;
        }

        private void HandleButtonUpdate(bool pressed)
        {
            if (pressed && !_isActuallyPressed)
            {
                _isActuallyJustPressed = true;
            }
            else if (!pressed && _isActuallyPressed)
            {
                _isActuallyJustReleased = true;
            }
            _isActuallyPressed = pressed;
        }
    }
}