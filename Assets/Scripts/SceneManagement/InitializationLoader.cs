using SaveSystem;
using UnityEngine;

namespace SceneManagement
{
    public class InitializationLoader: MonoBehaviour
    {
        [SerializeField] private SaveGameSystem saveSystem;
        private bool hasSaveData;
        
        public void Awake()
        {
            StartCoroutine(SceneLoader.LoadSceneAsync(SceneNameConstraints.MAIN_MENU_SCENE));
            LoadData();
            if(hasSaveData == false)
                InitNewGame();
        }

        private void LoadData()
        {
            saveSystem.LoadBaseData();
            hasSaveData = saveSystem.LoadSaveDataFromDisk();
            saveSystem.LoadPlayerPokemon();
        }

        private void InitNewGame()
        {
            saveSystem.InitFirstPlayGame();
        }
    }
}