using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

         public GameStates GameStates { get; private set;}

        #endregion
        
        public void SetGameStateGameplay()
        {
            GameStates = GameStates.Gameplay;
            //Get Input
        }
        public void SetGameStateSettingsScreen()
        {
            GameStates = GameStates.SettingsScreen;
        }
        
        public void SetGameStateLevelComplete()
        {
            GameStates = GameStates.LevelComplete;
            //OnLevelCompleted
        }
        
        public void SetGameStateLevelFail()
        {
            GameStates = GameStates.LevelFail;
            // OnLevelFailed
        }
       
    }
}