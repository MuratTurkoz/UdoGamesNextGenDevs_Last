using System.Collections;
using System.Collections.Generic;
using TMPro;
using UdoGames.NextGenDev;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}

    [SerializeField] private GameObject _generalPanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _playerShopPanel;
    [SerializeField] private GameObject _oceanPanel;
    [SerializeField] private Button _showUpgradeBtn;
    [SerializeField] private Button _diveBtn;
    [SerializeField] private Button _startDayBtn;
    [SerializeField] private Button _endDayBtn;
    [SerializeField] private GameObject _gameStartBtnsParent;
    [SerializeField] private GameObject _dailyEarningsPanel;

    [SerializeField] private Button _returnShopBtn;
    [SerializeField] private Button _closeDailyEarningsBtn;
    [SerializeField] private Button _closeUpgradePanelBtn;
    [SerializeField] private Button _settingsBtn;

    private void Awake() {
        Instance = this;
        _dailyChangeRowList = new List<DailyChangeRow>();
    }

    public void ShowReturnBtn()
    {
        _returnShopBtn.gameObject.SetActive(true);
    }

    public void CloseReturnBtn()
    {
        _returnShopBtn.gameObject.SetActive(false);
    }

    private void Start() {
        _showUpgradeBtn.onClick.AddListener(ShowUpgradePanel);
        _diveBtn.onClick.AddListener(StartDive);
        _startDayBtn.onClick.AddListener(StartSellPhase);
        _endDayBtn.onClick.AddListener(EndDay);
        _returnShopBtn.onClick.AddListener(ReturnShop);
        _closeUpgradePanelBtn.onClick.AddListener(CloseUpgradePanel);
        _closeDailyEarningsBtn.onClick.AddListener(SkipDailyEarnings);
        _settingsBtn.onClick.AddListener(ToggleSettings);
        _toggleSoundBtn.onClick.AddListener(ToggleSound);

        bool isOn = PlayerPrefs.GetInt("soundIsOn", 1) == 1;
        AudioListener.volume = isOn ? 1 : 0;
        _soundBtnImage.sprite = isOn ? _soundOn : _soundOff;

        _oceanPanel.SetActive(false);
        _playerShopPanel.SetActive(true);
        _generalPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(false);
        _endDayBtn.gameObject.SetActive(false);
        GameSceneManager.Instance.OnPlayerEnteredMarket += OnMarketOpened;
        GameSceneManager.Instance.OnPlayerEnteredOcean += CloseReturnBtn;
        _resetDayBtn.onClick.AddListener(ResetStartDay);

        _rentTMP.SetText("Rent: " + DayManager.Instance.Rent);
    }

    private void ToggleSound()
    {
        bool isOn = AudioListener.volume == 1;
        AudioListener.volume = isOn ? 0 : 1;
        PlayerPrefs.SetInt("soundIsOn", (int)AudioListener.volume);
        _soundBtnImage.sprite = isOn ? _soundOff : _soundOn;
    }

    private void ToggleSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
    }

    private void CloseUpgradePanel()
    {
        _upgradePanel.SetActive(false);
    }

    private void StartDive()
    {
        _returnShopBtn.gameObject.SetActive(false);
        _generalPanel.SetActive(false);
        _diveBtn.gameObject.SetActive(false);
        _playerShopPanel.SetActive(false);
        _oceanPanel.SetActive(true);
        GameSceneManager.Instance.ChangeScene(GameScene.Ocean);
    }

    private void OnMarketOpened()
    {
        _generalPanel.SetActive(true);
        _playerShopPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(true);
    }

    private void ReturnShop()
    {
        _oceanPanel.SetActive(false);
        CameraController.Instance.SetCameraTopdown();
        GameSceneManager.Instance.ChangeScene(GameScene.Market);
    }

    private void ShowUpgradePanel()
    {
        _upgradePanel.SetActive(true);
    }

    private void StartSellPhase()
    {
        _startDayBtn.gameObject.SetActive(false);
        _gameStartBtnsParent.SetActive(false);
        DayManager.Instance.StartSellPhase();

        CameraController.Instance.SetCameraPov();
    }

    public void ShowEndDayBtn()
    {
        _endDayBtn.gameObject.SetActive(true);
    }

    public void ShowBtns()
    {
        _gameStartBtnsParent.SetActive(true);
    }

    private void EndDay()
    {
        _endDayBtn.gameObject.SetActive(false);
        /* ShowDailyEarnings(); */
        DayManager.Instance.EndDay();
        CameraController.Instance.SetCameraTopdown();
    }

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _toggleSoundBtn;
    [SerializeField] private Image _soundBtnImage;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    [SerializeField] private Transform _dailyContentParent;
    [SerializeField] private DailyChangeRow _dailyChangeRowPrefab;
    private List<DailyChangeRow> _dailyChangeRowList;

    public void ShowDailyEarnings()
    {
        _gameStartBtnsParent.SetActive(false);
        if (_dailyChangeRowList.Count > 0)
        {
            for (int i = 0; i < _dailyChangeRowList.Count; i++)
            {
                Destroy(_dailyChangeRowList[i].gameObject);
            }
            _dailyChangeRowList.Clear();
        }
        foreach (var daily in CurrencyManager.Instance.PopCurrencyLogs())
        {
            var row = Instantiate(_dailyChangeRowPrefab, _dailyContentParent);
            row.InitRow(daily);
            _dailyChangeRowList.Add(row);
        }
        _dailyEarningsPanel.SetActive(true);
    }


    [SerializeField] private GameObject _loseGamePanel;
    [SerializeField] private Button _restartAllGameBtn;

    private void RestartAllGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField] private TextMeshProUGUI _rentTMP;

    private void SkipDailyEarnings()
    {
        _dailyEarningsPanel.SetActive(false);

        if (CurrencyManager.Instance.Gold >= 0)
        {
            ResetUIToStart();
        }
        else
        {
            _loseGamePanel.SetActive(true);
        }
        /* DayManager.Instance.EndDay(); */
    }

    public void ResetUIToStart()
    {
        foreach (var row in _dailyChangeRowList)
        {
            Destroy(row);
        }
        _dailyChangeRowList.Clear();
        _playerShopPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(false);
        _endDayBtn.gameObject.SetActive(false);
        _gameStartBtnsParent.SetActive(true);
        _diveBtn.gameObject.SetActive(true);
        _returnShopBtn.gameObject.SetActive(false);
        _rentTMP.SetText("Rent: " + DayManager.Instance.Rent);
    }

    [SerializeField] private GameObject _youDiedPanel;
    [SerializeField] private Button _resetDayBtn;

    public void ShowYouDiedUI()
    {
        _youDiedPanel.SetActive(true);
    }

    private void ResetStartDay()
    {
        _youDiedPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
