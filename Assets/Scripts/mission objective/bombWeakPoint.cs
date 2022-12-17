using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombWeakPoint : MonoBehaviour
{
    public enemyHealth healthController;
    public bombMain bm;
    bool isDestroyed;



    void Start()
    {
        bm.weakPointCount++;
    }
    void Update()
    {
        if(healthController.health == 0 && !isDestroyed)
        {
            isDestroyed = true;
            healthController.gameObject.SetActive(false);
            bm.weakPointsHit++;
        }
        
    }
}
