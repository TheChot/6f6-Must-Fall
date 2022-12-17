using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class shopManager : MonoBehaviour
{
    #region 
    public static shopManager instance;

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
    public class ingredients
    {
        public string name;
        public int ingredientNum;        
        //public GameObject uigfx;
    }
    public string shopType;
    public GameObject[] infoObjs;
    //the buttons to select items   
    public GameObject[] shopBtns;    
    public TextMeshProUGUI[] shopTexts;
    treasureManager tm;
    public TextMeshProUGUI itemName;    
    public TextMeshProUGUI itemValue;
    public TextMeshProUGUI unlockName;    
    List<string> shopItemName;
    List<ingredients> ingredientList;
    string selectedName;
    int selectedPrice;
    public GameObject unlockPane;
    public GameObject buyPane;
    public TextMeshProUGUI[] ingredientName;
    public TextMeshProUGUI[] ingredientCount;

    inventoryPlayer ip;
    // Start is called before the first frame update
    void Start()
    {
        tm = treasureManager.instance;
        shopItemName = new List<string>();
        ingredientList = new List<ingredients>();
        ip = levelController.instance.thePlayer.GetComponent<inventoryPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openShop()
    {
        uicontroller.instance.openShopScreen();
        if (shopItemName.Count > 0)
        {
            shopItemName.Clear();
        }
        for (int i = 0; i < infoObjs.Length; i++)
        {
            infoObjs[i].SetActive(false);
        }
        for (int i = 0; i < tm.treasureList.Length; i++)
        {
            if (tm.treasureList[i].inShop)
            {
                if (tm.treasureList[i].shopType == shopType)
                {                    
                    shopItemName.Add(tm.treasureList[i].name);
                }
            }
        }

        for (int i = 0; i < shopItemName.Count; i++)
        {
            shopBtns[i].SetActive(true);
            shopTexts[i].text = shopItemName[i];
        }
    }

    public void selectItem(int _invId)
    {
        for (int i = 0; i < tm.treasureList.Length; i++)
        {
            if (tm.treasureList[i].name == shopItemName[_invId])
            {                               

                if (tm.treasureList[i].isUnlocked)
                {
                    buyPane.SetActive(true);
                    unlockPane.SetActive(false);
                    itemName.text = tm.treasureList[i].name;
                    itemValue.text = tm.treasureList[i].price.ToString();
                    selectedName = tm.treasureList[i].name;
                    selectedPrice = tm.treasureList[i].price;
                }
                else
                {
                    unlockPane.SetActive(true);
                    buyPane.SetActive(false);
                    ingredientList.Clear();
                    unlockName.text = tm.treasureList[i].name;
                    selectedName = tm.treasureList[i].name;
                    for (int j = 0; j < ingredientName.Length; j++)
                    {
                        ingredientName[j].gameObject.SetActive(false);                        
                    }
                    for (int j = 0; j < tm.treasureList[i].unlockReq.Length; j++)
                    {                        
                        bool _dontAdd = false;
                        for (int k = 0; k < ingredientList.Count; k++)
                        {
                            if (ingredientList[k].name == tm.treasureList[i].unlockReq[j])
                            {
                                ingredientList[k].ingredientNum += 1;
                                _dontAdd = true;
                            }
                        }

                        if (!_dontAdd)
                        {
                            ingredients _ingredient = new ingredients();
                            _ingredient.name = tm.treasureList[i].unlockReq[j];
                            _ingredient.ingredientNum += 1;
                            ingredientList.Add(_ingredient);
                        }
                        
                    }

                    for (int j = 0; j < ingredientList.Count; j++)
                    {
                        ingredientName[j].gameObject.SetActive(true);
                        ingredientName[j].text = ingredientList[j].name;
                        ingredientCount[j].text = ingredientList[j].ingredientNum.ToString();
                    }

                }

            }
        }

    }

    //Unlock function here
    public void unlockItem()
    {
        bool _spend = true;
        // searches for each ingredient and makes sure that they are in the players inventory
        for (int i = 0; i < ingredientList.Count; i++)
        {            
            if (ip.searchAndCount(ingredientList[i].name, ingredientList[i].ingredientNum))
            {
                continue;
            }
            else
            {
                _spend = false;
                uicontroller.instance.openNoticeScreen("Not Enough " + ingredientList[i].name);                
                break;
            }
        }

        //if all the ingredients are there start spending on them
        if (_spend)
        {
            for (int i = 0; i < ingredientList.Count; i++)
            {
                if (ip.searchAndSpend(ingredientList[i].name, ingredientList[i].ingredientNum))
                {
                    continue;
                }                
            }
            for (int i = 0; i < tm.treasureList.Length; i++)
            {
                if (tm.treasureList[i].name == selectedName)
                {
                    tm.treasureList[i].isUnlocked = true;
                    uicontroller.instance.openNoticeScreen("Item Unlocked");                    
                    break;
                }
            }
        }

        for (int i = 0; i < infoObjs.Length; i++)
        {
            infoObjs[i].SetActive(false);
        }
    }
    //buy function here
    public void buyItem()
    {
        for (int i = 0; i < tm.treasureList.Length; i++)
        {
            if (tm.treasureList[i].name == selectedName)
            {
                if (ip.moneyCount >= selectedPrice)
                {
                    if (ip.addInventory(selectedName, 1))
                    {
                        ip.moneyCount -= selectedPrice;
                        uicontroller.instance.openNoticeScreen(selectedName + " Added To Inventory");
                        
                    }
                    else
                    {
                        uicontroller.instance.openNoticeScreen("Your Inventory is full");
                        
                    }
                    
                }
                else
                {
                    int _diff = selectedPrice - ip.moneyCount;
                    uicontroller.instance.openNoticeScreen("You need " + _diff.ToString() + " to purchase " + selectedName);                    
                }               
                
                break;
            }
        }

        for (int i = 0; i < infoObjs.Length; i++)
        {
            infoObjs[i].SetActive(false);
        }
    }
}
