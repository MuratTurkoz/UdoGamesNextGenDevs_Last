using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance { get; private set;}

        [SerializeField] private Int _currentGold;

        public int Gold => _currentGold.Value;

        private const string GOLD_SAVE_KEY = "playerGold";

        private void Awake()
        {
            Instance = this;
            LoadGold();
        }

        private void LoadGold()
        {
            _currentGold.Value = PlayerPrefs.GetInt(GOLD_SAVE_KEY, 0);
        }

        private void SaveGold()
        {
            PlayerPrefs.SetInt(GOLD_SAVE_KEY, _currentGold.Value);
        }

        public void AddGold(int delta)
        {
            _currentGold.Value += delta;
        }

        public void ReduceGold(int delta)
        {
            _currentGold.Value -= delta;
            _currentGold.Value = Mathf.Max(0, _currentGold.Value);
        }
    }
}