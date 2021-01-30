using UnityEngine;
using UnityEngine.Events;

namespace Unity.Utils.PatternObserver
{
    public class SignalListener : MonoBehaviour, ISignalListener
    {

        public SignalSender signal;
        public UnityEvent signalEvent;

        public void OnSignalRaised()
        {
            signalEvent.Invoke();
        }

        private void OnEnable()
        {
            signal.RegisterListener(this);
        }

        private void OnDisable()
        {
            signal.UnregisterListener(this);
        }
    }
}