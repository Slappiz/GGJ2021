using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Effects;
using DG.Tweening;
using UnityEngine;

namespace Controller
{
    public class DashModule : MovementModule
    {
        [Header("Modifier Settings")] 
        [SerializeField] private float _dashStrength = 20f;
        [SerializeField] private float _dashTime = 0.2f;
        [SerializeField] float _dashCooldown = 0.25f;
        [SerializeField] bool _resetDashsAfterTouchingWall = false;
        [Space]
        [Header("Camera Shake Settings")] 
        [SerializeField] private float _duration = 0.15f;
        [SerializeField] private float _strength = 0.3f;
        [SerializeField] private int _vibrato = 9;
        [Space]
        [Header("Particles")]
        public ParticleSystem _dashTrail;
        // public GhostController _ghostController;
        // [SerializeField] private float _ghostSpawnTime = 0.1f;
        // [SerializeField] private float _ghostDuration = 0.2f;
        
        private Vector2 _dashDirection;
        float _lastDashTime;
        bool _hasDashedAndNotTouchedGroundYet;
        private bool _isDashing;
        
        protected override void Init()
        {
            _characterController.LockMovement(true);
            _isDashing = true;
            _dashDirection = GetDirectionInput("Move").RawInput;
            
            if (Math.Abs(_dashDirection.x) > 0.01 || Math.Abs(_dashDirection.y) > 0.001)
            {
                var camera = Camera.main;
                if (camera != null)
                {
                    camera.transform.DOComplete();
                    camera.transform.DOShakePosition(_duration, _strength, _vibrato, 90, false, true);
                }
            }
            UseGravity(false);
            _lastDashTime = Time.fixedTime;
            _hasDashedAndNotTouchedGroundYet = true;
            
            StartCoroutine(DashWait());
            
            if (_dashDirection != Vector2.zero)
            {
                if (_dashDirection.x < 0) { _direction = Direction.Left; }
                else { _direction = Direction.Right; }
                _dashTrail.Play();
                //_ghostController.StartGhost(_ghostSpawnTime, _ghostDuration, _direction);
            }
            
        }

        protected override void Clear()
        {
            UseGravity(true);
            _dashTrail.Stop();
            _characterController.LockMovement(false);
        }

        protected override bool Applicable()
        {
            if (GetButtonInput(_inputString).WasJustPressed && CanStartDash())
            {
                return true;
            }

            if (_isDashing)
            {
                return true;
            }
            
            return false;
        }

        public override void UpdateInactiveModules()
        {
            if (Time.fixedTime - _lastDashTime < _dashCooldown) { return; }
            if (_overlapDetection.OnGround || (_overlapDetection.OnWall && _resetDashsAfterTouchingWall) )
            {
                _hasDashedAndNotTouchedGroundYet = false;
            }
        }

        public override void FixedUpdateModule()
        {
            //var velocity = Vector2.zero;
            var velocity = _rigidbody2D.velocity;
            velocity += _dashDirection.normalized * _dashStrength;
            _rigidbody2D.velocity = velocity;

            if (velocity.x == 0) { _direction = Direction.None;; }
            else {_direction = velocity.x > 0 ? Direction.Right : Direction.Left;}
        }
        
        private IEnumerator DashWait()
        {
            DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
            yield return new WaitForSeconds(_dashTime);

            _isDashing = false;
            _characterController.LockMovement(false);
        }
        
        public bool CanStartDash()
        {
            if (Time.fixedTime - _lastDashTime < _dashCooldown || _hasDashedAndNotTouchedGroundYet)
            {
                return false;
            }
            return true;
        }

        private void RigidbodyDrag(float x)
        {
            _rigidbody2D.drag = x;
        }
    }
}