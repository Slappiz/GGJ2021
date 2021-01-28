using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class ModuleAnimationHandler : MonoBehaviour
    {
        private Animator _animator;
        private string _currentAnimationName;
        private SpriteRenderer _spriteRenderer;
        private Direction _currentDirection;
        private static readonly int _isMoving = Animator.StringToHash("IsMoving");
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            //PlayAnimation("Idle");
        }

        public void SetAnimation(IModule module)
        {
            if (module == null) return;
            
            if (module.GetAnimationState() == "")
            {
                PlayAnimation("Idle");
            }
            
            _animator.SetBool(_isMoving, module.CurrentDirection != Direction.None);

            PlayAnimation(module.GetAnimationState());
            SetSpriteRenderer(module.CurrentDirection);
        }

        private void PlayAnimation(string name)
        {
            if (_currentAnimationName == name) return;
            _currentAnimationName = name;
            _animator.Play(name);
        }
        
        public void SetSpriteRenderer(Direction direction)
        {
            if (direction == _currentDirection) return;
            
            switch (direction)
            {
                case Direction.Left:
                    _spriteRenderer.flipX = true;
                    break;
                case Direction.Right:
                    _spriteRenderer.flipX = false;
                    break;
                default:
                    break;
            }

            _currentDirection = direction;
        }
    }
}