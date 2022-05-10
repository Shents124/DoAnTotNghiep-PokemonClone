using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleSystems.BattleUI;
using BattleSystems.BattleUnit;
using BattleSystems.States;
using GameEvents;
using Pokemons;
using SceneManagement;
using Random = UnityEngine.Random;

namespace BattleSystems
{
    public class EncounterPokemonBattleSystem : StateMachine
    {
        #region States
        public PlayerTurn PlayerTurn { get; private set; }
        public EnemyTurn EnemyTurn { get; private set; }
        public SelectionAction SelectionAction { get; private set; }
        public PokemonFainted PokemonFainted { get; private set; }
        private PlayerSwitchPokemon PlayerSwitchPokemon { get; set; }
        private CatchWildPokemon CatchWildPokemon { get; set; }
        
        #endregion

        public BoolEventSO onBattleEnded;
        public Dialog dialog;
        public ActionController actionController;
        public MoveHandle moveHandle;
        public PlayerBattleUnit playerPokemonUnit;
        public EncounterPokemonBattleUnit enemyPokemonUnit;
        public PartyUI partyUi;
        public PokemonFaintedUI pokemonFaintedUi;
        public Pokemon playerPokemon;
        public Pokemon wildPokemon;
        public List<Pokemon> pokemonParty;
        public PokemonEventSO pokemonEventSo;
        
        public string GetNamePokemon() => playerPokemon.BasePokemon.PokemonName;
        public string GetNameEnemyPokemon() => wildPokemon.BasePokemon.PokemonName;
        public bool IsEndTurn => isEndTurn;
        public int indexMove = 0;
        private bool isEndTurn;
        
        private void Awake()
        {
            PlayerTurn = new PlayerTurn(this);
            EnemyTurn = new EnemyTurn(this);
            SelectionAction = new SelectionAction(this);
            PlayerSwitchPokemon = new PlayerSwitchPokemon(this);
            PokemonFainted = new PokemonFainted(this);
            CatchWildPokemon = new CatchWildPokemon(this);
            isEndTurn = false;
        }

        public void InitBattle(List<Pokemon> pokemonParty, Pokemon wildPokemon)
        {
            this.pokemonParty = pokemonParty;
            playerPokemon = pokemonParty.FirstOrDefault(x => x.CurrentHp > 0);
            this.wildPokemon = wildPokemon;
            playerPokemonUnit.SetData(playerPokemon, dialog, CatchPokemon);
            enemyPokemonUnit.SetData(wildPokemon, dialog);
            moveHandle.SetData(playerPokemon.PokemonMoves);
            partyUi.SetData(pokemonParty, SwitchPokemon, SetCurrentState);
            StartCoroutine(BeginBattle());
        }

        private IEnumerator BeginBattle()
        {
            SetState(new Begin(this));
            yield break;
        }

        public IEnumerator SetDialogText(string text, bool isSetActive = false)
        {
            yield return StartCoroutine(dialog.SetDialogText(text, isSetActive));
        }

        public void PlayerPokemonAttack(int index)
        {
            indexMove = index;
            StopAllCoroutines();
            DoAttackTurn(index);
        }

        private void DoAttackTurn(int index)
        {
            var pokemonMove = playerPokemon.PokemonMoves[index];
            if (IsPlayerAttackFirst())
            {
                if (pokemonMove.CanUseMove())
                    SetState(PlayerTurn);
                else
                    StartCoroutine(SetDialogText("Out of PP"));
            }
            else
            {
                SetState(EnemyTurn);
            }
        }
        
        private void SwitchPokemon(Pokemon pokemonSwitch)
        {
            StartCoroutine(SwitchPokemonCoroutine(pokemonSwitch));
        }

        private IEnumerator SwitchPokemonCoroutine(Pokemon pokemonSwitch)
        { 
            if(pokemonSwitch == playerPokemon)
               yield return SetDialogText("Pokemon already in battle!", true);
            else
            {
                partyUi.gameObject.SetActive(false);
                playerPokemon = pokemonSwitch;
                moveHandle.SetData(playerPokemon.PokemonMoves);
                yield return playerPokemonUnit.PlaySwitchPokemon(pokemonSwitch);
                SetState(PlayerSwitchPokemon);
            }
        }

        private void CatchPokemon()
        {
            SetState(CatchWildPokemon);
        }
        
        private bool IsPlayerAttackFirst()
        {
            if (playerPokemon.Speed > wildPokemon.Speed)
                return true;
            if (playerPokemon.Speed < wildPokemon.Speed)
                return false;
            if (Random.Range(0, 10) <= 4)
                return true;
            return false;
        }

        private void SetCurrentState()
        {
            SetState(currentState);
        }

        public void ResetTurn() => isEndTurn = false;
        public void EndTurn() => isEndTurn = true;

        public void UpdateMoveUI()
        {
            moveHandle.ReloadData(playerPokemon.PokemonMoves);
        }

        public bool IsHasPokemonCanBattle()
        {
            var pokemon = pokemonParty.Find(x => x.CurrentHp > 0);
            return pokemon != null;
        }
        
        public IEnumerator CheckEffective(float dameEffectType)
        {
            if (dameEffectType > 1)
                yield return SetDialogText($"It's super effective");
            if (dameEffectType < 1)
                yield return SetDialogText($"It's not very effective");
        }

        public IEnumerator EndingBattle()
        {
            onBattleEnded.Raised(false);
            SoundManager.Instance.PlayMapMusic();
            yield return SceneLoader.UnLoadSceneAsync(SceneNameConstraints.BATTLE_SCENE);
        }

        public void Run()
        {
            onBattleEnded.Raised(false);
            SoundManager.Instance.PlayMapMusic();
            StartCoroutine(SceneLoader.UnLoadSceneAsync(SceneNameConstraints.BATTLE_SCENE));
        }

        public float CalculateDameEffectType(PokemonType pokemonType, PokemonType moveType)
        {
            float dameTypeEffect = 1;
            switch (pokemonType)
            {
                case PokemonType.Fire:
                    switch (moveType)
                    {
                        case PokemonType.Fire:
                            break;
                        case PokemonType.Grass:
                            dameTypeEffect = dameTypeEffect / 2;
                            break;
                        case PokemonType.Normal:
                            break;
                        case PokemonType.Water:
                            dameTypeEffect = dameTypeEffect * 2;
                            break;
                    }

                    break;
                case PokemonType.Normal:
                    switch (moveType)
                    {
                        case PokemonType.Fire:
                        case PokemonType.Grass:
                        case PokemonType.Normal:
                        case PokemonType.Water:
                            break;
                    }

                    break;

                case PokemonType.Water:
                    switch (moveType)
                    {
                        case PokemonType.Normal:
                            break;
                        case PokemonType.Fire:
                            dameTypeEffect = dameTypeEffect / 2;
                            break;
                        case PokemonType.Water:
                            break;
                        case PokemonType.Grass:
                            dameTypeEffect = dameTypeEffect * 2;
                            break;
                    }

                    break;
                case PokemonType.Grass:
                    switch (moveType)
                    {
                        case PokemonType.Normal:
                            break;
                        case PokemonType.Fire:
                            dameTypeEffect = dameTypeEffect * 2;
                            break;
                        case PokemonType.Water:
                            dameTypeEffect = dameTypeEffect / 2;
                            break;
                        case PokemonType.Grass:
                            break;
                    }

                    break;
                case PokemonType.None:
                    dameTypeEffect = 1;
                    break;
            }

            return dameTypeEffect;
        }
    }
}