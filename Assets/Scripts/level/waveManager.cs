using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    #region 
    public static waveManager instance;

    
    #endregion

    public Transform[] mcguffinPoints;
    public Transform[] bombLowPoints;
    int pointsFilled;
    public int pointsToIncrease = 2;
    public int maxPoints = 3;
    public int maxPointsToFill = 10;
    int round = 1;
    public int roundsToCount = 2;
    int countedRounds = 0;
    //int heightDifficulty;
    public GameObject mcguffin;
    public GameObject bomb;
    public GameObject rescue;


    public float waveTimer = 120;
    float waveTimerReset;
    public float restTimer =10;
    float restTimerReset;
    public bool roundStarted;
    bool spawnedItems;
    List<enemySpawner> es;
    public Transform enemyParent;
    public pickupManager pm;

    


    private void Awake()
    {
        instance = this;
        es = new List<enemySpawner>();
    }

    void Start()
    {
        waveTimerReset = waveTimer;
        restTimerReset = restTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelController.instance.isGameOver)
            return;

        uicontroller.instance.currentRound = round.ToString();

        if (!roundStarted)
        {
            restTimer -= Time.deltaTime;
            uicontroller.instance.restMode = "REST";
            uicontroller.instance.roundTime = Mathf.Round(restTimer).ToString();
            
            if (restTimer <= 0)
            {
                deActivateSpawners();
                roundStarted = true;
                restTimer = restTimerReset;
                pm.spawnPickups();
            }
            
        } else
        {
            if (!spawnedItems)
            {
                spawnRound();
                spawnedItems = true;
            }
            uicontroller.instance.restMode = "FIGHT";
            uicontroller.instance.roundTime = Mathf.Round(waveTimer).ToString();
            

            waveTimer -= Time.deltaTime;
            
            if (waveTimer <= 0)
            {
                roundStarted = false;
                waveTimer = waveTimerReset;
                spawnedItems = false;
                pointsFilled = 0;
                destroyAfterRound();
                enemyTargetManager.instance.emptyMcguffinList();
                round++;
                countedRounds++;
                
                if(countedRounds == roundsToCount)
                {
                    countedRounds = 0;
                    maxPoints += pointsToIncrease;

                    if(maxPoints > maxPointsToFill)
                    {
                        maxPoints = maxPointsToFill;
                    }
                }

                if(enemyParent.childCount > 0)
                {
                    deleteSpawnedEnemies();
                }
            }
        }
    }

    void spawnRound()
    {
        while (pointsFilled < maxPoints)
        {
            int whatToPut = Random.Range(0, 3);

            if(whatToPut == 0)
            {
                int whereToPut = Random.Range(0, mcguffinPoints.Length);
                
                if(mcguffinPoints[whereToPut].childCount > 0)
                {
                    continue;
                }
                else
                {
                    activateSpawners();
                    GameObject _item = Instantiate(mcguffin, mcguffinPoints[whereToPut].position, mcguffinPoints[whereToPut].rotation);
                    _item.transform.SetParent(mcguffinPoints[whereToPut]);
                    pointsFilled++;
                }
            } 
            else if (whatToPut == 1)
            {
                int whereToPut = Random.Range(0, bombLowPoints.Length);

                if (bombLowPoints[whereToPut].childCount > 0)
                {
                    continue;
                }
                else
                {
                    GameObject _item = Instantiate(bomb, bombLowPoints[whereToPut].position, bombLowPoints[whereToPut].rotation);
                    _item.transform.SetParent(bombLowPoints[whereToPut]);
                    pointsFilled++;
                }
            }
            else if (whatToPut == 2)
            {
                int whereToPut = Random.Range(0, bombLowPoints.Length);

                if (bombLowPoints[whereToPut].childCount > 0)
                {
                    continue;
                }
                else
                {
                    GameObject _item = Instantiate(rescue, bombLowPoints[whereToPut].position, bombLowPoints[whereToPut].rotation);
                    _item.transform.SetParent(bombLowPoints[whereToPut]);
                    pointsFilled++;
                }
            }
        }
    }

    void destroyAfterRound()
    {
        for (int i = 0; i < bombLowPoints.Length; i++)
        {
            if(bombLowPoints[i].childCount == 0)
            {
                continue;
            }
            else
            {
                Destroy(bombLowPoints[i].GetChild(0).gameObject);
            }

        }

        for (int i = 0; i < mcguffinPoints.Length; i++)
        {
            if (mcguffinPoints[i].childCount == 0)
            {
                continue;
            }
            else
            {
                if (mcguffinPoints[i].GetChild(0).gameObject.activeInHierarchy)
                {
                    scoreManager.instance.score += scoreManager.instance.scoreToAdd;
                }

                Destroy(mcguffinPoints[i].GetChild(0).gameObject);
            }

        }
    }

    public void addToSpawnerList(enemySpawner _es)
    {
        es.Add(_es);
    }

    void activateSpawners()
    {
        //Debug.Log("activated spawnr");
        for (int i = 0; i < es.Count; i++)
        {
            es[i].activateSpawner = true;
        }
    }

    void deActivateSpawners()
    {
        Debug.Log("deactivated spawnr");
        for (int i = 0; i < es.Count; i++)
        {
            es[i].activateSpawner = false;
        }
    }

    void deleteSpawnedEnemies()
    {
        for (int i = 0; i < enemyParent.childCount; i++)
        {
            Destroy(enemyParent.GetChild(i).gameObject);
        }
    }
}
