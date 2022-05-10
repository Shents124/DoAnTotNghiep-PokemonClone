using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace EventListeners
{
    [Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
        
    }
    public class BoolEventListener: MonoBehaviour
    {
        [SerializeField] private SaveGameSystem saveGameSystem;
        [SerializeField] BoolEventSO eventSo;
        public BoolEvent onRaisedEvent;

        private void OnEnable()
        {
            if(onRaisedEvent == null) return;
            eventSo.eventRaised += Respone;
        }

        private void OnDisable()
        {
            if(onRaisedEvent == null) return;
            eventSo.eventRaised -= Respone;
        }

        private void Respone(bool value)
        {
            onRaisedEvent?.Invoke(value);
            saveGameSystem.SaveDataToDisk();
        }
    }
}