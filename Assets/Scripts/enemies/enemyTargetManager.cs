using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTargetManager : MonoBehaviour
{
    
    public static enemyTargetManager instance;     
    List<mcguffinHealth> allMcguffins;
    int mcguffinCount;
    [HideInInspector] public int destroyedMcguffins = 0;
    [HideInInspector] public bool allMcguffinsGone;

    void Awake()
    {
        instance = this;
        allMcguffins = new List<mcguffinHealth>();
    }
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        mcguffinCount = allMcguffins.Count;
        allMcguffinsGone = destroyedMcguffins == allMcguffins.Count;
        
    }

    public Transform assignMcguffin(Vector3 itemPosition)
    {
        Transform _mctransform = null;
        float _distance = 10000;

        for (int i = 0; i < allMcguffins.Count; i++)
        {
            if (!allMcguffins[i].gameObject.activeInHierarchy)
            {
                //Debug.Log("looking for target");
                continue;
            }

            if(i == 0)
            {
                _distance = (allMcguffins[i].transform.position - itemPosition).magnitude;
            }

            float _newdistance = (allMcguffins[i].transform.position - itemPosition).magnitude;



            if (_newdistance <= _distance)
            {
                _distance = _newdistance;
                _mctransform = allMcguffins[i].transform;
            }
        }

        //Debug.Log(_mctransform);

        return _mctransform;
    }

    public void addToMcguffinList(mcguffinHealth _mg)
    {
        allMcguffins.Add(_mg);
    }

    public void emptyMcguffinList()
    {
        allMcguffins.Clear();
    }
}
