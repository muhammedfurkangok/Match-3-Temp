using Runtime.Extensions;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("Panels")]
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject levelFailPanel;
        
        [Header("Buttons")]
        [SerializeField] private Button settingsButton;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        
        private int coinAmount;
        
        private bool isSettingsClicked = false;

      
        private void Start()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            levelText.text = $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString()}";
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
        }

        private void OnLevelInitialize()
        {
            //todo coin refresh, level refresh,
        }

        private void OnSettingsButtonClicked()
        {
            isSettingsClicked = !isSettingsClicked;

            if (isSettingsClicked)
            {
                settingsPanel.SetActive(true);
                GameManager.Instance.SetGameStateSettingsScreen();
            }
            else
            {
                settingsPanel.SetActive(false);
                GameManager.Instance.SetGameStateGameplay();
            }
                
        }

        private void OnLevelSuccessful()
        {
            levelCompletePanel.SetActive(true);
            UpdateCoin();
            levelText.text = $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString()}";
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
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }
    }
}