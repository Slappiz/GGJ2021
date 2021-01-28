using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public enum Direction
    {
        None,
        Left, Right, 
        Up, Down,
        UpLeft, UpRight, 
        DownLeft, DownRight, 
    }
    
    public interface IModule
    {
        /// <summary>
        /// Set module active or inactive. 
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// Is module locked. 
        /// </summary>
        bool Locked { get; set; }
        /// <summary>
        /// Is a default module, i.e. starting module 
        /// </summary>
        bool Default { get;}
        /// <summary>
        /// Current Direction, used by the animation handler
        /// </summary>
        Direction CurrentDirection { get; }
        /// <summary>
        /// Name of the modules gameobject, used by the animation handler to set state.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Priority of module
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// Called whenever the module is active and updating (implementation by child modules)
        /// </summary>
        void FixedUpdateModule();
        /// <summary>
        /// Is a passive module, i.e. no input needed to activate in controller. 
        /// </summary>
        bool IsPassive { get;}
        /// <summary>
        /// If the module is applicable considering the current state of the character. 
        /// </summary>
        bool IsApplicable { get;}

        /// <summary>
        /// Called whenever the module is inactive and updating (implementation by child modules)
        /// </summary>
        void UpdateInactiveModules();
        /// <summary>
        /// Query whether this module can be ended without bad results (clipping etc.)
        /// </summary>
        bool CanEnd();
        
        /// <summary>
        /// Returns animation state for module
        /// </summary>
        string GetAnimationState();
    }
}