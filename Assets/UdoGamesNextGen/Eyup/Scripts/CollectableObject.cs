using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public event Action<GameObject> OnCollected;
    public CollectableProperty itemProperty;
    public TextMeshProUGUI nameText;

    void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        nameText.text = itemProperty.Name;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("girdi");
            OnCollected?.Invoke(gameObject);
            // Destroy(gameObject);
        }
    }
}
