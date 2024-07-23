using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Property 1", menuName = "CollectableProperty/Property", order = 1)]
public class CollectableProperty : ScriptableObject
{
    public int Cost;
    public string Name;
    public enum Conditions
    {
        Damaged,
        Moderate,
        Good
    }
    public Conditions condition;

    public enum Frequency{
        High,
        Avarage,
        Low
    }
    public Frequency frequency;
}
