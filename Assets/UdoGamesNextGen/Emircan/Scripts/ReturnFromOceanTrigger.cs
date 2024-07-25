using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromOceanTrigger : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowReturnBtn();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseReturnBtn();
        }
    }
}
