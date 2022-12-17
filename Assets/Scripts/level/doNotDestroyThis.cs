using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doNotDestroyThis : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Object.FindObjectsOfType<doNotDestroyThis>().Length; i++)
        {
            if(Object.FindObjectsOfType<doNotDestroyThis>()[i] != this)
            {
                if(Object.FindObjectsOfType<doNotDestroyThis>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
        
    }    
}
