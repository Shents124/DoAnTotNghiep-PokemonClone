using System.Collections;
using GameEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private VoidEventSO startGame;
        [SerializeField] private VoidEventSO quitGame;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            startGame.OnRaisedEvent += StartGame;
            quitGame.OnRaisedEvent += QuitGame;
        }

        private void OnDisable()
        {
            startGame.OnRaisedEvent -= StartGame;
            quitGame.OnRaisedEvent -= QuitGame;
        }

        private void StartGame()
        {
            StartCoroutine(LoadSceneAsync(SceneNameConstraints.WORLD_SCENE));
            StartCoroutine(UnLoadSceneAsync(SceneNameConstraints.MAIN_MENU_SCENE));
            SoundManager.Instance.PlayMapMusic();
        }

        private void QuitGame()
        {
            Application.Quit();
        }
        
        public static IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public static IEnumerator UnLoadSceneAsync(string sceneName)
        {
            yield return SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
