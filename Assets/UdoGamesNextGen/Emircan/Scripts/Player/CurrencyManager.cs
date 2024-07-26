using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [System.Serializable]
    public class CurrencyLog
    {
        public string Log;
        public int Change;

        public CurrencyLog(string log, int change)
        {
            this.Log = log;
            this.Change = change;
        }
    }

    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance { get; private set;}

        [SerializeField] private Int _currentGold;

        private MoneyIconManager _moneyIconManager;

        public int Gold => _currentGold.Value;

        private const string GOLD_SAVE_KEY = "playerGold";

        private List<CurrencyLog> _currencyLogs;

        private void Awake()
        {
            Instance = this;
            _currencyLogs = new List<CurrencyLog>();
            _moneyIconManager = FindObjectOfType<MoneyIconManager>();
            LoadGold();
        }

        public List<CurrencyLog> PopCurrencyLogs()
        {
            var logs = new List<CurrencyLog>(_currencyLogs);
            _currencyLogs.Clear();
            return logs;
        }

        private void LoadGold()
        {
            _currentGold.Value = PlayerPrefs.GetInt(GOLD_SAVE_KEY, 0);
        }

        private void SaveGold()
        {
            PlayerPrefs.SetInt(GOLD_SAVE_KEY, _currentGold.Value);
        }

        public void AddGold(int delta, string log)
        {
            AudioManager.Instance.PlayCoinSound();
            _currencyLogs.Add(new CurrencyLog(log, delta));
            deltaMoney = delta;
            _moneyIconManager.ShowMoneyIcons(UpdateMoney);
        }

        private int deltaMoney;

        private void UpdateMoney()
        {
            _currentGold.Value += deltaMoney;
            SaveGold();
        }

        public void ReduceGold(int delta, string log)
        {
            _currencyLogs.Add(new CurrencyLog(log, -delta));
            _currentGold.Value -= delta;
            _currentGold.Value = Mathf.Max(0, _currentGold.Value);
            SaveGold();
        }
    }
}