using System.Collections;

namespace BattleSystems.States
{
    public class PokemonFainted: State
    {
        public PokemonFainted(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.EndTurn();
            yield return battleSystem.SetDialogText($"{battleSystem.GetNamePokemon()} fainted!");
            yield return battleSystem.playerPokemonUnit.PlayPokemonFaintedAnimation();
            if (battleSystem.IsHasPokemonCanBattle())
            {
                yield return battleSystem.SetDialogText("What will you do?", true);
                yield return battleSystem.pokemonFaintedUi.DisplaySelections();
            }
            else
            {
                yield return battleSystem.SetDialogText("You have no more Pokemon that can battle!");
                yield return battleSystem.EndingBattle();
            }
        }
    }
}