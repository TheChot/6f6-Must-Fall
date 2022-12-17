using UnityEngine;

public class shopController : MonoBehaviour
{
    bool isInShop;
    // Update is called once per frame
    void Update()
    {
        if (isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shopManager.instance.openShop();
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
