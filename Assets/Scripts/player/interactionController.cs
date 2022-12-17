using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionController : MonoBehaviour
{
    public Transform interactionPoint;
    public float rayLength = 4f;
    public Transform carryPoint;
    public Transform dropPoint;
    //gunInventory gi;
    public LayerMask whatIsRescuee;
    //Add an array of characters that can be rescued and then use the index to find them
    public GameObject rescueCharacter;
    public GameObject rescuedCharacter;
    public bool isCarrying;
    public bool isInDropVicinity;
    // Start is called before the first frame update
    void Start()
    {
        //gi = gunManager.instance.gi;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(interactionPoint.position, interactionPoint.TransformDirection(Vector3.forward), out hit, rayLength, whatIsRescuee))
        {
            if (!isCarrying)
            {
                uicontroller.instance.hoverOverText = "Press E to Pick Up";
                Debug.Log("found rescuee");
                //if the player is not carrying they can carry
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("rescuing");
                    GameObject _rescueCharacter = Instantiate(rescueCharacter, carryPoint.position, carryPoint.rotation);
                    _rescueCharacter.transform.SetParent(carryPoint);
                    Destroy(hit.collider.gameObject);
                    isCarrying = true;
                    gunManager.instance.gi.unequipWeapon();
                }
            }
            
            
        }
        else
        {
            uicontroller.instance.hoverOverText = "";
        }

        if(isCarrying && isInDropVicinity)
        {
            uicontroller.instance.hoverOverText = "Press E to Drop";
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("rescuing");
                GameObject _droppedCharacter = Instantiate(rescuedCharacter, dropPoint.position, dropPoint.rotation);
                Destroy(carryPoint.GetChild(0).gameObject);
                isCarrying = false;
                missionManager.instance.totalRescued++;
                scoreManager.instance.score += scoreManager.instance.scoreToAdd;
                uicontroller.instance.noti = "RESCUE COMPLETE";
                uicontroller.instance.isNoti = true;

            }

        }
    }
}
