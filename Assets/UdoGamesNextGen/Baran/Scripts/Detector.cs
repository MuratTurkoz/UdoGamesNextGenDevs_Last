using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject enemyObject;
    private void OnTriggerEnter(Collider other) {
       if(other.gameObject.tag == "Obstackle"){
        if(enemyObject.GetComponent<EnemySwimming>().yAxis){
        enemyObject.GetComponent<EnemySwimming>().direction *= -1;
        /* enemyObject.transform.localScale = new Vector3(1,enemyObject.GetComponent<EnemySwimming>().direction, 1); */
        }
        else if(enemyObject.GetComponent<EnemySwimming>().xAxis){
        enemyObject.GetComponent<EnemySwimming>().direction *= -1;
        enemyObject.transform.localScale = new Vector3(enemyObject.GetComponent<EnemySwimming>().direction,1, 1);
        }
       } 
        
       
    }
}
