using Runtime.Signals;
using TMPro;
using Unity.VisualScripting;
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
        
        private bool isSettingsClicked = false;
        private void Start()
        {
            SubscribeEvents();
            UpdateUI();
        }

        private void UpdateUI()
        {
           coinAmount = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinsInt);
           coinText.text = coinAmount.ToString();
           levelText.text =  $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt)}";
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            UISignals.Instance.onSettingsButtonClicked += OnSettingsButtonClicked;
        }

        private void OnLevelInitialize()
        {
            //todo coin refresh, level refresh,
        }

        private void OnSettingsButtonClicked()
        {
            isSettingsClicked = !isSettingsClicked;
            
            if(isSettingsClicked) settingsPanel.SetActive(true);
            else settingsPanel.SetActive(false);
        }

        private void OnLevelSuccessful()
        {
            UpdateCoin();
            levelText.text = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString();
            PlayerPrefs.SetInt(PlayerPrefsKeys.CoinsInt, coinAmount);
        }

        private void UpdateCoin()
        {
            coinAmount += coinAmount + 100 * PlayerPrefs.GetInt(PlayerPrefsKeys.LevelValueInt);
            coinText.text = coinAmount.ToString();
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