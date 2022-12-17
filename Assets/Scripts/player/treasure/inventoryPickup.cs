using UnityEngine;

public class inventoryPickup : MonoBehaviour
{
    public string itemName;
    public int minAdd;
    public int maxAdd;
    treasureAttract theMagnet;

    void Start()
    {
        theMagnet = GetComponent<treasureAttract>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventoryPlayer playerInv = other.gameObject.GetComponent<inventoryPlayer>();
            int _numToAdd = Random.Range(minAdd, maxAdd);
            // inventory pickup function here
            if (playerInv.addToInventory(itemName, _numToAdd))
            {
                Destroy(gameObject);
            }
            else
            {
                theMagnet.enabled = false;
            }
            

        }
    }
}
