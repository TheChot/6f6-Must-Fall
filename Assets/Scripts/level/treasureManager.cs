using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureManager : MonoBehaviour
{
    
    #region 
    public static treasureManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [System.Serializable]
    public class treasure
    {
        public string name;        
        public GameObject treasureGfx; 
        public int rarity;
        public int invLimit;
        public string type;// crafting, useable, ammo
        public string shopType;
        public bool inShop;
        public bool isUnlocked;
        //write string multiple times if needs more than one of the ingredients
        //eg crteature scale creature scale will equal creature scale x2
        public string[] unlockReq;
        public int price;
        //public GameObject uigfx;
        

    }

    public treasure[] treasureList;
    
    public GameObject dropTreasure(string _tName)
    {
        for (int i = 0; i < treasureList.Length; i++)
        {
            if(_tName == treasureList[i].name)
            {
                int randomNum = Random.Range(0, 100);
                if(randomNum < treasureList[i].rarity)
                {
                    return treasureList[i].treasureGfx;
                }
            }
        }

        return null;
    }

    public bool addToPlayerInventory(string _name, inventoryPlayer playerInv)
    {
        for (int i = 0; i < treasureList.Length; i++)
        {
            if (treasureList[i].name == _name)
            {
                //add ui gfx later
                playerInv.inventoryToAdd(treasureList[i].name, treasureList[i].invLimit, treasureList[i].type);
                return true;
            }
        }

        return false;
    }

    public void saveUnlockedItems(saveController sc)
    {
        //count the items in the treasure list that arre in the store
        int _storeItemsCount = 0;

        for (int i = 0; i < treasureList.Length; i++)
        {
            if (treasureList[i].inShop)
            {
                if (treasureList[i].isUnlocked)
                {
                    _storeItemsCount++;
                }
            }
        }

        //save the indexes of the unlocked items
        int _storeCounter = 0;
        sc.unlockedStoreItems = new int[_storeItemsCount];
        for (int i = 0; i < treasureList.Length; i++)
        {
            if (treasureList[i].inShop)
            {
                if (treasureList[i].isUnlocked)
                {
                    sc.unlockedStoreItems[_storeCounter] = i;
                    _storeCounter++;
                }
            }
        }

    }

    public void loadUnlockedStoreItems(saveData _savedata)
    {
        if (_savedata != null)
        {
            //unlock items by index
            if (_savedata.unlockedStoreItems.Length > 0)
            {
                for (int i = 0; i < _savedata.unlockedStoreItems.Length; i++)
                {
                    treasureList[_savedata.unlockedStoreItems[i]].isUnlocked = true;
                }
            }
        }
        
        
    }
}
