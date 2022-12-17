using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryPlayer : MonoBehaviour
{
    [System.Serializable]
    public class inventoryItem
    {
        public string name;
        public int invLimit;
        public int invCount;
        public string itemType;
        //public GameObject uigfx;
    }

    List<inventoryItem> inventoryItems;
    public int inventoryLimit;
    int invCountAdd;
    public int moneyCount;
    public inventoryui invUI;
    public statusController sc;
    [HideInInspector]
    public int batteryCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new List<inventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventory();
        }
    }

    public bool addToInventory(string _name, int _invToAdd)
    {
        //check if item is in inventory
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].name == _name)
            {
                if (inventoryItems[i].invCount < inventoryItems[i].invLimit)
                {
                    inventoryItems[i].invCount += _invToAdd;
                    if (inventoryItems[i].invCount > inventoryItems[i].invLimit)
                    {
                        inventoryItems[i].invCount = inventoryItems[i].invLimit;
                    }
                    uicontroller.instance.refreshNoti(_invToAdd.ToString() + " " + _name + " Added To Inventory");
                    return true;
                }
                else 
                {
                    return false;
                }

            }
        }
        if (inventoryItems.Count < inventoryLimit)
        {
            invCountAdd = _invToAdd;
            // retrieve treasure data
            if (treasureManager.instance.addToPlayerInventory(_name, this))
            {
                uicontroller.instance.refreshNoti(_invToAdd.ToString() + " " + _name + " Added To Inventory");
                return true;
            }
            else
            {
                //Must not be shown
                uicontroller.instance.refreshNoti("Does not exist");
                return false;
            }
        }
        uicontroller.instance.refreshNoti("Inventory Full");
        return false;
    }

    //add ui gfx
    public void inventoryToAdd(string _name, int _invLimit, string _type)
    {
        inventoryItem _inv = new inventoryItem();
        _inv.name = _name;
        _inv.invLimit = _invLimit;
        _inv.itemType = _type;
        //_inv.uigfx = _uigfx;
        _inv.invCount = invCountAdd;
        if (invCountAdd > _invLimit)
        {
            _inv.invCount = _invLimit;
        }
        else
        {
            _inv.invCount = invCountAdd;
        }
        inventoryItems.Add(_inv);

    }

    public void openInventory()
    {
        uicontroller.instance.openInventoryScreen();
        invUI.refreshBtns();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            invUI.assignTexts(i, inventoryItems[i].name);
        }
    }

    public void assignAttributes(int _id)
    {
        invUI.assignInfoTexts(inventoryItems[_id].name, inventoryItems[_id].invCount, inventoryItems[_id].itemType);
    }

    public void deleteInvItem(int _id)
    {
        inventoryItems.RemoveAt(_id);
        invUI.refreshBtns();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            invUI.assignTexts(i, inventoryItems[i].name);
        }
    }

    public bool searchAndCount(string _name, int _count)
    {
        if (inventoryItems.Count > 0)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].name == _name)
                {
                    if (inventoryItems[i].invCount >= _count)
                    {
                        return true;
                    }
                }
            }
        }
        

        return false;
    }

    public bool searchAndSpend(string _name, int _count)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].name == _name)
            {
                if (inventoryItems[i].invCount >= _count)
                {
                    inventoryItems[i].invCount -= _count;
                    if (inventoryItems[i].invCount == 0)
                    {
                        inventoryItems.RemoveAt(i);
                    }
                    return true;
                }
            }
        }

        return false;
    }

    public bool addInventory(string _name, int _invToAdd)
    {
        
        //finish tomorrow
        //check if item is in inventory
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].name == _name)
            {
                if (inventoryItems[i].invCount < inventoryItems[i].invLimit)
                {
                    inventoryItems[i].invCount += _invToAdd;
                    if (inventoryItems[i].invCount > inventoryItems[i].invLimit)
                    {
                        inventoryItems[i].invCount = inventoryItems[i].invLimit;
                    }
                    
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        if (inventoryItems.Count < inventoryLimit)
        {
            invCountAdd = _invToAdd;
            // retrieve treasure data
            if (treasureManager.instance.addToPlayerInventory(_name, this))
            {                
                return true;
            }
            else
            {                
                return false;
            }
        }
        
        return false;
    }

    public void useItem(int _id)
    {
        inventoryItems[_id].invCount--;
        sc.consumeItem(inventoryItems[_id].name);
        if (inventoryItems[_id].invCount <= 0)
        {
            inventoryItems.RemoveAt(_id);
            //invUI.refreshBtns();
        }

        invUI.refreshBtns();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            invUI.assignTexts(i, inventoryItems[i].name);
        }

        
        uicontroller.instance.dismissInventoryMenu();
        
    }

    public void loadPlayerInventory(saveData _saveData = null)
    {
        inventoryItems.Clear();
        if (_saveData != null)
        {
            moneyCount = _saveData.moneyCount;
            if (_saveData.itemsInInventory.Length > 0)
            {
                
                for (int i = 0; i < _saveData.itemsInInventory.Length; i++)
                {
                    if (addInventory(_saveData.itemsInInventory[i], _saveData.itemsInInventoryCount[i]))
                    {
                        continue;
                    }
                }
            }
        }
    }
    public void savePlayerInventory(saveController sc)
    {
        sc.itemsInInventory = new string[inventoryItems.Count];
        sc.itemsInInventoryCount = new int[inventoryItems.Count];
        sc.moneyCount = moneyCount;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            sc.itemsInInventory[i] = inventoryItems[i].name;
            sc.itemsInInventoryCount[i] = inventoryItems[i].invCount;
        }
        

    }
}
