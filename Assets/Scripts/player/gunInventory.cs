using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunInventory : MonoBehaviour
{
    [System.Serializable]
    public class weapons
    {
        public string name;
        public string weaponType;
        public int clipSize;
        public int ammoCount;
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

        //weapon animation
        //sound file
        //weaponsize
    }

    List<weapons> weaponsInInventory;
    gunManager gm;
    public string[] weaponsToAdd;
    public int weaponLimit = 2;
    int equippedWeapon = 0;

    public Transform gunPoint;

    public bool weaponEquipped;
    uicontroller uicon;

    public string unremovableWeapon = "wrench";
    // Start is called before the first frame update
    void Start()
    {
        gm = gunManager.instance;
        gm.gi = this;
        weaponsInInventory = new List<weapons>();
        uicon = uicontroller.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponEquipped)        
        {
            uicon.weaponSlot.SetActive(false);
        }
        
        //change weapon
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (weaponsInInventory.Count > 0 && !weaponEquipped)
            {
                GameObject _weapon = Instantiate(weaponsInInventory[0].gunGfx, gunPoint.position, gunPoint.rotation);
                _weapon.transform.SetParent(gunPoint);
                equippedWeapon = 0;
                Transform _muzzlePoint = _weapon.GetComponent<weaponInfo>().muzzlePoint;
                gm.gc.assignVal(weaponsInInventory[0].ammoCount, weaponsInInventory[0].delayTime, weaponsInInventory[0].muzzleFlash, _muzzlePoint, weaponsInInventory[equippedWeapon].weaponRange, weaponsInInventory[equippedWeapon].singleFire, weaponsInInventory[equippedWeapon].dmgPoints, weaponsInInventory[equippedWeapon].weaponAnim, weaponsInInventory[0].isMelee, weaponsInInventory[0].isShotgun);
                weaponEquipped = true;
                assignToPlayerInfo();
            }
            if (weaponsInInventory.Count > 1)
            {
                equippedWeapon++;
                weaponEquipped = true;
                if (equippedWeapon > weaponsInInventory.Count - 1)
                {
                    equippedWeapon = 0;
                }

                if (gunPoint.childCount > 0)
                {
                    Destroy(gunPoint.GetChild(0).gameObject);
                }
                GameObject _weapon = Instantiate(weaponsInInventory[equippedWeapon].gunGfx, gunPoint.position, gunPoint.rotation);
                _weapon.transform.SetParent(gunPoint);
                Transform _muzzlePoint = _weapon.GetComponent<weaponInfo>().muzzlePoint;
                gm.gc.assignVal(weaponsInInventory[equippedWeapon].ammoCount, weaponsInInventory[equippedWeapon].delayTime, weaponsInInventory[equippedWeapon].muzzleFlash, _muzzlePoint, weaponsInInventory[equippedWeapon].weaponRange, weaponsInInventory[equippedWeapon].singleFire, weaponsInInventory[equippedWeapon].dmgPoints, weaponsInInventory[equippedWeapon].weaponAnim, weaponsInInventory[equippedWeapon].isMelee, weaponsInInventory[equippedWeapon].isShotgun);
                assignToPlayerInfo();
            }

        }
    }

    //Add weapon to inventory
    //
    public void addWeaponToInventory(bool engageNotice, string _name, string _weaponType, int _clipSize, float _delayTime, GameObject _gunGfx, GameObject _muzzleFlash, float _weaponRange, bool _singleFire, int _dmgPoints, string _weaponAnim, bool _isMelee, bool _isShotgun)
    {

        for (int i = 0; i < weaponsInInventory.Count; i++)
        {
            if (weaponsInInventory[i].name == _name)
            {
                //Debug.Log("ignore gun already");
                return;
            }
        }

        if (weaponsInInventory.Count == weaponLimit)
        {
            //Debug.Log("ignore gun limit");
            if (engageNotice)
            {
                uicontroller.instance.openNoticeScreen("Weapon Limit Reached");                
            }
            return;
        }

        weapons _weapon = new weapons();
        _weapon.name = _name;
        _weapon.weaponType = _weaponType;
        _weapon.ammoCount = _clipSize;
        _weapon.clipSize = _clipSize;
        _weapon.delayTime = _delayTime;
        _weapon.gunGfx = _gunGfx;
        _weapon.muzzleFlash = _muzzleFlash;
        _weapon.weaponRange = _weaponRange;
        _weapon.singleFire = _singleFire;
        _weapon.dmgPoints = _dmgPoints;
        _weapon.weaponAnim = _weaponAnim;
        _weapon.isMelee = _isMelee;
        _weapon.isShotgun = _isShotgun;
        weaponsInInventory.Add(_weapon);
        //Debug.Log(weaponsInInventory.Count);
        if (engageNotice)
        {
            uicontroller.instance.openNoticeScreen("Weapon Added");            
        }
    }

    public void deductAmmo(int _ammo)
    {
        weaponsInInventory[equippedWeapon].ammoCount -= _ammo;
        uicon.amooCount = weaponsInInventory[equippedWeapon].ammoCount.ToString();
    }

    public void unequipWeapon()
    {
        if (weaponEquipped)
        {
            Destroy(gunPoint.GetChild(0).gameObject);
            weaponEquipped = false;
        }

    }

