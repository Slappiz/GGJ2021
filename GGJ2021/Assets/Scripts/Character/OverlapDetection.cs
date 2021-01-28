using System;
using UnityEngine;

namespace Character
{
    public class OverlapDetection : MonoBehaviour
    {
        [Header("Collision")] 
        [SerializeField] private float overlapRadius = 0.1f;
        public Vector2 bottomOffset, rightOffset, leftOffset, topOffset;
        [SerializeField] private LayerMask groundLayer = 0;
        [SerializeField] private LayerMask wallLayer = 0;
        
        public bool OnGround { get; private set; }
        public bool OnCeiling { get; private set; }
        public bool OnWallLeft { get; private set; }
        public bool OnWallRight { get; private set; }

        private void Update()
        {
            var position = transform.position;
            
            OnGround = Physics2D.OverlapCircle((Vector2) position + bottomOffset, overlapRadius, groundLayer);
            OnWallLeft = Physics2D.OverlapCircle((Vector2) position + leftOffset, overlapRadius, wallLayer);
            OnWallRight = Physics2D.OverlapCircle((Vector2) position + rightOffset, overlapRadius, wallLayer);
            OnCeiling = Physics2D.OverlapCircle((Vector2) position + topOffset, overlapRadius, groundLayer);
        }
        
        public bool OnWall => OnWallLeft || OnWallRight;
        public int WallSide
        {
            get
            {
                if (OnWallLeft) return 1;
                if (OnWallRight) return -1;
                return 0;
            }
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var position = transform.position;
            
            if (OnGround) { Gizmos.color = Color.blue; }
            Gizmos.DrawSphere((Vector2) position + bottomOffset, overlapRadius);
            Gizmos.color = Color.red;
            
            if(OnWallLeft) { Gizmos.color = Color.blue; }
            Gizmos.DrawSphere((Vector2) position + leftOffset, overlapRadius);
            Gizmos.color = Color.red;
            
            if(OnWallRight) { Gizmos.color = Color.blue; }
            Gizmos.DrawSphere((Vector2) position + rightOffset, overlapRadius);
            Gizmos.color = Color.red;

            if(OnCeiling) { Gizmos.color = Color.blue; }
            Gizmos.DrawSphere((Vector2) position + topOffset, overlapRadius);
            Gizmos.color = Color.red;
            
        }
    }
}
