using System;
using Runtime.Data.UnityObject;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class MoverManager : SingletonMonoBehaviour<MoverManager>
    {
        
       [SerializeField] private int _moveCount;
        
        private void Start()
        {
            if (!RemoteConfigDummy.hasMoveCounter)
            {
                return;
            }

            Init();
        }

        private void Init()
        {
            _moveCount = RemoteConfigDummy.moves[PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt)];
            UIManager.Instance.moverText.text = _moveCount.ToString();
        }

        public void OnInputTaken()
        {
            if (!RemoteConfigDummy.hasMoveCounter)
            {
                return;
            }
            
            
            _moveCount--;
            UIManager.Instance.moverText.text = _moveCount.ToString();
            if (_moveCount <= 0)
            {
                OnMoveEnd();
            }
        }

        private void OnMoveEnd()
        {
            GameManager.Instance.SetGameStateLevelFail();
        }
        
    }
}