using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomToGo;
    public int doorToEnter;
    bool isAtDoor;
    levelController lc;
    uicontroller uicon;
    void Start()
    {
        lc = levelController.instance;
        uicon = uicontroller.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAtDoor)
        {
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //roomManager.instance.loadNextRoom(roomToGo, doorToEnter);
                lc.roomToLoad = roomToGo;
                lc.doorToEnter = doorToEnter;
                lc.goToNextRoom = true;
                lc.reloadTheScene();
            }
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uicon.refreshNoti("Press Q to Exit The Room");              
            isAtDoor = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAtDoor = false;
        }
    }
}
