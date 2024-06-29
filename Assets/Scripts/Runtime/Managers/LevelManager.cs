using Runtime.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
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