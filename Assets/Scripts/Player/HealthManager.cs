using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public GameObject player;
    public BloodEffectController bloodEffectController;
    public float healthAmount = 100f;
    public float initialHealthDecreaseRate = 11f;
    public float checkInterval = 0.5f;

    public OxygenManager oxygenManager;

    private bool isDecreasingHealth = false;
    private float currentHealthDecreaseRate;
    public float decreasingRate = 1.5f;


    [Header("Collectable Object Part")]
    public List<CollectableProperty> collectableProperties;

    void Start()
    {
        ResetHealth();
    }

    private void OnEnable() {
        if (GameSceneManager.Instance)
            GameSceneManager.Instance.OnPlayerEnteredOcean += ResetHealth;
    }

    private void OnDisable() {
        if (GameSceneManager.Instance)
            GameSceneManager.Instance.OnPlayerEnteredOcean -= ResetHealth;
    }

    private void ResetHealth()
    {
        isDecreasingHealth = false;
        /* bloodEffectController.StopEffect(); */
        healthAmount = 100;
        healthBar.fillAmount = 1;
        currentHealthDecreaseRate = initialHealthDecreaseRate;
        player.transform.tag = "Player";
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
            bloodEffectController.StartEffect();
            StartCoroutine(DecreaseHealthCoroutine());
        }
    }

    public void Died()
    {
        
        player.GetComponent<Swimming>().PlayerDied();
        player.transform.tag = "New tag";
        bloodEffectController.StopEffect();

        foreach (var item in collectableProperties)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                BaseInventory.Instance.RemoveItem(item.itemSO);
            }
            item.DeleteInventory(); // öldükten sonra tüm envanter siliniyor
        }

        UIManager.Instance.ShowYouDiedUI();
    }

    private float _lastDamageAudioTime;

    public void GetDamage(float damage)
    {
        if (Time.time >= _lastDamageAudioTime + 1f)
        {
            _lastDamageAudioTime = Time.time;
            AudioManager.Instance.PlayFishDamage(player.transform.position);
        }
        DamageIndicatorManager.Instance.ShowDamage(player.transform.position, damage, DamageIndicatorType.PlayerIndicator);
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        bloodEffectController.StartEffectJustOne();

        if(healthAmount <=0)
        {
            Died();
        }

    }

    private IEnumerator DecreaseHealthCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            healthAmount -= currentHealthDecreaseRate;
            DamageIndicatorManager.Instance.ShowDamage(player.transform.position + new Vector3(0, 0.5f, 0), (int)currentHealthDecreaseRate, DamageIndicatorType.PlayerIndicator);
            AudioManager.Instance.PlayStrangle(player.transform.position);
            healthBar.fillAmount = healthAmount / 100f;

            if (healthAmount <= 0)
            {
                healthAmount = 0;
                Died();
                break;
            }

            currentHealthDecreaseRate *= decreasingRate;
        }
    }
}
