using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class Inventory : MonoBehaviour
{
    [HideInInspector] public int Amount;
    public TextMeshProUGUI AmountText;
    public Image icon;

    public void SetUI(Sprite sprite, int Amount)
    {
        AmountText.text = Amount.ToString();
        icon.sprite = sprite;
    }

   
}
