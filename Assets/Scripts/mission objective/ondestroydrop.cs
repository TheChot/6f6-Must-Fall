using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ondestroydrop : MonoBehaviour
{
    enemyHealth eh;
    bool treasureDropped;
    public string[] treasureToDrop;
    public GameObject treasure;
    treasureManager tm;
    // Start is called before the first frame update
    void Start()
    {
        eh = GetComponent<enemyHealth>();
        tm = treasureManager.instance;

    }

    // Update is called once per frame
    public void dropTreasure()
    {
        GameObject _treasure = Instantiate(treasure, transform.position, transform.rotation);
        int _tPoint = 0;
        for (int i = 0; i < treasureToDrop.Length; i++)
        {
            GameObject _theTreasure = tm.dropTreasure(treasureToDrop[i]);

            if(_theTreasure != null)
            {
                GameObject _treasureToDrop = Instantiate(_theTreasure, transform.position, transform.rotation);

                _treasureToDrop.transform.SetParent(_treasure.transform.GetChild(_tPoint));
                _tPoint++;
                
            }
        }
    }
}
