using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Events/String Event Channel")]
    public class StringEventSO: ScriptableObject
    {
        public UnityAction<string> OnRaisedEvent;

        public void RaisedEvent(string value)
        {
            OnRaisedEvent?.Invoke(value);
        }
    }
}