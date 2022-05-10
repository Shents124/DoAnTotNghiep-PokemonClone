using System.Collections;

namespace BattleSystems.States
{
    public class Lost : State
    {
        public Lost(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
            
        }

        public override IEnumerator Start()
        {
            battleSystem.ResetTurn();
            yield return battleSystem.SetDialogText($"{battleSystem.GetNamePokemon()} fainted!");
            battleSystem.SetState(battleSystem.PokemonFainted);
        }
    }
}