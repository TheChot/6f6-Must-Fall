using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class saveData
{
    public int charLevel;
    public int invLevel;
    public int shieldLevel;
    public int gunLevel;
    public int charUpgradeToken;
    public int gunUpgradeToken;
    public int moneyCount;
    public string[] weaponsInInventory;
    public string[] itemsInInventory;
    public int[] itemsInInventoryCount;
    public int[] unlockedStoreItems;
    public int[] unlockedWeaponsStore;


    //for the rooms
    public int roomNumber;

    //For the doors and the levels
    //public int[] openDoors; //if a door is opened in the world its number in the array is added here

    public saveData (saveController _savedata)
    {
        charLevel = _savedata.charLevel;
        invLevel = _savedata.invLevel;
        shieldLevel = _savedata.shieldLevel;
        gunLevel = _savedata.gunLevel;
        charUpgradeToken = _savedata.charUpgradeToken;
        moneyCount = _savedata.moneyCount;
        weaponsInInventory = new string[_savedata.weaponsInInventory.Length];
        weaponsInInventory = _savedata.weaponsInInventory;
        itemsInInventory = new string[_savedata.itemsInInventory.Length];
        itemsInInventoryCount = new int[_savedata.itemsInInventoryCount.Length];
        itemsInInventory = _savedata.itemsInInventory;
        itemsInInventoryCount = _savedata.itemsInInventoryCount;
        unlockedStoreItems = new int[_savedata.unlockedStoreItems.Length];
        unlockedWeaponsStore = new int[_savedata.unlockedWeaponsStore.Length];
        unlockedStoreItems = _savedata.unlockedStoreItems;
        unlockedWeaponsStore = _savedata.unlockedWeaponsStore;

        roomNumber = _savedata.roomNumber;
    }
    
}
