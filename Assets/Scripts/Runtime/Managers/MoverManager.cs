using System;
using Runtime.Data.UnityObject;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class MoverManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moverText;
        private int _moveCount;


        private void Start()
        {
            _moveCount = Resources.Load<CD_LevelTime>("Data/CD_LevelTime").levelData[PlayerPrefs.GetInt(PlayerPrefsKeys.LevelValueInt)].moveCount;
            moverText.text = _moveCount.ToString();
            InputSignals.Instance.onInputTaken += OnInputTaken;
        }
        
        public void OnInputTaken()
        {
            _moveCount--;
            moverText.text = _moveCount.ToString();
            if (_moveCount <= 0)
            {
                OnMoveEnd();
            }
        }

        private void OnMoveEnd()
        {
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }

        private void OnDisable()
        {
            InputSignals.Instance.onInputTaken -= OnInputTaken;
        }
    }
}