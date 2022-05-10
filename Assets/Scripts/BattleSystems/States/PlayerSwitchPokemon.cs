using System.Collections;
using UnityEngine;

namespace BattleSystems.States
{
    public class PlayerSwitchPokemon: State
    {
        public PlayerSwitchPokemon(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            if (battleSystem.IsEndTurn)
            {
                yield return new WaitForSeconds(1f);
                battleSystem.SetState(battleSystem.SelectionAction);
                yield return null;
            }
            else
            {
                battleSystem.EndTurn();
                yield return new WaitForSeconds(1f);
                battleSystem.SetState(battleSystem.EnemyTurn);
                yield return null;
            }
        }
    }
}