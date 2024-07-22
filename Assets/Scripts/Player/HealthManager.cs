using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public float initialHealthDecreaseRate = 11f;
    public float checkInterval = 4f;

    public OxygenManager oxygenManager;

    private bool isDecreasingHealth = false;
    private float currentHealthDecreaseRate;
    public float decreasingRate = 1.5f;

    void Start()
    {
        healthBar.fillAmount = 1f;
        currentHealthDecreaseRate = initialHealthDecreaseRate;
    }

    void Update()
    {
        if (oxygenManager.isOxygenOut && !isDecreasingHealth)
        {
            DecreaseHealthOverTime();
        }
    }

    public void DecreaseHealthOverTime()
    {
        if (!isDecreasingHealth)
        {
            isDecreasingHealth = true;
            StartCoroutine(DecreaseHealthCoroutine());
        }
    }

    private IEnumerator DecreaseHealthCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            healthAmount -= currentHealthDecreaseRate;
            healthBar.fillAmount = healthAmount / 100f;
            
            if (healthAmount <= 0)
            {
                healthAmount = 0;
                Debug.Log("Sağlık tükendi!");
                break;
            }

            currentHealthDecreaseRate *= decreasingRate;
        }
    }
}
