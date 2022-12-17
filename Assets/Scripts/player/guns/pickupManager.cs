using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupManager : MonoBehaviour
{
    
    public Transform[] pickUpPoints;
    public GameObject ammoPickup;
    public int spawnChance = 10;

    //Spawn pickups at random pickup locations
    public void spawnPickups()
    {
        int _itemsSpawned = 0;
        for (int i = 0; i < pickUpPoints.Length; i++)
        {
            int _spawn = Random.Range(0, 100);

            if(_spawn < spawnChance)
            {
                if(pickUpPoints[i].childCount > 0)
                {
                    continue;
                }

                GameObject _pickup = Instantiate(ammoPickup, pickUpPoints[i].position, pickUpPoints[i].rotation);
                _pickup.transform.SetParent(pickUpPoints[i]);
                _itemsSpawned++;

            }
        }

        if(_itemsSpawned == 0)
        {
            
            int _spawnRand = Random.Range(0, pickUpPoints.Length);
            if (pickUpPoints[_spawnRand].childCount > 0)
            {
                return;
            }
            GameObject _pickup = Instantiate(ammoPickup, pickUpPoints[_spawnRand].position, pickUpPoints[_spawnRand].rotation);
            _pickup.transform.SetParent(pickUpPoints[_spawnRand]);

        }
    }
}
