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

        private const string DAY_SAVE_KEY = "daySave";
        private const string WEEK_SAVE_KEY = "weekSave";

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
                EndWeek();
            }
            else
            {
                UIManager.Instance.ShowDailyEarnings();
                /* UIManager.Instance.ResetUIToStart(); */
            }
        }

        private int rent = 300;

        private void EndWeek()
        {
            OnWeekEnd?.Invoke();
            _weeks.Value++;
            CurrencyManager.Instance.ReduceGold(rent, "Rent");
            UIManager.Instance.ShowDailyEarnings();
            /* UIManager.Instance.ResetUIToStart(); */
        }
    }
}