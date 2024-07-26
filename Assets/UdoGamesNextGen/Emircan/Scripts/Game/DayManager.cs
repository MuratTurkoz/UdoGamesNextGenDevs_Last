using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class DayManager : MonoBehaviour
    {
        public static DayManager Instance { get; private set; }

        [SerializeField] private Int _day;
        [SerializeField] private Int _weeks;

        public bool IsGameEnd;

        private const string DAY_SAVE_KEY = "daySave";
        private const string WEEK_SAVE_KEY = "weekSave";

        public int Rent => rent * _weeks.Value;

        public Action OnDayEnd;
        public Action OnWeekEnd;

        private void Awake()
        {
            Instance = this;
            LoadDays();
        }

        /* private void Start() {
            StartSellPhase();
        } */

        private void LoadDays()
        {
            _day.Value = PlayerPrefs.GetInt(DAY_SAVE_KEY, 1);
            _weeks.Value = PlayerPrefs.GetInt(WEEK_SAVE_KEY, 1);
        }

        private void SaveDays()
        {
            PlayerPrefs.SetInt(DAY_SAVE_KEY, _day.Value);
            PlayerPrefs.SetInt(WEEK_SAVE_KEY, _weeks.Value);
        }

        public void StartSellPhase()
        {
            CustomerManager.Instance.CreateCustomers();
            DealManager.Instance.CheckForCustomer();
        }

        public void EndDay()
        {
            OnDayEnd?.Invoke();
            _day.Value++;
            if (_day.Value % 7 == 0)
            {
                /* Debug.Log("day: " + _day.Value);
                Debug.Log("kalan: " + _day.Value % 7); */
                EndWeek();
            }
            else
            {
                UIManager.Instance.ShowDailyEarnings();
                SaveDays();
                /* UIManager.Instance.ResetUIToStart(); */
            }
        }

        [SerializeField] private int rent = 300;

        private void EndWeek()
        {
            OnWeekEnd?.Invoke();
            _weeks.Value++;
            if (CurrencyManager.Instance.Gold < rent * (_weeks.Value - 1)) IsGameEnd = true;
            CurrencyManager.Instance.ReduceGold(rent * (_weeks.Value - 1), "Rent");
            UIManager.Instance.ShowDailyEarnings();
            SaveDays();
            /* UIManager.Instance.ResetUIToStart(); */
        }
    }
}