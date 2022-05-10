using System.Collections;
using Moves;
using Pokemons;

namespace BattleSystems.States
{
    public class EnemyTurn : State
    {
        public EnemyTurn(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
            
        }

        public override IEnumerator Start()
        {
            battleSystem.moveHandle.DisableMoveUI();
            yield return Attack(battleSystem.wildPokemon, battleSystem.wildPokemon.GetRandomMove());
        }

        public override IEnumerator Attack(Pokemon pokemon, PokemonMove pokemonMove)
        {
            yield return
                battleSystem.SetDialogText($"{battleSystem.GetNameEnemyPokemon()} use {pokemonMove.GetName()}");
            yield return battleSystem.enemyPokemonUnit.PlayAttackAnimation();
            SoundManager.Instance.PlayHitSfx();
            pokemonMove.UseMove();

            var dameEffectType = GetEffectType(battleSystem.playerPokemon, pokemonMove);
            battleSystem.playerPokemon.GetDamage(pokemon.Attack, pokemonMove.Power(), pokemon.Level, dameEffectType);
            yield return battleSystem.playerPokemonUnit.UpdateHealth();
            
            yield return battleSystem.CheckEffective(dameEffectType);

            if (battleSystem.playerPokemon.IsDead())
                battleSystem.SetState(battleSystem.PokemonFainted);
            else
            {
                if(battleSystem.IsEndTurn)
                    battleSystem.SetState(battleSystem.SelectionAction);
                else
                {
                    battleSystem.EndTurn();
                    battleSystem.SetState(battleSystem.PlayerTurn);
                }
            }
        }
    }
}