using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyPickup : MonoBehaviour
{
    public int minAdd;
    public int maxAdd;
    
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventoryPlayer playerInv = other.gameObject.GetComponent<inventoryPlayer>();
            int _numToAdd = Random.Range(minAdd, maxAdd);
            playerInv.moneyCount += _numToAdd;
            uicontroller.instance.refreshNoti("Money Added K" + _numToAdd.ToString());
        }
    }
}
