using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class ModuleManager : MonoBehaviour
    {
        [SerializeField]private ModuleAnimationHandler _animationHandler;
        private readonly List<IModule> _modules = new List<IModule>();
        private IModule _currentlyUsedModule;
        
        public void AddModule(IModule module)
        {
            _modules.Add(module);
        }

        public void RemoveModule(IModule module)
        {
            _modules.Remove(module);
        }

        public bool IsModuleRunning => _currentlyUsedModule != null;
        public IModule GetCurrentModule => _currentlyUsedModule;
        
        public IModule GetModuleWithName(string name)
        {
            if (_modules == null)
            {
                return null;
            }
            for (int i = 0; i < _modules.Count; i ++)
            {
                if (_modules[i].Name == name)
                {
                    return _modules[i];
                }
            }
            return null;
        }
        
        void SetNewModule(IModule module)
        {
            if (module == _currentlyUsedModule)
            {
                return;
            }

            if (_currentlyUsedModule != null)
            {
                _currentlyUsedModule.Enabled = false;
            }
            if (module != null)
            {
                module.Enabled = true;
            }
            _currentlyUsedModule = module;
        }
        
        IModule GetBestApplicableModule()
        {
            int bestPriority = int.MinValue;
            IModule returnModule = null;

            for (int i = 0; i < _modules.Count; i++)
            {
                if (_modules[i].Priority > bestPriority)
                {
                    if (!_modules[i].Locked && _modules[i].IsApplicable)
                    {
                        returnModule = _modules[i];
                        bestPriority = _modules[i].Priority;
                    }
                }
            }

            return returnModule;
        }
        
        public void ForceExitModules()
        {
            if (_currentlyUsedModule != null)
            {
                _currentlyUsedModule.Enabled = false;
                _currentlyUsedModule = null;
            }
        }
        
        public void UpdateBestApplicableModule()
        {
            if (_currentlyUsedModule != null && (!_currentlyUsedModule.IsApplicable || _currentlyUsedModule.Locked))
            {
                if (_currentlyUsedModule.CanEnd())
                {
                    _currentlyUsedModule.Enabled = false;
                    _currentlyUsedModule = null;
                }
            }
            IModule bestApplicableModule = GetBestApplicableModule();
            if (_currentlyUsedModule != null && bestApplicableModule != _currentlyUsedModule)
            {
                if (!_currentlyUsedModule.CanEnd())
                {
                    return;
                }
            }
            SetNewModule(bestApplicableModule);
        }

        public void FixedUpdateCurrentModule()
        {
            if(_currentlyUsedModule != null)_currentlyUsedModule.FixedUpdateModule();

            foreach (var module in _modules)
            {
                if (module.IsPassive && module.IsApplicable && module != _currentlyUsedModule)
                {
                    module.FixedUpdateModule();
                }
            }
        }
        
        public void UpdateInactiveModules()
        {
            _animationHandler.SetAnimation(_currentlyUsedModule);
            foreach (var module in _modules)
            {
                module.UpdateInactiveModules();
            }
        }
    }
}