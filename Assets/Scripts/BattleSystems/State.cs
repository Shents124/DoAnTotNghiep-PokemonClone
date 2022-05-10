using System.Collections;
using Moves;
using Pokemons;

namespace BattleSystems
{
    public abstract class State
    {
        protected EncounterPokemonBattleSystem battleSystem;

        protected State(EncounterPokemonBattleSystem battleSystem)
        {
            this.battleSystem = battleSystem;
        }

        protected float GetEffectType(Pokemon pokemon, PokemonMove pokemonMove)
        {
            var dameEffectType1 = battleSystem.CalculateDameEffectType(pokemon.BasePokemon.Type1, pokemonMove.GetMoveType());
            var dameEffectType2 = battleSystem.CalculateDameEffectType(pokemon.BasePokemon.Type2, pokemonMove.GetMoveType());
            var dameEffectType = dameEffectType1 * dameEffectType2;
            return dameEffectType;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Attack(Pokemon pokemon, PokemonMove pokemonMove)
        {
            yield break;
        }
    }
}