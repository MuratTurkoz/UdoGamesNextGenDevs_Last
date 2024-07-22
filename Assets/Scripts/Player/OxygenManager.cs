using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OxygenManager : MonoBehaviour
{
    public Image oxygenBar;
    public float oxygenAmount = 97f;
    public float oxygenDepletionRate = 2f;
    public bool isUnderwater = false;

    void Start()
    {
        oxygenBar.fillAmount = 1f;
    }

    void Update()
    {
        if (isUnderwater)
        {
            OxygenOut();
        }
    }

    public void OxygenOut()
    {
        if (oxygenAmount > 0)
        {
            oxygenAmount -= oxygenDepletionRate * Time.deltaTime;
            oxygenBar.fillAmount = oxygenAmount / 100f;
        }
        else
        {
            Debug.Log("Oksijen tükendi!");
        }
    }

    public void SetUnderwaterStatus(bool status)
    {
        isUnderwater = status;
    }
}
