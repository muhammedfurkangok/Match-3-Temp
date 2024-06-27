using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject levelFailPanel;
        
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        private int coinAmount;
        private void Start()
        {
            SubscribeEvents();
            PlayerPrefs.SetInt(PlayerPrefsKeys.CoinsInt, coinAmount);
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        }

        private void OnLevelSuccessful()
        {
            coinAmount = coinAmount + 100 * PlayerPrefs.GetInt(PlayerPrefsKeys.LevelValueInt);
            coinText.text = coinAmount.ToString();
            levelText.text = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString();
            PlayerPrefs.SetInt(PlayerPrefsKeys.CoinsInt, coinAmount);
        }

        private void OnLevelFailed()
        {
            levelFailPanel.SetActive(true);
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
        }
        
        
        private void OnDisable()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        }
    }
}