using System.Collections;

namespace BattleSystems.States
{
    public class Won : State
    {
        public Won(EncounterPokemonBattleSystem battleSystem) : base(battleSystem)
        {
            
        }

        public override IEnumerator Start()
        {
            yield return battleSystem.SetDialogText($"{battleSystem.GetNameEnemyPokemon()} is fainted!");
            yield return battleSystem.enemyPokemonUnit.PlayPokemonFaintedAnimation();
            yield return battleSystem.EndingBattle();
        }
    }
}