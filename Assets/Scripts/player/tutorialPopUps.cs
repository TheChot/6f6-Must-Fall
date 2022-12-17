using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPopUps : MonoBehaviour
{
    public string messagePopUp;
    // Start is called before the first frame update
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uicontroller.instance.showMessage(messagePopUp);
            Destroy(gameObject);
        }
    }
}
