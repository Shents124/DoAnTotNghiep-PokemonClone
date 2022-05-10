using System.Collections;
using UnityEngine;

namespace BattleSystems.States
{
    public class Begin : State
    {
        public Begin(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
            
        }

        public override IEnumerator Start()
        {
            battleSystem.StartCoroutine(battleSystem.playerPokemonUnit.InitFlied());
            battleSystem.StartCoroutine(battleSystem.enemyPokemonUnit.InitFlied());
            yield return new WaitForSeconds(2f);
            battleSystem.dialog.EnableDialog();
            yield return battleSystem.StartCoroutine(battleSystem.SetDialogText("A wild pokemon appeared"));
            yield return battleSystem.playerPokemonUnit.PlayStartBattleAnimation();
            battleSystem.SetState(battleSystem.SelectionAction);
        }
    }
}
