using System;
using Character;
using Inputs;
using UnityEngine;

namespace Controller
{
    public abstract class MovementModule : MonoBehaviour, IModule
    {
        private ModuleManager _moduleManager;
        protected CharacterController _characterController;
        protected Rigidbody2D _rigidbody2D;
        protected OverlapDetection _overlapDetection;
        protected CapsuleController2D _colliderController;
        protected Direction _direction = Direction.None;
        protected bool _enabled;
        
        
        [Header("Settings")] 
        [SerializeField] protected int _priority = -1;
        [SerializeField] protected bool _isLocked = false;
        [SerializeField] protected bool _isDefault = false;
        [SerializeField] protected bool _isPassive = false;
        [SerializeField] protected string _animationState = "";
        [SerializeField] protected string _inputString = "Move";

        ///<inheritdoc/>
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                {
                    _enabled = true;
                    Init();
                }
                else
                {
                    _enabled = false;
                    Clear();
                }
            }
        }

        ///<inheritdoc/>
        public bool Locked { get => _isLocked; set => _isLocked = value; }
        ///<inheritdoc/>
        public bool Default => _isDefault; 
        ///<inheritdoc/>
        public Direction CurrentDirection => _direction;
        ///<inheritdoc/>
        public string Name => gameObject.name;
        ///<inheritdoc/>
        public bool IsPassive => _isPassive;
        ///<inheritdoc/>
        public int Priority => _priority;
        ///<inheritdoc/>
        public bool IsApplicable => Applicable();
        

        public virtual void Awake()
        {
            _moduleManager = GetComponentInParent<ModuleManager>();
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
            _overlapDetection = GetComponentInParent<OverlapDetection>();
            _colliderController = GetComponentInParent<CapsuleController2D>();
            _characterController = GetComponentInParent<CharacterController>();
            
            _moduleManager.AddModule(this);
        }

        ///<inheritdoc/>
        public abstract void FixedUpdateModule();
        
        /// <summary>
        /// Called when enabled
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Called when disabled
        /// </summary>
        protected abstract void Clear();

        /// <summary>
        /// If the module is applicable considering the current state of the character. 
        /// </summary>
        protected abstract bool Applicable();

        /// <summary>
        ///Query whether this module can be ended without bad results (clipping etc.)
        /// </summary>
        public virtual bool CanEnd() { return true; }

        ///<inheritdoc/>
        public string GetAnimationState()
        {
            return _animationState;
        }

        /// <summary>
        ///Query whether this module can be ended without bad results (clipping etc.)
        /// </summary>
        public virtual void UpdateInactiveModules() { }
        
        //Called from within modules to get a directional input by name
        protected DirectionInput GetDirectionInput(string name)
        {
            if (_characterController == null)
            {
                Debug.LogError("Character controller not found for " + Name + "," + _inputString +  "!");
                return null;
            }
            if (_characterController.GetPlayerInput() == null)
            {
                Debug.LogError("Player input not found for " + Name + "," + _inputString + "!");
                return null;
            }

            return  _characterController.GetPlayerInput().GetDirectionInput(name);
        }
        //Called from within modules to get a button input by name
        protected ButtonInput GetButtonInput(string name)
        {
            if (_characterController == null)
            {
                Debug.LogError("Character controller not found for " + Name + "!");
                return null;
            }
            if (_characterController.GetPlayerInput() == null)
            {
                Debug.LogError("Player input not found for " + Name + "!");
                return null;
            }
            return _characterController.GetPlayerInput().GetButton(name );
        }
    }
}