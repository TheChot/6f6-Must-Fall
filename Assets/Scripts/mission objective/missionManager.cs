using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missionManager : MonoBehaviour
{
    #region 
    public static missionManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [HideInInspector]public int numberOfPublicRescues = 0;
    [HideInInspector] public int numberOfPrivateRescues = 0;
    int totalToBeRescued;
    [HideInInspector] public int totalRescued;
    [HideInInspector] public int numberOfDeaths;
    [HideInInspector] public int numberOfPublicBombs = 0;
    [HideInInspector] public int numberOfPrivateBombs = 0;
    int totalToBeDefused;
    [HideInInspector] public int totalDefused;
    [HideInInspector] public int numberOfExplodes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalToBeRescued = numberOfPublicRescues + numberOfPrivateRescues;
        totalToBeDefused = numberOfPublicBombs + numberOfPrivateBombs;

    }
}
