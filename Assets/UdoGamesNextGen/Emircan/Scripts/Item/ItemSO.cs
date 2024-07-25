using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ ItemData")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public int EstimatedPrice;
    public Sprite Icon => collectableProperty.iconImage;
    public CollectableProperty collectableProperty;
}
