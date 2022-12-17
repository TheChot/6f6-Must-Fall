using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public enemyHealth healthController;
    ondestroydrop treasureDrop;
    void Start()
    {
        treasureDrop = GetComponent<ondestroydrop>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthController.health <= 0)
        {
            treasureDrop.dropTreasure();
            Destroy(gameObject);
        }
    }
}
