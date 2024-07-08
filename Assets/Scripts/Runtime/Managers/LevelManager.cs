using Runtime.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
            
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt) == 0)
                 PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelIndexInt, 1);
        }

        public void RestartLevel()
        {
             SceneManager.LoadScene("Level" +" " + PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt));
        }

        public void NextLevel()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelIndexInt, PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt) + 1);
            SceneManager.LoadScene("Level" + " "+PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt));
        }
    }
}