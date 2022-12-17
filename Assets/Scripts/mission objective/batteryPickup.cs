using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryPickup : MonoBehaviour
{
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<inventoryPlayer>().batteryCount++;
            uicontroller.instance.refreshNoti("Battery Added To Inventory");
            Destroy(gameObject);
        }
    }
}
