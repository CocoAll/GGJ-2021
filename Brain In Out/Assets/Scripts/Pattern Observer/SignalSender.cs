using System.Collections.Generic;
using UnityEngine;

namespace Unity.Utils.PatternObserver
{
    [CreateAssetMenu(fileName = "Signal Sender", menuName = "ScriptableObject/SignalSender", order = 100)]
    public class SignalSender : ScriptableObject
    {
        public List<ISignalListener> listeners = new List<ISignalListener>();

        public void Raise()
        {
            for(int i = listeners.Count -1; i >= 0; i--)
            {
                listeners[i].OnSignalRaised();
            }
        }

        public void RegisterListener(ISignalListener signalListener)
        {
            listeners.Add(signalListener);
        }

        public void UnregisterListener(ISignalListener signalListener)
        {
            listeners.Remove(signalListener);
        }
    }
}
