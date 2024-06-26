using Runtime.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevelIndex;
        
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