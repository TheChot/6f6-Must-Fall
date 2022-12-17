using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEffect : MonoBehaviour
{
    public int dmgToDo = 1;
    public bool poison;
    public bool burning;
    public bool stun;    
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerController>().damageThePlayer(dmgToDo);

            if (poison)
            {
                other.gameObject.GetComponent<statusController>().hitPlayer("psn");
            }
            else if (burning)
            {
                other.gameObject.GetComponent<statusController>().hitPlayer("brn");
            }
            else if (stun)
            {
                other.gameObject.GetComponent<statusController>().hitPlayer("stun");
            }

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}
