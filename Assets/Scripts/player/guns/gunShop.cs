using UnityEngine;

public class gunShop : MonoBehaviour
{
    bool isInShop;
    // Update is called once per frame
    void Update()
    {
        if (isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gunManager.instance.openWeaponPage();
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uicontroller.instance.refreshNoti("Press Q to Open the gun shop");
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
