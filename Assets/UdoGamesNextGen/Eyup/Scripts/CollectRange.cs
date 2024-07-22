using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRange : MonoBehaviour
{
    public float RangeAmount;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(RangeAmount, RangeAmount, RangeAmount);
    }
    private void OnTriggerEnter(Collider other)
    {
        var s = other.TryGetComponent<CollectableObject>(out CollectableObject component);
        if (component != null)
            component.nameText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        var s = other.TryGetComponent<CollectableObject>(out CollectableObject component);
        if (component != null)
            component.nameText.gameObject.SetActive(false);
    }

}
