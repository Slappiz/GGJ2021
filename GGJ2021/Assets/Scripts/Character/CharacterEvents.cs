using System;

namespace Character
{
    public static class CharacterEvents
    {
        public static Action InteractInitiated;

        private static EventHandler _interactEvent;

        public static event EventHandler OnInteractEvent {
            add {
                if (_interactEvent != null || value.GetInvocationList().Length > 1)
                {
                    //throw new InvalidOperationException("Only one handler allowed");
                }
                _interactEvent = (EventHandler)Delegate.Combine(_interactEvent, value);
            }
            remove {
                _interactEvent -= (EventHandler)Delegate.Remove(_interactEvent, value);
            }
        }
        
        public static void OnInteractInitiated()
        {
            InteractInitiated?.Invoke();
        }
        
        public static void OnInteract(object sender, EventArgs e)
        {
            _interactEvent.Invoke(sender, e);
        }
    }
}