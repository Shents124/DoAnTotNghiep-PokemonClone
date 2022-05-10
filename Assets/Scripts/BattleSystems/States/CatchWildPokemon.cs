using System.Collections;
using UnityEngine;

namespace BattleSystems.States
{
    public class CatchWildPokemon : State
    {
        public CatchWildPokemon(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            var wildPokemon = battleSystem.wildPokemon;
            yield return new WaitForSeconds(1f);

            if (CheckCatchSuccess(wildPokemon.MaxHp, wildPokemon.CurrentHp, wildPokemon.BasePokemon.Rate))
            {
                SoundManager.Instance.PlayCatchSuccess();
                var isAddToParty = true;
                yield return battleSystem.SetDialogText($"Gotcha! {wildPokemon.BasePokemon.PokemonName} was caught!");
                battleSystem.pokemonEventSo.RaisedEvent(wildPokemon, (result) =>
                {
                    isAddToParty = result;
                });
                if (isAddToParty)
                    yield return battleSystem.SetDialogText($"{wildPokemon.BasePokemon.PokemonName} has been added to your party");
                else
                    yield return battleSystem.SetDialogText($"{wildPokemon.BasePokemon.PokemonName} was transferred to PC");
                        
                yield return battleSystem.EndingBattle();
            }
            else
            {
                SoundManager.Instance.PlayBreakBall();
                battleSystem.playerPokemonUnit.CatchPokemonFail();
                yield return battleSystem.enemyPokemonUnit.BrokePokemonBall();
                yield return battleSystem.SetDialogText($"Oh, no! {wildPokemon.BasePokemon.PokemonName} broken free!");
                battleSystem.EndTurn();
                battleSystem.SetState(battleSystem.EnemyTurn);
            }
        }

        private bool CheckCatchSuccess(float maxHp, float currentHp, float rate)
        {
            var a = (3 * maxHp - 2 * currentHp) * rate / 3 / maxHp;
            var b = 1048560 / Mathf.Sqrt(Mathf.Sqrt(16711680) / a);
            var randomRate = Random.Range(0, 65535);
            return randomRate < b;
        }
    }
}