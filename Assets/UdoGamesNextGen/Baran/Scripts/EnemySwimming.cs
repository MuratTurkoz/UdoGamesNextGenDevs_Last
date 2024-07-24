using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwimming : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public float direction = 1f;
    public float enemyMoveSpeed = 0f;
    public int enemyDamage = 0;
    public bool yAxis = false;
    public bool xAxis = false;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        enemySwim();
    }
    void enemySwim(){
        if(yAxis){
            myRigidbody.velocity = new Vector3(0,enemyMoveSpeed * direction,0);
        }
        else if(xAxis){
            myRigidbody.velocity = new Vector3(enemyMoveSpeed * direction,0,0);
        }
        
    }
}
