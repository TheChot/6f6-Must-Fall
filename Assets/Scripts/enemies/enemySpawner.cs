using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float enemyInterval = 5.0f;
    float intervalReset;
    public bool activateSpawner;
    public GameObject enemy;
    public bool targetingEnemy;
    public Transform enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        intervalReset = enemyInterval;
        waveManager.instance.addToSpawnerList(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (activateSpawner)
        {
            enemyInterval -= Time.deltaTime;

            if(enemyInterval <= 0)
            {
                GameObject _enemy = Instantiate(enemy, transform.position, transform.rotation);
                _enemy.transform.SetParent(enemyParent);
                shootingEnemy em = _enemy.GetComponent<shootingEnemy>();
                //em.targetingEnemy = targetingEnemy;
                //em.targetMode = targetingEnemy;
                enemyInterval = intervalReset;
            }
        }
        else
        {
            enemyInterval = intervalReset;
        }
    }

    
}
