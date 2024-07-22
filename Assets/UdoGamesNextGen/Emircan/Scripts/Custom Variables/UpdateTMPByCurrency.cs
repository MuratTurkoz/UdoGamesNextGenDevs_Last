using TMPro;
using MelonMath;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class UpdateTMPByCurrency : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldTMP;
        [SerializeField] private Int _currencySO;

        private void Start()
        {
            UpdateTMPSimple(_currencySO.Value);
        }

        private void OnEnable() {
            UpdateTMP(_currencySO.Value);
            _currencySO.OnValueChanged += UpdateTMP;
        }

        private void OnDisable() {
            _currencySO.OnValueChanged -= UpdateTMP;
        }

        protected virtual void UpdateTMP(int amount)
        {
            _goldTMP.SetText(NumberConverter.ConvertToString(amount));
        }

        protected virtual void UpdateTMPSimple(int amount)
        {
            _goldTMP.SetText(NumberConverter.ConvertToString(amount));
        }
    }
}