// Add ammo from pickups 
    public bool addAmmo(string _weaponName, int _ammo)
    {
        for (int i = 0; i < weaponsInInventory.Count; i++)
        {
            if(_weaponName == weaponsInInventory[i].name)
            {
                weaponsInInventory[i].ammoCount += _ammo;

                if (weaponsInInventory[i].ammoCount > weaponsInInventory[i].clipSize)
                {
                    weaponsInInventory[i].ammoCount = weaponsInInventory[i].clipSize;
                }

                if(weaponsInInventory[i].name == weaponsInInventory[equippedWeapon].name)
                {
                    gm.gc.ammoCount = weaponsInInventory[i].ammoCount;
                }
                return true;
            }
        }    

        

        return false;
    }

    public bool isWeaponInInventory(string _weaponName)
    {
        for (int i = 0; i < weaponsInInventory.Count; i++)
        {
            if (weaponsInInventory[i].name == _weaponName)
            {                
                return true;
            }
        }

        return false;
    }

    public void removeWeapon(string _weaponName, bool isNotice)
    {
        if (unremovableWeapon == _weaponName)
        {
            if (isNotice)
            {
                uicontroller.instance.openNoticeScreen("Cannot Remove " + _weaponName);
                return;
            }
        }
        for (int i = 0; i < weaponsInInventory.Count; i++)
        {
            if (weaponsInInventory[i].name == _weaponName)
            {
                weaponsInInventory.RemoveAt(i);
                
                if (isNotice)
                {
                    uicontroller.instance.openNoticeScreen("Weapon Removed");
                }

                if (weaponsInInventory.Count > 0)
                {
                    if (gunPoint.childCount > 0)
                    {
                        Destroy(gunPoint.GetChild(0).gameObject);
                    }
                    GameObject _weapon = Instantiate(weaponsInInventory[0].gunGfx, gunPoint.position, gunPoint.rotation);
                    _weapon.transform.SetParent(gunPoint);
                    equippedWeapon = 0;
                    Transform _muzzlePoint = _weapon.GetComponent<weaponInfo>().muzzlePoint;
                    gm.gc.assignVal(weaponsInInventory[0].ammoCount, weaponsInInventory[0].delayTime, weaponsInInventory[0].muzzleFlash, _muzzlePoint, weaponsInInventory[equippedWeapon].weaponRange, weaponsInInventory[equippedWeapon].singleFire, weaponsInInventory[equippedWeapon].dmgPoints, weaponsInInventory[equippedWeapon].weaponAnim, weaponsInInventory[0].isMelee, weaponsInInventory[0].isShotgun);
                    weaponEquipped = true;
                    assignToPlayerInfo();
                }
            }
        }
    }

    public void refreshWeapons(saveData _saveData = null)
    {
        weaponsInInventory.Clear();
        if (gunPoint.childCount > 0)
        {
            Destroy(gunPoint.GetChild(0).gameObject);
        }
        if (_saveData != null)
        {
            //use save data instead
            for (int i = 0; i < _saveData.weaponsInInventory.Length; i++)
            {
                gm.addWeaponToInv(_saveData.weaponsInInventory[i]);
            }
            equippedWeapon = 0;
            GameObject _weapon = Instantiate(weaponsInInventory[0].gunGfx, gunPoint.position, gunPoint.rotation);
            _weapon.transform.SetParent(gunPoint);
            Transform _muzzlePoint = _weapon.GetComponent<weaponInfo>().muzzlePoint;
            GetComponent<gunControl>().assignVal(weaponsInInventory[0].ammoCount, weaponsInInventory[0].delayTime, weaponsInInventory[0].muzzleFlash, _muzzlePoint, weaponsInInventory[0].weaponRange, weaponsInInventory[0].singleFire, weaponsInInventory[0].dmgPoints, weaponsInInventory[0].weaponAnim, weaponsInInventory[0].isMelee, weaponsInInventory[0].isShotgun);
            weaponEquipped = true;
            assignToPlayerInfo();
        }
        else
        {
            for (int i = 0; i < weaponsToAdd.Length; i++)
            {
                gm.addWeaponToInv(weaponsToAdd[i]);
            }
            equippedWeapon = 0;
            GameObject _weapon = Instantiate(weaponsInInventory[0].gunGfx, gunPoint.position, gunPoint.rotation);
            _weapon.transform.SetParent(gunPoint);
            Transform _muzzlePoint = _weapon.GetComponent<weaponInfo>().muzzlePoint;
            GetComponent<gunControl>().assignVal(weaponsInInventory[0].ammoCount, weaponsInInventory[0].delayTime, weaponsInInventory[0].muzzleFlash, _muzzlePoint, weaponsInInventory[0].weaponRange, weaponsInInventory[0].singleFire, weaponsInInventory[0].dmgPoints, weaponsInInventory[0].weaponAnim, weaponsInInventory[0].isMelee, weaponsInInventory[0].isShotgun);
            weaponEquipped = true;
            assignToPlayerInfo();
        }
        
    }

    public void saveWeapons(saveController sc)
    {
        sc.weaponsInInventory = new string[weaponsInInventory.Count];
        for (int i = 0; i < sc.weaponsInInventory.Length; i++)
        {
            sc.weaponsInInventory[i] = weaponsInInventory[i].name;
        }
    }

    void assignToPlayerInfo()
    {
        uicon.weaponSlot.SetActive(true);
        uicon.weaponName = weaponsInInventory[equippedWeapon].name;
        uicon.amooCount = weaponsInInventory[equippedWeapon].ammoCount.ToString();
        uicon.clipSize = weaponsInInventory[equippedWeapon].clipSize.ToString();
    }
}
