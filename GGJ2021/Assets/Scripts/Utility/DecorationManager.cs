using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Level
{
    public class DecorationManager : MonoBehaviour
    {
        [SerializeField] private Decoration _decoration;

        public void Start()
        {
            _decoration.Setup(this.transform);
        }

        private void FixedUpdate()
        {
            _decoration.Act(this.transform);
        }
    }
    
}