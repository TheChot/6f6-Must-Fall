using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gunManager : MonoBehaviour
{
    #region 
    public static gunManager instance;

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
    public class weapons
    {
        public string name;
        public string weaponType;
        public int clipSize;
        public float delayTime;
        public GameObject gunGfx;
        //public GameObject icon;
        public GameObject muzzleFlash;
        public float weaponRange;
        public bool singleFire;
        public int dmgPoints;
        public string weaponAnim;
        public bool isShotgun;
        public bool isMelee;
        public int price;
        public bool isUnlocked;     
        //weapon animation
        //sound file
    }

    public weapons[] weaponsList;
    public gunInventory gi;
    public gunControl gc;

    public GameObject buyPane;
    //the buttons to select items
    public GameObject[] shopBtns;
    public TextMeshProUGUI[] weaponNamesText;
    //For the future
    //public TextMeshProUGUI[] weaponDmgText;
    //public TextMeshProUGUI[] weaponClipText;
    //public GameObject[] lock;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDmgText;
    public GameObject weaponPriceLabel;
    public TextMeshProUGUI weaponPriceText;
    public TextMeshProUGUI weaponClipText;
    public GameObject buyWeaponBtn;
    public GameObject equipWeaponBtn;
    public GameObject removeWeaponBtn;
    int selectedWeaponId;
    public inventoryPlayer ip;
    
    
    
    public void addWeaponToInv(string weaponName)
    {
        for (int i = 0; i < weaponsList.Length; i++)
        {
            if(weaponsList[i].name == weaponName)
            {
                gi.addWeaponToInventory(false, weaponsList[i].name, weaponsList[i].weaponType, weaponsList[i].clipSize, weaponsList[i].delayTime, weaponsList[i].gunGfx, weaponsList[i].muzzleFlash, weaponsList[i].weaponRange, weaponsList[i].singleFire, weaponsList[i].dmgPoints, weaponsList[i].weaponAnim, weaponsList[i].isMelee, weaponsList[i].isShotgun);
                break;
            }
        }
    }
    //open weapon page
    public void openWeaponPage()
    {
        uicontroller.instance.openWeaponScreen();
        
        buyPane.SetActive(false);

        for (int i = 0; i < weaponsList.Length; i++)
        {
            weaponNamesText[i].text = weaponsList[i].name;
            shopBtns[i].SetActive(true);
        }
    }

    public void selectWeaponBtn(int _id)
    {
        buyPane.SetActive(false);
        selectedWeaponId = _id;
        weaponNameText.gameObject.SetActive(true);
        weaponDmgText.gameObject.SetActive(true);
        weaponPriceText.gameObject.SetActive(true);
        weaponClipText.gameObject.SetActive(true);

        weaponNameText.text = weaponsList[_id].name;
        weaponDmgText.text = weaponsList[_id].dmgPoints.ToString();
        weaponPriceText.text = weaponsList[_id].price.ToString();
        weaponClipText.text = weaponsList[_id].clipSize.ToString();
        
        if (weaponsList[_id].isUnlocked)
        {
            
            buyWeaponBtn.SetActive(false);
            weaponPriceText.gameObject.SetActive(false);
            weaponPriceLabel.SetActive(false);
            if (gi.isWeaponInInventory(weaponsList[selectedWeaponId].name))
            {
                removeWeaponBtn.SetActive(true);
                equipWeaponBtn.SetActive(false);
                if (gi.unremovableWeapon == weaponsList[selectedWeaponId].name)
                {
                    removeWeaponBtn.SetActive(false);
                }
                
            }
            else
            {
                removeWeaponBtn.SetActive(false);
                equipWeaponBtn.SetActive(true);
            }
        }
        else
        {
            removeWeaponBtn.SetActive(false);
            equipWeaponBtn.SetActive(false);
            buyWeaponBtn.SetActive(true);
            weaponPriceText.gameObject.SetActive(true);
            weaponPriceLabel.SetActive(true);
        }
        buyPane.SetActive(true);
    }

    //unlockweapon in world
    public void worldUnlock(string _weapon)
    {
        for (int i = 0; i < weaponsList.Length; i++)
        {
            if (weaponsList[i].name == _weapon)
            {
                weaponsList[i].isUnlocked = true;
                break;
            }
        }
    }

    //unlock weapon in store
    public void buyWeapon()
    {
        if (weaponsList[selectedWeaponId].price < ip.moneyCount)
        {
            ip.moneyCount -= weaponsList[selectedWeaponId].price;
            weaponsList[selectedWeaponId].isUnlocked = true;
            uicontroller.instance.openNoticeScreen(weaponsList[selectedWeaponId].name + " Unlocked");            
        }
        else
        {
            uicontroller.instance.openNoticeScreen("Not Enough Money");            
        }

        buyPane.SetActive(false);

    }

    //add weapon to inventory with btn
    public void addToWeaponInv()
    {        
        gi.addWeaponToInventory(true, weaponsList[selectedWeaponId].name, weaponsList[selectedWeaponId].weaponType, weaponsList[selectedWeaponId].clipSize, weaponsList[selectedWeaponId].delayTime, weaponsList[selectedWeaponId].gunGfx, weaponsList[selectedWeaponId].muzzleFlash, weaponsList[selectedWeaponId].weaponRange, weaponsList[selectedWeaponId].singleFire, weaponsList[selectedWeaponId].dmgPoints, weaponsList[selectedWeaponId].weaponAnim, weaponsList[selectedWeaponId].isMelee, weaponsList[selectedWeaponId].isShotgun);
        buyPane.SetActive(false);
    }

    //remove weapon from inventory  with btn
    public void removeWeapon()
    {        
        if (gi.isWeaponInInventory(weaponsList[selectedWeaponId].name))
        {
            gi.removeWeapon(weaponsList[selectedWeaponId].name, true);
        }
        buyPane.SetActive(false);
    }

    public void saveUnlockedWeapons(saveController sc)
    {
        //count the unlocked weapons
        int unlockCount = 0;
        for (int i = 0; i < weaponsList.Length; i++)
        {
            if (weaponsList[i].isUnlocked)
            {
                unlockCount++;
            }
        }

        //store the unlocked
        int storeCounter = 0;
        sc.unlockedWeaponsStore = new int[unlockCount];
        for (int i = 0; i < weaponsList.Length; i++)
        {
            if (weaponsList[i].isUnlocked)
            {
                sc.unlockedWeaponsStore[storeCounter] = i; 
                storeCounter++;
            }
        }
    }

    public void loadUnlockedWeapons(saveData _savedata = null)
    {
        //unlock weapons by index
        if (_savedata != null)
        {
            if (_savedata.unlockedWeaponsStore.Length > 0)
            {
                for (int i = 0; i < _savedata.unlockedWeaponsStore.Length; i++)
                {
                    weaponsList[_savedata.unlockedWeaponsStore[i]].isUnlocked = true;
                }
            }
        }
        
        
    }

}
