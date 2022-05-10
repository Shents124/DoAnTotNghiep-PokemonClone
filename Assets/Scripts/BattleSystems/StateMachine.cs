using UnityEngine;
namespace BattleSystems
{
    public abstract class StateMachine: MonoBehaviour
    {
        protected State currentState;

        public void SetState(State state)
        {
            currentState = state;
            StartCoroutine(currentState.Start());
        }
    }
}