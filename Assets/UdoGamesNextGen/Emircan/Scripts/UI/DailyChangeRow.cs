using System;
using System.Collections;
using System.Collections.Generic;
using MelonMath;
using TMPro;
using UdoGames.NextGenDev;
using UnityEngine;

public class DailyChangeRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _priceTMP;

    public void InitRow(CurrencyLog currencyLog)
    {
        _nameTMP.SetText(currencyLog.Log);
        _priceTMP.SetText((currencyLog.Change > 0 ? "+" : "-")  + NumberConverter.ConvertToString(Math.Abs(currencyLog.Change)));
    }
}
