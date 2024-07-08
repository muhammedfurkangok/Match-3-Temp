using Cysharp.Threading.Tasks;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private int startTimeInSeconds;
        private int _time;
        
        private void Start()
        {
            startTimeInSeconds = Resources.Load<CD_LevelTime>("Data/CD_LevelTime").levelData[PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt)].timeInSeconds;
            _time = startTimeInSeconds;
            UpdateTimerText();
            PassTimeForCountDown();
        }

        private void UpdateTimerText()
        {
            int minutes = _time / 60;
            int seconds = _time % 60;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        private async void PassTimeForCountDown()
        {
            while (true)
            {
                await UniTask.WaitForSeconds(1);
                if (GameManager.Instance.GameStates != GameStates.Gameplay) await UniTask.WaitUntil(() => GameManager.Instance.GameStates == GameStates.Gameplay);

                _time--;
                UpdateTimerText();

                if (_time != 0) continue;
                OnTimerEnd();
                break;
            }
        }

        private void OnTimerEnd()
        {
            GameManager.Instance.SetGameStateLevelFail();
        }
    }
}