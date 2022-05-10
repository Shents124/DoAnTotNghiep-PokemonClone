using System.Collections;
using Moves;
using Pokemons;

namespace BattleSystems.States
{
    public class PlayerTurn : State
    {
        public PlayerTurn(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.moveHandle.DisableMoveUI();
            var movePokemon = battleSystem.playerPokemon.PokemonMoves[battleSystem.indexMove];
            yield return Attack(battleSystem.playerPokemon, movePokemon);
        }

        public override IEnumerator Attack(Pokemon pokemon, PokemonMove pokemonMove)
        {
            yield return base.Attack(pokemon,pokemonMove);
            yield return battleSystem.SetDialogText($"{battleSystem.GetNamePokemon()} use {pokemonMove.GetName()}");
            yield return battleSystem.playerPokemonUnit.PlayAttackAnimation();
            SoundManager.Instance.PlayHitSfx();
            pokemonMove.UseMove();

            var dameEffectType = GetEffectType(battleSystem.wildPokemon, pokemonMove);
            
            battleSystem.wildPokemon.GetDamage(pokemon.Attack, pokemonMove.Power(),pokemon.Level, dameEffectType);
            yield return battleSystem.enemyPokemonUnit.UpdateHealth();
            yield return battleSystem.CheckEffective(dameEffectType);
            battleSystem.UpdateMoveUI();
            
            if(battleSystem.wildPokemon.IsDead())
                battleSystem.SetState(new Won(battleSystem));
            else
            {
                if(battleSystem.IsEndTurn)
                    battleSystem.SetState(battleSystem.SelectionAction);
                else
                {
                    battleSystem.EndTurn();
                    battleSystem.SetState(battleSystem.EnemyTurn);
                }
            }
        }
    }
}