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
        [SerializeField] private Button retryLevelButton;
        [SerializeField] private Button restartLevelButton;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI levelText;
        
        private bool isSettingsClicked = false;

      
        private void Start()
        {
            AddListeners();
            SubscribeEvents();
            UpdateCoinText();
           
            levelText.text = $"Level {PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelIndexInt).ToString()}";
        }

        private void UpdateCoinText()
        {
            coinText.text = CurrencyManager.Instance.GetCoinAmount().ToString(); 
        }
        
        #region Buttons
        private void AddListeners()
        {
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
            restartLevelButton.onClick.AddListener(OnRestartLevelButtonClicked);
            retryLevelButton.onClick.AddListener(OnRestartLevelButtonClicked);
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
            CurrencyManager.Instance.IncressCoinAmount();
            //unitask 2 sn ve animasyon
            LevelManager.Instance.NextLevel();
        }
        
        private void OnRestartLevelButtonClicked()
        {
            LevelManager.Instance.RestartLevel();
        }
        
        private void RemoveListeners()
        {
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
            restartLevelButton.onClick.RemoveListener(OnRestartLevelButtonClicked);
            retryLevelButton.onClick.RemoveListener(OnRestartLevelButtonClicked);
        }
        #endregion
        #region Events
        private void SubscribeEvents()
        {
            GameManager.Instance.OnLevelSuccessful += OnLevelSuccessful;
            GameManager.Instance.OnLevelFailed += OnLevelFailed;
        }
        private void OnLevelSuccessful()
        {
            levelCompletePanel.SetActive(true);
        }
        
        private void OnLevelFailed()
        {
            levelFailPanel.SetActive(true);
        }
        
        private void UnSubscribeEvents()
        {
            GameManager.Instance.OnLevelSuccessful -= OnLevelSuccessful;
            GameManager.Instance.OnLevelFailed -= OnLevelFailed;
        }

        #endregion
        private void OnDisable()
        {
            UnSubscribeEvents();
            RemoveListeners();
        }
    }
}