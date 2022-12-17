using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedManager : MonoBehaviour
{
    #region 
    public static timedManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public float minTime = 30f;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        maxTime = waveManager.instance.waveTimer - 5f;        
    }

}
