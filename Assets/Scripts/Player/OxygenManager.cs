using System.Collections;
using System.Collections.Generic;
using UdoGames.NextGenDev;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public Int MaxOygen;
    public Slider oxygenBar;
    private float maxOxygenAmount = 20;
    private float oxygenAmount;
    private float oxygenDepletionRate = 1f;
    public bool isUnderwater = false;
    public bool isOxygenOut = false;

    public HealthManager healthManager;

    private bool oxygenDepletedMessageShown = false;

    void Start()
    {
        ResetOxygen();
    }

    private void OnEnable() {
        if (GameSceneManager.Instance)
            GameSceneManager.Instance.OnPlayerEnteredOcean += ResetOxygen;
    }

    private void OnDisable() {
        if (GameSceneManager.Instance)
            GameSceneManager.Instance.OnPlayerEnteredOcean -= ResetOxygen;
    }

    public void ResetOxygen()
    {
        if (MaxOygen != null) maxOxygenAmount = MaxOygen.Value;
        oxygenAmount = maxOxygenAmount;
        oxygenBar.value = 1f;
        isOxygenOut = false;
        oxygenDepletedMessageShown = false;
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
            oxygenBar.value = oxygenAmount / maxOxygenAmount;

            if (oxygenAmount <= 5f && !isOxygenOut)
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
