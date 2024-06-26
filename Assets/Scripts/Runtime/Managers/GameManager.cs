using System;
using Runtime.Enums;
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
            
            //InputSignals.Instance.onEnableInput();
        }
        public void SetGameStateSettingsScreen()
        {
            GameStates = GameStates.SettingsScreen;
            //InputSignals.Instance.onDisableInput();
        }
        
        public void SetGameStateLevelComplete()
        {
            GameStates = GameStates.LevelComplete;
            //CoreGameSignals.Instance.onLevelSuccessful();
        }
        
        public void SetGameStateLevelFail()
        {
            GameStates = GameStates.LevelFail;
            //CoreGameSignals.Instance.onLevelFailed();
        }
       
    }
}