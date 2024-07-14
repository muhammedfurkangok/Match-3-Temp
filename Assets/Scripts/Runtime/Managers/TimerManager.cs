using System;
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
       [SerializeField] private int _time;
       [SerializeField] private bool _isTimerActive;
        
        
        private void Start()
        {
            if (!RemoteConfigDummy.hasTimer)
            {
                return;
            }
            
            Init();
            UpdateTimerText();
            PassTimeForCountDown();
        }

        private void Init()
        {
            _isTimerActive = true;
            _time = RemoteConfigDummy.timers[PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt)];
        }

        private void UpdateTimerText()
        {
            int minutes = _time / 60;
            int seconds = _time % 60;
            UIManager.Instance.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        private async void PassTimeForCountDown()
        {
            while (_isTimerActive)
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

        private void OnDisable()
        {
            _isTimerActive = false;
        }
    }
}