using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
         public GameStates GameStates { get; private set;}
         
        #region Shortcuts
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SetGameStateLevelFail();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SetGameStateLevelComplete();
            }
              
        }
        #endregion
        
        public void SetGameStateGameplay()
        {
            GameStates = GameStates.Gameplay;
            InputSignals.Instance.onEnableInput?.Invoke();
        }
        public void SetGameStateSettingsScreen()
        {
            GameStates = GameStates.SettingsScreen;
            UISignals.Instance.onSettingsButtonClicked?.Invoke();   
            InputSignals.Instance.onDisableInput?.Invoke();
        }
        
        public void SetGameStateLevelComplete()
        {
            GameStates = GameStates.LevelComplete;
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }
        
        public void SetGameStateLevelFail()
        {
            GameStates = GameStates.LevelFail;
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }
        
    }
}