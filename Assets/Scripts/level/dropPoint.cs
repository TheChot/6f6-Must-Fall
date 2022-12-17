using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelController.instance.reloadTheScene();
        }

        
    }
}
