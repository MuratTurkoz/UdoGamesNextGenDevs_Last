using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class Swimming : MonoBehaviour
{

    public float swimSpeed = 5f;
    private bool isSwimming = false;
    private Rigidbody myRigidBody;
    public Animator myAnimator;
    public FixedJoystick fixedJoystick;
    
    // Start is called before the first frame update
    void Start()
    {
       myRigidBody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
       Swim();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Water"){
            isSwimming = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Water"){
            isSwimming = false;
        }
    }
   
   private void Swim() {
    if (isSwimming) {
        myRigidBody.useGravity = false;    

        float xInput = fixedJoystick.Horizontal;   
        float yInput = fixedJoystick.Vertical;

        UnityEngine.Vector3 targetVelocity = myRigidBody.velocity;

        if (xInput == 0 && yInput == 0) {

            if(myRigidBody.velocity.magnitude < 1){
                myAnimator.SetBool("isMove", false);
            }

            // No player input, apply vertical resonance movement
            float amplitude = 0.25f; // Adjust as needed
            float frequency = 1.0f; // Adjust as needed

            float resonanceY = amplitude * Mathf.Sin(Time.time * frequency);
            targetVelocity = new UnityEngine.Vector3(0, resonanceY, 0);
        } else {
            // Player input detected, move accordingly
            float xValue = xInput * swimSpeed;
            float yValue = yInput * swimSpeed;
            targetVelocity += new UnityEngine.Vector3(xValue, yValue, 0);

            myAnimator.SetBool("isMove", true);

            if (xInput > 0) {
                transform.localScale = new UnityEngine.Vector3(1, 1, 1);
            } else if (xInput < 0) {
                transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
            }
        }

        // Smoothly transition to the target velocity
        myRigidBody.velocity = UnityEngine.Vector3.Lerp(myRigidBody.velocity, targetVelocity, Time.deltaTime * 2.5f);
    } else {
        myRigidBody.useGravity = true; 
    }
}



}



