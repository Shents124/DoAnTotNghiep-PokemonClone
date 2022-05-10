using System.Collections;

namespace BattleSystems.States
{
    public class SelectionAction : State
    {
        public SelectionAction(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.ResetTurn();
            var text = $"What will {battleSystem.GetNamePokemon()} do?";
            yield return battleSystem.SetDialogText(text, true);
            battleSystem.actionController.EnableUi();
        }
    }
}