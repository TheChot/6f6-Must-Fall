using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class characterUpgrade : MonoBehaviour
{
    #region 
    public static characterUpgrade instance;

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
    public int baseHealth;
    public float baseJetpack;
    public int baseShield;
    public int baseInventory;
    public int baseGun;
    public int charLevel = 1;
    public int invLevel;
    public int shieldLevel;
    public int gunLevel = 0;
    public int increaseHealthBy = 1;
    public float increaseJetPackBy = 2;
    public int increaseInventoryBy = 2;
    public int increaseShieldBy = 2;
    public int increaseGunBy = 1;
    public int charUpgradeToken = 0;
    public int gunUpgradeToken = 0;
    public int invLevelMin;
    public bool unlockExtraInv;
    public playerMovement pm;
    public playerController pc;
    public inventoryPlayer ip;
    public gunInventory gi;
    public int baseInvPrice;
    public int baseShieldPrice;
    public int baseCharPrice;
    int invPrice;
    int shieldPrice;
    int charUpgradePrice;
    
    public TextMeshProUGUI charLevelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI jetpackText;
    public TextMeshProUGUI charUpgradePriceText;
    public TextMeshProUGUI shieldLevelText;
    public TextMeshProUGUI shieldPointsText;
    public TextMeshProUGUI shieldPriceText;
    public TextMeshProUGUI inventoryLevelText;
    public TextMeshProUGUI inventoryLimitText;
    public TextMeshProUGUI inventoryPriceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI tokensText;
    public TextMeshProUGUI gunTokensText;
    public TextMeshProUGUI gunLevelText;
    public TextMeshProUGUI gunLimitText;
    public TextMeshProUGUI gunPriceText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void openUpgradePage()
    {
        uicontroller.instance.openUpgradeScreen();
        int __increaseHealth = charLevel * increaseHealthBy;
        float __increaseJetpack = charLevel * increaseJetPackBy;
        int __invLimit = increaseInventoryBy * invLevel;
        int __shieldLevel = increaseShieldBy * shieldLevel;

        int _increaseHealth = __increaseHealth + baseHealth;
        float _increaseJetpack = __increaseJetpack + baseJetpack;
        int _invLimit = __invLimit + baseInventory;
        int _shieldLevel = __shieldLevel + baseShield;
        charLevelText.text = charLevel.ToString();
        healthText.text = _increaseHealth.ToString();
        jetpackText.text = _increaseJetpack.ToString();
        charUpgradePriceText.text = "1 Token";
        shieldLevelText.text = shieldLevel.ToString();
        shieldPointsText.text = _shieldLevel.ToString();
        gunTokensText.text = gunUpgradeToken.ToString();
        tokensText.text = charUpgradeToken.ToString();

        if (!pc.shieldActive)
        {
            shieldPointsText.text = "LOCKED";
        }
        
        shieldPriceText.text = shieldPrice.ToString();
        inventoryLevelText.text = invLevel.ToString();
        inventoryLimitText.text = _invLimit.ToString();
        inventoryPriceText.text = invPrice.ToString();
        moneyText.text = ip.moneyCount.ToString();
        tokensText.text = charUpgradeToken.ToString();

        gunLevelText.text = gunLevel.ToString();
        gunLimitText.text = gi.weaponLimit.ToString();
        gunPriceText.text = "1 Token";
    }

    public void upgradeCharacter()
    {
        if (charUpgradeToken > 0)
        {
            charUpgradeToken -= 1;
            charLevel += 1;
            int _increaseHealth = charLevel * increaseHealthBy;
            float _increaseJetpack = charLevel * increaseJetPackBy;
            pm.jetpackFuel = baseJetpack + _increaseJetpack;
            pc.health = baseHealth + _increaseHealth;
            pm.increaseJetpack();
            charLevelText.text = charLevel.ToString();
            healthText.text = pc.health.ToString();
            jetpackText.text = pm.jetpackFuel.ToString();
            charUpgradePriceText.text = "1 Token";
            tokensText.text = charUpgradeToken.ToString();
            uicontroller.instance.openNoticeScreen("Character Upgraded");
        }
        else
        {
            uicontroller.instance.openNoticeScreen("No Tokens");
        }
    }

    public void upgradeGunInventory()
    {
        if (gunUpgradeToken > 0)
        {
            gunUpgradeToken -= 1;
            gunLevel += 1;
            int _increaseInv = gunLevel * increaseGunBy;
            gi.weaponLimit = baseGun + _increaseInv;
            gunLevelText.text = gunLevel.ToString();
            gunLimitText.text = gi.weaponLimit.ToString();            
            gunPriceText.text = "1 Token";
            gunTokensText.text = gunUpgradeToken.ToString();            
            uicontroller.instance.openNoticeScreen("Weapon Inventory Upgraded");
        }
        else
        {
            uicontroller.instance.openNoticeScreen("No Tokens");
        }
    }
    public void upgradeShield()
    {
        if (ip.moneyCount > shieldPrice)
        {
            if (!pc.shieldActive)
            {
                ip.moneyCount -= shieldPrice;
                pc.shieldActive = true;
                int _shieldAdd = shieldLevel + 1;
                shieldPrice = baseShieldPrice * _shieldAdd;
                shieldLevelText.text = shieldLevel.ToString();
                shieldPointsText.text = pc.shield.ToString();
                shieldPriceText.text = shieldPrice.ToString();
                moneyText.text = ip.moneyCount.ToString();
                uicontroller.instance.openNoticeScreen("Shield Unlocked");
            }
            else
            {
                ip.moneyCount -= shieldPrice;
                shieldLevel += 1;
                int _shieldToAdd = increaseShieldBy * shieldLevel;
                pc.shield = baseShield + _shieldToAdd;
                int _shieldAdd = shieldLevel + 1;
                shieldPrice = baseShieldPrice * _shieldAdd;
                shieldLevelText.text = shieldLevel.ToString();
                shieldPointsText.text = pc.shield.ToString();
                shieldPriceText.text = shieldPrice.ToString();
                moneyText.text = ip.moneyCount.ToString();
                moneyText.text = ip.moneyCount.ToString();
                uicontroller.instance.openNoticeScreen("Shield Upgraded");
            }
        }
        else
        {
            uicontroller.instance.openNoticeScreen("Not Enough Money");
        }
    }

    public void upgradeInventory()
    {
        if (ip.moneyCount > invPrice)
        {
            invLevel += 1;
            int _invToAdd = increaseInventoryBy * invLevel;
            ip.inventoryLimit = baseInventory + _invToAdd;
            ip.moneyCount -= invPrice;            
            int _invAdd = invLevel + 1;            
            invPrice = baseInvPrice * _invAdd;
            if (invLevelMin < invLevel)
            {
                unlockExtraInv = true;
            }
            inventoryLevelText.text = invLevel.ToString();
            inventoryLimitText.text = ip.inventoryLimit.ToString();
            inventoryPriceText.text = invPrice.ToString();
            uicontroller.instance.openNoticeScreen("Inventory Upgraded");

        }
        else
        {
            uicontroller.instance.openNoticeScreen("Not Enough Money");
        }
    }

    public void setupPlayer(saveData _savedata = null)
    {
        if (_savedata != null)
        {
            int _increaseHealth = _savedata.charLevel * increaseHealthBy;
            float _increaseJetpack = _savedata.charLevel * increaseJetPackBy;
            int _invLimit = increaseInventoryBy * _savedata.invLevel;
            int _shieldLevel = increaseShieldBy * _savedata.shieldLevel;
            int _increaseGunInv = _savedata.gunLevel * increaseGunBy;
            gi.weaponLimit = baseGun + _increaseGunInv;
            pm.jetpackFuel = baseJetpack + _increaseJetpack;
            pc.health = baseHealth + _increaseHealth;
            pc.maxHealth = baseHealth + _increaseHealth;
            pc.shield = baseShield + _shieldLevel;
            ip.inventoryLimit = baseInventory + _invLimit;
            if (shieldLevel > 0)
            {
                pc.shieldActive = true;
            }
            if (invLevelMin < invLevel)
            {
                unlockExtraInv = true;
            }

            int _invAdd = invLevel + 1;
            int _shieldAdd = shieldLevel + 1;

            invPrice = baseInvPrice * _invAdd;
            shieldPrice = baseShieldPrice * _shieldAdd;
            charUpgradePrice = baseCharPrice;
            charUpgradeToken = _savedata.charUpgradeToken;
            gunUpgradeToken = _savedata.gunUpgradeToken;
        }
        else
        {
            
            gi.weaponLimit = baseGun;
            pm.jetpackFuel = baseJetpack;
            pc.health = baseHealth;
            pc.maxHealth = baseHealth;
            pc.shield = baseShield;
            ip.inventoryLimit = baseInventory;
            if (shieldLevel > 0)
            {
                pc.shieldActive = true;
            }
            if (invLevelMin < invLevel)
            {
                unlockExtraInv = true;
            }

            int _invAdd = invLevel + 1;
            int _shieldAdd = shieldLevel + 1;

            invPrice = baseInvPrice * _invAdd;
            shieldPrice = baseShieldPrice * _shieldAdd;
            charUpgradePrice = baseCharPrice;
        }
    }

    public void collectCharacterToken()
    {
        charUpgradeToken++;
    }

    public void collectGunUpgradeToken()
    {
        gunUpgradeToken++;
    }
}
