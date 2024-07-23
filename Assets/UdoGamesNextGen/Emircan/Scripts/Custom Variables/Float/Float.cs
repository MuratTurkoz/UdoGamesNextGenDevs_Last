using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    public class Float : BaseCustomVariable
    {
        public Action<float> OnValueChanged;
        [SerializeField] private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public override string GetText()
        {
            return _value.ToString();
        }
    }
}
