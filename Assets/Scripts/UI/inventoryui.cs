using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryui : MonoBehaviour
{
    public GameObject[] inventoryBtns;
    public TextMeshProUGUI[] inventoryTexts;    
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;
    public TextMeshProUGUI itemValue;
    public inventoryPlayer playerInventory;    
    int selectedItem;
    public GameObject[] infoObjs;
    public GameObject useBtn;
    public statusController sc;
    public string useableStringName = "useable";
    // Start is called before the first frame update
    
    public void selectItem(int _invId)
    {
        playerInventory.assignAttributes(_invId);
        selectedItem = _invId;
        
    }

    public void assignTexts(int _invId, string _name)
    {
        inventoryBtns[_invId].SetActive(true);
        inventoryTexts[_invId].text = _name;
        
    }

    public void assignInfoTexts(string _name, int _count, string _useable)
    {
        for (int i = 0; i < infoObjs.Length; i++)
        {
            infoObjs[i].SetActive(true);
        }
        itemName.text = _name;
        itemCount.text = _count.ToString();
        if (_useable == useableStringName)
        {
            useBtn.SetActive(true);
        }
        else
        {
            useBtn.SetActive(false);
        }
    }

    public void refreshBtns()
    {
        
        for (int i = 0; i < infoObjs.Length; i++)
        {
            infoObjs[i].SetActive(false);
        }
        for (int i = 0; i < inventoryBtns.Length; i++)
        {
            inventoryBtns[i].SetActive(false);
        }
    }

    public void deleteItemBtn()
    {
        playerInventory.deleteInvItem(selectedItem);
    }

    public void useItems()
    {
        playerInventory.useItem(selectedItem);
    }
}
