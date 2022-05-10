using System;
using Pokemons;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Events/Pokemon Event Channel")]
    public class PokemonEventSO : ScriptableObject
    {
        public UnityAction<Pokemon, Action<bool>> OnRasiedEvent;

        public void RaisedEvent(Pokemon pokemon, Action<bool> callback)
        {
            OnRasiedEvent?.Invoke(pokemon, callback);
        }
    }
}
