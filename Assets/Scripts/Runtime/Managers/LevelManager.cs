using Runtime.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private int CurrentLevelIndex = 1; 
        
        
        private void Start()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            //CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            //CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        }

       

        private void OnRestartLevel()
        {
             SceneManager.LoadScene("Level" + CurrentLevelIndex);
        }

        private void OnLevelSuccessful()
        {
            CurrentLevelIndex++;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelIndexInt, CurrentLevelIndex);
            SceneManager.LoadScene("Level" + CurrentLevelIndex);
        }
      

        private void OnDisable()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel; 
            //CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            //CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
        }
    }
}