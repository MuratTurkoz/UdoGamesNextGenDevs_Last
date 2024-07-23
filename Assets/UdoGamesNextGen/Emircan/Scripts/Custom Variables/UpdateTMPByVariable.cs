using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class UpdateTMPByVariable : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;
        [SerializeField] private Int _variable;

        private void Start()
        {
            UpdateTMP(_variable.Value);
        }

        private void OnEnable() {
            UpdateTMP(_variable.Value);
            _variable.OnValueChanged += UpdateTMP;
        }

        private void OnDisable() {
            _variable.OnValueChanged -= UpdateTMP;
        }

        protected virtual void UpdateTMP(int amount)
        {
            _tmp.SetText(_variable.GetText());
        }

    }
}