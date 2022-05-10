using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Events/Vector2 Event Channel")]
    public class Vector2EventSO: ScriptableObject
    {
        public UnityAction<Vector2> OnRaisedEvent;

        public void RaisedEvent(Vector2 vector2)
        {
            OnRaisedEvent?.Invoke(vector2);
        }
    }
}