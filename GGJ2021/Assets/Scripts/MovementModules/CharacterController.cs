using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Inputs;
using UnityEngine;

namespace Controller
{
    public class CharacterController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private ModuleManager _moduleManager = null;

        private PlayerInput _playerInput;
        private DirectionInput _movementInput;
        private ButtonInput _jumpInput;
        private OverlapDetection _overlapDetection;
        private bool _movementLocked;

        private void Awake()
        {
            _overlapDetection = GetComponent<OverlapDetection>();
            _playerInput = null; // GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (IsMovementLocked()) return;
            
            if (_moduleManager != null)
            {
                _moduleManager.UpdateBestApplicableModule();
                _moduleManager.UpdateInactiveModules();
                _moduleManager.FixedUpdateCurrentModule();
            }
        }
        
        
        public void LockMovement(bool lockInput)
        {
            _movementLocked = lockInput;
        }
        public bool IsMovementLocked()
        {
            return _movementLocked;
        }
        
        public virtual void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            if (playerInput.GetDirectionInput("Move") != null)
            {
                _movementInput = playerInput.GetDirectionInput("Move");
            }
            else
            {
                Debug.LogError("Move input not set up in character input");
            }
        }

        public virtual void ClearPlayerInput()
        {
            _playerInput = null;
        }
        
        public Vector2 GetInputMovement()
        {
            if (_movementInput != null) { return _movementInput.ClampedInput; }
            return Vector2.zero;
        }
        
        public PlayerInput GetPlayerInput()
        {
            return _playerInput;
        }
    }
}