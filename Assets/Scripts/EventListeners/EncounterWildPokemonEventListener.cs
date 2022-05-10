using System.Collections.Generic;
using BattleSystems;
using Pokemons;
using UnityEngine;

public class EncounterWildPokemonEventListener : MonoBehaviour
{
    [SerializeField] private EncounterWildPokemonEventSO eventSo;
    [SerializeField] private EncounterPokemonBattleSystem battleSystem;

    private void OnEnable()
    {
        eventSo.eventRaised += InitBattle;
    }

    private void OnDisable()
    {
        eventSo.eventRaised -= InitBattle;
    }

    private void InitBattle(List<Pokemon> pokemonParty, Pokemon wildPokemon)
    {
        battleSystem.InitBattle(pokemonParty, wildPokemon);
    }
}