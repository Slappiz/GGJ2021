using UnityEngine;
using System.Collections;
using Controller;
using CharacterController = Controller.CharacterController;

//--------------------------------------------------------------------
// PlayerInput uses Unity input (keyboard or controller) to register player input
// and send it to an CharacterControllerBase
//--------------------------------------------------------------------
namespace Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CharacterController _characterController;
        [SerializeField] InputElement[] _Inputs;
        
        // Called by Unity upon adding a new component to an object, 
        // or when Reset is selected in the context menu. Used here to provide default values.
        // Also used when fixing up components using the CharacterFixEditor button
        void Reset()
        {
            _characterController = transform.GetComponent<CharacterController>();
            EnsureJumpAndMoveInputsAreSet();
        }

        private void EnsureJumpAndMoveInputsAreSet()
        {
            if (_Inputs == null)
            {
                _Inputs = new InputElement[0];
            }
            bool moveFound = false;
            bool jumpFound = false;
        
            for (int i = 0; i < _Inputs.Length; i ++)
            {
                if (_Inputs[i].Name == "Move")
                {
                    moveFound = true;
                }
                if (_Inputs[i].Name == "Jump")
                {
                    jumpFound = true;
                }
            }
            if (!moveFound)
            {
                InputElement[] newInputs = new InputElement[_Inputs.Length + 1];
                for (int i = 0; i < _Inputs.Length; i ++)
                {
                    newInputs[i] = _Inputs[i];
                }
                newInputs[_Inputs.Length] = new InputElement();
                newInputs[_Inputs.Length].Name = "Move";
                newInputs[_Inputs.Length].ThisInputType = InputElement.InputType.Direction;
                newInputs[_Inputs.Length].HorizontalAxisName = "Horizontal";
                newInputs[_Inputs.Length].VerticalAxisName = "Vertical";
                newInputs[_Inputs.Length].DirectionThreshold = 0.6f;

                _Inputs = newInputs;
            }
            if (!jumpFound)
            {
                InputElement[] newInputs = new InputElement[_Inputs.Length + 1];
                for (int i = 0; i < _Inputs.Length; i++)
                {
                    newInputs[i] = _Inputs[i];
                }
                newInputs[_Inputs.Length] = new InputElement();
                newInputs[_Inputs.Length].Name = "Jump";
                newInputs[_Inputs.Length].ThisInputType = InputElement.InputType.Button;
                newInputs[_Inputs.Length].UnityInputType = ButtonInput.UnityInputType.Button;
                newInputs[_Inputs.Length].ButtonName = "Jump";

                _Inputs = newInputs;
            }
        }
        
        void Awake()
        {
            //Update entity controller inputs
            if (_characterController != null)
            {
                _characterController.SetPlayerInput(this);
            }
        }
        
        public CharacterController GetCharacterController()
        {
            return _characterController;
        }
        
        public bool DoesInputExist(string name)
        {
            for (int i = 0; i < _Inputs.Length; i++)
            {
                if (_Inputs[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public ButtonInput GetButton(string name)
        {
            for (int i = 0; i < _Inputs.Length; i++)
            {
                if (_Inputs[i].Name == name)
                { 
                    if (_Inputs[i].ThisInputType == InputElement.InputType.Button)
                    {
                        return _Inputs[i].GetButtonInput();
                    }
                    else
                    {
                        Debug.LogError("Requested input " + name + " is not of Button type but of " +_Inputs[i].ThisInputType.ToString() + " type.");
                        return null;
                    }
                }
            }
            Debug.LogError("Requesting a button input that is not defined: " + name);
            return null;
        }
        
        public DirectionInput GetDirectionInput(string name)
        {
            for (int i = 0; i < _Inputs.Length; i++)
            {
                if (_Inputs[i].Name == name)
                {
                    if (_Inputs[i].ThisInputType == InputElement.InputType.Direction)
                    {
                        return _Inputs[i].GetDirectionInput();
                    }
                    else
                    {
                        Debug.LogError("Requested input " + name + " is not of Direction type but of " + _Inputs[i].ThisInputType.ToString() + " type.");
                        return null;
                    }
                }
            }
            Debug.LogError("Requesting a Direction input that is not defined: " + name);
            return null;
        }
        
        void Update ()
        {    
            for (int i = 0; i < _Inputs.Length; i ++)
            {
                _Inputs[i].Update();
            }
        }

        void FixedUpdate()
        {
            for (int i = 0; i < _Inputs.Length; i++)
            {
                _Inputs[i].FixedUpdate();
            }
        }
    }
}