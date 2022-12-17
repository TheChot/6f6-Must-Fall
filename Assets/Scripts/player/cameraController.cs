using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 50f;
    public Transform playerBody;
    public Transform playerChest;
    float xRotation = 6f;
    public float maxRotY = 90f;
    public float minRotY = -90f;
    playerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        pm = levelController.instance.thePlayer.GetComponent<playerMovement>();
        //get sensitivity from settings
        //mouseXSensitivity = PlayerPrefs.GetFloat("X Sensitivity", 100f);
        //mouseYSensitivity = PlayerPrefs.GetFloat("Y Sensitivity", 50f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.canControl)
        {
            //get inputs 
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;
            //to look up n down you rotate on the x axis
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minRotY, maxRotY);

            //turn camera
            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //turn player chest
            playerChest.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //turn body from chest region when rig is available
            //playerChest.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
