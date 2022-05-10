using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventSO", menuName = "Events/BoolEventSO")]
public class BoolEventSO : ScriptableObject
{
    public UnityAction<bool> eventRaised;

    public void Raised(bool value)
    {
        eventRaised?.Invoke(value);
    }
}