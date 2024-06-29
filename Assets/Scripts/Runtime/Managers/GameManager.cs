using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
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
        }
        public void SetGameStateSettingsScreen()
        {
            GameStates = GameStates.SettingsScreen;
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