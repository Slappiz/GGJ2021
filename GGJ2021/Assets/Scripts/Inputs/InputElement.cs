using UnityEngine;

namespace Inputs
{
    [System.Serializable]
    public class InputElement
    {
        public enum InputType
        {
            Button,
            Direction
        }

        public string Name;
        public InputType ThisInputType;
        public ButtonInput.UnityInputType UnityInputType;
        public ButtonInput.UnityAxisRecognition UnityAxisRecognition;
        public string ButtonName;
        public string HorizontalAxisName;
        public string VerticalAxisName;
        public float DirectionThreshold;

        [HideInInspector] public bool IsFoldingOut;
        
        ButtonInput _buttonInput;
        DirectionInput _directionInput;
        
        public ButtonInput GetButtonInput()
        {
            if (_buttonInput == null)
            {
                _buttonInput = new ButtonInput(UnityInputType, UnityAxisRecognition, ButtonName);
            }
            return _buttonInput;
        }
        
        public DirectionInput GetDirectionInput()
        {
            if (_directionInput == null)
            {
                _directionInput = new DirectionInput(HorizontalAxisName, VerticalAxisName, DirectionThreshold);
            }
            return _directionInput;
        }
        
        public void Update()
        {
            switch (ThisInputType)
            {
                case InputType.Button:
                    GetButtonInput().Update();
                    break;
                case InputType.Direction:
                    GetDirectionInput().Update();
                    break;
            }
        }
        
        public void FixedUpdate()
        {
            switch (ThisInputType)
            {
                case InputType.Button:
                    GetButtonInput().FixedUpdate();
                    break;
            }
        }
    }
}