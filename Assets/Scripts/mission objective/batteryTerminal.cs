using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryTerminal : MonoBehaviour
{
    bool batteryPlaced;
    bool hasBattery;
    bool isInPlace;
    public Animator anim;
    public batteryHost bh;
    inventoryPlayer ip;

    // Update is called once per frame
    void Update()
    {
        if (!batteryPlaced && hasBattery && isInPlace)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                batteryPlaced = true;
                ip.batteryCount--;
                anim.SetBool("batteryPlaced", true);
                bh.btActiveCount += 1;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ip = other.gameObject.GetComponent<inventoryPlayer>();
            if (ip.batteryCount > 0)
            {
                if (!batteryPlaced)
                {
                    hasBattery = true;
                    isInPlace = true;
                    uicontroller.instance.refreshNoti("Press Q To Place Battery");
                }

            }
            else
            {
                if (!batteryPlaced)
                {
                    uicontroller.instance.refreshNoti("Needs Battery");
                }
                
            }
            
            //Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInPlace = false;
        }
    }
}
