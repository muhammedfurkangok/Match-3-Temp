using Runtime.Data.UnityObject;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private int startTimeInSeconds;

        private int _time;
        private bool _isTimerRunning;

        private void Start()
        {
            _time = Resources.Load<CD_LevelTime>("Data/CD_LevelTime").levelData[PlayerPrefs.GetInt(PlayerPrefsKeys.LevelValueInt)].timeInSeconds;
            _isTimerRunning = true;
            UpdateTimerText();
            StartCoroutine(TimerCountdown());
        }

        private void UpdateTimerText()
        {
            int minutes = _time / 60;
            int seconds = _time % 60;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private System.Collections.IEnumerator TimerCountdown()
        {
            while (_isTimerRunning && _time > 0)
            {
                yield return new WaitForSeconds(1);
                _time--;
                UpdateTimerText();

                if (_time <= 0)
                {
                    _isTimerRunning = false;
                    OnTimerEnd();
                }
            }
        }

        private void OnTimerEnd()
        {
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }
    }
}