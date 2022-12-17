using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    public int minAmmo;
    public int maxAmmo;
    public string weaponToAddTo;
    treasureAttract theMagnet;

    void Start()
    {
        theMagnet = GetComponent<treasureAttract>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int _ammoToAdd = Random.Range(minAmmo, maxAmmo);
            if (gunManager.instance.gi.addAmmo(weaponToAddTo, _ammoToAdd))
            {
                uicontroller.instance.refreshNoti(weaponToAddTo + " Ammo +" + _ammoToAdd.ToString());                
                Destroy(gameObject);
            }
            else
            {
                theMagnet.enabled = false;
            }

        }
    }
}
