using System;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "DecorationMover", menuName = "Decoration/Create new mover")]
    public class DecorationMover : Decoration
    {
        [SerializeField] private Direction _direction = Direction.Vertical;
        [SerializeField] private float _moveTime = 1f;
        [SerializeField] private float _distanceToMove = 2f;
        [SerializeField] private Ease _easyType = Ease.Linear;
        
        private Vector2 _targetOne;
        private Vector2 _targetTwo;
        private bool _isActing = false;
        
        private enum Direction
        {
            Vertical,
            Horizontal
        }
        
        public override void Setup(Transform actor)
        {
            _targetOne = actor.position;
            var position = actor.transform.position;
            _targetTwo = _direction == Direction.Horizontal
                ? new Vector2(position.x + _distanceToMove,
                    position.y)
                : new Vector2(position.x,
                    position.y + _distanceToMove);
            
        }

        public override void Act(Transform actor)
        {
            if ((Vector2)actor.position == _targetOne)
            {
                actor.DOMove(_targetTwo, _moveTime).SetEase (_easyType);
            }
            else if((Vector2)actor.position == _targetTwo){
                actor.DOMove(_targetOne, _moveTime).SetEase (_easyType);
            }
        }
    }
}