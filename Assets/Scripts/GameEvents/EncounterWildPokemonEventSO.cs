using System.Collections.Generic;
using Pokemons;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EncounterWildPokemonEvent", menuName = "Events/EncounterWildPokemon")]
public class EncounterWildPokemonEventSO : ScriptableObject
{
    public UnityAction<List<Pokemon>, Pokemon> eventRaised;

    public void Raised(List<Pokemon> pokemonParty, Pokemon pokemon)
    {
        eventRaised?.Invoke(pokemonParty, pokemon);
    }
}