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
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button restartLevelButton;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        
        private int coinAmount;
        private bool isSettingsClicked = false;

      
        private void Start()
        {
            AddListeners();
            levelText.text = $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString()}";
            SubscribeEvents();
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            coinText.text = PlayerPrefs.GetInt(PlayerPrefsKeys.CoinsInt).ToString();
            levelText.text =  $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt)}";
        }
        #region Buttons
        private void AddListeners()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
            restartLevelButton.onClick.AddListener(OnRestartLevelButtonClicked);
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
        
        private void OnNextLevelButtonClicked()
        {
            // CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            CurrencyManager.Instance.IncressCoinAmount(100);
            //level index ++ ?
            UpdateUI();
        }
        
        private void OnRestartLevelButtonClicked()
        {
           //CoreGameSignals.Instance.onRestartLevel?.Invoke();
        }
        
        
        private void RemoveListeners()
        {
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
            restartLevelButton.onClick.RemoveListener(OnRestartLevelButtonClicked);
        }
        #endregion
        #region Events
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        }
        private void OnLevelSuccessful()
        {
            levelCompletePanel.SetActive(true);
            UpdateUI();
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
        
      

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        }

        #endregion
        private void OnDisable()
        {
            UnSubscribeEvents();
            RemoveListeners();
        }
    }
}