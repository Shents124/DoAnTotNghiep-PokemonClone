using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventSO: ScriptableObject
    {
        public UnityAction OnRaisedEvent;

        public void RaisedEvent()
        {
            OnRaisedEvent?.Invoke();
        }
    }
}