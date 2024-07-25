using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UdoGames.NextGenDev;
using UnityEngine;

public class Swimming : MonoBehaviour
{
    [SerializeField] private Float paletMoveSpeed;
    public float swimSpeed = 5f;
    public float amplitude = 0.25f;
    public float frequency = 1.0f;
    private bool isSwimming = false;
    private Rigidbody myRigidBody;
    public Animator myAnimator;
    public FixedJoystick fixedJoystick;
    public float turnSpeed = 5f; // Yumuşak dönüş için bir hız değeri
    public GameObject playerBody; // Oyuncunun gövdesini temsil eden GameObject

    public Transform StartTransform;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
/*         ResetPosition(); */
        GameSceneManager.Instance.OnPlayerEnteredOcean += ResetPosition;
    }

    private void OnDisable() {
        fixedJoystick.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        if (GameSceneManager.Instance)
            GameSceneManager.Instance.OnPlayerEnteredOcean -= ResetPosition;
    }

    private void ResetPosition()
    {
        if (!StartTransform) return;
        fixedJoystick.gameObject.SetActive(true);
        this.transform.tag = "Player";
        transform.position = StartTransform.position;
        transform.eulerAngles = StartTransform.eulerAngles;
        myAnimator.SetBool("isDied", false);
        myAnimator.SetBool("isMove", false);
    }

    // Update is called once per frame
    void Update()
    {
        Swim();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Water") 
        {
            isSwimming = true;
        }
    }

    public void PlayerDied()
    {
        isSwimming = false;
        myAnimator.SetBool("isDied", true);
    }

    private void Swim() 
    {
        if (isSwimming) 
        {
            myRigidBody.useGravity = false;    

            float xInput = fixedJoystick.Horizontal;   
            float yInput = fixedJoystick.Vertical;

            Vector3 targetVelocity = myRigidBody.velocity;

            if (xInput == 0 && yInput == 0) 
            {
                if (myRigidBody.velocity.magnitude < 1)
                {
                    myAnimator.SetBool("isMove", false);
                }

                // No player input, apply vertical resonance movement
                float resonanceY = amplitude * Mathf.Sin(Time.time * frequency);
                targetVelocity = new Vector3(0, resonanceY, 0);
            } 
            else 
            {
                // Player input detected, move accordingly
                float xValue = xInput * (swimSpeed + (paletMoveSpeed != null ? paletMoveSpeed.Value : 0));
                float yValue = yInput * (swimSpeed + (paletMoveSpeed != null ? paletMoveSpeed.Value : 0));
                targetVelocity += new Vector3(xValue, yValue, 0);

                myAnimator.SetBool("isMove", true);

                // Determine the target rotation based on input direction
                Quaternion targetRotation = Quaternion.identity;
                if (xInput > 0) 
                {
                    targetRotation = Quaternion.Euler(0, 90, 0);
                } 
                else if (xInput < 0) 
                {
                    targetRotation = Quaternion.Euler(0, -90, 0);
                }

                // Smoothly transition to the target rotation for the player body
                playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }

            // Smoothly transition to the target velocity
            myRigidBody.velocity = Vector3.Lerp(myRigidBody.velocity, targetVelocity, Time.deltaTime * 2.5f);
        } 
        else 
        {
            myRigidBody.useGravity = true; 
        }
    }
}
