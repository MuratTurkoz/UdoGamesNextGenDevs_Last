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
            _weeks.Value = PlayerPrefs.GetInt(WEEK_SAVE_KEY, 0);
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

        private void EndDay()
        {
            
        }
    }
}