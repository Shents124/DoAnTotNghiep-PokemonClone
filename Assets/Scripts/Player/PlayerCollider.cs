using System.Collections;
using Map;
using Pokemons;
using SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] private PlayerPositionSO playerPositionSo;
        [SerializeField] private LayerMask grassLayer;
        [SerializeField] private PokemonPartyManager pokemonPartyManager;
        [SerializeField] private EncounterWildPokemonEventSO eventSo;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip grassSfx;
        private GrassMapArea grassMapArea;
        [SerializeField] private float timeDelay = 0.5f;
        [SerializeField] private float currentTime;
        [SerializeField]private Collider2D grassCollider2D;
        
        public void CheckEncounter()
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeDelay)
            {
                if (Physics2D.OverlapCircle(transform.position, 0.1f, grassLayer) == true)
                {
                    grassCollider2D = Physics2D.OverlapCircle(transform.position, 0.1f, grassLayer);
                    audioSource.PlayOneShot(grassSfx);
                    grassMapArea = grassCollider2D.GetComponent<GrassMapArea>();
                    if (Random.Range(0, 100) < 50)
                    {
                        currentTime = 0f;
                        StartCoroutine(LoadBattleScene());
                    }
                }
                currentTime = 0f;
            }
        }

        private IEnumerator LoadBattleScene()
        {
            GetComponent<PlayerController>().SetInputActionState(true);
            yield return SceneLoader.LoadSceneAsync(SceneNameConstraints.BATTLE_SCENE);
            var pokemonParty = pokemonPartyManager.GetListPokemonParty();
            eventSo.Raised(pokemonParty, grassMapArea.GetRandomWildPokemon());
            SetCurrentPos();
            SoundManager.Instance.PlayBattleMusic();
        }

        private void SetCurrentPos()
        {
            var position = transform.position;
            playerPositionSo.SetPos(position.x, position.y);
        }
    }
}