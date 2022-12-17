using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rescuePointController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interactionController>().isInDropVicinity = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<interactionController>().isInDropVicinity = false;
        }
    }
}
