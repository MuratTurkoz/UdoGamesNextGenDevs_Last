using System.Collections;
using System.Collections.Generic;
using UdoGames.NextGenDev;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}

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

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _showUpgradeBtn.onClick.AddListener(ShowUpgradePanel);
        _diveBtn.onClick.AddListener(StartDive);
        _startDayBtn.onClick.AddListener(StartSellPhase);
        _endDayBtn.onClick.AddListener(EndDay);
        _returnShopBtn.onClick.AddListener(ReturnShop);
        _closeUpgradePanelBtn.onClick.AddListener(CloseUpgradePanel);
        _closeDailyEarningsBtn.onClick.AddListener(SkipDailyEarnings);

        _oceanPanel.SetActive(false);
        _playerShopPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(false);
        _endDayBtn.gameObject.SetActive(false);
    }

    private void CloseUpgradePanel()
    {
        _upgradePanel.SetActive(false);
    }

    private void StartDive()
    {
        _diveBtn.gameObject.SetActive(false);
        _playerShopPanel.SetActive(false);
        _oceanPanel.SetActive(true);
    }

    private void ReturnShop()
    {
        _oceanPanel.SetActive(false);
        _playerShopPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(true);
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
        ShowDailyEarnings();
    }

    private void ShowDailyEarnings()
    {
        _gameStartBtnsParent.SetActive(false);
        _dailyEarningsPanel.SetActive(true);
    }

    private void SkipDailyEarnings()
    {
        _dailyEarningsPanel.SetActive(false);
        DayManager.Instance.EndDay();
    }

    public void ResetUIToStart()
    {
        _playerShopPanel.SetActive(true);
        _startDayBtn.gameObject.SetActive(false);
        _endDayBtn.gameObject.SetActive(false);
        _gameStartBtnsParent.SetActive(true);
        _diveBtn.gameObject.SetActive(true);
    }
    
}
