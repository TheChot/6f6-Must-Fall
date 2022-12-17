using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveController : MonoBehaviour
{
    bool isInSaveSpot;
    public int saveSlot = 0;

    public int charLevel = 1;
    public int invLevel;
    public int shieldLevel;
    public int gunLevel;
    public string[] weaponsInInventory;
    public string[] itemsInInventory;
    public int[] itemsInInventoryCount;
    public int charUpgradeToken;
    public int gunUpgradeToken;
    public int moneyCount;

    public int roomNumber;
    public int[] unlockedStoreItems;
    public int[] unlockedWeaponsStore;

    // Start is called before the first frame update
    void Start()
    {
        saveSlot = PlayerPrefs.GetInt("save slot", levelController.instance.testSaveSlot);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInSaveSpot)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                charLevel = characterUpgrade.instance.charLevel;
                invLevel = characterUpgrade.instance.invLevel;
                shieldLevel = characterUpgrade.instance.shieldLevel;
                charUpgradeToken = characterUpgrade.instance.charUpgradeToken;
                gunUpgradeToken = characterUpgrade.instance.gunUpgradeToken;
                roomNumber = roomManager.instance.currentRoom;
                gunLevel = characterUpgrade.instance.gunLevel;
                gunInventory gi = levelController.instance.thePlayer.GetComponent<gunInventory>();                               
                gi.saveWeapons(this);                
                inventoryPlayer pi = levelController.instance.thePlayer.GetComponent<inventoryPlayer>();                
                pi.savePlayerInventory(this);
                treasureManager.instance.saveUnlockedItems(this);
                gunManager.instance.saveUnlockedWeapons(this);

                //if you want to save items add things above this line
                saveManager.saveData(this, saveSlot);
                uicontroller.instance.refreshNoti("Game Saved");
                //saveManager.saveData
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uicontroller.instance.refreshNoti("Press Q to Save the Game");
            isInSaveSpot = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInSaveSpot = false;
        }
    }
}
