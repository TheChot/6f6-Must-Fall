using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeShop : MonoBehaviour
{
    bool isInShop;
    // Update is called once per frame
    void Update()
    {
        if (isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                characterUpgrade.instance.openUpgradePage();
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uicontroller.instance.refreshNoti("Press Q to Open the shop");
            isInShop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInShop = false;
        }
    }
}
