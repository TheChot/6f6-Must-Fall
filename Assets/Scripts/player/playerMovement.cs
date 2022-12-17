using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float jumpForce = 4f;
    public bool jetpackEquip = true;
    public float jetpackFloat = 10f;
    public float jetpackFuel = 20f;
    float jetpackFuelReset;
    public float fuelConsumption = 2f;
    public float jetpackRechargeDelay = 2f;
    float rechargeDelayReset;
    public float rechargeSpeed = 10f;
    public float gravity = -9.81f;
    public float gravityScale = 1;
    public float jetpackStartDelay = 0.1f;
    float jetpackStartDelayReset;
    float velocity;
    Vector3 gravityFall;
    public bool canControl = true;

    public Animator uianim;
    
    // Start is called before the first frame update
    void Start()
    {
        jetpackFuelReset = jetpackFuel;
        rechargeDelayReset = jetpackRechargeDelay;
        jetpackStartDelayReset = jetpackStartDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if its game over
        if (levelController.instance.isGameOver)
            return;
        //If grounded jump
        //float _jumpForce = 0;
        if (canControl)
        {
            if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                velocity = Mathf.Sqrt(jumpForce * -2f * (gravity * gravityScale));
                //Debug.Log(controller.isGrounded);

            }
            //Add fuel back to jetpack if grounded
            if (controller.isGrounded)
            {
                jetpackStartDelay = jetpackStartDelayReset;
                if (jetpackFuel < jetpackFuelReset)
                {
                    jetpackRechargeDelay -= fuelConsumption * Time.deltaTime;

                    if (jetpackRechargeDelay <= 0)
                    {
                        jetpackFuel += rechargeSpeed * Time.deltaTime;
                    }


                }

            }

            if (jetpackFuel > jetpackFuelReset)
            {
                jetpackFuel = jetpackFuelReset;
            }



            //jetpack float
            if (!controller.isGrounded)
            {
                jetpackStartDelay -= Time.deltaTime;
                if (jetpackStartDelay < 0)
                {
                    if (Input.GetKey(KeyCode.Space) && jetpackFuel > 0 && jetpackEquip)
                    {
                        velocity += jetpackFloat;
                        //calculate jetpack time limit
                        jetpackFuel -= fuelConsumption * Time.deltaTime;
                        jetpackRechargeDelay = rechargeDelayReset;
                        uianim.SetBool("isHover", true);
                    }
                    else
                    {
                        //calculate gravity       
                        uianim.SetBool("isHover", false);
                        velocity += gravity * gravityScale * Time.deltaTime;
                    }
                }

                if (jetpackFuel < 0)
                {
                    jetpackFuel = 0;
                }
            }

            //get movement input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float _speed = speed;

            //Debug.Log(controller.isGrounded);
            //calculate movement and apply gravity
            Vector3 move = transform.right * x + transform.forward * z + transform.up * velocity;
            //Debug.Log(move);
            //Vector3 _gravityFall = transform.up * velocity;
            //move characters
            controller.Move(move * _speed * Time.deltaTime);
            //controller.Move(_gravityFall * Time.deltaTime);

            uicontroller.instance.jetpackFuel = Mathf.Round(jetpackFuel).ToString();
        }
    }
    //moove n rotate
    public void warpPlayer(Vector3 _pos, float rotx, float roty, float rotz)
    {
        controller.enabled = false;
        transform.position = _pos;
        transform.rotation = Quaternion.Euler(rotx, roty, rotz);
        controller.enabled = true;
    }

    public void increaseJetpack()
    {
        jetpackFuelReset = jetpackFuel;
    }
}
