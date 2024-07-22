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
    public bool isOxygenOut = false;

    public HealthManager healthManager;

    private bool oxygenDepletedMessageShown = false;

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

            if (oxygenAmount <= 7f && !isOxygenOut)
            {
                isOxygenOut = true;
            }
        }

        if (isOxygenOut && !oxygenDepletedMessageShown)
        {
            Debug.Log("Oksijen tükendi!");
            oxygenDepletedMessageShown = true;
        }
    }

    public void SetUnderwaterStatus(bool status)
    {
        isUnderwater = status;
    }
}
