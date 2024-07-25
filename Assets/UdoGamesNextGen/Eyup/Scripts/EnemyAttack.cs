using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount;
    public HealthManager healthManager;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
            healthManager.GetDamage(damageAmount);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Player"))
            healthManager.GetDamage(damageAmount/100);
    }
}
