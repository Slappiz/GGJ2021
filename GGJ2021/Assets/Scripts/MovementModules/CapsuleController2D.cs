using System;
using Character;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class CapsuleController2D : MonoBehaviour
    {
        [Serializable]
        public struct CapsuleSettings
        {
            [Header("Collider")] 
            public Vector2 Offset;
            public Vector2 Size;
            public CapsuleDirection2D Direction;
            
            [Space]
            [Header("OverlapDetection")]
            public Vector2 MiddleBottomOffset;
        }
        
        private CapsuleCollider2D _collider;
        private OverlapDetection _overlapDetection;

        void Awake()
        {
            _collider = GetComponent<CapsuleCollider2D>();
            _overlapDetection = GetComponent<OverlapDetection>();
        }

        [Header("Settings")] 
        [SerializeField]private CapsuleSettings _crouchCapsule;
        [SerializeField]private CapsuleSettings _standingCapsule;
    }
}