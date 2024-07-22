using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class Int : ScriptableObject
    {
        public Action<int> OnValueChanged;
        [SerializeField] private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}
